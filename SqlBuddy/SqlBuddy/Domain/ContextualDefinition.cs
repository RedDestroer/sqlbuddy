using System;
using System.Collections.Generic;
using SqlBuddy.Conversion;

namespace SqlBuddy.Domain
{
    [Serializable]
    public abstract class ContextualDefinition
    {
        protected ContextualDefinition()
        {
            Context = new Dictionary<string, string>();
        }

        public IDictionary<string, string> Context { get; private set; }

        public T GetValue<T>(string key)
        {
            string value;
            if (!Context.TryGetValue(key, out value))
                throw new KeyNotFoundException(string.Format("Key '{0}' not found.", key));

            T result;
            if (!DataConversion.TryConvert(value, out result))
                throw new InvalidCastException(string.Format("Can't cast expression '{0}' to type '{1}'.", value, typeof(T).FullName));

            return result;
        }

        public T TryGetValue<T>(string key, T @default)
        {
            try
            {
                return GetValue<T>(key);
            }
            catch (Exception)
            {
                return @default;
            }
        }
    }
}