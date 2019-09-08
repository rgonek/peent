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
using Peent.Application.Currencies.Commands.CreateCurrency;
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
            var accountId = await SendAsync(GetCreateAccountCommand());
            var command = F.Build<EditAccountCommand>()
                .With(x => x.Id, accountId)
                .With(x => x.CurrencyId, _currencyId)
                .Create();

            await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.Name.Should().Be(command.Name);
            account.Description.Should().Be(command.Description);
            account.Type.Should().Be(command.Type);
            account.CurrencyId.Should().Be(command.CurrencyId);
        }

        [Fact]
        public async Task should_edit_account_by_another_user_in_the_same_workspace()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var accountId = await SendAsync(GetCreateAccountCommand());
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, workspace);
            var command = F.Build<EditAccountCommand>()
                .With(x => x.Id, accountId)
                .With(x => x.CurrencyId, _currencyId)
                .Create();

            await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.ModificationInfo.LastModifiedById.Should().Be(user2.Id);
        }

        [Fact]
        public async Task when_account_is_edited__lastModifiedBy_is_set_to_current_user()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var accountId = await SendAsync(GetCreateAccountCommand());
            var command = F.Build<EditAccountCommand>()
                .With(x => x.Id, accountId)
                .With(x => x.CurrencyId, _currencyId)
                .Create();

            await SendAsync(command);

            var account = await FindAsync<Account>(accountId);
            account.ModificationInfo.LastModifiedById.Should().Be(user.Id);
        }

        [Fact]
        public async Task when_account_is_edited__lastModificationDate_is_set_to_utc_now()
        {
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using (new ClockOverride(() => utcNow, () => utcNow.AddHours(2)))
            {
                var user = await CreateUserAsync();
                SetCurrentUser(user, await CreateWorkspaceAsync(user));
                var accountId = await SendAsync(GetCreateAccountCommand());
                var command = F.Build<EditAccountCommand>()
                    .With(x => x.Id, accountId)
                    .With(x => x.CurrencyId, _currencyId)
                    .Create();

                await SendAsync(command);

                var account = await FindAsync<Account>(accountId);
                account.ModificationInfo.LastModificationDate.Should().Be(utcNow);
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
