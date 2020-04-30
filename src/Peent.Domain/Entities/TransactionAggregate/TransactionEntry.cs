#nullable enable
using EnsureThat;
using Peent.Domain.Common;
using Peent.Domain.ValueObjects;

namespace Peent.Domain.Entities.TransactionAggregate
{
    public class TransactionEntry : AuditableEntity<long>
    {
        public Transaction Transaction { get; private set; }
        public Account Account { get; private set; }
        
        public Money Money { get; private set; }

        private TransactionEntry() { }

        public TransactionEntry(Transaction transaction, Account account, Money money)
        {
            Ensure.That(transaction, nameof(transaction)).IsNotNull();
            Ensure.That(account, nameof(account)).IsNotNull();
            Ensure.That(money, nameof(money)).IsNotNull();

            Transaction = transaction;
            Account = account;
            Money = money;
        }
    }
}
