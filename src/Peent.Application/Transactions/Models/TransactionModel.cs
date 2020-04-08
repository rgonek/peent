using System;
using System.Collections.Generic;
using System.Linq;
using Peent.Application.Accounts.Models;
using Peent.Application.Categories.Models;
using Peent.Application.Tags.Models;
using Peent.Domain.Entities;

namespace Peent.Application.Transactions.Models
{
    public sealed class TransactionModel
    {
        public long Id { get; }

        public string Title { get; }
        public string Description { get; }
        public DateTime Date { get; }
        public CategoryModel Category { get; }
        public ICollection<TagModel> Tags { get; }
        public AccountModel SourceAccount { get; }
        public AccountModel DestinationAccount { get; }
        public decimal Amount { get; }
        public TransactionType Type { get; }

        public int SourceAccountId { get; }
        public int DestinationAccountId { get; }
        public int CategoryId { get; }
        public ICollection<int> TagIds { get; }

        public TransactionModel(Transaction transaction)
        {
            Id = transaction.Id;
            Title = transaction.Title;
            Description = transaction.Description;
            Date = transaction.Date;
            Category = new CategoryModel(transaction.Category);
            Type = transaction.Type;
            Tags = transaction.TransactionTags.Select(x => new TagModel(x.Tag)).ToList();

            Amount = Math.Abs(transaction.Entries.First().Amount);
            SourceAccount = new AccountModel(transaction.Entries.First(x => x.Amount < 0).Account);
            DestinationAccount = new AccountModel(transaction.Entries.First(x => x.Amount > 0).Account);

            SourceAccountId = SourceAccount.Id;
            DestinationAccountId = DestinationAccount.Id;
            CategoryId = Category.Id;
            TagIds = Tags.Select(x => x.Id).ToList();
        }
    }
}
