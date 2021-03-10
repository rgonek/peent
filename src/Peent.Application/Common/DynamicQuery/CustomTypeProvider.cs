using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core.CustomTypeProviders;
using System.Reflection;

namespace Peent.Application.Common.DynamicQuery
{
    public class CustomTypeProvider : AbstractDynamicLinqCustomTypeProvider, IDynamicLinkCustomTypeProvider
    {
        public HashSet<Type> GetCustomTypes()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var set = new HashSet<Type>(FindTypesMarkedWithDynamicLinqTypeAttribute(assemblies));

            set.Add(typeof(Microsoft.EntityFrameworkCore.EF));
            set.Add(typeof(Microsoft.EntityFrameworkCore.DbFunctionsExtensions));

            return set;
        }

        public Dictionary<Type, List<MethodInfo>> GetExtensionMethods()
        {
            return new Dictionary<Type, List<MethodInfo>>();
        }

        public Type ResolveType(string typeName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return ResolveType(assemblies, typeName);
        }

        public Type ResolveTypeBySimpleName(string typeName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return ResolveTypeBySimpleName(assemblies, typeName);
        }
    }
}
