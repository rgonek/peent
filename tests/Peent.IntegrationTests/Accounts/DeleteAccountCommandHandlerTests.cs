using System;
using System.Threading.Tasks;
using FluentAssertions;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Common.Time;
using Peent.IntegrationTests.Infrastructure;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Accounts
{
    public class DeleteAccountCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_delete_account()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var accountId = await SendAsync(GetCreateAccountCommand());
            var command = new DeleteAccountCommand
            {
                Id = accountId
            };
            await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.DeletionDate.Should().NotBeNull();
        }

        [Fact]
        public async Task should_delete_account_by_another_user_in_the_same_workspace()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var accountId = await SendAsync(GetCreateAccountCommand());
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, workspace);
            var command = new DeleteAccountCommand
            {
                Id = accountId
            };
            await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.DeletionDate.Should().NotBeNull();
        }

        [Fact]
        public async Task when_account_is_deleted__deletedBy_is_set_to_current_user()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var accountId = await SendAsync(GetCreateAccountCommand());
            var command = new DeleteAccountCommand
            {
                Id = accountId
            };
            await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.DeletedById.Should().Be(user.Id);
        }

        [Fact]
        public async Task when_account_is_deleted__deletionDate_is_set_to_utc_now()
        {
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using (new ClockOverride(() => utcNow, () => utcNow.AddHours(2)))
            {
                var user = await CreateUserAsync();
                SetCurrentUser(user, await CreateWorkspaceAsync(user));
                var accountId = await SendAsync(GetCreateAccountCommand());
                var command = new DeleteAccountCommand
                {
                    Id = accountId
                };
                await SendAsync(command);

                var account = await FindAsync<Account>(accountId);
                account.DeletionDate.Should().Be(utcNow);
            }
        }

        private CreateAccountCommand GetCreateAccountCommand()
        {
            return F.Build<CreateAccountCommand>()
                .With(x => x.CurrencyId, _currencyId)
                .Create();
        }

        private static int? _currencyId;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            if (_currencyId.HasValue == false)
                _currencyId = await SendAsync(F.Create<CreateCurrencyCommand>());
        }
    }
}
