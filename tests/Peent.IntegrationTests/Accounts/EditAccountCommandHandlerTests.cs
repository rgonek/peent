using System;
using System.Threading.Tasks;
using FluentAssertions;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Accounts.Commands.EditAccount;
using Peent.Common.Time;
using Peent.IntegrationTests.Infrastructure;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Accounts
{
    public class EditAccountCommandHandlerTests : IClassFixture<IntegrationTest>
    {
        [Fact]
        public async Task should_edit_account()
        {
            await RunAsNewUserAsync();
            Account account = An.Account;
            var command = F.Build<EditAccountCommand>()
                .With(x => x.Id, account.Id)
                .With(x => x.CurrencyId, account.Currency.Id)
                .Create();

            await SendAsync(command);

            account = await FindAsync<Account>(account.Id);
            account.Name.Should().Be(command.Name);
            account.Description.Should().Be(command.Description);
            account.Currency.Id.Should().Be(command.CurrencyId);
        }

        [Fact]
        public async Task should_edit_account_by_another_user_in_the_same_workspace()
        {
            var runAs = await RunAsNewUserAsync();
            Account account = An.Account;
            var context = await RunAsNewUserAsync(runAs.Workspace);
            var command = F.Build<EditAccountCommand>()
                .With(x => x.Id, account.Id)
                .With(x => x.CurrencyId, account.Currency.Id)
                .Create();

            await SendAsync(command);

            account = await FindAsync<Account>(account.Id);
            account.LastModified.By.Should().Be(context.User);
        }

        [Fact]
        public async Task when_account_is_edited__lastModifiedBy_is_set_to_current_user()
        {
            var _baseContext = await RunAsNewUserAsync();
            Account account = An.Account;
            var command = F.Build<EditAccountCommand>()
                .With(x => x.Id, account.Id)
                .With(x => x.CurrencyId, account.Currency.Id)
                .Create();

            await SendAsync(command);

            account = await FindAsync<Account>(account.Id);
            account.LastModified.By.Should().Be(_baseContext.User);
        }

        [Fact]
        public async Task when_account_is_edited__lastModificationDate_is_set_to_utc_now()
        {
            await RunAsNewUserAsync();
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using (new ClockOverride(() => utcNow, () => utcNow.AddHours(2)))
            {
                Account account = An.Account;
                var command = F.Build<EditAccountCommand>()
                    .With(x => x.Id, account.Id)
                    .With(x => x.CurrencyId, account.Currency.Id)
                    .Create();

                await SendAsync(command);

                account = await FindAsync<Account>(account.Id);
                account.LastModified.On.Should().Be(utcNow);
            }
        }
    }
}