namespace Peent.Application.Common.DynamicQuery.Filters
{
    public enum Operator
    {
        [Symbol("=")]
        Equals,
        [Symbol("<")]
        LessThan,
        [Symbol("<=")]
        LessThanOrEquals,
        [Symbol(">")]
        GreaterThan,
        [Symbol(">=")]
        GreaterThanOrEquals
    }
}
