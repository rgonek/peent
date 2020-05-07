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
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Categories
{
    [Collection(nameof(SharedFixture))]
    public class EditCategoryCommandHandlerTests
    {
        [Fact]
        public async Task should_edit_category()
        {
            await RunAsNewUserAsync();
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
            var runAs = await RunAsNewUserAsync();
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var currentContext = RunAs(await CreateUserAsync(), runAs.Workspace);
            var command = F.Build<EditCategoryCommand>()
                .With(x => x.Id, categoryId)
                .Create();

            await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.LastModified.By.Should().Be(currentContext.User);
        }

        [Fact]
        public async Task when_category_is_edited__lastModifiedBy_is_set_to_current_user()
        {
            var runAs = await RunAsNewUserAsync();
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var command = F.Build<EditCategoryCommand>()
                .With(x => x.Id, categoryId)
                .Create();

            await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.LastModified.By.Should().Be(runAs.User);
        }

        [Fact]
        public async Task when_category_is_edited__lastModificationDate_is_set_to_utc_now()
        {
            await RunAsNewUserAsync();
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using var _ = new ClockOverride(() => utcNow, () => utcNow.AddHours(2));
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var command = F.Build<EditCategoryCommand>()
                .With(x => x.Id, categoryId)
                .Create();

            await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.LastModified.On.Should().Be(utcNow);
        }
    }
}