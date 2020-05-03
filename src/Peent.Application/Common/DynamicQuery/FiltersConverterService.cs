using System.Collections.Generic;
using System.Linq;
using EnsureThat;
using Peent.Application.Common.DynamicQuery.Contracts;
using Peent.Application.Common.DynamicQuery.Filters;
using Peent.Application.Common.DynamicQuery.Filters.Factory;

namespace Peent.Application.Common.DynamicQuery
{
    public static class FiltersConverterService
    {
        private static readonly FilterFactory FilterFactory = new FilterFactory();

        public static FilterNode Convert<TFilteredEntity>(IHaveFilters filtersContainer)
        {
            Ensure.That(filtersContainer, nameof(filtersContainer)).IsNotNull();

            return filtersContainer.Filters == null || filtersContainer.Filters.Any() == false
                ? null
                : new FilterGroup(Logic.And, filtersContainer.Filters.Select(ConvertToFilterNode<TFilteredEntity>));
        }

        private static FilterNode ConvertToFilterNode<TFilteredEntity>(FilterDto filter)
            => filter.IsGlobal
                ? new FilterGroup(Logic.Or, GetFiltersForAllFilterableProperties<TFilteredEntity>(filter))
                : FilterFactory.Create<TFilteredEntity>(filter);

        private static IEnumerable<FilterNode> GetFiltersForAllFilterableProperties<T>(FilterDto filter)
            => from property in typeof(T).GetProperties()
                where property.IsFilterable()
                select FilterFactory.Create(new FilterDto(property.Name, filter.Values, filter.IsNegated), property);
    }
}
