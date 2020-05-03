using System;
using System.Threading.Tasks;
using FluentAssertions;
using Peent.Application.Exceptions;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Peent.Application.Accounts.Commands.EditAccount;
using Peent.Common.Time;
using Peent.IntegrationTests.Infrastructure;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Accounts
{
    public class EditAccountCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_edit_account()
        {
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
            Account account = An.Account;
            var context = await RunAsNewUserAsync(BaseContext.Workspace);
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
            Account account = An.Account;
            var command = F.Build<EditAccountCommand>()
                .With(x => x.Id, account.Id)
                .With(x => x.CurrencyId, account.Currency.Id)
                .Create();

            await SendAsync(command);

            account = await FindAsync<Account>(account.Id);
            account.LastModified.By.Should().Be(BaseContext.User);
        }

        [Fact]
        public async Task when_account_is_edited__lastModificationDate_is_set_to_utc_now()
        {
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

        [Fact]
        public void when_account_with_given_name_exists__throws()
        {
            Account account = An.Account.OfAssetType();
            Account account2 = An.Account.OfAssetType();

            Invoking(async () => await SendAsync(new EditAccountCommand
            {
                Id = account.Id,
                Name = account2.Name,
                CurrencyId = account.Currency.Id
            })).Should().Throw<DuplicateException>();
        }

        [Fact]
        public async Task when_account_with_given_name_exists_but_is_deleted__do_not_throw()
        {
            Account account = An.Account;
            Account account2 = An.Account;
            await SendAsync(new DeleteAccountCommand(account.Id));

            await SendAsync(new EditAccountCommand
            {
                Id = account2.Id,
                Name = account.Name,
                CurrencyId = account2.Currency.Id
            });
        }

        [Fact]
        public async Task when_account_with_given_name_exists_in_another_workspace__do_not_throw()
        {
            Account account = An.Account;
            await RunAsNewUserAsync();
            Account account2 = An.Account;
            RunAs(BaseContext);

            await SendAsync(new EditAccountCommand
            {
                Id = account.Id,
                Name = account2.Name,
                CurrencyId = account.Currency.Id
            });
        }
    }
}
