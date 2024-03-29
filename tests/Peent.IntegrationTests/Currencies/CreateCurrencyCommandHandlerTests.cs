﻿using System.Threading.Tasks;
using Peent.Domain.Entities;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.IntegrationTests.Infrastructure;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Currencies
{
    [Collection(nameof(SharedFixture))]
    public class CreateCurrencyCommandHandlerTests
    {
        [Fact]
        public async Task should_create_currency()
        {
            await RunAsNewUserAsync();
            var command = F.Create<CreateCurrencyCommand>();

            var currencyId = await SendAsync(command);

            var currency = await FindAsync<Currency>(currencyId);
            currency.Code.Should().Be(command.Code);
            currency.Name.Should().Be(command.Name);
            currency.Symbol.Should().Be(command.Symbol);
            currency.DecimalPlaces.Should().Be(command.DecimalPlaces);
        }
    }
}
