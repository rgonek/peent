using System;
using AutoFixture;
using FluentAssertions;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Transaction;

namespace Peent.UnitTests.Domain.Entities.Transaction
{
    public class Transaction_SetDate_Tests
    {
        [Fact]
        public void when_date_is_passed__set_it()
        {
            var transaction = F.Create<Sut>();
            var newDate = F.Create<DateTime>();

            transaction.SetDate(newDate);

            transaction.Date.Should().Be(newDate);
        }
    }
}
