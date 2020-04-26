using System;
using AutoFixture;
using FluentAssertions;
using Peent.Common;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.TransactionAggregate.Transaction;

namespace Peent.UnitTests.Domain.Entities.TransactionAggregate.Transaction
{
    public class Transaction_SetCategory_Tests
    {
        [Fact]
        public void when_category_is_null__throws_argument_exception()
        {
            var transaction = F.Create<Sut>();

            Action act = () => transaction.SetCategory(null);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{nameof(Sut.Category).FirstDown()}*");
        }

        [Fact]
        public void when_category_is_not_null__does_not_throw()
        {
            var transaction = F.Create<Sut>();
            var newCategory = F.Create<Peent.Domain.Entities.Category>();

            transaction.SetCategory(newCategory);

            transaction.Category.Should().Be(newCategory);
        }
    }
}
