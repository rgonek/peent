using System.Reflection;

namespace Peent.Application.Common.DynamicQuery.Filters.Factory
{
    public static class PropertyInfoExtensions
    {
        public static bool IsFilterable(this PropertyInfo propertyInfo)
            => propertyInfo.GetFilterFactoryType() != FilterFactoryType.Unknown;

        public static FilterFactoryType GetFilterFactoryType(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                return FilterFactoryType.Unknown;
            }

            if (propertyInfo.PropertyType == typeof(string))
            {
                return FilterFactoryType.String;
            }

            if (propertyInfo.PropertyType.IsNumeric())
            {
                return FilterFactoryType.Numeric;
            }

            if (propertyInfo.PropertyType.IsEnum)
            {
                return FilterFactoryType.Enum;
            }

            return FilterFactoryType.Unknown;
        }
    }
}
