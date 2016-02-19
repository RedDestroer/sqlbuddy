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

        public override SqlProcedureExt VisitExtBlock(SqlProcedureExtParser.ExtBlockContext context)
        {
            _sqlProcedureExt = base.VisitExtBlock(context) ?? _sqlProcedureExt;

            var flagsContext = context.flags();
            
            if (flagsContext.PRIVATE() != null)
                _sqlProcedureExt.IsPublic = false;
            else if (flagsContext.PUBLIC() != null)
                _sqlProcedureExt.IsPublic = true;

            return _sqlProcedureExt;
        }
    }
}