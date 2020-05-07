using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Categories.Models;
using Peent.Application.Common;
using Peent.Application.Common.DynamicQuery;
using Peent.Application.Common.Extensions;

namespace Peent.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, PagedResult<CategoryModel>>
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentContextService _currentContextService;

        public GetCategoriesListQueryHandler(IApplicationDbContext db, ICurrentContextService currentContextService)
            => (_db, _currentContextService) = (db, currentContextService);

        public async Task<PagedResult<CategoryModel>> Handle(GetCategoriesListQuery query, CancellationToken token)
            => await _db.Categories
                .Where(x => x.Workspace.Id == _currentContextService.Workspace.Id)
                .ApplyFilters(query)
                .ApplySort(query)
                .GetPagedAsync(
                    query.PageIndex,
                    query.PageSize,
                    x => new CategoryModel(x),
                    token);
    }
}
