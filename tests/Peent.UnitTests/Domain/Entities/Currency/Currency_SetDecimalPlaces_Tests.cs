using System;
using AutoFixture;
using FluentAssertions;
using Peent.Common;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Currency;

namespace Peent.UnitTests.Domain.Entities.Currency
{
    public class Currency_SetCurrency_Tests
    {
        [Theory]
        [InlineData(0)]
        public void when_decimal_places_is_not_positive__throws_argument_exception(ushort newDecimalPlaces)
        {
            var currency = F.Create<Sut>();

            Action act = () => currency.SetDecimalPlaces(newDecimalPlaces);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{nameof(Sut.DecimalPlaces).FirstDown()}*");
        }

        [Fact]
        public void when_decimal_places_is_positive__does_not_throw()
        {
            var currency = F.Create<Sut>();
            var newDecimalPlaces = F.Create<ushort>();

            currency.SetDecimalPlaces(newDecimalPlaces);

            currency.DecimalPlaces.Should().Be(newDecimalPlaces);
        }
    }
}
