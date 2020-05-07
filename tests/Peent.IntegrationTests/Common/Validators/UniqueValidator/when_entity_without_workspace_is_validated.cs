using System.Linq;
using FluentAssertions;
using Peent.Domain.Entities;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static Peent.IntegrationTests.Common.Validators.ValidationFixture;

namespace Peent.IntegrationTests.Common.Validators.UniqueValidator
{
    [Collection(nameof(SharedFixture))]
    public class when_entity_without_workspace_is_validated
    {
        [Fact]
        public async void and_entity_is_unique__does_not_return_error()
        {
            await RunAsNewUserAsync();
            var result = await ValidateUniqueAsync<Currency>(x => x.Code == "test");

            result.Should().BeEmpty();
        }

        [Fact]
        public async void and_entity_is_duplicated__returns_error()
        {
            await RunAsNewUserAsync();
            Currency currency = A.Currency;
            
            var result = await ValidateUniqueAsync<Currency>(x => x.Code == currency.Code);

            result.Should().HaveCount(1);
            result.First().ErrorMessage.Should().Be($"Entity \"Currency\" (0) already exists.");
        }
    }
}