using Peent.Application.Common;

namespace Peent.Application.DynamicQuery.Filters
{
    public class NullFilter : FilterNode
    {
        public override string GetPredicate() => true.ToString();

        public static NullFilter Create(FilterDto filter, string reason)
        {
            // log
            return new NullFilter();
        }
    }
}
