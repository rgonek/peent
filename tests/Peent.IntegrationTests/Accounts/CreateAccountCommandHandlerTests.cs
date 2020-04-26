using System;
using System.Threading.Tasks;
using Peent.Domain.Entities;
using Xunit;
using FluentAssertions;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Peent.Application.Exceptions;
using Peent.Common.Time;
using Peent.IntegrationTests.Infrastructure;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Accounts
{
    public class CreateAccountCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_create_account()
        {
            var command = An.Account.WithCurrency(A.Currency).AsCommand();

            var accountId = await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.Name.Should().Be(command.Name);
            account.Description.Should().Be(command.Description);
            account.Type.Should().Be(command.Type);
            account.Currency.Id.Should().Be(command.CurrencyId);
        }

        [Fact]
        public async Task when_account_is_created__createdBy_is_set_to_current_user()
        {
            var command = An.Account.WithCurrency(A.Currency).AsCommand();

            var accountId = await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.Created.By.Should().Be(_context.User);
        }

        [Fact]
        public async Task when_account_is_created__creationDate_is_set_to_utc_now()
        {
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using (new ClockOverride(() => utcNow, () => utcNow.AddHours(2)))
            {
                var command = An.Account.WithCurrency(A.Currency).AsCommand();

                var accountId = await SendAsync(command);

                var account = await FindAsync<Account>(accountId);
                account.Created.On.Should().Be(utcNow);
            }
        }

        [Fact]
        public async Task when_account_is_created__workspace_is_set_to_current_user_workspace()
        {
            var command = An.Account.WithCurrency(A.Currency).AsCommand();

            var accountId = await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.WorkspaceId.Should().Be(_context.Workspace.Id);
            var fetchedWorkspace = await FindAsync<Workspace>(_context.Workspace.Id);
            fetchedWorkspace.Created.By.Should().Be(_context.User);
        }

        [Fact]
        public async Task when_account_with_given_name_exists__throws()
        {
            var command = An.Account.WithCurrency(A.Currency).AsCommand();

            await SendAsync(command);

            Invoking(async () => await SendAsync(command))
                .Should().Throw<DuplicateException>();
        }

        [Fact]
        public async Task when_account_with_given_name_exists_in_another_workspace__do_not_throw()
        {
            var command = An.Account.WithCurrency(A.Currency).AsCommand();

            await SendAsync(command);

            await SetUpAuthenticationContext();
            await SendAsync(command);
        }

        [Fact]
        public async Task when_account_with_given_name_exists_but_is_deleted__do_not_throw()
        {
            var command = An.Account.WithCurrency(A.Currency).AsCommand();
            var accountId = await SendAsync(command);
            await SendAsync(new DeleteAccountCommand(accountId));

            await SendAsync(command);
        }
    }
}
