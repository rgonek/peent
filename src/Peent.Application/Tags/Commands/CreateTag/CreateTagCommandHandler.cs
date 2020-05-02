using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Infrastructure.Extensions;
using Peent.Domain.Entities;

namespace Peent.Application.Tags.Commands.CreateTag
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public CreateTagCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
            => (_db, _userAccessor) = (db, userAccessor);

        public async Task<int> Handle(CreateTagCommand command, CancellationToken token)
        {
            var tag = new Tag(
                command.Name,
                command.Description,
                Workspace.FromId(_userAccessor.User.GetWorkspaceId()));

            _db.Tags.Attach(tag);

            await _db.SaveChangesAsync(token);

            return tag.Id;
        }
    }
}
