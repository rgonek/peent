using System.Threading.Tasks;
using Peent.Application.Accounts.Queries.GetAccount;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Application.Exceptions;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Accounts
{
    public class GetAccountQueryHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task when_account_exists__return_it()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Build<CreateAccountCommand>()
                .With(x => x.CurrencyId, _currencyId)
                .Create();
            var accountId = await SendAsync(command);

            var accountModel = await SendAsync(new GetAccountQuery { Id = accountId });

            accountModel.Id.Should().Be(accountId);
            accountModel.Name.Should().Be(command.Name);
            accountModel.Description.Should().Be(command.Description);
        }

        [Fact]
        public async Task when_account_do_not_exists__throws()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));

            Invoking(async () => await SendAsync(new GetAccountQuery { Id = 0 }))
                .Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task when_account_exists_but_is_deleted__throws()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = GetCreateAccountCommand();
            var accountId = await SendAsync(command);
            await SendAsync(new DeleteAccountCommand {Id = accountId});

            Invoking(async () => await SendAsync(new GetAccountQuery { Id = accountId }))
                .Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task when_account_exists_in_another_workspace__throws()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = GetCreateAccountCommand();
            var accountId = await SendAsync(command);

            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));

            Invoking(async () => await SendAsync(new GetAccountQuery { Id = accountId }))
                .Should().Throw<NotFoundException>();
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