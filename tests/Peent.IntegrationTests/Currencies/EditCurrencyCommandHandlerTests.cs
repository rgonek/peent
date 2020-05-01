using System.Threading.Tasks;
using FluentAssertions;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Application.Exceptions;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Currencies.Commands.EditCurrency;
using Peent.IntegrationTests.Infrastructure;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Currencies
{
    public class EditCurrencyCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_edit_currency()
        {
            var currencyId = await SendAsync(F.Create<CreateCurrencyCommand>());
            var command = F.Create<EditCurrencyCommand>();
            command.Id = currencyId;

            await SendAsync(command);

            var currency = await FindAsync<Currency>(currencyId);
            currency.Code.Should().Be(command.Code);
            currency.Name.Should().Be(command.Name);
            currency.Symbol.Should().Be(command.Symbol);
            currency.DecimalPlaces.Should().Be(command.DecimalPlaces);
        }

        [Fact]
        public async Task when_currency_with_given_code_exists__throws()
        {
            var currencyId = await SendAsync(F.Create<CreateCurrencyCommand>());
            var createCommand = F.Create<CreateCurrencyCommand>();
            await SendAsync(createCommand);

            var editCommand = F.Create<EditCurrencyCommand>();
            editCommand.Id = currencyId;
            editCommand.Code = createCommand.Code;

            Invoking(async () => await SendAsync(editCommand))
                .Should().Throw<DuplicateException>();
        }
    }
}
