using System;
using AutoFixture;
using FluentAssertions;
using Peent.Common;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Category;

namespace Peent.UnitTests.Domain.Entities.Category
{
    public class Category_SetName_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_name_is_null_or_white_space__throws_argument_exception(string newName)
        {
            var category = F.Create<Sut>();

            Action act = () => category.SetName(newName);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{nameof(Sut.Name).FirstDown()}*");
        }

        [Fact]
        public void when_name_is_not_null_or_white_space__does_not_throw()
        {
            var category = F.Create<Sut>();
            var newName = F.Create<string>();

            category.SetName(newName);

            category.Name.Should().Be(newName);
        }
    }
}
