using System;
using AutoFixture;
using FluentAssertions;
using Peent.Common;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Transaction;

namespace Peent.UnitTests.Domain.Entities.Transaction
{
    public class Transaction_SetTitle_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_title_is_null_or_white_space__throws_argument_exception(string newTitle)
        {
            var transaction = F.Create<Sut>();

            Action act = () => transaction.SetTitle(newTitle);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{nameof(Sut.Title).FirstDown()}*");
        }

        [Fact]
        public void when_title_is_not_null_or_white_space__does_not_throw()
        {
            var transaction = F.Create<Sut>();
            var newTitle = F.Create<string>();

            transaction.SetTitle(newTitle);

            transaction.Title.Should().Be(newTitle);
        }
    }
}
