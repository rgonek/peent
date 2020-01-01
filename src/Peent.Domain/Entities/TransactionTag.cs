namespace Peent.Domain.Entities
{
    public class TransactionTag
    {
        public long TransactionId { get; set; }
        public Transaction Transaction { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
