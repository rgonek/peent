using System.Threading.Tasks;
using Peent.Domain.Entities;
using AutoFixture;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Application.Infrastructure.Extensions;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Infrastructure
{
    public class AccountBuilder
    {
        private string _name;
        private string _description;
        private AccountType _type;
        private Currency _currency;

        public AccountBuilder WithRandomData()
        {
            _name = F.Create<string>();
            _description = F.Create<string>();
            _type = F.Create<AccountType>();
            _currency = F.Create<Currency>();
            return this;
        }

        public AccountBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public AccountBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public AccountBuilder OfAssetType()
        {
            return WithType(AccountType.Asset);
        }

        public AccountBuilder OfExpenseType()
        {
            return WithType(AccountType.Expense);
        }

        public AccountBuilder OfRevenueType()
        {
            return WithType(AccountType.Revenue);
        }

        public AccountBuilder WithType(AccountType type)
        {
            _type = type;
            return this;
        }

        public AccountBuilder WithCurrency(Currency currency)
        {
            _currency = currency;
            return this;
        }

        public CreateAccountCommand AsCommand()
        {
            return new CreateAccountCommand
            {
                Name = _name,
                Description = _description,
                Type = _type,
                CurrencyId = _currency.Id
            };
        }

        public async Task<Account> Build()
        {
            var account = new Account
            {
                Name = _name,
                Description = _description,
                Type = _type,
                WorkspaceId = UserAccessor.User.GetWorkspaceId()
            };
            if (_currency.Id == 0)
                account.Currency = _currency;
            else
                account.CurrencyId = _currency.Id;

            await InsertAsync(account);

            return account;
        }

        public static implicit operator Account(AccountBuilder builder)
        {
            return builder.Build().GetAwaiter().GetResult();
        }
    }
}
