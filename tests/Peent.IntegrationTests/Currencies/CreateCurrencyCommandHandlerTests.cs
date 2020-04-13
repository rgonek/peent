using System.Threading.Tasks;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Application.Exceptions;
using Peent.IntegrationTests.Infrastructure;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Currencies
{
    public class CreateCurrencyCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_create_currency()
        {
            var command = F.Create<CreateCurrencyCommand>();

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
            var command = F.Create<CreateCurrencyCommand>();
            await SendAsync(command);

            command = F.Build<CreateCurrencyCommand>()
                .With(x => x.Code, command.Code)
                .Create();

            Invoking(async () => await SendAsync(command))
                .Should().Throw<DuplicateException>();
        }
    }
}
