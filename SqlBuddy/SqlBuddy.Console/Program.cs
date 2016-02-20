using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using SqlBuddy.Domain;
using SqlBuddy.Parsers;

namespace SqlBuddy.Console
{
    public class Program
    {
        private static byte _tabCount;

        public static void Main(string[] args)
        {
            try
            {
                IParseTree tree;
                using (var inputStream = File.OpenRead("Test.txt"))
                {
                    var input = new AntlrInputStream(inputStream);

                    var lexer = new DataAccessLexer(input);
                    lexer.AddErrorListener(LexerThrowingErrorListener.Instance);

                    var tokens = new CommonTokenStream(lexer);

                    var parser = new DataAccessParser(tokens);
                    parser.AddErrorListener(ParserThrowingErrorListener.Instance);

                    tree = parser.compileUnit();
                }

                var visitor = new DataAccessVisitor();
                var dataAccess = visitor.Visit(tree);

                Output(dataAccess);
                System.Console.WriteLine("DataAccess parse success.");

                var sqlDatabaseDefinitionReader = new SqlDatabaseDefinitionReader(dataAccess);
                var sqlDatabaseDefinition = sqlDatabaseDefinitionReader.Read();

                foreach (string transformatorFullTypeName in dataAccess.Transformators)
                {
                    Transform(transformatorFullTypeName, sqlDatabaseDefinition);
                }

                Output(sqlDatabaseDefinition);

                System.Console.WriteLine();
                System.Console.WriteLine("SqlDatabaseDefinition read success.");
            }
            catch (Exception exception)
            {
                System.Console.WriteLine("Failure.");
                System.Console.WriteLine(exception);
            }

            System.Console.WriteLine("Press any key...");
            System.Console.ReadKey(true);
        }

        private static void Transform(string transformatorFullTypeName, SqlDatabaseDefinition sqlDatabaseDefinition)
        {
            var assembly = Assembly.GetAssembly(typeof(PrettyNameTransformator));
            var objectType = (from type in assembly.GetTypes()
                              where type.FullName == transformatorFullTypeName
                              select type)
                .SingleOrDefault();

            if (objectType == null)
                throw new InvalidOperationException(string.Format("Can't find class '{0}'.", transformatorFullTypeName));

            var transformator = (ITransformator)Activator.CreateInstance(objectType);

            transformator.Transform(sqlDatabaseDefinition);
        }

        private static void Output(SqlDatabaseDefinition sqlDatabaseDefinition)
        {
            if (sqlDatabaseDefinition == null) throw new ArgumentNullException("sqlDatabaseDefinition");

            System.Console.Write(new string(' ', _tabCount * 2));

            _tabCount++;

            foreach (var sqlSchemaDefinition in sqlDatabaseDefinition.Schemas)
            {
                Output(sqlSchemaDefinition);
            }

            _tabCount--;
        }

        private static void Output(SqlSchemaDefinition sqlSchemaDefinition)
        {
            System.Console.Write(new string(' ', _tabCount * 2));
            System.Console.WriteLine(sqlSchemaDefinition.Name);

            _tabCount++;

            foreach (var sqlProcedureDefinition in sqlSchemaDefinition.Procedures.Where(proc => proc.ExceptionMessage == null))
            {
                Output(sqlProcedureDefinition);
            }

            foreach (var sqlProcedureDefinition in sqlSchemaDefinition.Procedures.Where(proc => proc.ExceptionMessage != null))
            {
                System.Console.Write(new string(' ', _tabCount * 2));
                System.Console.WriteLine("Ignored procedure: {0,-28}, reason: {1}", sqlProcedureDefinition.Name, sqlProcedureDefinition.ExceptionMessage);
            }

            _tabCount--;
        }

        private static void Output(SqlProcedureDefinition sqlProcedureDefinition)
        {
            System.Console.Write(new string(' ', _tabCount * 2));
            System.Console.WriteLine(sqlProcedureDefinition.Name);

            _tabCount++;

            if (sqlProcedureDefinition.Context.Count > 0)
            {
                System.Console.Write(new string(' ', _tabCount * 2));
                System.Console.WriteLine("Procedure context:");

                _tabCount++;
                
                foreach (var kv in sqlProcedureDefinition.Context)
                {
                    Output(kv);
                }

                _tabCount--;
            }
            
            foreach (var sqlParameterDefinition in sqlProcedureDefinition.Parameters)
            {
                Output(sqlParameterDefinition);
            }

            _tabCount--;
        }

        private static void Output(KeyValuePair<string, string> kv)
        {
            System.Console.Write(new string(' ', _tabCount * 2));
            System.Console.Write("Key: {0, -20}, Value: '{1}'", kv.Key, kv.Value);

            System.Console.WriteLine();
        }

        private static void Output(SqlParameterDefinition sqlParameterDefinition)
        {
            System.Console.Write(new string(' ', _tabCount * 2));
            System.Console.Write("{0,-28}", sqlParameterDefinition.Name);
            System.Console.WriteLine(":");

            if (sqlParameterDefinition.Context.Count > 0)
            {
                System.Console.Write(new string(' ', _tabCount * 2));
                System.Console.WriteLine("Parameter context:");

                _tabCount++;

                foreach (var kv in sqlParameterDefinition.Context)
                {
                    Output(kv);
                }

                _tabCount--;
            }

            System.Console.Write(new string(' ', _tabCount * 2));
            System.Console.Write("Parameter state: ");

            var td = sqlParameterDefinition.TypeDefinition;
            System.Console.Write("SqlType:{0,-20}, ", td.SqlType);
            System.Console.Write("NetType:{0,-35}, ", td.NetType);

            if (td.Precision != null || td.Scale != null)
            {
                System.Console.Write("({0}, {1}), ", td.Precision, td.Scale);
            }

            if (sqlParameterDefinition.DefaultValue != null)
                System.Console.Write(
                    "Default: '{0}'.",
                    sqlParameterDefinition.DefaultValue.Value ?? "NULL");

            System.Console.WriteLine();
        }

        private static void Output(DataAccess dataAccess)
        {
            if (dataAccess == null) throw new ArgumentNullException("dataAccess");

            System.Console.WriteLine("ClassName: {0}.", dataAccess.ClassName);
            System.Console.WriteLine("Generator: {0}.", dataAccess.Generator);

            foreach (var kv in dataAccess.Connection)
            {
                System.Console.WriteLine("key: {0}, value: {1}.", kv.Key, kv.Value);
            }

            foreach (var s in dataAccess.Transformators)
            {
                System.Console.WriteLine("transformator: {0}.", s);
            }

            System.Console.WriteLine();
        }
    }
}
