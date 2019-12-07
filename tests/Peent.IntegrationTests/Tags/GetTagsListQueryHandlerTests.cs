using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Tags.Commands.CreateTag;
using Peent.Application.Tags.Commands.DeleteTag;
using Peent.Application.Tags.Queries.GetTagsList;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Tags
{
    public class GetTagsListQueryHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_returns_tags_list()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
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
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var tagId1 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId2 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId3 = await SendAsync(F.Create<CreateTagCommand>());
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));
            var tagId4 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId5 = await SendAsync(F.Create<CreateTagCommand>());

            SetCurrentUser(user, workspace);
            var tagsPaged = await SendAsync(new GetTagsListQuery());

            tagsPaged.Results.Should()
                .Contain(x => x.Id == tagId1)
                .And.Contain(x => x.Id == tagId2)
                .And.Contain(x => x.Id == tagId3)
                .And.NotContain(x => x.Id == tagId4)
                .And.NotContain(x => x.Id == tagId5);
        }

        [Fact]
        public async Task should_should_not_returns_deleted_tags()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var tagId1 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId2 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId3 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId4 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId5 = await SendAsync(F.Create<CreateTagCommand>());
            await SendAsync(new DeleteTagCommand { Id = tagId4 });
            await SendAsync(new DeleteTagCommand { Id = tagId5 });

            var tagsPaged = await SendAsync(new GetTagsListQuery());

            tagsPaged.Results.Should()
                .Contain(x => x.Id == tagId1)
                .And.Contain(x => x.Id == tagId2)
                .And.Contain(x => x.Id == tagId3)
                .And.NotContain(x => x.Id == tagId4)
                .And.NotContain(x => x.Id == tagId5);
        }

        [Fact]
        public async Task test()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var tagId1 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId2 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId3 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId4 = await SendAsync(F.Create<CreateTagCommand>());
            var tagId5 = await SendAsync(F.Create<CreateTagCommand>());

            var tagsPaged = await SendAsync(new GetTagsListQuery{PageIndex = 1, PageSize = 2});
        }
    }
}
