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
            var transactionId = F.Create<long>();
            var customizer = new FixedConstructorParameter<long>(
                transactionId, nameof(Sut.TransactionId).FirstDown());

            var transactionTag = Create<Sut>(customizer);

            transactionTag.TransactionId.Should().Be(transactionId);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void when_tag_id_is_not_positive__throws_argument_exception(int tagId)
        {
            var parameterName = nameof(Sut.TagId).FirstDown();
            var customizer = new FixedConstructorParameter<int>(
                tagId, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_tag_id_is_positive__does_not_throw()
        {
            var tagId = F.Create<int>();
            var customizer = new FixedConstructorParameter<int>(
                tagId, nameof(Sut.TagId).FirstDown());

            var transactionTag = Create<Sut>(customizer);

            transactionTag.TagId.Should().Be(tagId);
        }
    }
}
