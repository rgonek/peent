using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;

namespace Peent.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, long>
    {
        private readonly IApplicationDbContext _db;

        public CreateTransactionCommandHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<long> Handle(CreateTransactionCommand command, CancellationToken token)
        {
            var category = await _db.Categories
                .Where(x => x.Id == command.CategoryId)
                .SingleOrDefaultAsync(token);
            if (category == null)
                throw NotFoundException.Create<Category>(x => x.Id, command.CategoryId);

            var tags = new List<Tag>();
            if (command.TagIds != null && command.TagIds.Any())
            {
                var ids = command.TagIds.ToList();
                tags = await _db.Tags
                    .Where(x => ids.Contains(x.Id))
                    .ToListAsync(token);
                if (command.TagIds.Count != tags.Count)
                    throw NotFoundException.Create<Tag>(x => x.Id,
                        string.Join(", ", command.TagIds));
            }

            var sourceAccount = await _db.Accounts
                .Where(x => x.Id == command.SourceAccountId)
                .SingleOrDefaultAsync(token);
            if (sourceAccount == null)
                throw NotFoundException.Create<Account>(x => x.Id, command.SourceAccountId);

            var destinationAccount = await _db.Accounts
                .Where(x => x.Id == command.DestinationAccountId)
                .SingleOrDefaultAsync(token);
            if (destinationAccount == null)
                throw NotFoundException.Create<Account>(x => x.Id, command.DestinationAccountId);

            var entries = new List<TransactionEntry>
            {
                new TransactionEntry(sourceAccount, -command.Amount, sourceAccount.CurrencyId),
                new TransactionEntry(destinationAccount, command.Amount, destinationAccount.CurrencyId)
            };
            var transaction = new Transaction(command.Title, command.Date, command.Description, command.CategoryId, entries);
            if (tags.Any())
                transaction.AddTags(tags);

            _db.Transactions.Add(transaction);

            await _db.SaveChangesAsync(token);

            return transaction.Id;
        }
    }
}
