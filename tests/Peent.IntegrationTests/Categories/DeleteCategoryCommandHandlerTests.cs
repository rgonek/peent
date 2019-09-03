using System;
using System.Threading.Tasks;
using FluentAssertions;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Categories.Commands.DeleteCategory;
using Peent.Common.Time;
using Peent.IntegrationTests.Infrastructure;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Categories
{
    public class DeleteCategoryCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_delete_category()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var command = new DeleteCategoryCommand
            {
                Id = categoryId
            };
            await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.DeletionInfo.DeletionDate.Should().NotBeNull();
        }

        [Fact]
        public async Task should_delete_category_by_another_user_in_the_same_workspace()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, workspace);
            var command = new DeleteCategoryCommand
            {
                Id = categoryId
            };
            await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.DeletionInfo.DeletionDate.Should().NotBeNull();
        }

        [Fact]
        public async Task when_category_is_deleted__deletedBy_is_set_to_current_user()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var command = new DeleteCategoryCommand
            {
                Id = categoryId
            };
            await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.DeletionInfo.DeletedById.Should().Be(user.Id);
        }

        [Fact]
        public async Task when_category_is_deleted__deletionDate_is_set_to_utc_now()
        {
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using (new ClockOverride(() => utcNow, () => utcNow.AddHours(2)))
            {
                var user = await CreateUserAsync();
                SetCurrentUser(user, await CreateWorkspaceAsync(user));
                var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
                var command = new DeleteCategoryCommand
                {
                    Id = categoryId
                };
                await SendAsync(command);

                var category = await FindAsync<Category>(categoryId);
                category.DeletionInfo.DeletionDate.Should().Be(utcNow);
            }
        }
    }
}
