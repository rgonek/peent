using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Peent.Application.Specifications
{
    public static class QuerySpecificationExtensions
    {
        public static IQueryable<T> Specify<T>(this IQueryable<T> query, BaseSpecification<T> spec)
            where T : class
        {
            query = spec.Includes
                .Aggregate(query, (current, include) => current.Include(include));

            query = spec.IncludeStrings
                .Aggregate(query, (current, include) => current.Include(include));

            return spec.Criteria is null
                ? query
                : query.Where(spec.Criteria);
        }
    }
}
