using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using SqlBuddy.Dal;
using SqlBuddy.Parsers;

namespace SqlBuddy.Domain
{
    public class SqlDatabaseDefinitionReader
    {
        private readonly DataAccess _dataAccess;
        private IExecutionContext _executionContext;

        public SqlDatabaseDefinitionReader(DataAccess dataAccess)
        {
            if (dataAccess == null) throw new ArgumentNullException("dataAccess");

            _dataAccess = dataAccess;
        }

        public SqlDatabaseDefinition Read()
        {
            var connectionString = string.Format(
                "Server={0};Database={1};User Id={2};Password={3};MultipleActiveResultSets=true", 
                GetConnectionValue(ConnectionKeys.Server),
                GetConnectionValue(ConnectionKeys.Database),
                GetConnectionValue(ConnectionKeys.User),
                GetConnectionValue(ConnectionKeys.Password));
            _executionContext = new ExecutionContext(connectionString, TimeSpan.FromSeconds(30));

            SqlDatabaseDefinition sqlDatabaseDefinition;
            using (var command = new SqlCommand(Sql.Use(GetConnectionValue(ConnectionKeys.Database))))
            {
                _executionContext.Open();
                _executionContext.ExecuteNonQuery(command);

                sqlDatabaseDefinition = ReadSqlDatabaseDefinition();

                _executionContext.Close();
            }

            _executionContext = null;

            return sqlDatabaseDefinition;
        }

        private SqlDatabaseDefinition ReadSqlDatabaseDefinition()
        {
            var shemas = ReadSqlSchemaDefinitions();

            return new SqlDatabaseDefinition(shemas.ToArray());
        }

        private IEnumerable<SqlSchemaDefinition> ReadSqlSchemaDefinitions()
        {
            using (var command = new SqlCommand(Sql.SelectAllSchemas()))
            using (var reader = _executionContext.ExecuteReader(command))
            {
                // Read sql schemas
                while (reader.Read())
                {
                    var sqlSchemaDefinition = ReadSqlSchemaDefinition(reader);

                    if (sqlSchemaDefinition != null)
                        yield return sqlSchemaDefinition;
                }
            }
        }

        private SqlSchemaDefinition ReadSqlSchemaDefinition(IDataReader reader)
        {
            // ObjectId schema
            var schemaId = reader.GetInt32(0);
                        
            // Schema name
            var schemaName = reader.GetString(1);
            
            if (_dataAccess.HaveToReadSchema(schemaName))
            {
                // Schema procedure list
                var procedures = ReadSqlProcedureDefinitions(schemaId, schemaName);
            
                return new SqlSchemaDefinition(schemaId, schemaName, procedures.ToArray());
            }

            return null;
        }

        private IEnumerable<SqlProcedureDefinition> ReadSqlProcedureDefinitions(int schemaId, string schemaName)
        {
            using (var command = new SqlCommand(Sql.SelectProceduresForSchema(schemaId)))
            using (var reader = _executionContext.ExecuteReader(command))
            {
                // Read procedures
                while (reader.Read())
                {
                    var sqlProcedureDefinition = ReadSqlProcedureDefinition(reader, schemaName);

                    if (sqlProcedureDefinition != null)
                        yield return sqlProcedureDefinition;
                }
            }
        }

        private SqlProcedureDefinition ReadSqlProcedureDefinition(IDataReader reader, string schemaName)
        {
            // ObjectId procedure
            var procedureId = reader.GetInt32(0);
                                
            // Procedure name
            var procedureName = reader.GetString(1);
            var procText = new StringBuilder();

            try
            {
                if (_dataAccess.HaveToReadSqlProcedure(schemaName, procedureName))
                {
                    // Procedure text
                    using (var command = new SqlCommand(Sql.GetProcedureText(procedureId)))
                    using (var procTextReader = _executionContext.ExecuteReader(command))
                    {
                        while (procTextReader.Read())
                        {
                            procText.Append(procTextReader.GetString(0));
                        }
                    }
                    // Procedure param list
                    var parameters = ReadSqlProcedureParameterDefinitions(procText);

                    return new SqlProcedureDefinition(procedureId, procedureName, procText, parameters.ToArray());
                }
            }
            catch (NotSupportedException exception)
            {
                return new SqlProcedureDefinition(procedureName, exception, procText);
            }

            return null;
        }

        private IEnumerable<SqlParameterDefinition> ReadSqlProcedureParameterDefinitions(StringBuilder procText)
        {
            // Parse procedure to find out its parameters
            var input = new AntlrInputStream(procText.ToString());

            try
            {
                var lexer = new SqlProcedureHeaderLexer(input);
                lexer.AddErrorListener(LexerThrowingErrorListener.Instance);

                var tokens = new CommonTokenStream(lexer);

                var parser = new SqlProcedureHeaderParser(tokens);
                parser.AddErrorListener(ParserThrowingErrorListener.Instance);

                IParseTree tree = parser.compileUnit();

                var visitor = new SqlProcedureHeaderVisitor();
                var parameters = visitor.Visit(tree);

                return parameters;
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        private string GetConnectionValue(string key)
        {
            switch (key)
            {
                case ConnectionKeys.Server:
                    return GetValue(ConnectionKeys.Server, "(local)");
                case ConnectionKeys.Database:
                    return GetValue(ConnectionKeys.Database, "master");
                case ConnectionKeys.User:
                    return GetValue(ConnectionKeys.User, null);
                case ConnectionKeys.Password:
                    return GetValue(ConnectionKeys.Password, null);
            }

            return null;
        }

        private string GetValue(string key, string @default)
        {
            if (_dataAccess.Connection.ContainsKey(key))
                return _dataAccess.Connection[key];

            return @default;
        }
    }
}