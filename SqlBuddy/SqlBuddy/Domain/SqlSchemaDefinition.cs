using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SqlBuddy.Domain
{
    [Serializable]
    public class SqlSchemaDefinition
    {
        public SqlSchemaDefinition(int id, string name, params SqlProcedureDefinition[] procedures)
        {
            if (name == null) throw new ArgumentNullException("name");

            ID = id;
            Name = name;
            Procedures = new ReadOnlyCollection<SqlProcedureDefinition>(procedures);
        }

        public int ID { get; private set; }
        public string Name { get; private set; }
        public IList<SqlProcedureDefinition> Procedures { get; private set; }
    }
}