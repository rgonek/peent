using System.Threading.Tasks;
using Peent.Application.Tags.Queries.GetTag;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Tags.Commands.CreateTag;
using Peent.Application.Tags.Commands.DeleteTag;
using Peent.Application.Exceptions;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Tags
{
    public class GetTagQueryHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task when_tag_exists__return_it()
        {
            var command = F.Create<CreateTagCommand>();
            var tagId = await SendAsync(command);

            var tagModel = await SendAsync(new GetTagQuery { Id = tagId });

            tagModel.Id.Should().Be(tagId);
            tagModel.Name.Should().Be(command.Name);
            tagModel.Description.Should().Be(command.Description);
        }

        [Fact]
        public async Task when_tag_do_not_exists__throws()
        {
            Invoking(async () => await SendAsync(new GetTagQuery { Id = 0 }))
                .Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task when_tag_exists_but_is_deleted__throws()
        {
            var command = F.Create<CreateTagCommand>();
            var tagId = await SendAsync(command);
            await SendAsync(new DeleteTagCommand {Id = tagId});

            Invoking(async () => await SendAsync(new GetTagQuery { Id = tagId }))
                .Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task when_tag_exists_in_another_workspace__throws()
        {
            var command = F.Create<CreateTagCommand>();
            var tagId = await SendAsync(command);

            await RunAsNewUserAsync();

            Invoking(async () => await SendAsync(new GetTagQuery { Id = tagId }))
                .Should().Throw<NotFoundException>();
        }
    }
}