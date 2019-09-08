using System.Threading.Tasks;
using AutoFixture;
using FluentValidation.TestHelper;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Application.Categories.Commands.DeleteCategory;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Categories
{
    public class CreateCategoryCommandValidatorTests : IntegrationTestBase
    {
        [Fact]
        public async Task when_category_with_given_name_not_exists__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();

            await ValidateAsync<CreateCategoryCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, command));
        }

        [Fact]
        public async Task when_category_with_given_name_exists__should_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();
            await SendAsync(command);

            await ValidateAsync<CreateCategoryCommandValidator>(validator =>
                validator.ShouldHaveValidationErrorFor(x => x.Name, command));
        }

        [Fact]
        public async Task when_category_with_given_name_exists_in_another_workspace__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();
            await SendAsync(command);

            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));

            await ValidateAsync<CreateCategoryCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, command));
        }

        [Fact]
        public async Task when_category_with_given_name_exists_but_is_deleted__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();
            var categoryId = await SendAsync(command);
            await SendAsync(new DeleteCategoryCommand { Id = categoryId });


            await ValidateAsync<CreateCategoryCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, command));
        }
    }
}
