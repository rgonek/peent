using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;
using Peent.Domain.Entities.TransactionAggregate;

namespace Peent.Application.Transactions.Commands.DeleteTransaction
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
                .SingleOrDefaultAsync(x =>
                        x.Id == command.Id,
                    token);

            if (transaction == null)
                throw NotFoundException.Create<Transaction>(x => x.Id, command.Id);

            _db.Remove(transaction);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}
