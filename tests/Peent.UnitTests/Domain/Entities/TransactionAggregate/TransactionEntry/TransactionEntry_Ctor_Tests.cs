﻿using System;
using AutoFixture;
using FluentAssertions;
using Peent.Common;
using Peent.CommonTests.AutoFixture;
using Peent.Domain.ValueObjects;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.TransactionAggregate.TransactionEntry;

namespace Peent.UnitTests.Domain.Entities.TransactionAggregate.TransactionEntry
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
        public void when_money_is_null__throws_argument_exception()
        {
            var parameterName = nameof(Sut.Money).FirstDown();
            var customizer = new FixedConstructorParameter<Money>(
                null, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_money_is_not_null__does_not_throw()
        {
            var money = F.Create<Money>();
            var customizer = new FixedConstructorParameter<Money>(
                money, nameof(Sut.Money).FirstDown());

            var transactionEntry = Create<Sut>(customizer);

            transactionEntry.Money.Should().Be(money);
        }

        [Fact]
        public void when_transaction_is_null__throws_argument_exception()
        {
            Peent.Domain.Entities.TransactionAggregate.Transaction transaction = null;

            Action act = () => CreateTransactionEntry(transaction);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{nameof(Sut.Transaction).FirstDown()}*");
        }

        [Fact]
        public void when_transaction_is_not_null__does_not_throw()
        {
            var transaction = F.Create<Peent.Domain.Entities.TransactionAggregate.Transaction>();

            var transactionEntry = CreateTransactionEntry(transaction);

            transactionEntry.Transaction.Should().Be(transaction);
        }

        private static Sut CreateTransactionEntry(Peent.Domain.Entities.TransactionAggregate.Transaction transaction)
        {
            return new Sut(
                transaction,
                F.Create<Peent.Domain.Entities.Account>(),
                F.Create<Money>());
        }
    }
}
