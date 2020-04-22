using System;
using System.Collections.Generic;
using System.Linq;
using EnsureThat;

namespace Peent.Application.Common
{
    public class FilterDto
    {
        public string Field { get; }
        public IEnumerable<string> Values { get; }
        public bool IsNegated { get; }

        public const char NegationSymbol = '!';
        public const string Global = "Q";
        public bool IsGlobal => Field.Equals(Global, StringComparison.InvariantCultureIgnoreCase);

        public FilterDto(string field, IEnumerable<string> values, bool? isNegated = null)
        {
            Ensure.That(field, nameof(field)).IsNotNullOrWhiteSpace();
            Ensure.That(values, nameof(values)).IsNotNull();

            Field = field;
            Values = values.Select(x => x.TrimStart(NegationSymbol));

            if (isNegated.HasValue == false)
            {
                var firstValue = values.FirstOrDefault();
                IsNegated = firstValue.StartsWith(NegationSymbol);
            }
        }
    }
}
