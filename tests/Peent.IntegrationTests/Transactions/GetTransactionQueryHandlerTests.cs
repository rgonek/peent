using FluentAssertions;
using Peent.Application.Transactions.Commands.CreateTransaction;
using Peent.IntegrationTests.Infrastructure;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using Xunit;
using System.Threading.Tasks;
using AutoFixture;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Application.Transactions.Queries.GetTransaction;

namespace Peent.IntegrationTests.Transactions
{
    public class GetTransactionQueryHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task when_transaction_exists__return_it()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = await GetCreateTransactionCommand();
            var transactionId = await SendAsync(command);

            var transactionModel = await SendAsync(new GetTransactionQuery { Id = transactionId });

            transactionModel.Id.Should().Be(transactionId);
            transactionModel.Title.Should().Be(command.Title);
            transactionModel.Description.Should().Be(command.Description);
            transactionModel.Type.Should().Be(command.Type);
            transactionModel.Category.Id.Should().Be(command.CategoryId);
            transactionModel.Entries[0].Account.Id.Should().Be(command.FromAccountId);
            transactionModel.Entries[1].Account.Id.Should().Be(command.ToAccountId);
        }

        private async Task<CreateTransactionCommand> GetCreateTransactionCommand()
        {
            var categoryId = await SendAsync(F.Create<CreateCategoryCommand>());
            var accountId = await SendAsync(GetCreateAccountCommand());
            var accountId2 = await SendAsync(GetCreateAccountCommand());
            return F.Build<CreateTransactionCommand>()
                .With(x => x.CategoryId, categoryId)
                .With(x => x.CurrencyId, _currencyId)
                .With(x => x.ForeignCurrencyId, (int?)null)
                .With(x => x.FromAccountId, accountId)
                .With(x => x.ToAccountId, accountId2)
                .Create();
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
