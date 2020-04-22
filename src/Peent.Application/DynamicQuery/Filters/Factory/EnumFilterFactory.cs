using System.Linq;
using System.Reflection;
using EnumsNET;
using Peent.Application.Common;

namespace Peent.Application.DynamicQuery.Filters.Factory
{
    public class EnumFilterFactory : IFilterFactory
    {
        public FilterNode Create(FilterDto filter, PropertyInfo propertyInfo)
        {
            if (Enums.TryParse(propertyInfo.PropertyType, filter.Values.FirstOrDefault(), true, out object @enum) == false)
            {
                return NullFilter.Create(filter, "Cannot parse enum.");
            }

            var value = Enums.ToInt32(propertyInfo.PropertyType, @enum);

            return new Filter(filter.Field, Operator.Equals, filter.IsNegated, value.ToString());
        }
    }
}
