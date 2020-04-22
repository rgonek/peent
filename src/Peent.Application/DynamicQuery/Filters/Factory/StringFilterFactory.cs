using System.Linq;
using System.Reflection;
using Peent.Application.Common;

namespace Peent.Application.DynamicQuery.Filters.Factory
{
    public class StringFilterFactory : IFilterFactory
    {
        public FilterNode Create(FilterDto filter, PropertyInfo propertyInfo)
        {
            return new StringFilter(
                filter.Field,
                StringMethod.Contains,
                filter.IsNegated,
                filter.Values.FirstOrDefault());
        }
    }
}
