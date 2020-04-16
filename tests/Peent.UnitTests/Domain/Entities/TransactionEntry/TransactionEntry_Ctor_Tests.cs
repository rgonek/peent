using System;
using AutoFixture;
using FluentAssertions;
using Peent.Common;
using Peent.CommonTests.AutoFixture;
using Peent.Domain.Entities;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.TransactionEntry;

namespace Peent.UnitTests.Domain.Entities.TransactionEntry
{
    public class TransactionEntry_Ctor_Tests
    {
        [Fact]
        public void when_account_is_null__throws_argument_exception()
        {
            Peent.Domain.Entities.Account account = null;
            var parameterName = nameof(Sut.Account).FirstDown();
            var customizer = new FixedConstructorParameter<Peent.Domain.Entities.Account>(
                account, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_account_is_not_null__does_not_throw()
        {
            var account = F.Create<Peent.Domain.Entities.Account>();
            var customizer = new FixedConstructorParameter<Peent.Domain.Entities.Account>(
                account, nameof(Sut.Account).FirstDown());

            var transactionEntry = Create<Sut>(customizer);

            transactionEntry.Account.Should().Be(account);
        }

        [Fact]
        public void when_amount_is_zero__throws_argument_exception()
        {
            var amount = 0m;
            var parameterName = nameof(Sut.Amount).FirstDown();
            var customizer = new FixedConstructorParameter<decimal>(
                amount, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_amount_is_negative__does_not_throw()
        {
            var amount = -F.Create<decimal>();
            var customizer = new FixedConstructorParameter<decimal>(
                amount, nameof(Sut.Amount).FirstDown());

            var transactionEntry = Create<Sut>(customizer);

            transactionEntry.Amount.Should().Be(amount);
        }

        [Fact]
        public void when_amount_is_positive__does_not_throw()
        {
            var amount = F.Create<decimal>();
            var customizer = new FixedConstructorParameter<decimal>(
                amount, nameof(Sut.Amount).FirstDown());

            var transactionEntry = Create<Sut>(customizer);

            transactionEntry.Amount.Should().Be(amount);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void when_currency_id_is_not_positive__throws_argument_exception(int currencyId)
        {
            var parameterName = nameof(Sut.CurrencyId).FirstDown();
            var customizer = new FixedConstructorParameter<int>(
                currencyId, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_currency_id_is_positive__does_not_throw()
        {
            var currencyId = F.Create<int>();
            var customizer = new FixedConstructorParameter<int>(
                currencyId, nameof(Sut.CurrencyId).FirstDown());

            var transactionEntry = Create<Sut>(customizer);

            transactionEntry.CurrencyId.Should().Be(currencyId);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void when_transaction_id_is_not_positive__throws_argument_exception(long transactionId)
        {
            var parameterName = nameof(Sut.TransactionId).FirstDown();
            var customizer = new FixedConstructorParameter<long>(
                transactionId, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_transaction_id_is_positive__does_not_throw()
        {
            var transactionId = F.Create<int>();
            var customizer = new FixedConstructorParameter<long>(
                transactionId, nameof(Sut.TransactionId).FirstDown());

            var transactionEntry = Create<Sut>(customizer);

            transactionEntry.TransactionId.Should().Be(transactionId);
        }

        [Fact]
        public void when_transaction_is_null__throws_argument_exception()
        {
            Peent.Domain.Entities.Transaction transaction = null;

            Action act = () => CreateTransactionEntry(transaction);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{nameof(Sut.Transaction).FirstDown()}*");
        }

        [Fact]
        public void when_transaction_is_not_null__does_not_throw()
        {
            var transaction = F.Create<Peent.Domain.Entities.Transaction>();

            var transactionEntry = CreateTransactionEntry(transaction);

            transactionEntry.Transaction.Should().Be(transaction);
        }

        private static Sut CreateTransactionEntry(Peent.Domain.Entities.Transaction transaction)
        {
            return new Sut(
                transaction,
                F.Create<Peent.Domain.Entities.Account>(),
                F.Create<decimal>(),
                F.Create<int>());
        }
    }
}
