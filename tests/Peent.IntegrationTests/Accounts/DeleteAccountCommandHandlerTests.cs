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
            Account account = An.Account.WithCurrency(A.Currency);
            var command = new DeleteAccountCommand(account.Id);

            await SendAsync(command);

            (await FindAsync<Account>(account.Id)).Should().BeNull();
        }

        [Fact]
        public async Task should_delete_account_by_another_user_in_the_same_workspace()
        {
            Account account = An.Account.WithCurrency(A.Currency);
            await SetUpAuthenticationContext(BaseContext.Workspace);
            var command = new DeleteAccountCommand(account.Id);

            await SendAsync(command);

            (await FindAsync<Account>(account.Id)).Should().BeNull();
        }
    }
}
