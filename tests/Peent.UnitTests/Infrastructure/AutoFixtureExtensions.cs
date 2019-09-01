using AutoFixture;

namespace Peent.UnitTests.Infrastructure
{
    public static  class AutoFixtureExtensions
    {
        public  static string CreateString(this Fixture fixture, int stringLength)
        {
            return string.Join(string.Empty, fixture.CreateMany<char>(stringLength));
        }
    }
}
