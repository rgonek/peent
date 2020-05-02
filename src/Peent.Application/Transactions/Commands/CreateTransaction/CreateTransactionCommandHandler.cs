using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Domain.Entities;
using Peent.Domain.Entities.TransactionAggregate;

namespace Peent.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, long>
    {
        private readonly IApplicationDbContext _db;

        public CreateTransactionCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<long> Handle(CreateTransactionCommand command, CancellationToken token)
        {
            var tags = await GetTagsOrThrowsIfNotExistsAsync(command, token);
            var category = await _db.Categories.FindAsync(new object[] {command.CategoryId}, token);
            var fromAccount = await _db.Accounts.FindAsync(new object[] {command.FromAccountId}, token);
            await _db.Entry(fromAccount).Reference(x => x.Currency).LoadAsync(token);
            var toAccount = await _db.Accounts.FindAsync(new object[] {command.ToAccountId}, token);
            await _db.Entry(toAccount).Reference(x => x.Currency).LoadAsync(token);

            var transaction = new Transaction(command.Title, command.Date, command.Description, category,
                command.Amount, fromAccount, toAccount, tags);

            _db.Transactions.Attach(transaction);

            await _db.SaveChangesAsync(token);

            return transaction.Id;
        }

        private async Task<List<Tag>> GetTagsOrThrowsIfNotExistsAsync(CreateTransactionCommand command, CancellationToken token)
        {
            var tags = new List<Tag>();
            if (command.TagIds == null || !command.TagIds.Any())
            {
                return tags;
            }
            
            var ids = command.TagIds.ToList();
            tags = await _db.Tags
                .Where(x => ids.Contains(x.Id))
                .ToListAsync(token);
            if (command.TagIds.Count != tags.Count)
            {
                throw NotFoundException.Create<Tag>(x => x.Id,
                    string.Join(", ", command.TagIds));
            }

            return tags;
        }
    }
}