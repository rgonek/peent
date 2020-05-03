using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Common;
using Peent.Application.Common.Extensions;
using Peent.Application.Exceptions;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Commands.EditAccount
{
    public class EditAccountCommandHandler : IRequestHandler<EditAccountCommand, Unit>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public EditAccountCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
            => (_db, _userAccessor) = (db, userAccessor);

        public async Task<Unit> Handle(EditAccountCommand command, CancellationToken token)
        {
            var account = await _db.Accounts.GetAsync(command.Id, token);
            await ThrowsIfDuplicateAsync(command, token, account);

            account.SetName(command.Name);
            account.SetDescription(command.Description);
            account.SetCurrency(await _db.Currencies.GetAsync(command.CurrencyId, token));

            _db.Attach(account);
            await _db.SaveChangesAsync(token);

            return default;
        }

        private async Task ThrowsIfDuplicateAsync(EditAccountCommand command, CancellationToken token, Account account)
        {
            var existingAccount = await _db.Accounts
                .SingleOrDefaultAsync(x =>
                        x.Id != command.Id &&
                        x.Name == command.Name &&
                        x.Type == account.Type &&
                        x.Workspace.Id == _userAccessor.User.GetWorkspaceId(),
                    token);
            if (existingAccount != null)
            {
                throw DuplicateException.Create<Account>(x => x.Name, command.Name);
            }
        }
    }
}