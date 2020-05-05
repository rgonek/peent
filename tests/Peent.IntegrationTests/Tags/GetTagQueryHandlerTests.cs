using System.Threading.Tasks;
using Peent.Application.Tags.Queries.GetTag;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Tags.Commands.CreateTag;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Tags
{
    public class GetTagQueryHandlerTests : IClassFixture<IntegrationTest>
    {
        [Fact]
        public async Task when_tag_exists__return_it()
        {
            await RunAsNewUserAsync();
            var command = F.Create<CreateTagCommand>();
            var tagId = await SendAsync(command);

            var tagModel = await SendAsync(new GetTagQuery { Id = tagId });

            tagModel.Id.Should().Be(tagId);
            tagModel.Name.Should().Be(command.Name);
            tagModel.Description.Should().Be(command.Description);
        }
    }
}