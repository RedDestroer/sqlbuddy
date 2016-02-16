using System;

namespace SqlBuddy.Domain
{
    [Serializable]
    public class SqlParameterDefinition
    {
        public SqlParameterDefinition(string name, SqlTypeDefinition typeDefinition, bool nullable, DefaultValue defaultValue)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (typeDefinition == null) throw new ArgumentNullException("typeDefinition");

            Name = name;
            TypeDefinition = typeDefinition;
            Nullable = nullable;
            DefaultValue = defaultValue;
        }

        public string Name { get; private set; }
        public SqlTypeDefinition TypeDefinition { get; private set; }
        public bool Nullable { get; private set; }
        public DefaultValue DefaultValue { get; private set; }
    }
}