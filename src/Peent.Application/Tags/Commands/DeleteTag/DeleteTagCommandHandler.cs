using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Common;

namespace Peent.Application.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, Unit>
    {
        private readonly IApplicationDbContext _db;

        public DeleteTagCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<Unit> Handle(DeleteTagCommand command, CancellationToken token)
        {
            var tag = await _db.Tags.GetAsync(command.Id, token);
            _db.Remove(tag);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}
