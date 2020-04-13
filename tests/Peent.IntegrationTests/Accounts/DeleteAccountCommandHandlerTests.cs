using System.Threading.Tasks;
using FluentAssertions;
using Peent.Domain.Entities;
using Xunit;
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
            Account account = An.Account.WithCurrency(A.Currency);
            var command = new DeleteAccountCommand(account.Id);

            await SendAsync(command);

            (await FindAsync<Account>(account.Id)).Should().BeNull();
        }

        [Fact]
        public async Task should_delete_account_by_another_user_in_the_same_workspace()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            Account account = An.Account.WithCurrency(A.Currency);
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, workspace);
            var command = new DeleteAccountCommand(account.Id);

            await SendAsync(command);

            (await FindAsync<Account>(account.Id)).Should().BeNull();
        }
    }
}
