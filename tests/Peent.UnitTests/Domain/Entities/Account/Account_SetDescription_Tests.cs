using AutoFixture;
using FluentAssertions;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Account;

namespace Peent.UnitTests.Domain.Entities.Account
{
    public class Account_SetDescription_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_description_is_null_or_white_space__does_not_throw(string newDescription)
        {
            var account = F.Create<Sut>();

            account.SetDescription(newDescription);

            account.Description.Should().Be(newDescription);
        }

        [Fact]
        public void when_description_is_not_null_or_white_space__does_not_throw()
        {
            var account = F.Create<Sut>();
            var newDescription = F.Create<string>();

            account.SetDescription(newDescription);

            account.Description.Should().Be(newDescription);
        }
    }
}
