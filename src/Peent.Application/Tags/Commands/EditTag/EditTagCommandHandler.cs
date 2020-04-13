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

namespace Peent.Application.Tags.Commands.EditTag
{
    public class EditTagCommandHandler : IRequestHandler<EditTagCommand, Unit>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public EditTagCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(EditTagCommand command, CancellationToken token)
        {
            var tag = await _db.Tags
                .SingleOrDefaultAsync(x =>
                        x.Id == command.Id &&
                        x.WorkspaceId == _userAccessor.User.GetWorkspaceId(),
                    token);

            if (tag == null)
                throw NotFoundException.Create<Tag>(x => x.Id, command.Id);

            var existingTag = await _db.Tags
                .SingleOrDefaultAsync(x =>
                    x.Id != command.Id &&
                    x.Name == command.Name &&
                    x.WorkspaceId == _userAccessor.User.GetWorkspaceId(),
                    token);

            if (existingTag != null)
                throw DuplicateException.Create<Tag>(x => x.Name, command.Name);

            tag.SetName(command.Name);
            tag.SetDescription(command.Description);
            tag.SetDate(command.Date);

            _db.Update(tag);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}
