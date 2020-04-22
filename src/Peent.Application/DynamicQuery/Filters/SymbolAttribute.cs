using System;

namespace Peent.Application.DynamicQuery.Filters
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SymbolAttribute : Attribute
    {
        public string Symbol { get; }

        public SymbolAttribute(string symbol)
        {
            Symbol = symbol;
        }
    }
}
