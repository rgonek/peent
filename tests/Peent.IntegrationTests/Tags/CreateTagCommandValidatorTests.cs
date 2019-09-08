using System.Threading.Tasks;
using AutoFixture;
using FluentValidation.TestHelper;
using Peent.Application.Tags.Commands.CreateTag;
using Peent.Application.Tags.Commands.DeleteTag;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Tags
{
    public class CreateTagCommandValidatorTests : IntegrationTestBase
    {
        [Fact]
        public async Task when_tag_with_given_name_not_exists__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateTagCommand>();

            await ValidateAsync<CreateTagCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, command));
        }

        [Fact]
        public async Task when_tag_with_given_name_exists__should_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateTagCommand>();
            await SendAsync(command);

            await ValidateAsync<CreateTagCommandValidator>(validator =>
                validator.ShouldHaveValidationErrorFor(x => x.Name, command));
        }

        [Fact]
        public async Task when_tag_with_given_name_exists_in_another_workspace__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateTagCommand>();
            await SendAsync(command);

            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));

            await ValidateAsync<CreateTagCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, command));
        }

        [Fact]
        public async Task when_tag_with_given_name_exists_but_is_deleted__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateTagCommand>();
            var tagId = await SendAsync(command);
            await SendAsync(new DeleteTagCommand { Id = tagId });


            await ValidateAsync<CreateTagCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, command));
        }
    }
}
