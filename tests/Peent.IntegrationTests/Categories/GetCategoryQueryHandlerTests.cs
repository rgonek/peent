using System.Threading.Tasks;
using Peent.Application.Categories.Queries.GetCategory;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Application.Categories.Commands.DeleteCategory;
using Peent.Application.Exceptions;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Categories
{
    public class GetCategoryQueryHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task when_category_exists__return_it()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();
            var categoryId = await SendAsync(command);

            var categoryModel = await SendAsync(new GetCategoryQuery { Id = categoryId });

            categoryModel.Id.Should().Be(categoryId);
            categoryModel.Name.Should().Be(command.Name);
            categoryModel.Description.Should().Be(command.Description);
        }

        [Fact]
        public async Task when_category_do_not_exists__throws()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));

            Invoking(async () => await SendAsync(new GetCategoryQuery { Id = 0 }))
                .Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task when_category_exists_but_is_deleted__throws()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();
            var categoryId = await SendAsync(command);
            await SendAsync(new DeleteCategoryCommand {Id = categoryId});

            Invoking(async () => await SendAsync(new GetCategoryQuery { Id = categoryId }))
                .Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task when_category_exists_in_another_workspace__throws()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = new CreateCategoryCommand
            {
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };
            var categoryId = await SendAsync(command);

            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));

            Invoking(async () => await SendAsync(new GetCategoryQuery { Id = categoryId }))
                .Should().Throw<NotFoundException>();
        }
    }
}