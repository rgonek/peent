using System.Threading.Tasks;
using Peent.Application.Tags.Commands.CreateTag;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Tags.Commands.DeleteTag;
using Peent.IntegrationTests.Infrastructure;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Tags
{
    public class DeleteTagCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_delete_tag()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var tagId = await SendAsync(F.Create<CreateTagCommand>());
            var command = new DeleteTagCommand
            {
                Id = tagId
            };
            await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.Should().BeNull();
        }

        [Fact]
        public async Task should_delete_tag_by_another_user_in_the_same_workspace()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var tagId = await SendAsync(F.Create<CreateTagCommand>());
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, workspace);
            var command = new DeleteTagCommand
            {
                Id = tagId
            };
            await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.Should().BeNull();
        }
    }
}
