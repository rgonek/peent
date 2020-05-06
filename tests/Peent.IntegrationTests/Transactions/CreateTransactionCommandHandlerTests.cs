using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Peent.Domain.Entities;
using Peent.Domain.Entities.TransactionAggregate;
using Peent.Domain.ValueObjects;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Transactions
{
    public class CreateTransactionCommandHandlerTests : IClassFixture<IntegrationTest>
    {
        [Fact]
        public async Task should_create_transaction()
        {
//            await RunAsNewUserAsync();
//            Account fromAccount = An.Account.OfAssetType();
//            Account toAccount = An.Account.OfExpenseType();
//            var command = A.Transaction
//                .From(fromAccount)
//                .To(toAccount)
//                .WithCategory(A.Category)
//                .AsCommand();
//
//            var transactionId = await SendAsync(command);
//
//            var transaction = await FindAsync(transactionId);
//            transaction.Category.Id.Should().Be(command.CategoryId);
//            transaction.Title.Should().Be(command.Title);
//            transaction.Description.Should().Be(command.Description);
//            transaction.Date.Should().Be(command.Date);
//            transaction.Entries.Should().HaveCount(2)
//                .And.SatisfyRespectively(
//                    first => { first.Money.Should().Be(new Money(command.Amount, fromAccount.Currency)); },
//                    second => { second.Money.Should().Be(new Money(-command.Amount, toAccount.Currency)); });
        }

        private static async ValueTask<Transaction> FindAsync(long id)
        {
            return await ExecuteDbContextAsync(db => new ValueTask<Transaction>(
                db.Transactions
                    .Include(x => x.Entries)
                    .ThenInclude(x => x.Money.Currency)
                    .Include(x => x.Category)
                    .Where(x => x.Id == id)
                    .SingleOrDefaultAsync()));
        }
    }
}