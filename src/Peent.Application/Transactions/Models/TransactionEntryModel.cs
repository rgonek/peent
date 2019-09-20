using Peent.Application.Accounts.Models;
using Peent.Application.Currencies.Models;
using Peent.Domain.Entities;

namespace Peent.Application.Transactions.Models
{
    public class TransactionEntryModel
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public CurrencyModel Currency { get; set; }
        public decimal? ForeignAmount { get; set; }
        public CurrencyModel ForeignCurrency { get; set; }
        public AccountModel Account { get; set; }

        public TransactionEntryModel(TransactionEntry entry)
        {
            Id = entry.Id;
            Amount = entry.Amount;
            Currency = new CurrencyModel(entry.Currency);
            ForeignAmount = entry.ForeignAmount;
            ForeignCurrency = entry.ForeignCurrency != null ?
                new CurrencyModel(entry.ForeignCurrency) : null;
            Account = new AccountModel(entry.Account);
        }
    }
}
