using EnsureThat;

namespace Peent.Application.DynamicQuery.Filters
{
    public class Filter : FilterNode
    {
        private readonly string _field;
        private readonly Operator _operator;
        private readonly bool _isNegated;
        private readonly string _value;

        public Filter(string field, Operator @operator, bool isNegated, string value)
        {
            Ensure.That(field, nameof(field)).IsNotNullOrWhiteSpace();
            Ensure.That(value, nameof(value)).IsNotNull();

            _field = field;
            _operator = @operator;
            _isNegated = isNegated;
            _value = value;
        }

        public override string GetPredicate()
        {
            return (_isNegated ? "not " : "") +
                   $"({_field} {_operator.GetSymbol()} {_value})";
        }
    }
}
