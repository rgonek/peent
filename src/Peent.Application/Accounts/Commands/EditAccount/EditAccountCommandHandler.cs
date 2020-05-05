using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Common;

namespace Peent.Application.Accounts.Commands.EditAccount
{
    public class EditAccountCommandHandler : IRequestHandler<EditAccountCommand, Unit>
    {
        private readonly IApplicationDbContext _db;

        public EditAccountCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<Unit> Handle(EditAccountCommand command, CancellationToken token)
        {
            var account = await _db.Accounts.GetAsync(command.Id, token);

            account.SetName(command.Name);
            account.SetDescription(command.Description);
            account.SetCurrency(await _db.Currencies.GetAsync(command.CurrencyId, token));

            _db.Attach(account);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}