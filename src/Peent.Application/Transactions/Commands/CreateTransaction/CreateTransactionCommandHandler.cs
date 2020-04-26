﻿using System.Collections.Generic;
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
        {
            _db = db;
        }

        public async Task<long> Handle(CreateTransactionCommand command, CancellationToken token)
        {
            var category = await _db.Categories.SingleOrDefaultAsync(x => x.Id == command.CategoryId, token);
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

            var fromAccount = await _db.Accounts.SingleOrDefaultAsync(x => x.Id == command.FromAccountId, token);
            if (fromAccount == null)
                throw NotFoundException.Create<Account>(x => x.Id, command.FromAccountId);

            var toAccount = await _db.Accounts.SingleOrDefaultAsync(x => x.Id == command.ToAccountId, token);
            if (toAccount == null)
                throw NotFoundException.Create<Account>(x => x.Id, command.ToAccountId);

            var transaction = new Transaction(command.Title, command.Date, command.Description, category, command.Amount, fromAccount, toAccount, tags);

            _db.Transactions.Add(transaction);

            await _db.SaveChangesAsync(token);

            return transaction.Id;
        }
    }
}
