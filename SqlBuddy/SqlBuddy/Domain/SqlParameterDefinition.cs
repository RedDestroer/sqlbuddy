using System;
using SqlBuddy.Parsers;

namespace SqlBuddy.Domain
{
    [Serializable]
    public class SqlParameterDefinition
        : ContextualDefinition
    {
        public SqlParameterDefinition(string name, SqlTypeDefinition typeDefinition, bool nullable, DefaultValue defaultValue, Direction direction)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (typeDefinition == null) throw new ArgumentNullException("typeDefinition");

            Name = name;
            TypeDefinition = typeDefinition;
            Nullable = nullable;
            DefaultValue = defaultValue;
            Direction = direction;

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
        public bool Nullable { get; private set; }
        public DefaultValue DefaultValue { get; private set; }
        public Direction Direction { get; private set; }
    }
}