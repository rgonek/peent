#nullable enable
using EnsureThat;
using Peent.Domain.Common;

namespace Peent.Domain.Entities.TransactionAggregate
{
    public class TransactionEntry : AuditableEntity, IEntity<long>
    {
        public long Id { get; private set; }

        public Transaction Transaction { get; private set; }
        public Account Account { get; private set; }
        
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }

        private TransactionEntry() { }

        public TransactionEntry(Account account, decimal amount, Currency currency)
        {
            Ensure.That(account, nameof(account)).IsNotNull();
            Ensure.That(amount, nameof(amount)).IsNotZero();
            Ensure.That(currency, nameof(currency)).IsNotNull();

            Account = account;
            Amount = amount;
            Currency = currency;
        }

        public TransactionEntry(Transaction transaction, Account account, decimal amount, Currency currency)
            : this(account, amount, currency)
        {
            Ensure.That(transaction, nameof(transaction)).IsNotNull();

            Transaction = transaction;
        }
    }
}
