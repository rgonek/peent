using System.Linq;
using FluentAssertions;
using Peent.Domain.Entities;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Common.Validators.ValidationFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Common.Validators.UniqueValidator
{
    public class when_entity_with_workspace_is_validated : IntegrationTestBase
    {
        [Fact]
        public async void and_entity_is_unique__does_not_return_error()
        {
            var result = await ValidateUniqueAsync<Account>(x => x.Name == "test");

            result.Should().BeEmpty();
        }

        [Fact]
        public async void and_entity_is_duplicated__returns_error()
        {
            Account account = An.Account;
            
            var result = await ValidateUniqueAsync<Account>(x => x.Name == account.Name);

            result.Should().HaveCount(1);
            result.First().ErrorMessage.Should().Be($"Entity \"Account\" (0) already exists.");
        }
        
        [Fact]
        public async void and_entity_exists_in_another_workspace__does_not_return_error()
        {
            Account account = An.Account;
            await RunAsNewUserAsync();

            var result = await ValidateUniqueAsync<Account>(x => x.Name == account.Name);

            result.Should().BeEmpty();
        }
    }
}