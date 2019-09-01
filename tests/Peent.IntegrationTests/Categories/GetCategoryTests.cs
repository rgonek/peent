using System;
using System.Threading.Tasks;
using Peent.Application.Categories.Queries.GetCategory;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Application.Exceptions;
using Xunit;
using static Peent.IntegrationTests.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Categories
{
    public class GetCategoryTests : IntegrationTestBase
    {
        [Fact]
        public async Task when_category_exists__return_it()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = new CreateCategoryCommand
            {
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };
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