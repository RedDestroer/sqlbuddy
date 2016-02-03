using System;
using SqlBuddy.Domain;

namespace SqlBuddy.VisualStudio
{
    public class DatabaseTemplateHost
        : CustomTemplateHost, IDatabaseProvider
    {
        public DatabaseTemplateHost(DatabaseDefinition databaseDefinition, string @namespace, SqlDatabaseDefinition sqlDatabaseDefinition)
        {
            if (databaseDefinition == null) throw new ArgumentNullException("databaseDefinition");
            if (@namespace == null) throw new ArgumentNullException("namespace");
            if (sqlDatabaseDefinition == null) throw new ArgumentNullException("sqlDatabaseDefinition");

            DatabaseDefinition = databaseDefinition;
            Namespace = @namespace;
            SqlDatabaseDefinition = sqlDatabaseDefinition;
        }

        public DatabaseDefinition DatabaseDefinition { get; private set; }
        public string Namespace { get; private set; }
        public SqlDatabaseDefinition SqlDatabaseDefinition { get; private set; }
    }
}