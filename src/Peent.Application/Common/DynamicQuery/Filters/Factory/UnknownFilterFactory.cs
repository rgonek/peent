using System.Reflection;

namespace Peent.Application.Common.DynamicQuery.Filters.Factory
{
    public class UnknownFilterFactory : IFilterFactory
    {
        public FilterNode Create(FilterDto filter, PropertyInfo propertyInfo)
        {
            return NullFilter.Create(filter, "Unknown.");
        }
    }
}
