using System;

namespace Peent.Application.DynamicQuery.Filters
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
