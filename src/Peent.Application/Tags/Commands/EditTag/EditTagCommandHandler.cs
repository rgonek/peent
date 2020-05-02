using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Infrastructure.Extensions;
using Peent.Common.Time;
using Peent.Domain.Entities;
using Peent.Domain.ValueObjects;

namespace Peent.Application.Tags.Commands.EditTag
{
    public class EditTagCommandHandler : IRequestHandler<EditTagCommand, Unit>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public EditTagCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
            => (_db, _userAccessor) = (db, userAccessor);

        public async Task<Unit> Handle(EditTagCommand command, CancellationToken token)
        {
            await ThrowsIfDuplicateAsync(command, token);
            
            var tag = await _db.Tags.FindAsync(new[] {command.Id}, token);
            tag.SetName(command.Name);
            tag.SetDescription(command.Description);

            _db.Attach(tag);
            await _db.SaveChangesAsync(token);

            return default;
        }

        private async Task ThrowsIfDuplicateAsync(EditTagCommand command, CancellationToken token)
        {
            var existingTag = await _db.Tags
                .SingleOrDefaultAsync(x =>
                        x.Id != command.Id &&
                        x.Name == command.Name &&
                        x.Workspace.Id == _userAccessor.User.GetWorkspaceId(),
                    token);

            if (existingTag != null)
                throw DuplicateException.Create<Tag>(x => x.Name, command.Name);
        }
    }
}
