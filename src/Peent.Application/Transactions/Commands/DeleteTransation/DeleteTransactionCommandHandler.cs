using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;

namespace Peent.Application.Transactions.Commands.DeleteTransation
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Unit>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public DeleteTransactionCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(DeleteTransactionCommand command, CancellationToken token)
        {
            var transaction = await _db.Transactions
                .Include(x => x.Entries)
                .SingleOrDefaultAsync(x =>
                        x.Id == command.Id &&
                        x.WorkspaceId == _userAccessor.User.GetWorkspaceId(),
                    token);

            if (transaction == null)
                throw NotFoundException.Create<Transaction>(x => x.Id, command.Id);

            transaction.MarkAsDeleted(_userAccessor.User.GetUserId());

            _db.Update(transaction);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}
