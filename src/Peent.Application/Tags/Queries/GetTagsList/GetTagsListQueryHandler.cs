using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Tags.Models;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;

namespace Peent.Application.Tags.Queries.GetTagsList
{
    public class GetTagsListQueryHandler : IRequestHandler<GetTagsListQuery, IList<TagModel>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetTagsListQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<IList<TagModel>> Handle(GetTagsListQuery query, CancellationToken token)
        {
            var categories = await _db.Tags
                .Where(x => x.WorkspaceId == _userAccessor.User.GetWorkspaceId() &&
                    x.DeletionDate.HasValue == false)
                .ToListAsync(token);

            return categories.Select(x => new TagModel(x)).ToList();
        }
    }
}
