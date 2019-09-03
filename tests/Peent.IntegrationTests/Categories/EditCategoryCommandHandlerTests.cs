using System;
using System.Threading.Tasks;
using FluentAssertions;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Application.Exceptions;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Categories.Commands.DeleteCategory;
using Peent.Application.Categories.Commands.EditCategory;
using Peent.Common.Time;
using Peent.IntegrationTests.Infrastructure;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Categories
{
    public class EditCategoryCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_edit_category()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var command = new EditCategoryCommand
            {
                Id = categoryId,
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };
            await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.Name.Should().Be(command.Name);
            category.Description.Should().Be(command.Description);
        }

        [Fact]
        public async Task should_edit_category_by_another_user_in_the_same_workspace()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, workspace);
            var command = new EditCategoryCommand
            {
                Id = categoryId,
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };
            await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.ModificationInfo.LastModifiedById.Should().Be(user2.Id);
        }

        [Fact]
        public async Task when_category_is_edited__lastModifiedBy_is_set_to_current_user()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var command = new EditCategoryCommand
            {
                Id = categoryId,
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };
            await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.ModificationInfo.LastModifiedById.Should().Be(user.Id);
        }

        [Fact]
        public async Task when_category_is_edited__lastModificationDate_is_set_to_utc_now()
        {
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using (new ClockOverride(() => utcNow, () => utcNow.AddHours(2)))
            {
                var user = await CreateUserAsync();
                SetCurrentUser(user, await CreateWorkspaceAsync(user));
                var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
                var command = new EditCategoryCommand
                {
                    Id = categoryId,
                    Name = F.Create<string>(),
                    Description = F.Create<string>()
                };
                await SendAsync(command);

                var category = await FindAsync<Category>(categoryId);
                category.ModificationInfo.LastModificationDate.Should().Be(utcNow);
            }
        }

        [Fact]
        public async Task when_category_with_given_name_exists__throws()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();
            var categoryId = await SendAsync(command);
            var command2 = F.Create<CreateCategoryCommand>();
            await SendAsync(command2);

            Invoking(async () => await SendAsync(new EditCategoryCommand
                {
                    Id = categoryId,
                    Name = command2.Name
                }))
                .Should().Throw<DuplicateException>();
        }

        [Fact]
        public async Task when_category_with_given_name_exists_but_is_deleted__do_not_throw()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();
            var categoryId = await SendAsync(command);
            var command2 = F.Create<CreateCategoryCommand>();
            await SendAsync(command2);
            await SendAsync(new DeleteCategoryCommand { Id = categoryId });

            await SendAsync(command);
        }

        [Fact]
        public async Task when_category_with_given_name_exists_in_another_workspace__do_not_throw()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var command = F.Create<CreateCategoryCommand>();
            var categoryId = await SendAsync(command);
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));
            var command2 = F.Create<CreateCategoryCommand>();
            await SendAsync(command2);
            SetCurrentUser(user, workspace);

            await SendAsync(new EditCategoryCommand
                {
                    Id = categoryId,
                    Name = command2.Name
                });
        }
    }
}
