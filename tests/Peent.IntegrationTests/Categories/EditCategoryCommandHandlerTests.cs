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
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Categories
{
    public class EditCategoryCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_edit_category()
        {
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
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var currentContext = RunAs(await CreateUserAsync(), BaseContext.Workspace);
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
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var command = F.Build<EditCategoryCommand>()
                .With(x => x.Id, categoryId)
                .Create();

            await SendAsync(command);

            var category = await FindAsync<Category>(categoryId);
            category.LastModified.By.Should().Be(BaseContext.User);
        }

        [Fact]
        public async Task when_category_is_edited__lastModificationDate_is_set_to_utc_now()
        {
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

        [Fact]
        public async Task when_category_with_given_name_exists__throws()
        {
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
            var command = F.Create<CreateCategoryCommand>();
            var categoryId = await SendAsync(command);
            var command2 = F.Create<CreateCategoryCommand>();
            await SendAsync(command2);
            await SendAsync(new DeleteCategoryCommand(categoryId));

            await SendAsync(command);
        }

        [Fact]
        public async Task when_category_with_given_name_exists_in_another_workspace__do_not_throw()
        {
            var command = F.Create<CreateCategoryCommand>();
            var categoryId = await SendAsync(command);
            
            await RunAsNewUserAsync();
            var command2 = F.Create<CreateCategoryCommand>();
            await SendAsync(command2);
            
            RunAs(BaseContext);

            await SendAsync(new EditCategoryCommand
            {
                Id = categoryId,
                Name = command2.Name
            });
        }
    }
}