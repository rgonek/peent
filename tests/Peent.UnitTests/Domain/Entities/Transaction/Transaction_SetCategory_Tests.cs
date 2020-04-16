using System;
using AutoFixture;
using FluentAssertions;
using Peent.Common;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Transaction;

namespace Peent.UnitTests.Domain.Entities.Transaction
{
    public class Transaction_SetCategory_Tests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void when_category_id_is_not_positive__throws_argument_exception(int newCategoryId)
        {
            var transaction = F.Create<Sut>();

            Action act = () => transaction.SetCategory(newCategoryId);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{nameof(Sut.CategoryId).FirstDown()}*");
        }

        [Fact]
        public void when_category_id_is_positive__does_not_throw()
        {
            var transaction = F.Create<Sut>();
            var newCategoryId = F.Create<int>();

            transaction.SetCategory(newCategoryId);

            transaction.CategoryId.Should().Be(newCategoryId);
        }
    }
}
