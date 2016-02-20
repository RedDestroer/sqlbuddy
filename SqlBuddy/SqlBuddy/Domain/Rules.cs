using System;
using System.Collections.Generic;

namespace SqlBuddy.Domain
{
    [Serializable]
    public class Rules
    {
        public Rules()
        {
            ProcessRules = new List<ProcessRule>();
            IsGoodByDefault = true;
        }

        public bool IsGoodByDefault { get; set; }
        public IList<ProcessRule> ProcessRules { get; private set; }

        public bool IsGood(string s)
        {
            string pattern;
            return IsGood(s, out pattern);
        }

        public bool IsGood(string s, out string pattern)
        {
            foreach (var processRule in ProcessRules)
            {
                if (!processRule.Ignore(s))
                {
                    pattern = processRule.Pattern;
                    return processRule.IsGood(s);
                }
            }

            pattern = null;
            return IsGoodByDefault;
        }
    }
}