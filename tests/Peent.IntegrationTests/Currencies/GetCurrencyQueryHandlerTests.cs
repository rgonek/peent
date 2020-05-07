using System.Threading.Tasks;
using Peent.Application.Currencies.Queries.GetCurrency;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Application.Exceptions;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Currencies
{
    [Collection(nameof(SharedFixture))]
    public class GetCurrencyQueryHandlerTests
    {
        [Fact]
        public async Task when_currency_exists__return_it()
        {
            await RunAsNewUserAsync();
            var command = F.Create<CreateCurrencyCommand>();
            var currencyId = await SendAsync(command);

            var currencyModel = await SendAsync(new GetCurrencyQuery { Id = currencyId });

            currencyModel.Id.Should().Be(currencyId);
            currencyModel.Code.Should().Be(command.Code);
            currencyModel.Name.Should().Be(command.Name);
            currencyModel.Symbol.Should().Be(command.Symbol);
            currencyModel.DecimalPlaces.Should().Be(command.DecimalPlaces);
        }
    }
}