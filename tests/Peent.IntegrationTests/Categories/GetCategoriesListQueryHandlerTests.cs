using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Application.Categories.Commands.DeleteCategory;
using Peent.Application.Categories.Queries.GetCategoriesList;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Categories
{
    public class GetCategoriesListQueryHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_returns_categories_list()
        {
            var categoryId1 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId2 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId3 = await SendAsync(F.Create<CreateCategoryCommand>());

            var categories = await SendAsync(new GetCategoriesListQuery());

            categories.Results.Should()
                .Contain(x => x.Id == categoryId1)
                .And.Contain(x => x.Id == categoryId2)
                .And.Contain(x => x.Id == categoryId3);
        }

        [Fact]
        public async Task should_returns_categories_list_only_for_given_user()
        {
            var categoryId1 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId2 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId3 = await SendAsync(F.Create<CreateCategoryCommand>());

            await RunAsNewUserAsync();
            var categoryId4 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId5 = await SendAsync(F.Create<CreateCategoryCommand>());

            RunAs(BaseContext);
            var categories = await SendAsync(new GetCategoriesListQuery());

            categories.Results.Should()
                .Contain(x => x.Id == categoryId1)
                .And.Contain(x => x.Id == categoryId2)
                .And.Contain(x => x.Id == categoryId3)
                .And.NotContain(x => x.Id == categoryId4)
                .And.NotContain(x => x.Id == categoryId5);
        }

        [Fact]
        public async Task should_should_not_returns_deleted_categories()
        {
            var categoryId1 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId2 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId3 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId4 = await SendAsync(F.Create<CreateCategoryCommand>());
            var categoryId5 = await SendAsync(F.Create<CreateCategoryCommand>());
            await SendAsync(new DeleteCategoryCommand(categoryId4));
            await SendAsync(new DeleteCategoryCommand(categoryId5));

            var categories = await SendAsync(new GetCategoriesListQuery());

            categories.Results.Should()
                .Contain(x => x.Id == categoryId1)
                .And.Contain(x => x.Id == categoryId2)
                .And.Contain(x => x.Id == categoryId3)
                .And.NotContain(x => x.Id == categoryId4)
                .And.NotContain(x => x.Id == categoryId5);
        }
    }
}
