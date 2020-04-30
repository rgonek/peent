using System;
using System.Collections.Generic;
using System.Linq;
using EnsureThat;
using Peent.Domain.Common;

namespace Peent.Domain.Entities.TransactionAggregate
{
    public class Transaction : AuditableEntity<long>
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public Category Category { get; private set; }
        public TransactionType Type { get; private set; }

        private readonly List<TransactionTag> _transactionTags = new List<TransactionTag>();
        public IReadOnlyCollection<TransactionTag> TransactionTags => _transactionTags.AsReadOnly();

        private readonly List<TransactionEntry> _entries = new List<TransactionEntry>();
        public IReadOnlyCollection<TransactionEntry> Entries => _entries.AsReadOnly();

        #region Ctors

        private Transaction()
        {
        }

        public Transaction(
            string title,
            DateTime date,
            Category category,
            decimal amount,
            Account fromAccount,
            Account toAccount)
            : this(title, date, null, category, amount, fromAccount, toAccount)
        {
        }

        public Transaction(
            string title,
            DateTime date,
            string description,
            Category category,
            decimal amount,
            Account fromAccount,
            Account toAccount)
            : this(title, date, description, category, amount, fromAccount, toAccount, Enumerable.Empty<Tag>())
        {
        }

        public Transaction(
            string title,
            DateTime date,
            Category category,
            decimal amount,
            Account fromAccount,
            Account toAccount,
            IEnumerable<Tag> tags)
            : this(title, date, null, category, amount, fromAccount, toAccount, tags)
        {
        }

        #endregion

        public Transaction(
            string title,
            DateTime date,
            string description,
            Category category,
            decimal amount,
            Account fromAccount,
            Account toAccount,
            IEnumerable<Tag> tags)
        {
            Ensure.That(amount, nameof(amount)).IsPositive();
            Ensure.That(fromAccount, nameof(fromAccount)).IsNotNull();
            Ensure.That(fromAccount.Currency, nameof(fromAccount.Currency)).IsNotNull();
            Ensure.That(toAccount, nameof(toAccount)).IsNotNull();
            Ensure.That(toAccount.Currency, nameof(toAccount.Currency)).IsNotNull();

            SetTitle(title);
            SetDate(date);
            SetDescription(description);
            SetCategory(category);
            AddTags(tags ?? Enumerable.Empty<Tag>());
            _entries.Add(new TransactionEntry(this, fromAccount, amount, fromAccount.Currency));
            _entries.Add(new TransactionEntry(this, toAccount, -amount, toAccount.Currency));
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

        public void SetCategory(Category category)
        {
            Ensure.That(category, nameof(category)).IsNotNull();

            Category = category;
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

            _transactionTags.Add(new TransactionTag(this, tag));
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
                _ => throw new InvalidOperationException(
                    $"Cannot determine transation type. Source type {sourceAccount.Type}, destination type {destinationAccount.Type}.")
                };
    }
}