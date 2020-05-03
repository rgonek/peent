using System.Threading.Tasks;
using Peent.Application.Accounts.Queries.GetAccount;
using FluentAssertions;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Peent.Application.Exceptions;
using Peent.Domain.Entities;
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
            Account account = An.Account;

            var accountModel = await SendAsync(new GetAccountQuery { Id = account.Id });

            accountModel.Id.Should().Be(account.Id);
            accountModel.Name.Should().Be(account.Name);
            accountModel.Description.Should().Be(account.Description);
            accountModel.Type.Should().Be(account.Type);
            accountModel.Currency.Id.Should().Be(account.Currency.Id);
        }

        [Fact]
        public void when_account_do_not_exists__throws()
        {
            Invoking(async () => await SendAsync(new GetAccountQuery { Id = 0 }))
                .Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task when_account_exists_but_is_deleted__throws()
        {
            Account account = An.Account;
            await SendAsync(new DeleteAccountCommand(account.Id));

            Invoking(async () => await SendAsync(new GetAccountQuery { Id = account.Id }))
                .Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task when_account_exists_in_another_workspace__throws()
        {
            Account account = An.Account;

            await RunAsNewUserAsync();

            Invoking(async () => await SendAsync(new GetAccountQuery { Id = account.Id }))
                .Should().Throw<NotFoundException>();
        }
    }
}