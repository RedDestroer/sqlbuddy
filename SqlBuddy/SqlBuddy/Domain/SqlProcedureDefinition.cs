using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SqlBuddy.Domain
{
    [Serializable]
    public class SqlProcedureDefinition
    {
        public SqlProcedureDefinition(string name, Exception exception)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (exception == null) throw new ArgumentNullException("exception");

            Name = name;
            Exception = exception;
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
        public IEnumerable<SqlParameterDefinition> Parameters { get; private set; }
        public Exception Exception { get; private set; }
    }
}