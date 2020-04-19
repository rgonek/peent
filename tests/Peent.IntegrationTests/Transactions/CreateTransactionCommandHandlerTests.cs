using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Peent.Domain.Entities;
using Peent.Domain.Entities.TransactionAggregate;
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

            var command = A.Transaction
                .From(An.Account.OfAssetType())
                .To(An.Account.OfExpenseType())
                .WithCategory(A.Category)
                .AsCommand();

            var transactionId = await SendAsync(command);

            var transaction = await FindAsync(transactionId);
            transaction.CategoryId.Should().Be(command.CategoryId);
            transaction.Title.Should().Be(command.Title);
            transaction.Description.Should().Be(command.Description);
            transaction.Date.Should().Be(command.Date);
            transaction.Entries.Should().HaveCount(2)
                .And.SatisfyRespectively(
                    first => { first.Amount.Should().Be(-command.Amount); },
                    second => { second.Amount.Should().Be(command.Amount); });
        }

        private async ValueTask<Transaction> FindAsync(long id)
        {
            return await ExecuteDbContextAsync(db => new ValueTask<Transaction>(
                db.Transactions
                    .Include(x => x.Entries)
                    .Where(x => x.Id == id)
                    .SingleOrDefaultAsync()));
        }
    }
}
