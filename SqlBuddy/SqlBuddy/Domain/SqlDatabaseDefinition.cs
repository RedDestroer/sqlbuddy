using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SqlBuddy.Domain
{
    [Serializable]
    public class SqlDatabaseDefinition
    {
        public SqlDatabaseDefinition(string connectionString, params SqlSchemaDefinition[] schemas)
        {
            if (connectionString == null) throw new ArgumentNullException("connectionString");

            ConnectionString = connectionString;
            Schemas = new ReadOnlyCollection<SqlSchemaDefinition>(schemas);
        }

        public string ConnectionString { get; private set; }
        public IEnumerable<SqlSchemaDefinition> Schemas { get; private set; }
    }
}