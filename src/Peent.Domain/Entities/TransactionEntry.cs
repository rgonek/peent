﻿using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class TransactionEntry : AuditableEntity
    {
        public long Id { get; set; }

        public long TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public decimal? ForeignAmount { get; set; }
        public int? ForeignCurrencyId { get; set; }
        public Currency? ForeignCurrency { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
