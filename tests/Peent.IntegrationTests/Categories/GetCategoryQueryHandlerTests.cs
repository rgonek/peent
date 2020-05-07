using System.Threading.Tasks;
using Peent.Application.Categories.Queries.GetCategory;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Categories
{
    [Collection(nameof(SharedFixture))]
    public class GetCategoryQueryHandlerTests
    {
        [Fact]
        public async Task when_category_exists__return_it()
        {
            await RunAsNewUserAsync();
            var command = F.Create<CreateCategoryCommand>();
            var categoryId = await SendAsync(command);

            var categoryModel = await SendAsync(new GetCategoryQuery { Id = categoryId });

            categoryModel.Id.Should().Be(categoryId);
            categoryModel.Name.Should().Be(command.Name);
            categoryModel.Description.Should().Be(command.Description);
        }
    }
}