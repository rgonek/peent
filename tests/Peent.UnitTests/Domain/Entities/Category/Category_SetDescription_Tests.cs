using AutoFixture;
using FluentAssertions;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Category;

namespace Peent.UnitTests.Domain.Entities.Category
{
    public class Category_SetDescription_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_description_is_null_or_white_space__does_not_throw(string newDescription)
        {
            var category = F.Create<Sut>();

            category.SetDescription(newDescription);

            category.Description.Should().Be(newDescription);
        }

        [Fact]
        public void when_description_is_not_null_or_white_space__does_not_throw()
        {
            var category = F.Create<Sut>();
            var newDescription = F.Create<string>();

            category.SetDescription(newDescription);

            category.Description.Should().Be(newDescription);
        }
    }
}
