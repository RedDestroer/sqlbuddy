using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using SqlBuddy.Domain;
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
            var builder = new DataAccessBuilder();
            builder.Build(inputFileContents);

            if (builder.Errors.Count > 0)
            {
                var errorMsg = builder.Errors.Aggregate((s1, s2) => string.Format("{0}\r\n{1}", s1, s2));

                progressCallback.GeneratorError(0, 0, errorMsg, 0, 0);

                return Encoding.UTF8.GetBytes(errorMsg);
            }

            var dataAccess = builder.GetProduct();
            var databaseReader = new SqlDatabaseDefinitionReader(dataAccess);
            var dir = Path.GetDirectoryName(inputFilePath) ?? string.Empty;
            var templateFileName = Path.Combine(dir, dataAccess.Generator);

            var database = databaseReader.Read();

            foreach (string transformatorFullTypeName in dataAccess.Transformators)
            {
                try
                {
                    Transform(transformatorFullTypeName, database);
                }
                catch (Exception exception)
                {
                    var errorMsg = exception.Message;

                    progressCallback.GeneratorError(0, 0, errorMsg, 0, 0);

                    return Encoding.UTF8.GetBytes(errorMsg);
                }
            }

            var host = new DatabaseTemplateHost(dataAccess, defaultNamespace, database);
            var engine = new Engine();
            host.TemplateFileValue = templateFileName;
            string input = File.ReadAllText(templateFileName);
            string output = engine.ProcessTemplate(input, host);

            string errors = string.Empty;
            foreach (CompilerError error in host.Errors)
            {
                errors += error.ToString();
                errors += "\r\n";
            }

            if (!string.IsNullOrEmpty(errors))
            {
                progressCallback.GeneratorError(0, 0, errors, 0, 0);

                return Encoding.UTF8.GetBytes(errors);
            }

            return Encoding.UTF8.GetBytes(output);
        }

        private void Transform(string transformatorFullTypeName, SqlDatabaseDefinition sqlDatabaseDefinition)
        {
            var assembly = Assembly.GetAssembly(typeof (PrettyNameTransformator));
            var objectType = (from type in assembly.GetTypes()
                where type.FullName == transformatorFullTypeName
                select type)
                .SingleOrDefault();

            ////var objectType = typeof(SqlBuddy.Domain.PrettyNameTransformator);

            if (objectType == null)
                throw new InvalidOperationException(string.Format("Can't find class '{0}'.", transformatorFullTypeName));

            var transformator = (ITransformator)Activator.CreateInstance(objectType);

            transformator.Transform(sqlDatabaseDefinition);
        }
    }
}