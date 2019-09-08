using System;
using System.Threading.Tasks;
using FluentAssertions;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Categories.Commands.EditCategory;
using Peent.Common.Time;
using Peent.IntegrationTests.Infrastructure;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

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
            var command = F.Build<EditCategoryCommand>()
                .With(x => x.Id, categoryId)
                .Create();

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
            var command = F.Build<EditCategoryCommand>()
                .With(x => x.Id, categoryId)
                .Create();

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
            var command = F.Build<EditCategoryCommand>()
                .With(x => x.Id, categoryId)
                .Create();

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
                var command = F.Build<EditCategoryCommand>()
                    .With(x => x.Id, categoryId)
                    .Create();

                await SendAsync(command);

                var category = await FindAsync<Category>(categoryId);
                category.ModificationInfo.LastModificationDate.Should().Be(utcNow);
            }
        }
    }
}
