using System;
using SqlBuddy.Parsers;

namespace SqlBuddy.Domain
{
    [Serializable]
    public class SqlParameterDefinition
        : ContextualDefinition
    {
        public SqlParameterDefinition(string name, SqlTypeDefinition typeDefinition, DefaultValue defaultValue, Direction direction, uint row, uint column)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (typeDefinition == null) throw new ArgumentNullException("typeDefinition");

            Name = name;
            TypeDefinition = typeDefinition;
            DefaultValue = defaultValue;
            Direction = direction;
            Row = row;
            Column = column;

            if (name.Length > 0)
            {
                var parameterName = name.Substring(1, name.Length - 1);

                Context[SqlParameterContextKeys.NetParameterName] = Helper.NormalizeNetName(parameterName);
                Context[SqlParameterContextKeys.SqlParameterName] = "@" + Context[SqlParameterContextKeys.NetParameterName];
                Context[SqlParameterContextKeys.NetTypeName] = Helper.NormalizeNetTypeName(typeDefinition.NetType);
            }
        }

        public string Name { get; private set; }
        public SqlTypeDefinition TypeDefinition { get; private set; }
        public DefaultValue DefaultValue { get; private set; }
        public Direction Direction { get; private set; }
        public uint Row { get; private set; }
        public uint Column { get; private set; }
    }
}