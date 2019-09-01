using System.Threading.Tasks;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Categories.Commands.CreateCategory;
using static Peent.IntegrationTests.DatabaseFixture;

namespace Peent.IntegrationTests.Categories
{
    public class CreateCategoryCommandHandlerTests : IntegrationTestBase
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

        [Fact]
        public async Task when_category_is_created_createdBy_is_set_to_current_user()
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
            category.CreationInfo.CreatedById.Should().Be(user.Id);
        }

        [Fact]
        public async Task when_category_is_created_workspace_is_set_to_current_user_workspace()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var command = new CreateCategoryCommand
            {
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };

            var categoryId = await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.WorkspaceId.Should().Be(workspace.Id);
            var fetchedWorkspace = await FindAsync<Workspace>(workspace.Id);
            fetchedWorkspace.CreationInfo.CreatedById.Should().Be(user.Id);
        }
    }
}
