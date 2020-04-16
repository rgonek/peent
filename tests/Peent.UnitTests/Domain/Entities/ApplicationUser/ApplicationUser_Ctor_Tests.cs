using System;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using Peent.Common;
using Peent.CommonTests.AutoFixture;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.ApplicationUser;

namespace Peent.UnitTests.Domain.Entities.ApplicationUser
{
    public class ApplicationUser_Ctor_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_first_name_is_null_or_white_space__throws_argument_exception(string firstName)
        {
            var parameterName = nameof(Sut.FirstName).FirstDown();
            var customizer = new FixedConstructorParameter<string>(
                firstName, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_name_is_not_null_or_white_space__does_not_throw()
        {
            var firstName = F.Create<string>();
            var customizer = new FixedConstructorParameter<string>(
                firstName, nameof(Sut.FirstName).FirstDown());

            var account = Create<Sut>(customizer);

            account.FirstName.Should().Be(firstName);
        }
    }
}
