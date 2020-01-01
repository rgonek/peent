using System.Threading.Tasks;
using FluentAssertions;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Peent.IntegrationTests.Infrastructure;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Accounts
{
    public class DeleteAccountCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_delete_account()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var accountId = await SendAsync(F.Create<CreateAccountCommand>());
            var command = new DeleteAccountCommand
            {
                Id = accountId
            };
            await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.Should().BeNull();
        }

        [Fact]
        public async Task should_delete_account_by_another_user_in_the_same_workspace()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var accountId = await SendAsync(F.Create<CreateAccountCommand>());
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, workspace);
            var command = new DeleteAccountCommand
            {
                Id = accountId
            };
            await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.Should().BeNull();
        }
    }
}
