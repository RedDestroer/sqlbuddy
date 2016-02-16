using System;
using System.Data;

namespace SqlBuddy.Domain
{
    [Serializable]
    public class SqlTypeDefinition
    {
        public SqlTypeDefinition(SqlDbType? sqlType, Type netType, int? precision, int? scale)
        {
            SqlType = sqlType;
            NetType = netType;
            Precision = precision;
            Scale = scale;
        }

        public SqlDbType? SqlType { get; private set; }
        public Type NetType { get; private set; }
        public int? Precision { get; private set; }
        public int? Scale { get; private set; }
    }
}