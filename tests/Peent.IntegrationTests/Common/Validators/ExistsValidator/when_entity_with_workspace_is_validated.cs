using System.Linq;
using FluentAssertions;
using Peent.Domain.Entities;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Common.Validators.ValidationFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Common.Validators.ExistsValidator
{
    [Collection(nameof(SharedFixture))]
    public class when_entity_with_workspace_is_validated
    {
        [Fact]
        public async void and_entity_exists__does_not_return_error()
        {
            await RunAsNewUserAsync();
            Account account = An.Account;

            var result = await ValidateExistsAsync<Account>(account.Id);

            result.Should().BeEmpty();
        }

        [Fact]
        public async void and_entity_does_not_exist__returns_error()
        {
            await RunAsNewUserAsync();
            var result = await ValidateExistsAsync<Account>(0);

            result.Should().HaveCount(1);
            result.First().ErrorMessage.Should().Be("Entity \"Account\" (0) was not found.");
        }

        [Fact]
        public async void and_entity_exists_in_another_workspace__returns_error()
        {
            await RunAsNewUserAsync();
            Account account = An.Account;
            await RunAsNewUserAsync();

            var result = await ValidateExistsAsync<Account>(account.Id);

            result.Should().HaveCount(1);
            result.First().ErrorMessage.Should().Be($"Entity \"Account\" ({account.Id}) was not found.");
        }
    }
}