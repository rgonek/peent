using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Peent.Application.Tags.Commands.EditTag
{
    public class EditTagCommandHandler : IRequestHandler<EditTagCommand, Unit>
    {
        private readonly IApplicationDbContext _db;

        public EditTagCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
            => _db = db;

        public async Task<Unit> Handle(EditTagCommand command, CancellationToken token)
        {
            var tag = await _db.Tags.FindAsync(new[] {command.Id}, token);
            tag.SetName(command.Name);
            tag.SetDescription(command.Description);

            _db.Attach(tag);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}
