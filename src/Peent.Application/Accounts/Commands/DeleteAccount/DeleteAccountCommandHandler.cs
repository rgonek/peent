using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Infrastructure.Extensions;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Unit>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public DeleteAccountCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
            => (_db, _userAccessor) = (db, userAccessor);

        public async Task<Unit> Handle(DeleteAccountCommand command, CancellationToken token)
        {
            var account = await _db.Accounts
                .SingleOrDefaultAsync(x =>
                        x.Id == command.Id &&
                        x.Workspace.Id == _userAccessor.User.GetWorkspaceId(),
                    token);

            if (account == null)
                throw NotFoundException.Create<Account>(x => x.Id, command.Id);

            _db.Remove(account);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}
