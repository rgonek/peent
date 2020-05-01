using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Categories.Models;
using Peent.Application.Common;
using Peent.Application.DynamicQuery;
using Peent.Application.Infrastructure.Extensions;

namespace Peent.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, PagedResult<CategoryModel>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetCategoriesListQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
            => (_db, _userAccessor) = (db, userAccessor);

        public async Task<PagedResult<CategoryModel>> Handle(GetCategoriesListQuery query, CancellationToken token)
            => await _db.Categories
                .Where(x => x.Workspace.Id == _userAccessor.User.GetWorkspaceId())
                .ApplyFilters(query)
                .ApplySort(query)
                .GetPagedAsync(
                    query.PageIndex,
                    query.PageSize,
                    x => new CategoryModel(x),
                    token);
    }
}
