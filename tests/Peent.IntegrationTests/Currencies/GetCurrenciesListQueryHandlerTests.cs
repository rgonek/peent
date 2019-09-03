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
            var currencyId1 = await SendAsync(F.Create<CreateCurrencyCommand>());
            var currencyId2 = await SendAsync(F.Create<CreateCurrencyCommand>());
            var currencyId3 = await SendAsync(F.Create<CreateCurrencyCommand>());

            var currencies = await SendAsync(new GetCurrenciesListQuery());

            currencies.Should()
                .Contain(x => x.Id == currencyId1)
                .And.Contain(x => x.Id == currencyId2)
                .And.Contain(x => x.Id == currencyId3);
        }
    }
}
