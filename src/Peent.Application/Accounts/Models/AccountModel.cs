using System;
using Peent.Application.Currencies.Models;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Models
{
    public class AccountModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public AccountType Type { get; set; }
        public CurrencyModel Currency { get; set; }
        public int CurrencyId { get; set; }

        public AccountModel(Account account)
        {
            Id = account.Id;
            Name = account.Name;
            Description = account.Description;
            Type = account.Type;
            CurrencyId = account.CurrencyId;
            Currency = new CurrencyModel(account.Currency);
        }
    }
}
