using System;
using System.Threading.Tasks;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Common.Time;
using Peent.IntegrationTests.Infrastructure;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Categories
{
    [Collection(nameof(SharedFixture))]
    public class CreateCategoryCommandHandlerTests
    {
        [Fact]
        public async Task should_create_category()
        {
            await RunAsNewUserAsync();
            var command = F.Create<CreateCategoryCommand>();

            var categoryId = await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.Name.Should().Be(command.Name);
            category.Description.Should().Be(command.Description);
        }

        [Fact]
        public async Task when_category_is_created__createdBy_is_set_to_current_user()
        {
            var runAs = await RunAsNewUserAsync();
            var command = F.Create<CreateCategoryCommand>();

            var categoryId = await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.Created.By.Should().Be(runAs.User);
        }

        [Fact]
        public async Task when_category_is_created__creationDate_is_set_to_utc_now()
        {
            await RunAsNewUserAsync();
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using (new ClockOverride(() => utcNow, () => utcNow.AddHours(2)))
            {
                var command = F.Create<CreateCategoryCommand>();

                var categoryId = await SendAsync(command);

                var category = await FindAsync<Category>(categoryId);
                category.Created.On.Should().Be(utcNow);
            }
        }

        [Fact]
        public async Task when_category_is_created__workspace_is_set_to_current_user_workspace()
        {
            var runAs = await RunAsNewUserAsync();
            var command = F.Create<CreateCategoryCommand>();

            var categoryId = await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.Workspace.Should().Be(runAs.Workspace);
            var fetchedWorkspace = await FindAsync<Workspace>(runAs.Workspace.Id);
            fetchedWorkspace.Created.By.Should().Be(runAs.User);
        }
    }
}