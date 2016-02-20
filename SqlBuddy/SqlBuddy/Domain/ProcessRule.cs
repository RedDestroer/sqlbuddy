using System;

namespace SqlBuddy.Domain
{
    [Serializable]
    public abstract class ProcessRule
    {
        protected ProcessRule(string pattern)
        {
            Pattern = pattern;
        }

        public string Pattern { get; private set; }

        public bool Ignore(string s)
        {
            return string.Compare(s, Pattern, StringComparison.InvariantCulture) != 0;
        }

        public bool IsGood(string s)
        {
            var matched = string.Compare(s, Pattern, StringComparison.InvariantCulture) == 0;
            return matched && IsGoodOnMatch();
        }

        protected abstract bool IsGoodOnMatch();
    }
}