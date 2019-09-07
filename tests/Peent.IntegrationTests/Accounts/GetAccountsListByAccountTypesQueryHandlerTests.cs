using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Peent.Application.Accounts.Queries.GetAccountsListByAccountTypes;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Domain.Entities;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Accounts
{
    public class GetAccountsListByAccountTypesQueryHandlerTests : IntegrationTestBase
    {
        [Theory]
        [InlineData(AccountType.Expense)]
        [InlineData(AccountType.Asset)]
        [InlineData(AccountType.Cash)]
        [InlineData(AccountType.Debt)]
        [InlineData(AccountType.InitialBalance)]
        [InlineData(AccountType.Loan)]
        [InlineData(AccountType.Mortgage)]
        [InlineData(AccountType.Reconciliation)]
        [InlineData(AccountType.Revenue)]
        public async Task should_return_account_of_given_types(AccountType type)
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var accountId1 = await SendAsync(GetCreateAccountCommand(type));
            var accountId2 = await SendAsync(GetCreateAccountCommand(type));
            var accountId3 = await SendAsync(GetCreateAccountCommand(AccountType.Unknown));

            var accounts = await SendAsync(new GetAccountsListByAccountTypesQuery { Types = new[] { type } });

            accounts.Count.Should().Be(2);
            accounts.Should()
                .Contain(x => x.Id == accountId1)
                .And.Contain(x => x.Id == accountId2)
                .And.NotContain(x => x.Id == accountId3);
        }

        [Fact]
        public async Task should_returns_accounts_list_only_for_given_user()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var accountId1 = await SendAsync(GetCreateAccountCommand(AccountType.Unknown));
            var accountId2 = await SendAsync(GetCreateAccountCommand(AccountType.Unknown));
            var accountId3 = await SendAsync(GetCreateAccountCommand(AccountType.Unknown));
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));
            var accountId4 = await SendAsync(GetCreateAccountCommand(AccountType.Unknown));
            var accountId5 = await SendAsync(GetCreateAccountCommand(AccountType.Unknown));

            SetCurrentUser(user, workspace);
            var accounts = await SendAsync(new GetAccountsListByAccountTypesQuery { Types = new[] { AccountType.Unknown } });

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
            var accountId1 = await SendAsync(GetCreateAccountCommand(AccountType.Unknown));
            var accountId2 = await SendAsync(GetCreateAccountCommand(AccountType.Unknown));
            var accountId3 = await SendAsync(GetCreateAccountCommand(AccountType.Unknown));
            var accountId4 = await SendAsync(GetCreateAccountCommand(AccountType.Unknown));
            var accountId5 = await SendAsync(GetCreateAccountCommand(AccountType.Unknown));
            await SendAsync(new DeleteAccountCommand { Id = accountId4 });
            await SendAsync(new DeleteAccountCommand { Id = accountId5 });

            var accounts = await SendAsync(new GetAccountsListByAccountTypesQuery { Types = new[] { AccountType.Unknown } });

            accounts.Should()
                .Contain(x => x.Id == accountId1)
                .And.Contain(x => x.Id == accountId2)
                .And.Contain(x => x.Id == accountId3)
                .And.NotContain(x => x.Id == accountId4)
                .And.NotContain(x => x.Id == accountId5);
        }

        private CreateAccountCommand GetCreateAccountCommand(AccountType? type = null)
        {
            var builder = F.Build<CreateAccountCommand>()
                .With(x => x.CurrencyId, _currencyId);

            if (type.HasValue)
            {
                builder = builder.With(x => x.Type, type.Value);
            }

            return builder.Create();
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
