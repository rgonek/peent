using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Peent.Application.Currencies.Commands.DeleteCurrency
{
    public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, Unit>
    {
        private readonly IApplicationDbContext _db;

        public DeleteCurrencyCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<Unit> Handle(DeleteCurrencyCommand command, CancellationToken token)
        {
            var currency = await _db.Currencies.FindAsync(new[]{command.Id}, token);
            _db.Remove(currency);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}
