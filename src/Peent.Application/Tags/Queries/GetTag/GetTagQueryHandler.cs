using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Tags.Models;
using Peent.Domain.Entities;

namespace Peent.Application.Tags.Queries.GetTag
{
    public class GetTagQueryHandler : IRequestHandler<GetTagQuery, TagModel>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetTagQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<TagModel> Handle(GetTagQuery query, CancellationToken token)
        {
            var tag = await _db.Tags
                .SingleOrDefaultAsync(x => x.Id == query.Id &&
                    x.WorkspaceId == _userAccessor.User.GetWorkspaceId(),
                    cancellationToken: token);

            if (tag == null)
                throw NotFoundException.Create<Tag>(x => x.Id, query.Id);

            return new TagModel(tag);
        }
    }
}
