using System.Threading.Tasks;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Currencies.Commands.DeleteCurrency;
using static Peent.IntegrationTests.DatabaseFixture;

namespace Peent.IntegrationTests.Currencies
{
    public class DeleteCurrencyCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_delete_currency()
        {
            var createCommand = new CreateCurrencyCommand
            {
                Code = F.Create<string>().Substring(0, 3),
                Name = F.Create<string>(),
                Symbol = F.Create<string>().Substring(0, 12),
                DecimalPlaces = F.Create<ushort>()
            };
            var currencyId = await SendAsync(createCommand);
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
