using System.Threading.Tasks;
using Peent.Application.Categories.Queries.GetCategory;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Application.Categories.Commands.DeleteCategory;
using Peent.Application.Categories.Queries.GetCategoriesList;
using Peent.Application.Exceptions;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Categories
{
    public class GetCategoriesListQueryHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_returns_categories_list()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var categoryId1 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId2 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId3 = await SendAsync(F.Create<CreateCategoryCommand>());

            var categories = await SendAsync(new GetCategoriesListQuery());

            categories.Should()
                .Contain(x => x.Id == categoryId1)
                .And.Contain(x => x.Id == categoryId2)
                .And.Contain(x => x.Id == categoryId3);
        }

        [Fact]
        public async Task should_returns_categories_list_only_for_given_user()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var categoryId1 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId2 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId3 = await SendAsync(F.Create<CreateCategoryCommand>());
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));
            var categoryId4 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId5 = await SendAsync(F.Create<CreateCategoryCommand>());

            SetCurrentUser(user, workspace);
            var categories = await SendAsync(new GetCategoriesListQuery());

            categories.Should()
                .Contain(x => x.Id == categoryId1)
                .And.Contain(x => x.Id == categoryId2)
                .And.Contain(x => x.Id == categoryId3)
                .And.NotContain(x => x.Id == categoryId4)
                .And.NotContain(x => x.Id == categoryId5);
        }

        [Fact]
        public async Task should_should_not_returns_deleted_categories()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var categoryId1 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId2 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId3 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId4 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId5 = await SendAsync(F.Create<CreateCategoryCommand>());
            await SendAsync(new DeleteCategoryCommand {Id = categoryId4});
            await SendAsync(new DeleteCategoryCommand {Id = categoryId5});

            var categories = await SendAsync(new GetCategoriesListQuery());

            categories.Should()
                .Contain(x => x.Id == categoryId1)
                .And.Contain(x => x.Id == categoryId2)
                .And.Contain(x => x.Id == categoryId3)
                .And.NotContain(x => x.Id == categoryId4)
                .And.NotContain(x => x.Id == categoryId5);
        }
    }
}
