using System;
using System.Threading.Tasks;
using FluentAssertions;
using Peent.Application.Tags.Commands.CreateTag;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Tags.Commands.EditTag;
using Peent.Common.Time;
using Peent.IntegrationTests.Infrastructure;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Tags
{
    public class EditTagCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_edit_tag()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var tagId = await SendAsync(F.Create<CreateTagCommand>());
            var command = F.Build<EditTagCommand>()
                .With(x => x.Id, tagId)
                .Create();

            await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.Name.Should().Be(command.Name);
            tag.Description.Should().Be(command.Description);
            tag.Date.Should().Be(command.Date);
        }

        [Fact]
        public async Task should_edit_tag_by_another_user_in_the_same_workspace()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var tagId = await SendAsync(F.Create<CreateTagCommand>());
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, workspace);
            var command = F.Build<EditTagCommand>()
                .With(x => x.Id, tagId)
                .Create();

            await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.LastModifiedById.Should().Be(user2.Id);
        }

        [Fact]
        public async Task when_tag_is_edited__lastModifiedBy_is_set_to_current_user()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var tagId = await SendAsync(F.Create<CreateTagCommand>());
            var command = F.Build<EditTagCommand>()
                .With(x => x.Id, tagId)
                .Create();

            await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.LastModifiedById.Should().Be(user.Id);
        }

        [Fact]
        public async Task when_tag_is_edited__lastModificationDate_is_set_to_utc_now()
        {
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using (new ClockOverride(() => utcNow, () => utcNow.AddHours(2)))
            {
                var user = await CreateUserAsync();
                SetCurrentUser(user, await CreateWorkspaceAsync(user));
                var tagId = await SendAsync(F.Create<CreateTagCommand>());
                var command = F.Build<EditTagCommand>()
                    .With(x => x.Id, tagId)
                    .Create();

                await SendAsync(command);

                var tag = await FindAsync<Tag>(tagId);
                tag.LastModificationDate.Should().Be(utcNow);
            }
        }
    }
}
