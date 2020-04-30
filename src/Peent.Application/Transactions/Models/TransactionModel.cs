using System;
using System.Collections.Generic;
using System.Linq;
using Peent.Application.Accounts.Models;
using Peent.Application.Categories.Models;
using Peent.Application.Tags.Models;
using Peent.Domain.Entities;
using Peent.Domain.Entities.TransactionAggregate;

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
        public decimal RelativeAmount
        {
            get { return Type == TransactionType.Deposit ? Amount : -Amount; }
        }
        public TransactionType Type { get; }

        public TransactionModel(Transaction transaction)
        {
            Id = transaction.Id;
            Title = transaction.Title;
            Description = transaction.Description;
            Date = transaction.Date;
            Category = new CategoryModel(transaction.Category);
            Type = transaction.Type;
            Tags = transaction.TransactionTags.Select(x => new TagModel(x.Tag)).ToList();

            Amount = Math.Abs(transaction.Entries.First().Money.Amount);
            SourceAccount = new AccountModel(transaction.Entries.First(x => x.Money.Amount < 0).Account);
            DestinationAccount = new AccountModel(transaction.Entries.First(x => x.Money.Amount > 0).Account);
        }
    }
}
