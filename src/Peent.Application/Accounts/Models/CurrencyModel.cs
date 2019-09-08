using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Models
{
    public class AccountCurrencyModel
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public ushort DecimalPlaces { get; set; }

        public AccountCurrencyModel(Currency currency)
        {
            Id = currency.Id;
            Code = currency.Code;
            Name = currency.Name;
            Symbol = currency.Symbol;
            DecimalPlaces = currency.DecimalPlaces;
        }
    }
}
