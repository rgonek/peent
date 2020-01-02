using System;
using System.Threading.Tasks;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Accounts.Commands.CreateAccount;
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
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = An.Account.WithCurrency(A.Currency).AsCommand();

            var accountId = await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.Name.Should().Be(command.Name);
            account.Description.Should().Be(command.Description);
            account.Type.Should().Be(command.Type);
            account.CurrencyId.Should().Be(command.CurrencyId);
        }

        [Fact]
        public async Task when_account_is_created__createdBy_is_set_to_current_user()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = An.Account.WithCurrency(A.Currency).AsCommand();

            var accountId = await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.CreatedById.Should().Be(user.Id);
        }

        [Fact]
        public async Task when_account_is_created__creationDate_is_set_to_utc_now()
        {
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using (new ClockOverride(() => utcNow, () => utcNow.AddHours(2)))
            {
                var user = await CreateUserAsync();
                SetCurrentUser(user, await CreateWorkspaceAsync(user));
                var command = An.Account.WithCurrency(A.Currency).AsCommand();

                var accountId = await SendAsync(command);

                var account = await FindAsync<Account>(accountId);
                account.CreationDate.Should().Be(utcNow);
            }
        }

        [Fact]
        public async Task when_account_is_created__workspace_is_set_to_current_user_workspace()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var command = An.Account.WithCurrency(A.Currency).AsCommand();

            var accountId = await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.WorkspaceId.Should().Be(workspace.Id);
            var fetchedWorkspace = await FindAsync<Workspace>(workspace.Id);
            fetchedWorkspace.CreatedById.Should().Be(user.Id);
        }

        [Fact]
        public async Task when_account_with_given_name_exists__throws()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = An.Account.WithCurrency(A.Currency).AsCommand();

            await SendAsync(command);

            Invoking(async () => await SendAsync(command))
                .Should().Throw<DuplicateException>();
        }

        [Fact]
        public async Task when_account_with_given_name_exists_in_another_workspace__do_not_throw()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = An.Account.WithCurrency(A.Currency).AsCommand();

            await SendAsync(command);

            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));
            await SendAsync(command);
        }

        [Fact]
        public async Task when_account_with_given_name_exists_but_is_deleted__do_not_throw()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = An.Account.WithCurrency(A.Currency).AsCommand();
            var accountId = await SendAsync(command);
            await SendAsync(new DeleteAccountCommand {Id = accountId});

            await SendAsync(command);
        }
    }
}
