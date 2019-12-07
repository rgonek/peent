using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Common;
using Peent.Application.Tags.Models;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;

namespace Peent.Application.Tags.Queries.GetTagsList
{
    public class GetTagsListQueryHandler : IRequestHandler<GetTagsListQuery, PagedResult<TagModel>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetTagsListQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<PagedResult<TagModel>> Handle(GetTagsListQuery query, CancellationToken token)
        {
            var tagsPaged = await _db.Tags
                .Where(x => x.WorkspaceId == _userAccessor.User.GetWorkspaceId() &&
                            x.DeletionDate.HasValue == false)
                .OrderBy(x => x.Id)
                .GetPagedAsync(
                    query.PageIndex,
                    query.PageSize,
                    x => new TagModel(x),
                    token);

            return tagsPaged;
        }
    }
}
