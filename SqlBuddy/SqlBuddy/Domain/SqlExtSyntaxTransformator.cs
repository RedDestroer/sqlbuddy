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

                sqlProcedureDefinition.Context[SqlProcedureContextKeys.Description] = sqlProcedureExt.Description;
                if (sqlProcedureExt.SqlProcedureFlags != null)
                {
                    sqlProcedureDefinition.Context[SqlProcedureContextKeys.IsPublic] = sqlProcedureExt.SqlProcedureFlags.IsPublic.ToString();
                }

                foreach (var paramExtension in sqlProcedureExt.ParamExtensions)
                {
                    var maxColumn = sqlProcedureDefinition.Parameters
                                                          .Where(o => o.Row == paramExtension.Row && o.Column < paramExtension.Column)
                                                          .DefaultIfEmpty()
                                                          .Max(o => o.Column);
                    var param = sqlProcedureDefinition.Parameters.FirstOrDefault(o => o.Row == paramExtension.Row && o.Column == maxColumn);
                    if (param != null)
                    {
                        param.Context[SqlParameterContextKeys.Description] = paramExtension.Description;
                        param.Context[SqlParameterContextKeys.IsNullable] = paramExtension.IsNullable.ToString();
                        param.TypeDefinition.NetType = Helper.GetNetType(param.TypeDefinition.SqlType, paramExtension.IsNullable);
                        param.Context[SqlParameterContextKeys.NetTypeName] = Helper.NormalizeNetTypeName(param.TypeDefinition.NetType);
                    }
                }
            }
            catch (Exception exception)
            {
                sqlProcedureDefinition.ExceptionMessage = exception.Message;
            }
        }
    }
}