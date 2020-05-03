using System.Threading.Tasks;
using Peent.Application.Tags.Commands.CreateTag;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Tags.Commands.DeleteTag;
using Peent.IntegrationTests.Infrastructure;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Tags
{
    public class DeleteTagCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_delete_tag()
        {
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
            var tagId = await SendAsync(F.Create<CreateTagCommand>());

            RunAs(await CreateUserAsync(), BaseContext.Workspace);
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
