using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EnsureThat;

namespace Peent.Application.Common.DynamicQuery.Filters
{
    public static class TypeExtensions
    {
        private static readonly IEnumerable<Type> NumericTypes = new[]
        {
            typeof(sbyte),   typeof(sbyte?),  typeof(byte),   typeof(byte?),
            typeof(short),   typeof(short?),  typeof(ushort), typeof(ushort?),
            typeof(int),     typeof(int?),    typeof(int),    typeof(int?),
            typeof(long),    typeof(long?),   typeof(long),   typeof(long?),
            typeof(float),   typeof(float?),  typeof(float),  typeof(float?),
            typeof(double),  typeof(double?), typeof(double), typeof(double?),
            typeof(decimal), typeof(decimal?)
        };

        public static bool IsNumeric(this Type type)
        {
            return NumericTypes.Contains(type);
        }

        public static PropertyInfo GetNestedPropertyInfo(this Type type, string propertyName)
        {
            Ensure.That(type, nameof(type)).IsNotNull();
            Ensure.That(propertyName, nameof(propertyName)).IsNotNull();

            if (!propertyName.Contains("."))
            {
                return type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            }

            var parts = propertyName.Split('.', 2);
            return GetNestedPropertyInfo(GetNestedPropertyInfo(type, parts[0])?.PropertyType, parts[1]);
        }
    }
}
