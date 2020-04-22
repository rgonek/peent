namespace Peent.Application.DynamicQuery.Filters
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
