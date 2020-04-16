using System;
using AutoFixture;
using FluentAssertions;
using Peent.Common;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Tag;

namespace Peent.UnitTests.Domain.Entities.Tag
{
    public class Tag_SetName_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_name_is_null_or_white_space__throws_argument_exception(string newName)
        {
            var tag = F.Create<Sut>();

            Action act = () => tag.SetName(newName);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{nameof(Sut.Name).FirstDown()}*");
        }

        [Fact]
        public void when_name_is_not_null_or_white_space__does_not_throw()
        {
            var tag = F.Create<Sut>();
            var newName = F.Create<string>();

            tag.SetName(newName);

            tag.Name.Should().Be(newName);
        }
    }
}
