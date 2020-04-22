using System.Reflection;
using Peent.Application.Common;

namespace Peent.Application.DynamicQuery.Filters.Factory
{
    public class UnknownFilterFactory : IFilterFactory
    {
        public FilterNode Create(FilterDto filter, PropertyInfo propertyInfo)
        {
            return NullFilter.Create(filter, "Unknown.");
        }
    }
}
