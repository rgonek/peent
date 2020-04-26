using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Infrastructure.Extensions;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Commands.EditAccount
{
    public class EditAccountCommandHandler : IRequestHandler<EditAccountCommand, Unit>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public EditAccountCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(EditAccountCommand command, CancellationToken token)
        {
            var account = await _db.Accounts
                .SingleOrDefaultAsync(x =>
                        x.Id == command.Id &&
                        x.WorkspaceId == _userAccessor.User.GetWorkspaceId(),
                    token);

            if (account == null)
                throw NotFoundException.Create<Account>(x => x.Id, command.Id);

            var existingAccount = await _db.Accounts
                .SingleOrDefaultAsync(x =>
                    x.Id != command.Id &&
                    x.Name == command.Name &&
                    x.Type == account.Type &&
                    x.WorkspaceId == _userAccessor.User.GetWorkspaceId(),
                    token);
            if (existingAccount != null)
                throw DuplicateException.Create<Account>(x => x.Name, command.Name);

            var currency = await _db.Currencies.SingleOrDefaultAsync(x => x.Id == command.CurrencyId, token);
            if (currency == null)
                throw NotFoundException.Create<Currency>(x => x.Id, command.CurrencyId);

            account.SetName(command.Name);
            account.SetDescription(command.Description);
            account.SetCurrency(currency);

            _db.Update(account);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}
