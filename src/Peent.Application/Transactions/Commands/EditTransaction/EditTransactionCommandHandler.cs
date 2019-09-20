using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;

namespace Peent.Application.Transactions.Commands.EditTransaction
{
    public class EditTransactionCommandHandler : IRequestHandler<EditTransactionCommand, long>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public EditTransactionCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<long> Handle(EditTransactionCommand command, CancellationToken token)
        {
            var transaction = await _db.Transactions
                .Include(x => x.Entries)
                .SingleOrDefaultAsync(x =>
                        x.Id == command.Id &&
                        x.WorkspaceId == _userAccessor.User.GetWorkspaceId(),
                    token);

            if (transaction == null)
                throw NotFoundException.Create<Transaction>(x => x.Id, command.Id);

            transaction.CategoryId = command.CategoryId;
            transaction.Date = command.Date;
            transaction.Title = command.Title;
            transaction.Description = command.Description;
            transaction.Type = command.Type;

            var chargedEntry = transaction.Entries.Single(x => x.Amount < 0);
            var depositEntry = transaction.Entries.Single(x => x.Amount > 0);

            chargedEntry.Amount = -command.Amount;
            chargedEntry.ForeignAmount = -command.ForeignAmount;
            chargedEntry.CurrencyId = command.CurrencyId;
            chargedEntry.ForeignCurrencyId = command.ForeignCurrencyId;
            chargedEntry.AccountId = command.FromAccountId;

            depositEntry.Amount = command.Amount;
            depositEntry.ForeignAmount = command.ForeignAmount;
            depositEntry.CurrencyId = command.CurrencyId;
            depositEntry.ForeignCurrencyId = command.ForeignCurrencyId;
            depositEntry.AccountId = command.ToAccountId;

            transaction.MarkAsModified(_userAccessor.User.GetUserId());

            _db.Update(transaction);

            await _db.SaveChangesAsync(token);

            return transaction.Id;
        }
    }
}
