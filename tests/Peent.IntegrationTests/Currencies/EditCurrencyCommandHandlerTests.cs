using System.Threading.Tasks;
using FluentAssertions;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Currencies.Commands.EditCurrency;
using Peent.IntegrationTests.Infrastructure;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Currencies
{
    public class EditCurrencyCommandHandlerTests : IClassFixture<IntegrationTest>
    {
        [Fact]
        public async Task should_edit_currency()
        {
            await RunAsNewUserAsync();
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
    }
}