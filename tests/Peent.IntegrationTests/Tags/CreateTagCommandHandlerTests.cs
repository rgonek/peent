using System;
using System.Threading.Tasks;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Tags.Commands.CreateTag;
using Peent.Application.Tags.Commands.DeleteTag;
using Peent.Application.Exceptions;
using Peent.Common.Time;
using Peent.IntegrationTests.Infrastructure;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Tags
{
    public class CreateTagCommandHandlerTests : IClassFixture<IntegrationTest>
    {
        [Fact]
        public async Task should_create_tag()
        {
             await RunAsNewUserAsync();
            var command = F.Create<CreateTagCommand>();

            var tagId = await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.Name.Should().Be(command.Name);
            tag.Description.Should().Be(command.Description);
        }

        [Fact]
        public async Task when_tag_is_created__createdBy_is_set_to_current_user()
        {
            var runAs = await RunAsNewUserAsync();
            var command = F.Create<CreateTagCommand>();

            var tagId = await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.Created.By.Should().Be(runAs.User);
        }

        [Fact]
        public async Task when_tag_is_created__creationDate_is_set_to_utc_now()
        {
            await RunAsNewUserAsync();
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using (new ClockOverride(() => utcNow, () => utcNow.AddHours(2)))
            {
                var command = F.Create<CreateTagCommand>();

                var tagId = await SendAsync(command);

                var tag = await FindAsync<Tag>(tagId);
                tag.Created.On.Should().Be(utcNow);
            }
        }

        [Fact]
        public async Task when_tag_is_created__workspace_is_set_to_current_user_workspace()
        {
            var runAs = await RunAsNewUserAsync();
            var command = F.Create<CreateTagCommand>();

            var tagId = await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.Workspace.Should().Be(runAs.Workspace);
            var fetchedWorkspace = await FindAsync<Workspace>(runAs.Workspace.Id);
            fetchedWorkspace.Created.By.Should().Be(runAs.User);
        }
    }
}
