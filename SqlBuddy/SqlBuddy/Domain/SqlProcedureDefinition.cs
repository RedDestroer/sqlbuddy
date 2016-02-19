using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SqlBuddy.Domain
{
    [Serializable]
    public class SqlProcedureDefinition
    {
        public SqlProcedureDefinition(string name, Exception exception, StringBuilder procText)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (exception == null) throw new ArgumentNullException("exception");
            if (procText == null) throw new ArgumentNullException("procText");

            Name = name;
            Exception = exception;
            ProcText = procText;
        }

        public SqlProcedureDefinition(int id, string name, StringBuilder procText, params SqlParameterDefinition[] parametersDefinition)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (procText == null) throw new ArgumentNullException("procText");

            ID = id;
            Name = name;
            ProcText = procText;
            Parameters = new ReadOnlyCollection<SqlParameterDefinition>(parametersDefinition);
        }

        public int ID { get; private set; }
        public string Name { get; private set; }
        public IList<SqlParameterDefinition> Parameters { get; private set; }
        public Exception Exception { get; private set; }
        public StringBuilder ProcText { get; private set; }
    }
}