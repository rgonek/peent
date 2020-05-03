using System.Reflection;

namespace Peent.Application.Common.DynamicQuery.Filters.Factory
{
    public interface IFilterFactory
    {
        FilterNode Create(FilterDto filter, PropertyInfo propertyInfo);
    }
}
