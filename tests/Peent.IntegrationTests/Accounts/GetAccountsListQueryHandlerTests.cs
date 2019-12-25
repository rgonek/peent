using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Peent.Application.Accounts.Queries.GetAccountsList;
using Peent.Application.Currencies.Commands.CreateCurrency;
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

            var currencyId = await SendAsync(F.Create<CreateCurrencyCommand>());
            var accountId1 = await SendAsync(CreateAccountCommand(currencyId));
            var accountId2 = await SendAsync(CreateAccountCommand(currencyId));
            var accountId3 = await SendAsync(CreateAccountCommand(currencyId));

            var accountsPaged = await SendAsync(new GetAccountsListQuery());

            accountsPaged.Results.Should()
                .Contain(x => x.Id == accountId1)
                .And.Contain(x => x.Id == accountId2)
                .And.Contain(x => x.Id == accountId3);
        }

        [Fact]
        public async Task should_returns_accounts_list_only_for_given_user()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var currencyId = await SendAsync(F.Create<CreateCurrencyCommand>());
            var accountId1 = await SendAsync(CreateAccountCommand(currencyId));
            var accountId2 = await SendAsync(CreateAccountCommand(currencyId));
            var accountId3 = await SendAsync(CreateAccountCommand(currencyId));
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));
            var accountId4 = await SendAsync(CreateAccountCommand(currencyId));
            var accountId5 = await SendAsync(CreateAccountCommand(currencyId));

            SetCurrentUser(user, workspace);
            var accountsPaged = await SendAsync(new GetAccountsListQuery());

            accountsPaged.Results.Should()
                .Contain(x => x.Id == accountId1)
                .And.Contain(x => x.Id == accountId2)
                .And.Contain(x => x.Id == accountId3)
                .And.NotContain(x => x.Id == accountId4)
                .And.NotContain(x => x.Id == accountId5);
        }

        [Fact]
        public async Task should_should_not_returns_deleted_accounts()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var currencyId = await SendAsync(F.Create<CreateCurrencyCommand>());
            var accountId1 = await SendAsync(CreateAccountCommand(currencyId));
            var accountId2 = await SendAsync(CreateAccountCommand(currencyId));
            var accountId3 = await SendAsync(CreateAccountCommand(currencyId));
            var accountId4 = await SendAsync(CreateAccountCommand(currencyId));
            var accountId5 = await SendAsync(CreateAccountCommand(currencyId));
            await SendAsync(new DeleteAccountCommand { Id = accountId4 });
            await SendAsync(new DeleteAccountCommand { Id = accountId5 });

            var accountsPaged = await SendAsync(new GetAccountsListQuery());

            accountsPaged.Results.Should()
                .Contain(x => x.Id == accountId1)
                .And.Contain(x => x.Id == accountId2)
                .And.Contain(x => x.Id == accountId3)
                .And.NotContain(x => x.Id == accountId4)
                .And.NotContain(x => x.Id == accountId5);
        }

        private CreateAccountCommand CreateAccountCommand(int currencyId)
        {
            return new CreateAccountCommand
            {
                CurrencyId = currencyId,
                Name = F.Create<string>(),
                Description = F.Create<string>(),
                Type = F.Create<AccountType>()
            };
        }
    }
}
