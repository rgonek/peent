using AutoFixture;
using FluentAssertions;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Tag;

namespace Peent.UnitTests.Domain.Entities.Tag
{
    public class Tag_SetDescription_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_description_is_null_or_white_space__does_not_throw(string newDescription)
        {
            var tag = F.Create<Sut>();

            tag.SetDescription(newDescription);

            tag.Description.Should().Be(newDescription);
        }

        [Fact]
        public void when_description_is_not_null_or_white_space__does_not_throw()
        {
            var tag = F.Create<Sut>();
            var newDescription = F.Create<string>();

            tag.SetDescription(newDescription);

            tag.Description.Should().Be(newDescription);
        }
    }
}
