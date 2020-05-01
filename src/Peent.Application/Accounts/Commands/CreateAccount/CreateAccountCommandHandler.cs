using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Infrastructure.Extensions;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public CreateAccountCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
            => (_db, _userAccessor) = (db, userAccessor);

        public async Task<int> Handle(CreateAccountCommand command, CancellationToken token)
        {
            var existingAccount = await _db.Accounts
                .SingleOrDefaultAsync(x =>
                    x.Name == command.Name &&
                    x.Workspace.Id == _userAccessor.User.GetWorkspaceId() &&
                    x.Type == command.Type,
                    token);
            if (existingAccount != null)
                throw DuplicateException.Create<Account>(x => x.Name, command.Name);

            var currency = await _db.Currencies.SingleOrDefaultAsync(x => x.Id == command.CurrencyId, token);
            if (currency == null)
                throw NotFoundException.Create<Currency>(x => x.Id, command.CurrencyId);

            var account = new Account(
                command.Name,
                command.Description,
                command.Type,
                currency,
                Workspace.FromId(_userAccessor.User.GetWorkspaceId()));

            _db.Accounts.Attach(account);

            await _db.SaveChangesAsync(token);

            return account.Id;
        }
    }
}
