using System;
using System.Threading.Tasks;
using Peent.Domain.Entities;
using Xunit;
using FluentAssertions;
using Peent.Common.Time;
using Peent.IntegrationTests.Infrastructure;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Accounts
{
    public class CreateAccountCommandHandlerTests : IntegrationTest // IClassFixture<IntegrationTest>
    {
        [Fact]
        public async Task should_create_account()
        {
            await RunAsNewUserAsync();
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
            var runAs = await RunAsNewUserAsync();
            var command = An.Account.WithCurrency(A.Currency).AsCommand();

            var accountId = await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.Created.By.Should().Be(runAs.User);
        }

        [Fact]
        public async Task when_account_is_created__creationDate_is_set_to_utc_now()
        {
            await RunAsNewUserAsync();
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
            var runAs = await RunAsNewUserAsync();
            var command = An.Account.WithCurrency(A.Currency).AsCommand();

            var accountId = await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.Workspace.Should().Be(runAs.Workspace);
            var fetchedWorkspace = await FindAsync<Workspace>(runAs.Workspace.Id);
            fetchedWorkspace.Created.By.Should().Be(runAs.User);
        }
    }
}