using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Common;
using Peent.Application.Common.Extensions;
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
            var account = new Account(
                command.Name,
                command.Description,
                command.Type,
                await _db.Currencies.GetAsync(command.CurrencyId, token),
                Workspace.FromId(_userAccessor.User.GetWorkspaceId()));

            _db.Accounts.Attach(account);

            await _db.SaveChangesAsync(token);

            return account.Id;
        }
    }
}
