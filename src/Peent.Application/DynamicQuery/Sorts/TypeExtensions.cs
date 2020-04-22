using System;
using System.Linq;
using EnsureThat;

namespace Peent.Application.DynamicQuery.Sorts
{
    public static class TypeExtensions
    {
        public static bool IsAssignableFromRawGeneric(this Type extendType, Type baseType)
        {
            Ensure.That(extendType, nameof(extendType)).IsNotNull();
            Ensure.That(baseType, nameof(baseType)).IsNotNull();

            return baseType.GetInterfaces().Any(x => extendType == (x.IsGenericType ? x.GetGenericTypeDefinition() : x));
        }
    }
}
