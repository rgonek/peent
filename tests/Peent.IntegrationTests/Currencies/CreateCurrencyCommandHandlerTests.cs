using System.Threading.Tasks;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Application.Exceptions;
using Peent.IntegrationTests.Infrastructure;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Currencies
{
    public class CreateCurrencyCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_create_currency()
        {
            var command = new CreateCurrencyCommand
            {
                Code = F.Create<string>().Substring(0, 3),
                Name = F.Create<string>(),
                Symbol = F.Create<string>().Substring(0, 12),
                DecimalPlaces = F.Create<ushort>()
            };

            var currencyId = await SendAsync(command);

            var currency = await FindAsync<Currency>(currencyId);
            currency.Code.Should().Be(command.Code);
            currency.Name.Should().Be(command.Name);
            currency.Symbol.Should().Be(command.Symbol);
            currency.DecimalPlaces.Should().Be(command.DecimalPlaces);
        }

        [Fact]
        public async Task when_currency_with_given_code_exists__throws()
        {
            var command = new CreateCurrencyCommand
            {
                Code = F.Create<string>().Substring(0, 3),
                Name = F.Create<string>(),
                Symbol = F.Create<string>().Substring(0, 12),
                DecimalPlaces = F.Create<ushort>()
            };
            await SendAsync(command);

            command = new CreateCurrencyCommand
            {
                Code = command.Code,
                Name = F.Create<string>(),
                Symbol = F.Create<string>().Substring(0, 12),
                DecimalPlaces = F.Create<ushort>()
            };

            Invoking(async () => await SendAsync(command))
                .Should().Throw<DuplicateException>();
        }
    }
}
