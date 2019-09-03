using System;
using System.Threading.Tasks;
using FluentAssertions;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Application.Exceptions;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using Peent.Application.Currencies.Commands.DeleteCurrency;
using Peent.Application.Currencies.Commands.EditCurrency;
using Peent.Common.Time;
using static Peent.IntegrationTests.DatabaseFixture;
using static FluentAssertions.FluentActions;

namespace Peent.IntegrationTests.Currencies
{
    public class EditCurrencyCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task should_edit_currency()
        {
            var createCommand = new CreateCurrencyCommand
            {
                Code = F.Create<string>().Substring(0, 3),
                Name = F.Create<string>(),
                Symbol = F.Create<string>().Substring(0, 12),
                DecimalPlaces = F.Create<ushort>()
            };
            var currencyId = await SendAsync(createCommand);
            var command = new EditCurrencyCommand
            {
                Id = currencyId,
                Code = F.Create<string>().Substring(0, 3),
                Name = F.Create<string>(),
                Symbol = F.Create<string>().Substring(0, 12),
                DecimalPlaces = F.Create<ushort>()
            };
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
            var createCommand = new CreateCurrencyCommand
            {
                Code = F.Create<string>().Substring(0, 3),
                Name = F.Create<string>(),
                Symbol = F.Create<string>().Substring(0, 12),
                DecimalPlaces = F.Create<ushort>()
            };
            var currencyId = await SendAsync(createCommand);
            var createCommand2 = new CreateCurrencyCommand
            {
                Code = F.Create<string>().Substring(0, 3),
                Name = F.Create<string>(),
                Symbol = F.Create<string>().Substring(0, 12),
                DecimalPlaces = F.Create<ushort>()
            };
            await SendAsync(createCommand2);

            Invoking(async () => await SendAsync(new EditCurrencyCommand
                {
                    Id = currencyId,
                    Code = createCommand2.Code,
                    Name = F.Create<string>(),
                    Symbol = F.Create<string>().Substring(0, 12),
                    DecimalPlaces = F.Create<ushort>()
                }))
                .Should().Throw<DuplicateException>();
        }
    }
}
