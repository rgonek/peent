using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Common;
using Peent.Application.DynamicQuery;
using Peent.Application.Tags.Models;
using Peent.Application.Infrastructure.Extensions;

namespace Peent.Application.Tags.Queries.GetTagsList
{
    public class GetTagsListQueryHandler : IRequestHandler<GetTagsListQuery, PagedResult<TagModel>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetTagsListQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
            => (_db, _userAccessor) = (db, userAccessor);

        public async Task<PagedResult<TagModel>> Handle(GetTagsListQuery query, CancellationToken token)
            => await _db.Tags
                .Where(x => x.Workspace.Id == _userAccessor.User.GetWorkspaceId())
                .ApplyFilters(query)
                .ApplySort(query)
                .GetPagedAsync(
                    query.PageIndex,
                    query.PageSize,
                    x => new TagModel(x),
                    token);
    }
}
