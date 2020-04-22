using System.Reflection;
using Peent.Application.Common;

namespace Peent.Application.DynamicQuery.Filters.Factory
{
    public interface IFilterFactory
    {
        FilterNode Create(FilterDto filter, PropertyInfo propertyInfo);
    }
}
