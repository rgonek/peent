using EnsureThat;

namespace Peent.Domain.Entities.TransactionAggregate
{
    public class TransactionTag
    {
        public Transaction Transaction { get; private set; }
        public Tag Tag { get; private set; }

        private TransactionTag() { }

        public TransactionTag(Transaction transaction, Tag tag)
        {
            Ensure.That(transaction, nameof(transaction)).IsNotNull();
            Ensure.That(tag, nameof(tag)).IsNotNull();

            Transaction = transaction;
            Tag = tag;
        }
    }
}
