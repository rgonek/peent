using System;
using System.Collections.Generic;
using System.Linq;
using Peent.Application.Categories.Models;
using Peent.Domain.Entities;

namespace Peent.Application.Transactions.Models
{
    public class TransactionModel
    {
        public long Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public CategoryModel Category { get; set; }
        public TransactionType Type { get; set; }
        public IList<TransactionEntryModel> Entries { get; set; }

        public TransactionModel(Transaction transaction)
        {
            Id = transaction.Id;
            Title = transaction.Title;
            Description = transaction.Description;
            Date = transaction.Date;
            Category = new CategoryModel(transaction.Category);
            Type = transaction.Type;
            Entries = transaction.Entries.Select(x => new TransactionEntryModel(x)).ToList();
        }
    }
}
