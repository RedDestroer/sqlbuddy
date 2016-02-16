using System;

namespace SqlBuddy.Domain
{
    [Serializable]
    public class DefaultValue
    {
        public DefaultValue(object value)
        {
            Value = value;
        }

        public object Value { get; private set; }
    }
}