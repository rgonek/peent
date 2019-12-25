using System;
using System.Threading.Tasks;
using FluentAssertions;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Application.Exceptions;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Peent.Application.Accounts.Commands.EditAccount;
using Peent.Common.Time;
using Peent.IntegrationTests.Infrastructure;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Accounts
{
    public class EditAccountCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_edit_account()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var accountId = await SendAsync(F.Create<CreateAccountCommand>());
            var command = F.Build<EditAccountCommand>()
                .With(x => x.Id, accountId)
                .Create();

            await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.Name.Should().Be(command.Name);
            account.Description.Should().Be(command.Description);
            account.CurrencyId.Should().Be(command.CurrencyId);
        }

        [Fact]
        public async Task should_edit_account_by_another_user_in_the_same_workspace()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var accountId = await SendAsync(F.Create<CreateAccountCommand>());
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, workspace);
            var command = F.Build<EditAccountCommand>()
                .With(x => x.Id, accountId)
                .Create();

            await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.LastModifiedById.Should().Be(user2.Id);
        }

        [Fact]
        public async Task when_account_is_edited__lastModifiedBy_is_set_to_current_user()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var accountId = await SendAsync(F.Create<CreateAccountCommand>());
            var command = F.Build<EditAccountCommand>()
                .With(x => x.Id, accountId)
                .Create();

            await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.LastModifiedById.Should().Be(user.Id);
        }

        [Fact]
        public async Task when_account_is_edited__lastModificationDate_is_set_to_utc_now()
        {
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using (new ClockOverride(() => utcNow, () => utcNow.AddHours(2)))
            {
                var user = await CreateUserAsync();
                SetCurrentUser(user, await CreateWorkspaceAsync(user));
                var accountId = await SendAsync(F.Create<CreateAccountCommand>());
                var command = F.Build<EditAccountCommand>()
                    .With(x => x.Id, accountId)
                    .Create();

                await SendAsync(command);

                var account = await FindAsync<Account>(accountId);
                account.LastModificationDate.Should().Be(utcNow);
            }
        }

        [Fact]
        public async Task when_account_with_given_name_exists__throws()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateAccountCommand>();
            var accountId = await SendAsync(command);
            var command2 = F.Create<CreateAccountCommand>();
            await SendAsync(command2);

            Invoking(async () => await SendAsync(new EditAccountCommand
                {
                    Id = accountId,
                    Name = command2.Name
                }))
                .Should().Throw<DuplicateException>();
        }

        [Fact]
        public async Task when_account_with_given_name_exists_but_is_deleted__do_not_throw()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateAccountCommand>();
            var accountId = await SendAsync(command);
            var command2 = F.Create<CreateAccountCommand>();
            await SendAsync(command2);
            await SendAsync(new DeleteAccountCommand { Id = accountId });

            await SendAsync(command);
        }

        [Fact]
        public async Task when_account_with_given_name_exists_in_another_workspace__do_not_throw()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var command = F.Create<CreateAccountCommand>();
            var accountId = await SendAsync(command);
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));
            var command2 = F.Create<CreateAccountCommand>();
            await SendAsync(command2);
            SetCurrentUser(user, workspace);

            await SendAsync(new EditAccountCommand
                {
                    Id = accountId,
                    Name = command2.Name
                });
        }
    }
}
