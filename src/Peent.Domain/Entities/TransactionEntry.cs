using Peent.Domain.ValueObjects;

namespace Peent.Domain.Entities
{
    public class TransactionEntry : IHaveAuditInfo
    {
        public long Id { get; set; }

        public long TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public decimal? ForeignAmount { get; set; }
        public int ForeignCurrencyId { get; set; }
        public Currency ForeignCurrency { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public CreationInfo CreationInfo { get; set; }
        public ModificationInfo ModificationInfo { get; set; }
        public DeletionInfo DeletionInfo { get; set; }
    }
}
