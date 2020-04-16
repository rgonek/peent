using System;
using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Tag;

namespace Peent.UnitTests.Domain.Entities.Tag
{
    public class Tag_SetDate_Tests
    {
        [Fact]
        public void when_date_is_null__does_not_throw()
        {
            var tag = F.Create<Sut>();
            DateTime? newDate = null;

            tag.SetDate(newDate);

            tag.Date.Should().Be(newDate);
        }

        [Fact]
        public void when_date_is_not_null__does_not_throw()
        {
            var tag = F.Create<Sut>();
            var newDate = F.Create<DateTime?>();

            tag.SetDate(newDate);

            tag.Date.Should().Be(newDate);
        }
    }
}
