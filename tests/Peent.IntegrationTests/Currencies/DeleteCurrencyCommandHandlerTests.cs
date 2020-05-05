using System.Threading.Tasks;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Currencies.Commands.DeleteCurrency;
using Peent.IntegrationTests.Infrastructure;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Currencies
{
    public class DeleteCurrencyCommandHandlerTests : IClassFixture<IntegrationTest>
    {
        [Fact]
        public async Task should_delete_currency()
        {
            await RunAsNewUserAsync();
            var currencyId = await SendAsync(F.Create<CreateCurrencyCommand>());
            var command = new DeleteCurrencyCommand
            {
                Id = currencyId
            };

            await SendAsync(command);

            var currency = await FindAsync<Currency>(currencyId);
            currency.Should().BeNull();
        }
    }
}
