using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Common;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IApplicationDbContext _db;

        public CreateAccountCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<int> Handle(CreateAccountCommand command, CancellationToken token)
        {
            var account = new Account(
                command.Name,
                command.Description,
                command.Type,
                await _db.Currencies.GetAsync(command.CurrencyId, token));

            _db.Accounts.Attach(account);

            await _db.SaveChangesAsync(token);

            return account.Id;
        }
    }
}
