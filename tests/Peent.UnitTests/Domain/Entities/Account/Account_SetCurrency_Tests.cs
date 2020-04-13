using System;
using AutoFixture;
using FluentAssertions;
using Peent.Common;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Account;

namespace Peent.UnitTests.Domain.Entities.Account
{
    public class Account_SetCurrency_Tests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void when_currency_id_is_not_positive__throws_argument_exception(int newCurrencyId)
        {
            var account = F.Create<Sut>();

            Action act = () => account.SetCurrency(newCurrencyId);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{nameof(Sut.CurrencyId).FirstDown()}*");
        }

        [Fact]
        public void when_currency_id_is_positive__does_not_throw()
        {
            var account = F.Create<Sut>();
            var newCurrencyId = F.Create<int>();

            account.SetCurrency(newCurrencyId);

            account.CurrencyId.Should().Be(newCurrencyId);
        }
    }
}
