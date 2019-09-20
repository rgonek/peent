using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;
using Peent.Domain.ValueObjects;

namespace Peent.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, long>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public CreateTransactionCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<long> Handle(CreateTransactionCommand command, CancellationToken token)
        {
            var transaction = new Transaction
            {
                CategoryId = command.CategoryId,
                Date = command.Date,
                Title = command.Title,
                Description = command.Description,
                Type = command.Type,
                WorkspaceId =  _userAccessor.User.GetWorkspaceId(),
                CreationInfo = new CreationInfo(_userAccessor.User.GetUserId())
            };
            var chargedEntry = new TransactionEntry
            {
                Amount = -command.Amount,
                ForeignAmount = -command.ForeignAmount,
                CurrencyId = command.CurrencyId,
                ForeignCurrencyId = command.ForeignCurrencyId,
                AccountId = command.FromAccountId,
                CreationInfo = new CreationInfo(_userAccessor.User.GetUserId()),
                Transaction = transaction
            };
            var depositEntry = new TransactionEntry
            {
                Amount = command.Amount,
                ForeignAmount = command.ForeignAmount,
                CurrencyId = command.CurrencyId,
                ForeignCurrencyId = command.ForeignCurrencyId,
                AccountId = command.ToAccountId,
                CreationInfo = new CreationInfo(_userAccessor.User.GetUserId()),
                Transaction = transaction
            };

            transaction.Entries.Add(chargedEntry);
            transaction.Entries.Add(depositEntry);

            _db.Transactions.Add(transaction);

            await _db.SaveChangesAsync(token);

            return transaction.Id;
        }
    }
}
