using System;
using AutoFixture;
using FluentAssertions;
using Peent.Common;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Currency;

namespace Peent.UnitTests.Domain.Entities.Currency
{
    public class Currency_SetSymbol_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_symbol_is_null_or_white_space__throws_argument_exception(string newSymbol)
        {
            var currency = F.Create<Sut>();

            Action act = () => currency.SetSymbol(newSymbol);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{nameof(Sut.Symbol).FirstDown()}*");
        }

        [Fact]
        public void when_symbol_is_not_null_or_white_space__does_not_throw()
        {
            var currency = F.Create<Sut>();
            var newSymbol = F.Create<string>();

            currency.SetSymbol(newSymbol);

            currency.Symbol.Should().Be(newSymbol);
        }
    }
}
