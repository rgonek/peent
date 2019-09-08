using System.Threading.Tasks;
using AutoFixture;
using FluentValidation.TestHelper;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Application.Categories.Commands.DeleteCategory;
using Peent.Application.Categories.Commands.EditCategory;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Categories
{
    public class EditCategoryCommandValidatorTests : IntegrationTestBase
    {
        [Fact]
        public async Task when_category_with_given_name_not_exists__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();
            var categoryId = await SendAsync(command);
            var editCommand = new EditCategoryCommand
            {
                Id = categoryId,
                Name = F.Create<string>()
            };

            await ValidateAsync<EditCategoryCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, editCommand));
        }

        [Fact]
        public async Task when_category_with_given_name_exists__should_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();
            var categoryId = await SendAsync(command);
            var command2 = F.Create<CreateCategoryCommand>();
            await SendAsync(command2);
            var editCommand = new EditCategoryCommand
            {
                Id = categoryId,
                Name = command2.Name
            };

            await ValidateAsync<EditCategoryCommandValidator>(validator =>
                validator.ShouldHaveValidationErrorFor(x => x.Name, editCommand));
        }

        [Fact]
        public async Task when_category_with_given_name_exists_in_another_workspace__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCategoryCommand>();
            var categoryId = await SendAsync(command);
            var command2 = F.Create<CreateCategoryCommand>();
            var categoryId2 = await SendAsync(command2);
            await SendAsync(new DeleteCategoryCommand { Id = categoryId2 });
            var editCommand = new EditCategoryCommand
            {
                Id = categoryId,
                Name = command2.Name
            };

            await ValidateAsync<EditCategoryCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, editCommand));
        }

        [Fact]
        public async Task when_category_with_given_name_exists_but_is_deleted__should_not_have_error()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var command = F.Create<CreateCategoryCommand>();
            var categoryId = await SendAsync(command);
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));
            var command2 = F.Create<CreateCategoryCommand>();
            await SendAsync(command2);
            SetCurrentUser(user, workspace);
            var editCommand = new EditCategoryCommand
            {
                Id = categoryId,
                Name = command2.Name
            };

            await ValidateAsync<EditCategoryCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, editCommand));
        }
    }
}
