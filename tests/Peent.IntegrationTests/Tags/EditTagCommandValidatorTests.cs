using System.Threading.Tasks;
using AutoFixture;
using FluentValidation.TestHelper;
using Peent.Application.Tags.Commands.CreateTag;
using Peent.Application.Tags.Commands.DeleteTag;
using Peent.Application.Tags.Commands.EditTag;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Tags
{
    public class EditTagCommandValidatorTests : IntegrationTestBase
    {
        [Fact]
        public async Task when_tag_with_given_name_not_exists__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateTagCommand>();
            var tagId = await SendAsync(command);
            var editCommand = new EditTagCommand
            {
                Id = tagId,
                Name = F.Create<string>()
            };

            await ValidateAsync<EditTagCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, editCommand));
        }

        [Fact]
        public async Task when_tag_with_given_name_exists__should_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateTagCommand>();
            var tagId = await SendAsync(command);
            var command2 = F.Create<CreateTagCommand>();
            await SendAsync(command2);
            var editCommand = new EditTagCommand
            {
                Id = tagId,
                Name = command2.Name
            };

            await ValidateAsync<EditTagCommandValidator>(validator =>
                validator.ShouldHaveValidationErrorFor(x => x.Name, editCommand));
        }

        [Fact]
        public async Task when_tag_with_given_name_exists_in_another_workspace__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateTagCommand>();
            var tagId = await SendAsync(command);
            var command2 = F.Create<CreateTagCommand>();
            var tagId2 = await SendAsync(command2);
            await SendAsync(new DeleteTagCommand { Id = tagId2 });
            var editCommand = new EditTagCommand
            {
                Id = tagId,
                Name = command2.Name
            };

            await ValidateAsync<EditTagCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, editCommand));
        }

        [Fact]
        public async Task when_tag_with_given_name_exists_but_is_deleted__should_not_have_error()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var command = F.Create<CreateTagCommand>();
            var tagId = await SendAsync(command);
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));
            var command2 = F.Create<CreateTagCommand>();
            await SendAsync(command2);
            SetCurrentUser(user, workspace);
            var editCommand = new EditTagCommand
            {
                Id = tagId,
                Name = command2.Name
            };

            await ValidateAsync<EditTagCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, editCommand));
        }
    }
}
