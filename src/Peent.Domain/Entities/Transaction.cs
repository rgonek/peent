using System;
using System.Collections.Generic;
using System.Linq;
using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class Transaction : AuditableEntity
    {
        public Transaction()
        {
            TransactionTags = new HashSet<TransactionTag>();
            Entries = new HashSet<TransactionEntry>();
        }

        public long Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<TransactionTag> TransactionTags { get; }
        public ICollection<TransactionEntry> Entries { get; }
        public TransactionType Type { get; set; }

        public void AddTags(List<Tag> tags)
        {
            foreach (var tag in tags)
                AddTag(tag);
        }

        public void AddTag(Tag tag)
        {
            TransactionTags.Add(new TransactionTag
            {
                Tag = tag,
                Transaction = this
            });
        }

        public void AddEntry(Account account, int currencyId, decimal amount)
        {
            Entries.Add(new TransactionEntry
            {
                Transaction = this,
                Account = account,
                CurrencyId = currencyId,
                Amount = amount
            });

            if (Entries.Count == 2)
                Type = GetTransactionType(
                    Entries.First().Account,
                    Entries.Last().Account);
        }

        private TransactionType GetTransactionType(Account sourceAccount, Account destinationAccount)
        {
            if (sourceAccount.Type == AccountType.Asset)
                return destinationAccount.Type == AccountType.Asset
                    ? TransactionType.Transfer
                    : TransactionType.Withdrawal;

            if (sourceAccount.Type == AccountType.Revenue)
                return TransactionType.Deposit;

            if (sourceAccount.Type == AccountType.InitialBalance)
                return TransactionType.OpeningBalance;

            if (sourceAccount.Type == AccountType.Reconciliation)
                return TransactionType.Reconciliation;

            throw new InvalidOperationException();
        }
    }
}
