using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Common.Time;
using Peent.Domain.Entities;
using Peent.Domain.ValueObjects;

namespace Peent.Application.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, Unit>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public DeleteTagCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(DeleteTagCommand command, CancellationToken token)
        {
            var tag = await _db.Tags
                .SingleOrDefaultAsync(x =>
                        x.Id == command.Id &&
                        x.WorkspaceId == _userAccessor.User.GetWorkspaceId(),
                    token);

            if (tag == null)
                throw NotFoundException.Create<Tag>(x => x.Id, command.Id);

            tag.DeletionDate = Clock.UtcNow;
            tag.DeletedById = _userAccessor.User.GetUserId();

            _db.Update(tag);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}
