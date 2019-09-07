using System.Threading.Tasks;
using Peent.Application.Accounts.Queries.GetAccount;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Peent.Application.Accounts.Queries.GetAccountsList;
using Peent.Application.Currencies.Commands.CreateCurrency;
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
            var accountId1 = await SendAsync(GetCreateAccountCommand());
            var accountId2 = await SendAsync(GetCreateAccountCommand());
            var accountId3 = await SendAsync(GetCreateAccountCommand());

            var accounts = await SendAsync(new GetAccountsListQuery());

            accounts.Should()
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
            var accountId1 = await SendAsync(GetCreateAccountCommand());
            var accountId2 = await SendAsync(GetCreateAccountCommand());
            var accountId3 = await SendAsync(GetCreateAccountCommand());
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));
            var accountId4 = await SendAsync(GetCreateAccountCommand());
            var accountId5 = await SendAsync(GetCreateAccountCommand());

            SetCurrentUser(user, workspace);
            var accounts = await SendAsync(new GetAccountsListQuery());

            accounts.Should()
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
            var accountId1 = await SendAsync(GetCreateAccountCommand());
            var accountId2 = await SendAsync(GetCreateAccountCommand());
            var accountId3 = await SendAsync(GetCreateAccountCommand());
            var accountId4 = await SendAsync(GetCreateAccountCommand());
            var accountId5 = await SendAsync(GetCreateAccountCommand());
            await SendAsync(new DeleteAccountCommand {Id = accountId4});
            await SendAsync(new DeleteAccountCommand {Id = accountId5});

            var accounts = await SendAsync(new GetAccountsListQuery());

            accounts.Should()
                .Contain(x => x.Id == accountId1)
                .And.Contain(x => x.Id == accountId2)
                .And.Contain(x => x.Id == accountId3)
                .And.NotContain(x => x.Id == accountId4)
                .And.NotContain(x => x.Id == accountId5);
        }

        private CreateAccountCommand GetCreateAccountCommand()
        {
            return F.Build<CreateAccountCommand>()
                .With(x => x.CurrencyId, _currencyId)
                .Create();
        }

        private static int? _currencyId;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            if (_currencyId.HasValue == false)
                _currencyId = await SendAsync(F.Create<CreateCurrencyCommand>());
        }
    }
}
