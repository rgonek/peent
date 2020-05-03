using System;

namespace Peent.Application.Common.DynamicQuery.Filters
{
    [AttributeUsage(AttributeTargets.Field)]
    public class PatternAttribute : Attribute
    {
        public string Pattern { get; }

        public PatternAttribute(string pattern)
        {
            Pattern = pattern;
        }
    }
}
