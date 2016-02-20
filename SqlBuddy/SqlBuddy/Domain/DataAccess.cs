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
            SchemaRules = new Rules();
            ProcedureRules = new Dictionary<string, Rules>();
        }

        public string ClassName { get; internal set; }
        public string DatabaseName { get; internal set; }
        public string Generator { get; internal set; }
        public IDictionary<string, string> Connection { get; private set; }
        public IList<string> Transformators { get; private set; }
        public Rules SchemaRules { get; private set; }
        public IDictionary<string, Rules> ProcedureRules { get; private set; }

        public bool HaveToReadSchema(string schemaName)
        {
            return SchemaRules.IsGood(schemaName);
        }

        public bool HaveToReadSqlProcedure(string schemaName, string procedureName)
        {
            string pattern;
            if (SchemaRules.IsGood(schemaName, out pattern))

            if (pattern == null)
                return true;

            if (!ProcedureRules.ContainsKey(pattern))
                return true;

            return ProcedureRules[pattern].IsGood(procedureName);
        }
    }
}