using EnsureThat;

namespace Peent.Application.Common.DynamicQuery.Filters
{
    public class StringFilter : FilterNode
    {
        private readonly string _field;
        private readonly StringMethod _method;
        private readonly bool _isNegated;
        private readonly string _value;

        public StringFilter(string field, StringMethod method, bool isNegated, string value)
        {
            Ensure.That(field, nameof(field)).IsNotNullOrWhiteSpace();
            Ensure.That(value, nameof(value)).IsNotNullOrWhiteSpace();

            _field = field;
            _method = method;
            _isNegated = isNegated;
            _value = value;
        }

        public override string GetPredicate()
        {
            var value = string.Format(_method.GetPattern(), _value);
            return (_isNegated ? "not " : "") +
                $"DbFunctionsExtensions.Like(EF.Functions, {_field}, \"{value}\")";
        }
    }
}
