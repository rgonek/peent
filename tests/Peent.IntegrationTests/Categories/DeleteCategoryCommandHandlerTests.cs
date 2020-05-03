using System.Threading.Tasks;
using FluentAssertions;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Categories.Commands.DeleteCategory;
using Peent.IntegrationTests.Infrastructure;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Categories
{
    public class DeleteCategoryCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_delete_category()
        {
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var command = new DeleteCategoryCommand(categoryId);
            
            await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.Should().BeNull();
        }

        [Fact]
        public async Task should_delete_category_by_another_user_in_the_same_workspace()
        {
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            RunAs(await CreateUserAsync(), BaseContext.Workspace);
            var command = new DeleteCategoryCommand(categoryId);
            
            await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.Should().BeNull();
        }
    }
}
