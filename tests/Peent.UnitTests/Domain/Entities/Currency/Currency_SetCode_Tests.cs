using System;
using AutoFixture;
using FluentAssertions;
using Peent.Common;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Currency;

namespace Peent.UnitTests.Domain.Entities.Currency
{
    public class Currency_SetCode_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_code_is_null_or_white_space__throws_argument_exception(string newCode)
        {
            var currency = F.Create<Sut>();

            Action act = () => currency.SetCode(newCode);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{nameof(Sut.Code).FirstDown()}*");
        }

        [Fact]
        public void when_code_is_not_null_or_white_space__does_not_throw()
        {
            var currency = F.Create<Sut>();
            var newCode = F.Create<string>();

            currency.SetCode(newCode);

            currency.Code.Should().Be(newCode);
        }
    }
}
