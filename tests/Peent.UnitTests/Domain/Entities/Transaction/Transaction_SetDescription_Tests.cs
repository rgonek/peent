using AutoFixture;
using FluentAssertions;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Transaction;

namespace Peent.UnitTests.Domain.Entities.Transaction
{
    public class Transaction_SetDescription_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_description_is_null_or_white_space__does_not_throw(string newDescription)
        {
            var transaction = F.Create<Sut>();

            transaction.SetDescription(newDescription);

            transaction.Description.Should().Be(newDescription);
        }

        [Fact]
        public void when_description_is_not_null_or_white_space__does_not_throw()
        {
            var transaction = F.Create<Sut>();
            var newDescription = F.Create<string>();

            transaction.SetDescription(newDescription);

            transaction.Description.Should().Be(newDescription);
        }
    }
}
