using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using SqlBuddy.Builders;
using SqlBuddy.Parsers;

namespace SqlBuddy.VisualStudio
{
    [ComVisible(true)]
    [Guid(GuidList.SqlBuddyGeneratorGuidString)]
    [ProvideObject(typeof(SqlBuddyCodeGenerator))]
    [ProvideCodeGeneratorExtension("SqlBuddyCodeGenerator", ".dcs")]
    [CodeGeneratorRegistration(typeof(SqlBuddyCodeGenerator), "SqlBuddyCodeGenerator", GuidList.VsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(SqlBuddyCodeGenerator), "SqlBuddyCodeGenerator", GuidList.VsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    // [CodeGeneratorRegistration(typeof(SqlBuddyCodeGenerator), "SqlBuddyCodeGenerator", GuidList.VsContextGuidVJSProject, GeneratesDesignTimeSource = true)]
    public class SqlBuddyCodeGenerator
        : CustomToolBase
    {
        protected override string DefaultExtension()
        {
            return ".cs";
        }

        protected override byte[] Generate(string inputFilePath, string inputFileContents, string defaultNamespace, IVsGeneratorProgress progressCallback)
        {
            var parser = new DatabaseParser();
            var parseResult = parser.TryParse(inputFileContents);

            if (parseResult.Field0 < 0)
            {
                var errorInfo = parser.GetMaxRollbackPosAndNames();
                var rollBaskIds = parser.GetMaxRollbackPosAndIds();
                var location = parser.ParsingSource.GetSourceLine(rollBaskIds.Field0);
                var lineColumn = location.StartLineColumn;
                var startPos = location.StartPos;

                var errorMsg = string.Format(
                        @"Input data error: row: {0}, column: {1}, expected: {2}.",
                        lineColumn.Field0,
                        rollBaskIds.Field0 - startPos,
                        rollBaskIds.Field1.Select(id => parser.GetRuleName(id)).Aggregate((s1, s2) => string.Format("{0} или {1}", s1, s2)));

                progressCallback.GeneratorError(0, 0, errorMsg, (uint)lineColumn.Field0, (uint)(errorInfo.Field0 - startPos));
                return Encoding.UTF8.GetBytes(errorMsg);
            }

            var databaseDefinition = parseResult.Field1;

            ////TestNemerle();
            ////TestNemerlePeg();
            ////TestNemerleMacros();
            ////TestNemerleLinq();
            ////TestNemerleCompiler();
            ////TestSqlBuddy();
            ////var compileUnit = new CodeCompileUnit();

            var templateFileName = databaseDefinition.Template;
            var databaseReader = new SqlDatabaseDefinitionReader(databaseDefinition);

            // Если имени нет, то идём путём через NativeBuilder
            if (templateFileName == null)
            {
                var builder = new CsNativeDatabaseBuilder(databaseReader);
                var compileUnit = builder.Build(defaultNamespace);
                var codeStringBuilder = new StringBuilder();
                var codeWriter = new StringWriter(codeStringBuilder);
                var codeProvider = CodeDomProvider.CreateProvider("CSharp");
                var codeOptions = new CodeGeneratorOptions
                {
                    BlankLinesBetweenMembers = true,
                    VerbatimOrder = true,
                    BracingStyle = "C",
                    IndentString = "    "
                };

                codeProvider.GenerateCodeFromCompileUnit(compileUnit, codeWriter, codeOptions);

                return Encoding.UTF8.GetBytes(codeStringBuilder.ToString());
            }

            var dir = Path.GetDirectoryName(inputFilePath);

            templateFileName = Path.Combine(dir, databaseDefinition.Template) + ".tt";

            var database = databaseReader.ReadDatabase();
            var host = new DatabaseTemplateHost(databaseDefinition, defaultNamespace, database);
            var engine = new Engine();
            host.TemplateFileValue = templateFileName;
            string input = File.ReadAllText(templateFileName);
            string output = engine.ProcessTemplate(input, host);

            string errors = string.Empty;
            foreach (CompilerError error in host.Errors)
            {
                errors += error.ToString();
            }

            if (!string.IsNullOrEmpty(errors))
            {
                progressCallback.GeneratorError(0, 0, errors, 0, 0);
                return Encoding.UTF8.GetBytes(errors);
            }

            return Encoding.UTF8.GetBytes(output);
        }

        ////private void TestSqlBuddy()
        ////{
        ////    var type = typeof(SqlBuddy.Builders.DbReader);
        ////}

        ////private void TestNemerleCompiler()
        ////{
        ////    var type = typeof(Nemerle.Compiler.BailOutException);
        ////}

        ////private void TestNemerleLinq()
        ////{
        ////    var type = typeof(Nemerle.Linq.LexerLinq);
        ////}

        ////private void TestNemerleMacros()
        ////{
        ////    var type = typeof(Nemerle.Peg.CaptureKind);
        ////}

        ////private void TestNemerlePeg()
        ////{
        ////    var type = typeof(Nemerle.Peg.Located);
        ////}

        ////private void TestNemerle()
        ////{
        ////    var type = typeof(Nemerle.TailRecursionTransparentAttribute);
        ////}
    }
}