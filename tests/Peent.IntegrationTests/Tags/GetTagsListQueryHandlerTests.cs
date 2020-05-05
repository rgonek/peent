using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Tags.Commands.CreateTag;
using Peent.Application.Tags.Commands.DeleteTag;
using Peent.Application.Tags.Queries.GetTagsList;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Tags
{
    public class GetTagsListQueryHandlerTests : IClassFixture<IntegrationTest>
    {
        [Fact]
        public async Task should_returns_tags_list()
        {
            await RunAsNewUserAsync();
            var tagId1 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId2 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId3 = await SendAsync(F.Create<CreateTagCommand>());

            var tagsPaged = await SendAsync(new GetTagsListQuery());

            tagsPaged.Results.Should()
                .Contain(x => x.Id == tagId1)
                .And.Contain(x => x.Id == tagId2)
                .And.Contain(x => x.Id == tagId3);
        }

        [Fact]
        public async Task should_returns_tags_list_only_for_given_user()
        {
            var runAs = await RunAsNewUserAsync();
            var tagId1 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId2 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId3 = await SendAsync(F.Create<CreateTagCommand>());

            await RunAsNewUserAsync();
            var tagId4 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId5 = await SendAsync(F.Create<CreateTagCommand>());

            RunAs(runAs);
            var tagsPaged = await SendAsync(new GetTagsListQuery());

            tagsPaged.Results.Should()
                .Contain(x => x.Id == tagId1)
                .And.Contain(x => x.Id == tagId2)
                .And.Contain(x => x.Id == tagId3)
                .And.NotContain(x => x.Id == tagId4)
                .And.NotContain(x => x.Id == tagId5);
        }
    }
}
