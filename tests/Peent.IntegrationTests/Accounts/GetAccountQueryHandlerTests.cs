using System.Threading.Tasks;
using Peent.Application.Accounts.Queries.GetAccount;
using FluentAssertions;
using Peent.Domain.Entities;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Accounts
{
    [Collection(nameof(SharedFixture))]
    public class GetAccountQueryHandlerTests
    {
        [Fact]
        public async Task when_account_exists__return_it()
        {
            await RunAsNewUserAsync();
            Account account = An.Account;

            var accountModel = await SendAsync(new GetAccountQuery {Id = account.Id});

            accountModel.Id.Should().Be(account.Id);
            accountModel.Name.Should().Be(account.Name);
            accountModel.Description.Should().Be(account.Description);
            accountModel.Type.Should().Be(account.Type);
            accountModel.Currency.Id.Should().Be(account.Currency.Id);
        }
    }
}