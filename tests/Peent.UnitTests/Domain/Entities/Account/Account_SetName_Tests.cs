using System;
using AutoFixture;
using FluentAssertions;
using Peent.Common;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Account;

namespace Peent.UnitTests.Domain.Entities.Account
{
    public class Account_SetName_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_name_is_null_or_white_space__throws_argument_exception(string name)
        {
            var account = F.Create<Sut>();

            Action act = () => account.SetName(name);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{nameof(Sut.Name).FirstDown()}*");
        }

        [Fact]
        public void when_name_is_not_null_or_white_space__does_not_throw()
        {
            var account = F.Create<Sut>();
            var newName = F.Create<string>();

            account.SetName(newName);

            account.Name.Should().Be(newName);
        }
    }
}
