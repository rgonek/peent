using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Common;

namespace Peent.Application.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query,
            int page, int pageSize, CancellationToken token = default) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = await query.CountAsync(token)
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(token);

            return result;
        }

        public static async Task<PagedResult<U>> GetPagedAsync<T, U>(
            this IQueryable<T> query,
            int page, int pageSize,
            Func<T, U> map,
            CancellationToken token = default)
            where U : class
            where T : class
        {
            var pagedResult = await query.GetPagedAsync<T>(page, pageSize, token);
            return new PagedResult<U>
            {
                CurrentPage = pagedResult.CurrentPage,
                PageSize = pagedResult.PageSize,
                RowCount = pagedResult.RowCount,
                PageCount = pagedResult.PageCount,
                Results = pagedResult.Results.Select(map).ToList()
            };
        }

        public static IOrderedQueryable<TSource> SortBy<TSource, TKey>(
            this IOrderedQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector,
            SortDirection direction,
            int sortIndex)
        {
            if (sortIndex == 0)
                return direction == SortDirection.Asc
                    ? source.OrderBy(keySelector)
                    : source.OrderByDescending(keySelector);

            return direction == SortDirection.Asc
                ? source.ThenBy(keySelector)
                : source.ThenByDescending(keySelector);
        }
    }
}
