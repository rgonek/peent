using System;
using System.Threading.Tasks;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Application.Categories.Commands.DeleteCategory;
using Peent.Application.Exceptions;
using Peent.Common.Time;
using Peent.IntegrationTests.Infrastructure;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Categories
{
    public class CreateCategoryCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_create_category()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();

            var categoryId = await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.Name.Should().Be(command.Name);
            category.Description.Should().Be(command.Description);
        }

        [Fact]
        public async Task when_category_is_created__createdBy_is_set_to_current_user()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();

            var categoryId = await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.CreationInfo.CreatedById.Should().Be(user.Id);
        }

        [Fact]
        public async Task when_category_is_created__creationDate_is_set_to_utc_now()
        {
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using (new ClockOverride(() => utcNow, () => utcNow.AddHours(2)))
            {
                var user = await CreateUserAsync();
                SetCurrentUser(user, await CreateWorkspaceAsync(user));
                var command = F.Create<CreateCategoryCommand>();

                var categoryId = await SendAsync(command);

                var category = await FindAsync<Category>(categoryId);
                category.CreationInfo.CreationDate.Should().Be(utcNow);
            }
        }

        [Fact]
        public async Task when_category_is_created__workspace_is_set_to_current_user_workspace()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var command = F.Create<CreateCategoryCommand>();

            var categoryId = await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.WorkspaceId.Should().Be(workspace.Id);
            var fetchedWorkspace = await FindAsync<Workspace>(workspace.Id);
            fetchedWorkspace.CreationInfo.CreatedById.Should().Be(user.Id);
        }

        [Fact]
        public async Task when_category_with_given_name_exists__throws()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();
            await SendAsync(command);

            Invoking(async () => await SendAsync(command))
                .Should().Throw<DuplicateException>();
        }

        [Fact]
        public async Task when_category_with_given_name_exists_in_another_workspace__do_not_throw()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();
            await SendAsync(command);

            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));
            await SendAsync(command);
        }

        [Fact]
        public async Task when_category_with_given_name_exists_but_is_deleted__do_not_throw()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();
            var categoryId = await SendAsync(command);
            await SendAsync(new DeleteCategoryCommand {Id = categoryId});

            await SendAsync(command);
        }
    }
}
