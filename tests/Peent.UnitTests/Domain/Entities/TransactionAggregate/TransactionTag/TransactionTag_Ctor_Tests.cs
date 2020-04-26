using System;
using AutoFixture;
using FluentAssertions;
using Peent.Common;
using Peent.CommonTests.AutoFixture;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.TransactionAggregate.TransactionTag;

namespace Peent.UnitTests.Domain.Entities.TransactionAggregate.TransactionTag
{
    public class TransactionTag_Ctor_Tests
    {
        [Fact]
        public void when_transaction_is_null__throws_argument_exception()
        {
            var parameterName = nameof(Sut.Transaction).FirstDown();
            var customizer = new FixedConstructorParameter<Peent.Domain.Entities.TransactionAggregate.Transaction>(
                null, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_transaction_is_not_null__does_not_throw()
        {
            var transaction = F.Create<Peent.Domain.Entities.TransactionAggregate.Transaction>();
            var customizer = new FixedConstructorParameter<Peent.Domain.Entities.TransactionAggregate.Transaction>(
                transaction, nameof(Sut.Transaction).FirstDown());

            var transactionTag = Create<Sut>(customizer);

            transactionTag.Transaction.Should().Be(transaction);
        }

        [Fact]
        public void when_tag_is_null__throws_argument_exception()
        {
            var parameterName = nameof(Sut.Tag).FirstDown();
            var customizer = new FixedConstructorParameter<Peent.Domain.Entities.Tag>(
                null, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_tag_is_not_null__does_not_throw()
        {
            var tag = F.Create<Peent.Domain.Entities.Tag>();
            var customizer = new FixedConstructorParameter<Peent.Domain.Entities.Tag>(
                tag, nameof(Sut.Tag).FirstDown());

            var transactionTag = Create<Sut>(customizer);

            transactionTag.Tag.Should().Be(tag);
        }
    }
}
