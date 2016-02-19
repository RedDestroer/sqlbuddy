using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlBuddy.Domain
{
    public class PrettyNameTransformator
        : ITransformator
    {
        public static readonly IDictionary<string, string> PrettyNames = new ConcurrentDictionary<string, string>(new List<KeyValuePair<string, string>>(new[]
        {
            new KeyValuePair<string, string>("guid", "guid"),
        }));

        public void Transform(SqlDatabaseDefinition sqlDatabaseDefinition)
        {
            if (sqlDatabaseDefinition == null) throw new ArgumentNullException("sqlDatabaseDefinition");

            var parameters =
                from sqlSchemaDefinition in sqlDatabaseDefinition.Schemas
                from sqlProcedureDefinition in sqlSchemaDefinition.Procedures
                from sqlParameterDefinition in sqlProcedureDefinition.Parameters
                select sqlParameterDefinition;

            Parallel.ForEach(parameters, PrettyName);
        }

        private void PrettyName(SqlParameterDefinition sqlParameterDefinition)
        {
            var netParameterName = sqlParameterDefinition.TryGetValue<string>(SqlParameterContextKeys.NetParameterName, null);
            if (string.IsNullOrEmpty(netParameterName))
                return;

            netParameterName = netParameterName.ToLowerInvariant();

            if (!PrettyNames.ContainsKey(netParameterName))
                return;

            sqlParameterDefinition.Context[SqlParameterContextKeys.NetParameterName] = PrettyNames[netParameterName];
            sqlParameterDefinition.Context[SqlParameterContextKeys.SqlParameterName] = "@" + PrettyNames[netParameterName];
        }
    }
}