using System;
using SqlBuddy.Domain;

namespace SqlBuddy.VisualStudio
{
    public class DatabaseTemplateHost
        : CustomTemplateHost, IDatabaseProvider
    {
        public DatabaseTemplateHost(DataAccess dataAccess, string @namespace, SqlDatabaseDefinition sqlDatabaseDefinition)
        {
            if (dataAccess == null) throw new ArgumentNullException("dataAccess");
            if (@namespace == null) throw new ArgumentNullException("namespace");
            if (sqlDatabaseDefinition == null) throw new ArgumentNullException("sqlDatabaseDefinition");

            DataAccess = dataAccess;
            Namespace = @namespace;
            SqlDatabaseDefinition = sqlDatabaseDefinition;
        }

        public DataAccess DataAccess { get; private set; }
        public string Namespace { get; private set; }
        public SqlDatabaseDefinition SqlDatabaseDefinition { get; private set; }
    }
}