using System;
using System.Text;
using Antlr4.Runtime.Tree;
using SqlBuddy.Domain;

namespace SqlBuddy.Parsers
{
    public class SqlProcedureExtVisitor
        : SqlProcedureExtBaseVisitor<SqlProcedureExt>
    {
        private SqlProcedureExt _sqlProcedureExt;

        public SqlProcedureExtVisitor()
        {
            _sqlProcedureExt = new SqlProcedureExt();
        }

        public override SqlProcedureExt VisitCompileUnit(SqlProcedureExtParser.CompileUnitContext context)
        {
            _sqlProcedureExt = base.VisitCompileUnit(context) ?? _sqlProcedureExt;

            return _sqlProcedureExt;
        }

        public override SqlProcedureExt VisitExtBlock(SqlProcedureExtParser.ExtBlockContext context)
        {
            _sqlProcedureExt = base.VisitExtBlock(context) ?? _sqlProcedureExt;

            var flagsContext = context.flags();
            var fieldContext = context.field();

            if (flagsContext != null)
            {
                if (_sqlProcedureExt.SqlProcedureFlags != null)
                    throw new InvalidOperationException("Sql procedure flags defined more then one time.");

                var sqlProcedureFlags = new SqlProcedureFlags(flagsContext.Start.Line, flagsContext.Start.Column);

                if (flagsContext.PRIVATE() != null)
                    sqlProcedureFlags.IsPublic = false;
                else if (flagsContext.PUBLIC() != null)
                    sqlProcedureFlags.IsPublic = true;

                _sqlProcedureExt.SqlProcedureFlags = sqlProcedureFlags;

                var descriptionContext = context.description();
                if (descriptionContext != null)
                {
                    _sqlProcedureExt.Description = GetString(descriptionContext.ANY());
                }
            }
            else if (fieldContext != null)
            {
                var sqlProocedureParameterExt = new SqlProcedureParameterExt(fieldContext.Start.Line, fieldContext.Start.Column);
                sqlProocedureParameterExt.IsNullable = IsNullable(fieldContext);

                var descriptionContext = context.description();
                if (descriptionContext != null)
                {
                    sqlProocedureParameterExt.Description = GetString(descriptionContext.ANY());
                }

                _sqlProcedureExt.ParamExtensions.Add(sqlProocedureParameterExt);
            }
            
            return _sqlProcedureExt;
        }

        private bool IsNullable(SqlProcedureExtParser.FieldContext context)
        {
            if (context.NULL() == null)
                return true;

            if (context.NOT() == null)
                return true;

            return false;
        }

        private string GetString(ITerminalNode[] nodes)
        {
            var sb = new StringBuilder();

            foreach (var node in nodes)
            {
                sb.Append(node);
            }

            return sb.ToString();
        }
    }
}