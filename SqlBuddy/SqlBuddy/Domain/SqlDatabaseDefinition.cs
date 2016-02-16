using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SqlBuddy.Domain
{
    [Serializable]
    public class SqlDatabaseDefinition
    {
        public SqlDatabaseDefinition(params SqlSchemaDefinition[] schemas)
        {
            Schemas = new ReadOnlyCollection<SqlSchemaDefinition>(schemas);
        }

        public IEnumerable<SqlSchemaDefinition> Schemas { get; private set; }
    }
}