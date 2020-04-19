using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Categories.Models;
using Peent.Application.Common;
using Peent.Application.DynamicQuery;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Common;
using Peent.Domain.Entities;

namespace Peent.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, PagedResult<CategoryModel>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetCategoriesListQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<PagedResult<CategoryModel>> Handle(GetCategoriesListQuery query, CancellationToken token)
        {
            dynamic categoriesQuery = _db.Categories
                .Where(x => x.WorkspaceId == _userAccessor.User.GetWorkspaceId())
            .OrderBy(x => x.CreationDate);

            if (query.Sort.Any())
                categoriesQuery = Sort(categoriesQuery, query.Sort);

            if (query.Filters.Any())
                categoriesQuery = Filter(categoriesQuery, query.Filters);

            var categoriesPaged = await ((IQueryable<Category>)categoriesQuery)
                .GetPagedAsync(
                    query.PageIndex,
                    query.PageSize,
                    x => new CategoryModel(x),
                    token);

            return categoriesPaged;
        }

        private IOrderedQueryable<Category> Sort(IOrderedQueryable<Category> categoriesQuery, IList<SortDto> sorts)
        {
            for (var i = 0; i < sorts.Count; i++)
            {
                var sort = sorts[i];
                categoriesQuery = sort.Field.FirstUp() switch
                {
                    nameof(Category.Name) => categoriesQuery.SortBy(x => x.Name, sort.Direction, i),
                    nameof(Category.Description) => categoriesQuery.SortBy(x => x.Description, sort.Direction, i),
                    _ => categoriesQuery
                };
            }

            return categoriesQuery;
        }

        private IQueryable<Category> Filter(IQueryable<Category> categoriesQuery, IList<FilterDto> filters)
        {
            foreach (var filter in filters.Where(x => x.Values.Any(y => y.HasValue())))
            {
                categoriesQuery = filter.Field.FirstUp() switch
                {
                    nameof(Category.Name) => categoriesQuery.Where(x => EF.Functions.Like(x.Name, $"%{filter.Values.FirstOrDefault()}%")),
                    nameof(Category.Description) => categoriesQuery.Where(x =>
                        x.Description.Contains(filter.Values.FirstOrDefault())),
                    FilterDto.Global => categoriesQuery.Where(x =>
                        x.Name.Contains(filter.Values.FirstOrDefault()) ||
                        x.Description.Contains(filter.Values.FirstOrDefault())),
                    _ => categoriesQuery
                };
            }

            return categoriesQuery;
        }
    }
}
