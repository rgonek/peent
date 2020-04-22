#nullable enable
using EnsureThat;
using Peent.Domain.Common;

namespace Peent.Domain.Entities.TransactionAggregate
{
    public class TransactionEntry : AuditableEntity, IEntity<long>
    {
        public long Id { get; private set; }

        public long TransactionId { get; private set; }
        public Transaction Transaction { get; private set; }
        public decimal Amount { get; private set; }
        public int CurrencyId { get; private set; }
        public Currency Currency { get; private set; }
        public decimal? ForeignAmount { get; private set; }
        public int? ForeignCurrencyId { get; private set; }
        public Currency? ForeignCurrency { get; private set; }
        public int AccountId { get; private set; }
        public Account Account { get; private set; }

        private TransactionEntry() { }

        public TransactionEntry(Account account, decimal amount, int currencyId)
        {
            Ensure.That(account, nameof(account)).IsNotNull();
            Ensure.That(amount, nameof(amount)).IsNotZero();
            Ensure.That(currencyId, nameof(currencyId)).IsPositive();

            Account = account;
            Amount = amount;
            CurrencyId = currencyId;
        }

        public TransactionEntry(long transactionId, Account account, decimal amount, int currencyId)
            : this(account, amount, currencyId)
        {
            Ensure.That(transactionId, nameof(transactionId)).IsPositive();

            TransactionId = transactionId;
        }

        public TransactionEntry(Transaction transaction, Account account, decimal amount, int currencyId)
            : this(account, amount, currencyId)
        {
            Ensure.That(transaction, nameof(transaction)).IsNotNull();

            Transaction = transaction;
        }
    }
}
