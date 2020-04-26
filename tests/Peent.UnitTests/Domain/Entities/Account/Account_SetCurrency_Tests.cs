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

        [Fact]
        public void when_currency_is_null__throws_argument_exception()
        {
            var account = F.Create<Sut>();

            Action act = () => account.SetCurrency(null);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{nameof(Sut.Currency).FirstDown()}*");
        }

        [Fact]
        public void when_currency_is_not_null__does_not_throw()
        {
            var account = F.Create<Sut>();
            var newCurrency = F.Create<Peent.Domain.Entities.Currency>();

            account.SetCurrency(newCurrency);

            account.Currency.Should().Be(newCurrency);
        }
    }
}
