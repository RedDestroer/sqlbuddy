using System.Collections.Generic;

namespace SqlBuddy.Domain
{
    public class SqlProcedureExt
    {
        public SqlProcedureExt()
        {
            ParamExtensions = new List<SqlProocedureParameterExt>();
        }

        public SqlProcedureFlags SqlProcedureFlags { get; set; }
        public string Description { get; set; }
        public IList<SqlProocedureParameterExt> ParamExtensions { get; private set; }
    }
}