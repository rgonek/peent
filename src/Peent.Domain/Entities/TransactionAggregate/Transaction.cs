using System;
using System.Collections.Generic;
using System.Linq;
using EnsureThat;
using Peent.Domain.Common;

namespace Peent.Domain.Entities.TransactionAggregate
{
    public class Transaction : AuditableEntity
    {
        public long Id { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }
        public TransactionType Type { get; private set; }

        private readonly List<TransactionTag> _transactionTags;
        public IReadOnlyCollection<TransactionTag> TransactionTags => _transactionTags.AsReadOnly();

        private readonly List<TransactionEntry> _entries;
        public IReadOnlyCollection<TransactionEntry> Entries => _entries.AsReadOnly();

        #region Ctors
        private Transaction() { }

        public Transaction(string title, DateTime date, int categoryId, IEnumerable<TransactionEntry> entries)
            : this(title, date, null, categoryId, entries)
        {
        }

        public Transaction(string title, DateTime date, string description, int categoryId, IEnumerable<TransactionEntry> entries)
            : this(title, date, description, categoryId, entries, Enumerable.Empty<TransactionTag>())
        {
        }

        public Transaction(string title, DateTime date, int categoryId, IEnumerable<TransactionEntry> entries, IEnumerable<TransactionTag> transactionTags)
            : this(title, date, null, categoryId, entries, transactionTags)
        {
        }

        #endregion

        public Transaction(
            string title,
            DateTime date,
            string description,
            int categoryId,
            IEnumerable<TransactionEntry> entries,
            IEnumerable<TransactionTag> transactionTags)
        {
            Ensure.That(title, nameof(title)).IsNotNullOrWhiteSpace();
            Ensure.That(categoryId, nameof(categoryId)).IsPositive();
            Ensure.That(entries, nameof(entries)).IsNotNull();
            Ensure.That(entries.Count(), nameof(entries)).Is(2);

            Title = title;
            Date = date;
            Description = description;
            CategoryId = categoryId;
            _transactionTags = (transactionTags ?? Enumerable.Empty<TransactionTag>()).ToList();
            _entries = entries.ToList();
            Type = GetTransactionType(
                Entries.First().Account,
                Entries.Last().Account);
        }

        public void SetTitle(string title)
        {
            Ensure.That(title, nameof(title)).IsNotNullOrWhiteSpace();

            Title = title;
        }

        public void SetDate(DateTime date) => Date = date;

        public void SetDescription(string description) => Description = description;

        public void SetCategory(int categoryId)
        {
            Ensure.That(categoryId, nameof(categoryId)).IsPositive();

            CategoryId = categoryId;
        }

        public void AddTags(IEnumerable<Tag> tags)
        {
            Ensure.That(tags, nameof(tags)).IsNotNull();

            foreach (var tag in tags)
                AddTag(tag);
        }

        public void AddTag(Tag tag)
        {
            Ensure.That(tag, nameof(tag)).IsNotNull();

            AddTag(tag.Id);
        }

        public void AddTag(int tagId)
        {
            Ensure.That(tagId, nameof(tagId)).IsPositive();

            _transactionTags.Add(new TransactionTag(this, tagId));
        }

        private static TransactionType GetTransactionType(Account sourceAccount, Account destinationAccount)
            => sourceAccount.Type switch
            {
                AccountType.Asset => (destinationAccount.Type == AccountType.Asset
                    ? TransactionType.Transfer
                    : TransactionType.Withdrawal),
                AccountType.Revenue => TransactionType.Deposit,
                AccountType.InitialBalance => TransactionType.OpeningBalance,
                AccountType.Reconciliation => TransactionType.Reconciliation,
                _ => throw new InvalidOperationException($"Cannot determine transation type. Source type {sourceAccount.Type}, destination type {destinationAccount.Type}.")
            };
    }
}
