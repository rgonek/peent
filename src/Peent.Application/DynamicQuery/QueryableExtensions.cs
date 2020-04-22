using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using EnsureThat;
using Peent.Application.DynamicQuery.Filters;
using Peent.Application.DynamicQuery.Sorts;
using Peent.Application.Infrastructure;

namespace Peent.Application.DynamicQuery
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, IHaveFilters filtersContainer)
        {
            Ensure.That(query, nameof(query)).IsNotNull();
            Ensure.That(filtersContainer, nameof(filtersContainer)).IsNotNull();

            return query.ApplyFilter(FiltersConverterService.Convert<T>(filtersContainer));
        }

        private static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, FilterNode filter)
        {
            if (filter == null)
            {
                return query;
            }

            var config = new ParsingConfig
            {
                CustomTypeProvider = new CustomTypeProvider(),
                UseParameterizedNamesInDynamicQuery = true
            };
            return query.Where(config, filter.GetPredicate());
        }

        public static IQueryable<T> ApplySort<T>(this IQueryable<T> query, IHaveSorts sortsContainer, Sort @default = null)
        {
            Ensure.That(query, nameof(query)).IsNotNull();
            Ensure.That(sortsContainer, nameof(sortsContainer)).IsNotNull();

            if (sortsContainer.Sorts == null || sortsContainer.Sorts.Any() == false)
            {
                return query.ApplySort(new[] { @default ?? Sort.Default<T>() });
            }

            return query.ApplySort(sortsContainer.Sorts.Select(x => new Sort(x.Field, x.Direction)));
        }

        private static IQueryable<T> ApplySort<T>(this IQueryable<T> query, IEnumerable<Sort> sorts)
        {
            Ensure.That(query, nameof(query)).IsNotNull();

            if (sorts == null || sorts.Any() == false)
            {
                return query;
            }

            var orderBy = string.Join(", ", sorts.Select(x => x.GetPredicate()));
            return query.OrderBy(orderBy);
        }
    }
}
