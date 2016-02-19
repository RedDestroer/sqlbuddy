using System;
using System.Linq;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using SqlBuddy.Parsers;

namespace SqlBuddy.Domain
{
    public class SqlExtSyntaxTransformator
        : ITransformator
    {
        public void Transform(SqlDatabaseDefinition sqlDatabaseDefinition)
        {
            var procedures =
                from sqlSchemaDefinition in sqlDatabaseDefinition.Schemas
                from sqlProcedureDefinition in sqlSchemaDefinition.Procedures
                where sqlProcedureDefinition.ExceptionMessage == null
                select sqlProcedureDefinition;

            Parallel.ForEach(procedures, TransformProcedure);
        }

        private void TransformProcedure(SqlProcedureDefinition sqlProcedureDefinition)
        {
            var procText = sqlProcedureDefinition.ProcText.ToString();

            var input = new AntlrInputStream(procText);

            try
            {
                var lexer = new SqlProcedureExtLexer(input);
                lexer.AddErrorListener(LexerThrowingErrorListener.Instance);

                var tokens = new CommonTokenStream(lexer);

                var parser = new SqlProcedureExtParser(tokens);
                parser.AddErrorListener(ParserThrowingErrorListener.Instance);

                IParseTree tree = parser.compileUnit();

                var visitor = new SqlProcedureExtVisitor();
                var sqlProcedureExt = visitor.Visit(tree);

                sqlProcedureDefinition.Context[SqlProcedureContextKeys.IsPublic] = sqlProcedureExt.IsPublic.ToString();
            }
            catch (Exception exception)
            {
                sqlProcedureDefinition.ExceptionMessage = exception.Message;
            }
        }
    }
}