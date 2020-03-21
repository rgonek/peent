using System;
using Peent.Application.Categories.Models;
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
        //public ICollection<TransactionTag> TransactionTags { get; }
        //public ICollection<TransactionEntry> Entries { get; }
        public TransactionType Type { get; }

        public TransactionModel(Transaction transaction)
        {
            Id = transaction.Id;
            Title = transaction.Title;
            Description = transaction.Description;
            Date = transaction.Date;
            Category = new CategoryModel(transaction.Category);
            Type = transaction.Type;
        }
    }
}
