using System.Collections.Generic;

namespace SqlBuddy.Domain
{
    public class SqlProcedureExt
    {
        public SqlProcedureExt()
        {
            ParamExtensions = new List<SqlProcedureParameterExt>();
        }

        public SqlProcedureFlags SqlProcedureFlags { get; set; }
        public string Description { get; set; }
        public IList<SqlProcedureParameterExt> ParamExtensions { get; private set; }
    }
}