using System;
using AutoFixture;
using FluentAssertions;
using Peent.Common;
using Peent.CommonTests.AutoFixture;
using Peent.Domain.Entities;
using Peent.Domain.ValueObjects;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;

namespace Peent.UnitTests.Domain.ValueObjects
{
    public class Money_Ctor_Tests
    {
        [Fact]
        public void when_amount_is_zero__throws_argument_exception()
        {            
            var parameterName = nameof(Money.Amount).FirstDown();
            var customizer = new FixedConstructorParameter<decimal>(
                0, parameterName);

            Action act = () => Create<Money>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        public void when_amount_is_not_zero__does_not_throw(decimal amount)
        {
            var customizer = new FixedConstructorParameter<decimal>(
                amount, nameof(Money.Amount).FirstDown());

            var money = Create<Money>(customizer);

            money.Amount.Should().Be(amount);
        }
        
        [Fact]
        public void when_currency_is_null__throws_argument_exception()
        {
            var parameterName = nameof(Money.Currency).FirstDown();
            var customizer = new FixedConstructorParameter<Currency>(null, parameterName);

            Action act = () => Create<Money>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_currency_is_not_null__does_not_throw()
        {
            var currency = F.Create<Currency>();
            var customizer = new FixedConstructorParameter<Currency>(
                currency, nameof(Money.Currency).FirstDown());

            var account = Create<Money>(customizer);

            account.Currency.Should().Be(currency);
        }
    }
}
