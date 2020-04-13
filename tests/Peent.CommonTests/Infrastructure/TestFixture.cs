using AutoFixture;
using AutoFixture.Kernel;
using Microsoft.EntityFrameworkCore.Internal;
using Peent.CommonTests.AutoFixture;

namespace Peent.CommonTests.Infrastructure
{
    public class TestFixture
    {
        public static readonly Fixture F = new Fixture();

        static TestFixture()
        {
            F.Configure();
        }

        public static IFixture Fixture(params ISpecimenBuilder[] specimenBuilders)
        {
            var fixture = new Fixture();
            fixture.Configure();

            if (specimenBuilders != null && specimenBuilders.Any())
            {
                foreach (var specimenBuilder in specimenBuilders)
                {
                    fixture.Customizations.Add(specimenBuilder);
                }
            }

            return fixture;
        }
    }
}
