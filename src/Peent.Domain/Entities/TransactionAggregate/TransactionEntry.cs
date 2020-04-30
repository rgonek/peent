#nullable enable
using EnsureThat;
using Peent.Domain.Common;

namespace Peent.Domain.Entities.TransactionAggregate
{
    public class TransactionEntry : AuditableEntity<long>
    {
        public Transaction Transaction { get; private set; }
        public Account Account { get; private set; }
        
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }

        private TransactionEntry() { }

        public TransactionEntry(Transaction transaction, Account account, decimal amount, Currency currency)
        {
            Ensure.That(transaction, nameof(transaction)).IsNotNull();
            Ensure.That(account, nameof(account)).IsNotNull();
            Ensure.That(amount, nameof(amount)).IsNotZero();
            Ensure.That(currency, nameof(currency)).IsNotNull();

            Transaction = transaction;
            Account = account;
            Amount = amount;
            Currency = currency;
        }
    }
}
