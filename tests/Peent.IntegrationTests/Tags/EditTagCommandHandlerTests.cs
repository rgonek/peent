using System;
using System.Threading.Tasks;
using FluentAssertions;
using Peent.Application.Tags.Commands.CreateTag;
using Peent.Application.Exceptions;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Tags.Commands.DeleteTag;
using Peent.Application.Tags.Commands.EditTag;
using Peent.Common.Time;
using static Peent.IntegrationTests.DatabaseFixture;
using static FluentAssertions.FluentActions;

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
            var command = new EditTagCommand
            {
                Id = tagId,
                Name = F.Create<string>(),
                Description = F.Create<string>(),
                Date = F.Create<DateTime>()
            };
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
            var command = new EditTagCommand
            {
                Id = tagId,
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };
            await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.ModificationInfo.LastModifiedById.Should().Be(user2.Id);
        }

        [Fact]
        public async Task when_tag_is_edited__lastModifiedBy_is_set_to_current_user()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var tagId = await SendAsync(F.Create<CreateTagCommand>());
            var command = new EditTagCommand
            {
                Id = tagId,
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };
            await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.ModificationInfo.LastModifiedById.Should().Be(user.Id);
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
                var command = new EditTagCommand
                {
                    Id = tagId,
                    Name = F.Create<string>(),
                    Description = F.Create<string>()
                };
                await SendAsync(command);

                var tag = await FindAsync<Tag>(tagId);
                tag.ModificationInfo.LastModificationDate.Should().Be(utcNow);
            }
        }

        [Fact]
        public async Task when_tag_with_given_name_exists__throws()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateTagCommand>();
            var tagId = await SendAsync(command);
            var command2 = F.Create<CreateTagCommand>();
            await SendAsync(command2);

            Invoking(async () => await SendAsync(new EditTagCommand
                {
                    Id = tagId,
                    Name = command2.Name
                }))
                .Should().Throw<DuplicateException>();
        }

        [Fact]
        public async Task when_tag_with_given_name_exists_but_is_deleted__do_not_throw()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateTagCommand>();
            var tagId = await SendAsync(command);
            var command2 = F.Create<CreateTagCommand>();
            await SendAsync(command2);
            await SendAsync(new DeleteTagCommand { Id = tagId });

            await SendAsync(command);
        }

        [Fact]
        public async Task when_tag_with_given_name_exists_in_another_workspace__do_not_throw()
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

            await SendAsync(new EditTagCommand
                {
                    Id = tagId,
                    Name = command2.Name
                });
        }
    }
}
