﻿using System;
using System.Threading.Tasks;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Tags.Commands.CreateTag;
using Peent.Application.Tags.Commands.DeleteTag;
using Peent.Application.Exceptions;
using Peent.Common.Time;
using static Peent.IntegrationTests.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Tags
{
    public class CreateTagCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_create_tag()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = new CreateTagCommand
            {
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };

            var tagId = await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.Name.Should().Be(command.Name);
            tag.Description.Should().Be(command.Description);
            tag.Date.Should().Be(command.Date);
        }

        [Fact]
        public async Task when_tag_is_created__createdBy_is_set_to_current_user()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = new CreateTagCommand
            {
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };

            var tagId = await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.CreationInfo.CreatedById.Should().Be(user.Id);
        }

        [Fact]
        public async Task when_tag_is_created__creationDate_is_set_to_utc_now()
        {
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using (new ClockOverride(() => utcNow, () => utcNow.AddHours(2)))
            {
                var user = await CreateUserAsync();
                SetCurrentUser(user, await CreateWorkspaceAsync(user));
                var command = new CreateTagCommand
                {
                    Name = F.Create<string>(),
                    Description = F.Create<string>()
                };

                var tagId = await SendAsync(command);

                var tag = await FindAsync<Tag>(tagId);
                tag.CreationInfo.CreationDate.Should().Be(utcNow);
            }
        }

        [Fact]
        public async Task when_tag_is_created__workspace_is_set_to_current_user_workspace()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var command = new CreateTagCommand
            {
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };

            var tagId = await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.WorkspaceId.Should().Be(workspace.Id);
            var fetchedWorkspace = await FindAsync<Workspace>(workspace.Id);
            fetchedWorkspace.CreationInfo.CreatedById.Should().Be(user.Id);
        }

        [Fact]
        public async Task when_tag_with_given_name_exists__throws()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = new CreateTagCommand
            {
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };
            await SendAsync(command);

            Invoking(async () => await SendAsync(command))
                .Should().Throw<DuplicateException>();
        }

        [Fact]
        public async Task when_tag_with_given_name_exists_in_another_workspace__do_not_throw()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = new CreateTagCommand
            {
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };
            await SendAsync(command);

            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));
            await SendAsync(command);
        }

        [Fact]
        public async Task when_tag_with_given_name_exists_but_is_deleted__do_not_throw()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = new CreateTagCommand
            {
                Name = F.Create<string>(),
                Description = F.Create<string>()
            };
            var tagId = await SendAsync(command);
            await SendAsync(new DeleteTagCommand {Id = tagId});

            await SendAsync(command);
        }
    }
}
