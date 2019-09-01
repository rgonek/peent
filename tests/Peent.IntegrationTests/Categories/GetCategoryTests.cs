using System.Threading.Tasks;
using Peent.Application.Categories.Queries.GetCategory;
using Peent.Domain.Entities;
using AutoFixture;
using FluentAssertions;
using Peent.Domain.ValueObjects;
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
            var category = new Category
            {
                Name = F.Create<string>(),
                Description = F.Create<string>(),
                Workspace = new Workspace
                {
                    CreationInfo = new CreationInfo(user.Id)
                },
                CreationInfo = new CreationInfo(user.Id)
            };
            await InsertAsync(category);

            var categoryModel = await SendAsync(new GetCategoryQuery { Id = category.Id });

            categoryModel.Id.Should().Be(category.Id);
            categoryModel.Name.Should().Be(category.Name);
            categoryModel.Description.Should().Be(category.Description);
        }
    }
}