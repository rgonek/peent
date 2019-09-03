using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Application.Currencies.Queries.GetCurrenciesList;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Currencies
{
    public class GetCurrenciesListQueryHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_returns_currencies_list()
        {
            var currencyId1 = await SendAsync(GetCreateCurrencyCommand());
            var currencyId2 = await SendAsync(GetCreateCurrencyCommand());
            var currencyId3 = await SendAsync(GetCreateCurrencyCommand());

            var currencies = await SendAsync(new GetCurrenciesListQuery());

            currencies.Should()
                .Contain(x => x.Id == currencyId1)
                .And.Contain(x => x.Id == currencyId2)
                .And.Contain(x => x.Id == currencyId3);
        }

        private CreateCurrencyCommand GetCreateCurrencyCommand()
        {
            return new CreateCurrencyCommand
            {
                Code = F.Create<string>().Substring(0, 3),
                Name = F.Create<string>(),
                Symbol = F.Create<string>().Substring(0, 12),
                DecimalPlaces = F.Create<ushort>()
            };
        }
    }
}
