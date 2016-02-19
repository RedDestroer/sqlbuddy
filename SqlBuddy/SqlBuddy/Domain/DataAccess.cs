using System;
using System.Collections.Generic;

namespace SqlBuddy.Domain
{
    [Serializable]
    public class DataAccess
    {
        public DataAccess()
        {
            Connection = new Dictionary<string, string>();
            Transformators = new List<string>();
        }

        public string ClassName { get; internal set; }
        public string DatabaseName { get; internal set; }
        public string Generator { get; internal set; }
        public IDictionary<string, string> Connection { get; private set; }
        public IList<string> Transformators { get; private set; }

        public bool HaveToReadSchema(string schemaName)
        {
            if (!(schemaName == "dbo"))
                return false;

            return true;
        }

        public bool HaveToReadSqlProcedure(string schemaName, string procedureName)
        {
            if (!(schemaName == "dbo" && procedureName == "AllowedDivisionList"))
                return false;

            return true;
        }
    }
}