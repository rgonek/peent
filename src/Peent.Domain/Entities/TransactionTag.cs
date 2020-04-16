using EnsureThat;

namespace Peent.Domain.Entities
{
    public class TransactionTag
    {
        public long TransactionId { get; private set; }
        public Transaction Transaction { get; private set; }

        public int TagId { get; private set; }
        public Tag Tag { get; private set; }

        private TransactionTag() { }

        private TransactionTag(int tagId)
        {
            Ensure.That(tagId, nameof(tagId)).IsPositive();

            TagId = tagId;
        }

        public TransactionTag(long transactionId, int tagId)
            : this(tagId)
        {
            Ensure.That(transactionId, nameof(transactionId)).IsPositive();

            TransactionId = transactionId;
        }

        public TransactionTag(Transaction transaction, int tagId)
            : this(tagId)
        {
            Ensure.That(transaction, nameof(transaction)).IsNotNull();

            Transaction = transaction;
        }
    }
}
