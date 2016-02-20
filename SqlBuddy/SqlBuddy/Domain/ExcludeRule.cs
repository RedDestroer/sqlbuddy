using System;

namespace SqlBuddy.Domain
{
    [Serializable]
    public class ExcludeRule
        : ProcessRule
    {
        public ExcludeRule(string pattern)
            : base(pattern)
        {
        }

        protected override bool IsGoodOnMatch()
        {
            return false;
        }
    }
}