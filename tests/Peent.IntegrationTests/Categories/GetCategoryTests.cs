using System.Threading.Tasks;
using Peent.Application.Categories.Queries.GetCategory;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Categories.Commands.CreateCategory;
using Xunit;
using static Peent.IntegrationTests.DatabaseFixture;

namespace Peent.IntegrationTests.Categories
{
    public class GetCategoryTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_get_category()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = new CreateCategoryCommand
            {
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };
            var categoryId = await SendAsync(command);

            var categoryModel = await SendAsync(new GetCategoryQuery { Id = categoryId });

            categoryModel.Id.Should().Be(categoryId);
            categoryModel.Name.Should().Be(command.Name);
            categoryModel.Description.Should().Be(command.Description);
        }
    }
}