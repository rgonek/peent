using System;
using System.Collections.Generic;
using System.Reflection;
using EnumsNET;
using Peent.Application.Common;

namespace Peent.Application.DynamicQuery.Filters.Factory
{
    public class FilterFactory
    {
        private readonly Dictionary<FilterFactoryType, IFilterFactory> _factories;

        public FilterFactory()
        {
            _factories = new Dictionary<FilterFactoryType, IFilterFactory>();

            foreach (var filterFactoryType in Enums.GetValues<FilterFactoryType>())
            {
                var factoryType = Type.GetType($"Peent.Application.DynamicQuery.Filters.Factory.{filterFactoryType}FilterFactory");
                if (factoryType == null)
                {
                    throw new InvalidOperationException($"Not found factory {filterFactoryType}FilterFactory");
                }
                var factory = (IFilterFactory)Activator.CreateInstance(factoryType);
                _factories.Add(filterFactoryType, factory);
            }
        }

        public FilterNode Create<TFilteredEntity>(FilterDto filter)
            => Create(filter, typeof(TFilteredEntity).GetNestedPropertyInfo(filter.Field));

        public FilterNode Create(FilterDto filter, PropertyInfo propertyInfo)
            => _factories[propertyInfo.GetFilterFactoryType()].Create(filter, propertyInfo);
    }
}
