using System.Linq;
using FluentAssertions;
using Peent.Domain.Entities;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static Peent.IntegrationTests.Common.Validators.ValidationFixture;

namespace Peent.IntegrationTests.Common.Validators.ExistsValidator
{
    [Collection(nameof(SharedFixture))]
    public class when_entity_without_workspace_is_validated
    {
        [Fact]
        public async void and_entity_exists__does_not_return_error()
        {
            await RunAsNewUserAsync();
            Currency currency = A.Currency;

            var result = await ValidateExistsAsync<Currency>(currency.Id);

            result.Should().BeEmpty();
        }

        [Fact]
        public async void and_entity_does_not_exist__returns_error()
        {
            await RunAsNewUserAsync();
            var result = await ValidateExistsAsync<Currency>(0);

            result.Should().HaveCount(1);
            result.First().ErrorMessage.Should().Be("Entity \"Currency\" (0) was not found.");
        }
    }
}