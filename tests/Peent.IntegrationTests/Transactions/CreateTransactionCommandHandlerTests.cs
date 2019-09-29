using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Application.Transactions.Commands.CreateTransaction;
using Peent.Common.Time;
using Peent.Domain.Entities;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Transactions
{
    public class CreateTransactionCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_create_transaction()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var accountId = await SendAsync(GetCreateAccountCommand());
            var accountId2 = await SendAsync(GetCreateAccountCommand());

            var command = F.Build<CreateTransactionCommand>()
                .With(x => x.CategoryId, categoryId)
                .With(x => x.CurrencyId, _currencyId)
                .With(x => x.ForeignCurrencyId, (int?)null)
                .With(x => x.FromAccountId, accountId)
                .With(x => x.ToAccountId, accountId2)
                .Create();

            var transactionId = await SendAsync(command);

            var transaction = await FindAsync<Transaction>(transactionId);
            transaction.Title.Should().Be(command.Title);
            transaction.Description.Should().Be(command.Description);
            transaction.Type.Should().Be(command.Type);

            var entries = await GetAsync<TransactionEntry>(x => x.TransactionId == transactionId);
            entries.Should().HaveCount(2);
            entries[0].Amount.Should().Be(-entries[1].Amount);
            foreach (var entry in entries)
            {
                entry.CurrencyId.Should().Be(command.CurrencyId);
                entry.ForeignCurrencyId.Should().Be(command.ForeignCurrencyId);
                Math.Abs(entry.Amount).Should().Be(command.Amount);
                Math.Abs(entry.ForeignAmount.Value).Should().Be(command.ForeignAmount.Value);
            }
        }

        [Fact]
        public async Task when_transaction_is_created__create_two_entries_with_amount_sum_equal_zero()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var accountId = await SendAsync(GetCreateAccountCommand());
            var accountId2 = await SendAsync(GetCreateAccountCommand());

            var command = F.Build<CreateTransactionCommand>()
                .With(x => x.CategoryId, categoryId)
                .With(x => x.CurrencyId, _currencyId)
                .With(x => x.ForeignCurrencyId, (int?)null)
                .With(x => x.FromAccountId, accountId)
                .With(x => x.ToAccountId, accountId2)
                .Create();

            var transactionId = await SendAsync(command);

            var entries = await GetAsync<TransactionEntry>(x => x.TransactionId == transactionId);
            entries.Should().HaveCount(2);
            entries[0].Amount.Should().Be(-entries[1].Amount);
            entries.Sum(x => x.Amount).Should().Be(0);
        }

        [Fact]
        public async Task when_transaction_is_created__createdBy_is_set_to_current_user()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var accountId = await SendAsync(GetCreateAccountCommand());
            var accountId2 = await SendAsync(GetCreateAccountCommand());

            var command = F.Build<CreateTransactionCommand>()
                .With(x => x.CategoryId, categoryId)
                .With(x => x.CurrencyId, _currencyId)
                .With(x => x.ForeignCurrencyId, (int?)null)
                .With(x => x.FromAccountId, accountId)
                .With(x => x.ToAccountId, accountId2)
                .Create();

            var transactionId = await SendAsync(command);

            var transaction = await FindAsync<Transaction>(transactionId);
            transaction.CreatedById.Should().Be(user.Id);
            var entries = await GetAsync<TransactionEntry>(x => x.TransactionId == transactionId);
            foreach (var entry in entries)
            {
                entry.CreatedById.Should().Be(user.Id);
            }
        }

        [Fact]
        public async Task when_transaction_is_created__creationDate_is_set_to_utc_now()
        {
            var utcNow = new DateTime(2019, 02, 02, 11, 28, 32);
            using (new ClockOverride(() => utcNow, () => utcNow.AddHours(2)))
            {
                var user = await CreateUserAsync();
                SetCurrentUser(user, await CreateWorkspaceAsync(user));
                var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
                var accountId = await SendAsync(GetCreateAccountCommand());
                var accountId2 = await SendAsync(GetCreateAccountCommand());

                var command = F.Build<CreateTransactionCommand>()
                    .With(x => x.CategoryId, categoryId)
                    .With(x => x.CurrencyId, _currencyId)
                    .With(x => x.ForeignCurrencyId, (int?)null)
                    .With(x => x.FromAccountId, accountId)
                    .With(x => x.ToAccountId, accountId2)
                    .Create();

                var transactionId = await SendAsync(command);

                var transaction = await FindAsync<Transaction>(transactionId);
                transaction.CreationDate.Should().Be(utcNow);
                var entries = await GetAsync<TransactionEntry>(x => x.TransactionId == transactionId);
                foreach (var entry in entries)
                {
                    entry.CreationDate.Should().Be(utcNow);
                }
            }
        }

        [Fact]
        public async Task when_transaction_is_created__workspace_is_set_to_current_user_workspace()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var accountId = await SendAsync(GetCreateAccountCommand());
            var accountId2 = await SendAsync(GetCreateAccountCommand());

            var command = F.Build<CreateTransactionCommand>()
                .With(x => x.CategoryId, categoryId)
                .With(x => x.CurrencyId, _currencyId)
                .With(x => x.ForeignCurrencyId, (int?)null)
                .With(x => x.FromAccountId, accountId)
                .With(x => x.ToAccountId, accountId2)
                .Create();

            var transactionId = await SendAsync(command);

            var transaction = await FindAsync<Transaction>(transactionId);
            transaction.WorkspaceId.Should().Be(workspace.Id);
        }

        [Fact]
        public async Task when_transaction_is_created__entry_with_negative_amount_should_have_account_set_to_from_account()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var accountId = await SendAsync(GetCreateAccountCommand());
            var accountId2 = await SendAsync(GetCreateAccountCommand());

            var command = F.Build<CreateTransactionCommand>()
                .With(x => x.CategoryId, categoryId)
                .With(x => x.CurrencyId, _currencyId)
                .With(x => x.ForeignCurrencyId, (int?)null)
                .With(x => x.FromAccountId, accountId)
                .With(x => x.ToAccountId, accountId2)
                .Create();

            var transactionId = await SendAsync(command);

            var entries = await GetAsync<TransactionEntry>(x => x.TransactionId == transactionId);
            var chargeEntry = entries.Single(x => x.Amount < 0);
            chargeEntry.AccountId.Should().Be(command.FromAccountId);
        }

        [Fact]
        public async Task when_transaction_is_created__entry_with_positive_amount_should_have_account_set_to_to_account()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var accountId = await SendAsync(GetCreateAccountCommand());
            var accountId2 = await SendAsync(GetCreateAccountCommand());

            var command = F.Build<CreateTransactionCommand>()
                .With(x => x.CategoryId, categoryId)
                .With(x => x.CurrencyId, _currencyId)
                .With(x => x.ForeignCurrencyId, (int?)null)
                .With(x => x.FromAccountId, accountId)
                .With(x => x.ToAccountId, accountId2)
                .Create();

            var transactionId = await SendAsync(command);

            var entries = await GetAsync<TransactionEntry>(x => x.TransactionId == transactionId);
            var depositEntry = entries.Single(x => x.Amount > 0);
            depositEntry.AccountId.Should().Be(command.ToAccountId);
        }

        private CreateAccountCommand GetCreateAccountCommand()
        {
            return F.Build<CreateAccountCommand>()
                .With(x => x.CurrencyId, _currencyId.Value)
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
