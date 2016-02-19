using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SqlBuddy.Domain
{
    [Serializable]
    public class SqlProcedureDefinition
        : ContextualDefinition
    {
        public SqlProcedureDefinition(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            Name = name;
        }

        public SqlProcedureDefinition(int id, string name, params SqlParameterDefinition[] parametersDefinition)
        {
            if (name == null) throw new ArgumentNullException("name");

            ID = id;
            Name = name;
            Parameters = new ReadOnlyCollection<SqlParameterDefinition>(parametersDefinition);
        }

        public int ID { get; private set; }
        public string Name { get; private set; }
        public IList<SqlParameterDefinition> Parameters { get; private set; }
        public string ExceptionMessage { get; set; }
        public StringBuilder ProcText { get; set; }
    }
}