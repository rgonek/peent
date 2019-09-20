using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Application.Transactions.Commands.CreateTransaction;
using Peent.Application.Transactions.Commands.DeleteTransation;
using Peent.Common.Time;
using Peent.Domain.Entities;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Transactions
{
    public class DeleteTransactionCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_delete_transaction()
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

            await SendAsync(new DeleteTransactionCommand { Id = transactionId });

            var transaction = await FindAsync<Transaction>(transactionId);
            transaction.DeletionInfo.DeletionDate.Should().NotBeNull();

            var entries = await GetAsync<TransactionEntry>(x => x.TransactionId == transactionId);
            foreach (var entry in entries)
            {
                entry.DeletionInfo.DeletionDate.Should().NotBeNull();
            }
        }

        [Fact]
        public async Task should_delete_transaction_by_another_user_in_the_same_workspace()
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

            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, workspace);
            await SendAsync(new DeleteTransactionCommand { Id = transactionId });

            var transaction = await FindAsync<Transaction>(transactionId);
            transaction.DeletionInfo.DeletionDate.Should().NotBeNull();

            var entries = await GetAsync<TransactionEntry>(x => x.TransactionId == transactionId);
            foreach (var entry in entries)
            {
                entry.DeletionInfo.DeletionDate.Should().NotBeNull();
            }
        }

        [Fact]
        public async Task when_transaction_is_deleted__deletedBy_is_set_to_current_user()
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

            await SendAsync(new DeleteTransactionCommand { Id = transactionId });

            var transaction = await FindAsync<Transaction>(transactionId);
            transaction.DeletionInfo.DeletedById.Should().Be(user.Id);

            var entries = await GetAsync<TransactionEntry>(x => x.TransactionId == transactionId);
            foreach (var entry in entries)
            {
                entry.DeletionInfo.DeletedById.Should().Be(user.Id);
            }
        }

        [Fact]
        public async Task when_transaction_is_deleted__deletionDate_is_set_to_utc_now()
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

                await SendAsync(new DeleteTransactionCommand { Id = transactionId });

                var transaction = await FindAsync<Transaction>(transactionId);
                transaction.DeletionInfo.DeletionDate.Should().Be(utcNow);

                var entries = await GetAsync<TransactionEntry>(x => x.TransactionId == transactionId);
                foreach (var entry in entries)
                {
                    entry.DeletionInfo.DeletionDate.Should().Be(utcNow);
                }
            }
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
