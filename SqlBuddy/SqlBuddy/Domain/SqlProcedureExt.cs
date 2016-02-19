using System.Collections.Generic;

namespace SqlBuddy.Domain
{
    public class SqlProcedureExt
    {
        public SqlProcedureExt()
        {
            ParamExtensions = new Dictionary<string, IDictionary<string, string>>();
            IsPublic = true;
        }

        public bool IsPublic { get; set; }

        public IDictionary<string, IDictionary<string, string>> ParamExtensions { get; private set; }
    }
}