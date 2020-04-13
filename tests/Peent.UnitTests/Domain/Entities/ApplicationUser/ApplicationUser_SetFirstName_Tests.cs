using System;
using AutoFixture;
using FluentAssertions;
using Peent.Common;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.ApplicationUser;

namespace Peent.UnitTests.Domain.Entities.ApplicationUser
{
    public class ApplicationUser_SetFirstName_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_name_is_null_or_white_space__throws_argument_exception(string newFirstName)
        {
            var user = F.Create<Sut>();

            Action act = () => user.SetFirstName(newFirstName);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{nameof(Sut.FirstName).FirstDown()}*");
        }

        [Fact]
        public void when_name_is_not_null_or_white_space__does_not_throw()
        {
            var user = F.Create<Sut>();
            var newFirstName = F.Create<string>();

            user.SetFirstName(newFirstName);

            user.FirstName.Should().Be(newFirstName);
        }
    }
}
