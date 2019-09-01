using System.Threading.Tasks;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Categories.Commands.CreateCategory;
using static Peent.IntegrationTests.DatabaseFixture;

namespace Peent.IntegrationTests.Categories
{
    public class CreateCategoryTests: IntegrationTestBase
    {
        [Fact]
        public async Task should_create_category()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = new CreateCategoryCommand
            {
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };

            var categoryId = await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.Name.Should().Be(command.Name);
            category.Description.Should().Be(command.Description);
        }
    }
}
