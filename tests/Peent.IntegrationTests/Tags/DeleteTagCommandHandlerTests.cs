using System;
using System.Threading.Tasks;
using FluentAssertions;
using Peent.Application.Tags.Commands.CreateTag;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Tags.Commands.DeleteTag;
using Peent.Common.Time;
using static Peent.IntegrationTests.DatabaseFixture;

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
            tag.DeletionInfo.DeletionDate.Should().NotBeNull();
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
            tag.DeletionInfo.DeletionDate.Should().NotBeNull();
        }

        [Fact]
        public async Task when_tag_is_deleted__deletedBy_is_set_to_current_user()
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
            tag.DeletionInfo.DeletedById.Should().Be(user.Id);
        }

        [Fact]
        public async Task when_tag_is_deleted__deletionDate_is_set_to_utc_now()
        {
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using (new ClockOverride(() => utcNow, () => utcNow.AddHours(2)))
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
                tag.DeletionInfo.DeletionDate.Should().Be(utcNow);
            }
        }
    }
}
