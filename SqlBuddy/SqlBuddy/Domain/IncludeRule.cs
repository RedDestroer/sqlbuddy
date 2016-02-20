using System;

namespace SqlBuddy.Domain
{
    [Serializable]
    public class IncludeRule
        : ProcessRule
    {
        public IncludeRule(string pattern)
            : base(pattern)
        {
        }

        protected override bool IsGoodOnMatch()
        {
            return true;
        }
    }
}