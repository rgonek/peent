using System;
using System.Threading.Tasks;
using FluentAssertions;
using Peent.Application.Tags.Commands.CreateTag;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Tags.Commands.DeleteTag;
using Peent.Application.Tags.Commands.EditTag;
using Peent.Common.Time;
using Peent.IntegrationTests.Infrastructure;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Tags
{
    [Collection(nameof(SharedFixture))]
    public class EditTagCommandHandlerTests
    {
        [Fact]
        public async Task should_edit_tag()
        {
            await RunAsNewUserAsync();
            var tagId = await SendAsync(F.Create<CreateTagCommand>());
            var command = F.Build<EditTagCommand>()
                .With(x => x.Id, tagId)
                .Create();

            await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.Name.Should().Be(command.Name);
            tag.Description.Should().Be(command.Description);
        }

        [Fact]
        public async Task should_edit_tag_by_another_user_in_the_same_workspace()
        {
            var runAs = await RunAsNewUserAsync();
            var tagId = await SendAsync(F.Create<CreateTagCommand>());
            var context = RunAs(await CreateUserAsync(), runAs.Workspace);
            var command = F.Build<EditTagCommand>()
                .With(x => x.Id, tagId)
                .Create();

            await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.LastModified.By.Should().Be(context.User);
        }

        [Fact]
        public async Task when_tag_is_edited__lastModifiedBy_is_set_to_current_user()
        {
            var runAs = await RunAsNewUserAsync();
            var tagId = await SendAsync(F.Create<CreateTagCommand>());
            var command = F.Build<EditTagCommand>()
                .With(x => x.Id, tagId)
                .Create();

            await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.LastModified.By.Should().Be(runAs.User);
        }

        [Fact]
        public async Task when_tag_is_edited__lastModificationDate_is_set_to_utc_now()
        {
            await RunAsNewUserAsync();
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using var _ = new ClockOverride(() => utcNow, () => utcNow.AddHours(2));
            var tagId = await SendAsync(F.Create<CreateTagCommand>());
            var command = F.Build<EditTagCommand>()
                .With(x => x.Id, tagId)
                .Create();

            await SendAsync(command);

            var tag = await FindAsync<Tag>(tagId);
            tag.LastModified.On.Should().Be(utcNow);
        }
    }
}