using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Models
{
    public class AccountModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public AccountType Type { get; set; }
        public AccountCurrencyModel Currency { get; set; }

        public AccountModel(Account account)
        {
            Id = account.Id;
            Name = account.Name;
            Description = account.Description;
            Currency = new AccountCurrencyModel(account.Currency);
        }
    }
}
