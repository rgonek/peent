using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Peent.Application.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Unit>
    {
        private readonly IApplicationDbContext _db;

        public DeleteAccountCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<Unit> Handle(DeleteAccountCommand command, CancellationToken token)
        {
            _db.Remove(await _db.Accounts.FindAsync(new[] {command.Id}, token));
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}