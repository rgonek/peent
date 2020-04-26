using EnsureThat;

namespace Peent.Domain.Entities.TransactionAggregate
{
    public class TransactionTag
    {
        // Just to create composite key
        public long TransactionId { get; private set; }
        public int TagId { get; private set; }

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
