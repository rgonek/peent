using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Peent.Application.Accounts.Queries.GetAccountsList;
using Peent.Domain.Entities;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Accounts
{
    public class GetAccountsListQueryHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_returns_accounts_list()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            Currency currency = A.Currency;
            Account account1 = An.Account.WithCurrency(currency);
            Account account2 = An.Account.WithCurrency(currency);
            Account account3 = An.Account.WithCurrency(currency);

            var accountsPaged = await SendAsync(new GetAccountsListQuery());

            accountsPaged.Results.Should()
                .Contain(x => x.Id == account1.Id)
                .And.Contain(x => x.Id == account2.Id)
                .And.Contain(x => x.Id == account3.Id);
        }

        [Fact]
        public async Task should_returns_accounts_list_only_for_given_user()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);

            Currency currency = A.Currency;
            Account account1 = An.Account.WithCurrency(currency);
            Account account2 = An.Account.WithCurrency(currency);
            Account account3 = An.Account.WithCurrency(currency);
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));
            Account account4 = An.Account.WithCurrency(currency);
            Account account5 = An.Account.WithCurrency(currency);

            SetCurrentUser(user, workspace);
            var accountsPaged = await SendAsync(new GetAccountsListQuery());

            accountsPaged.Results.Should()
                .Contain(x => x.Id == account1.Id)
                .And.Contain(x => x.Id == account2.Id)
                .And.Contain(x => x.Id == account3.Id)
                .And.NotContain(x => x.Id == account4.Id)
                .And.NotContain(x => x.Id == account5.Id);
        }

        [Fact]
        public async Task should_should_not_returns_deleted_accounts()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            Currency currency = A.Currency;
            Account account1 = An.Account.WithCurrency(currency);
            Account account2 = An.Account.WithCurrency(currency);
            Account account3 = An.Account.WithCurrency(currency);
            Account account4 = An.Account.WithCurrency(currency);
            Account account5 = An.Account.WithCurrency(currency);
            await SendAsync(new DeleteAccountCommand(account4.Id));
            await SendAsync(new DeleteAccountCommand(account5.Id));

            var accountsPaged = await SendAsync(new GetAccountsListQuery());

            accountsPaged.Results.Should()
                .Contain(x => x.Id == account1.Id)
                .And.Contain(x => x.Id == account2.Id)
                .And.Contain(x => x.Id == account3.Id)
                .And.NotContain(x => x.Id == account4.Id)
                .And.NotContain(x => x.Id == account5.Id);
        }
    }
}
