using System.Linq;
using System.Reflection;
using Peent.Application.Common;

namespace Peent.Application.DynamicQuery.Filters.Factory
{
    public class NumericFilterFactory : IFilterFactory
    {
        private static readonly char[] AllowedOperators = { '<', '>', '=' };

        public FilterNode Create(FilterDto filter, PropertyInfo propertyInfo)
        {
            var value = filter.Values.FirstOrDefault();
            var trimmedValue = value.TrimStart(AllowedOperators);
            var operatorString = value.Replace(trimmedValue, "");

            if (TryParseOperator(operatorString, out var @operator) == false)
            {
                return NullFilter.Create(filter, "Incorrect operator prefix.");
            }
            if (IsValidNumber(trimmedValue) == false)
            {
                return NullFilter.Create(filter, "Incorrect number format.");
            }

            return new Filter(filter.Field, @operator, filter.IsNegated, trimmedValue);
        }

        private static bool TryParseOperator(string operatorString, out Operator @operator)
        {
            if (string.IsNullOrEmpty(operatorString) == false)
            {
                @operator = Operator.Equals;
                return false;
            }

            return operatorString.TryParseSymbol(out @operator);
        }

        private static bool IsValidNumber(string trimmedValue)
        {
            return decimal.TryParse(trimmedValue, out _);
        }
    }
}
