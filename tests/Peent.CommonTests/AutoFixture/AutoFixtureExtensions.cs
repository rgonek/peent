using AutoFixture;

namespace Peent.CommonTests.AutoFixture
{
    public static class AutoFixtureExtensions
    {
        public static string CreateString(this Fixture fixture, int stringLength)
        {
            return string.Join(string.Empty, fixture.CreateMany<char>(stringLength));
        }

        public static IFixture ConstructorArgumentFor<TTargetType, TValueType>(
            this IFixture fixture,
            string paramName,
            TValueType value)
        {
            fixture.Customizations.Add(
                new ConstructorArgumentRelay<TTargetType, TValueType>(paramName, value)
            );
            return fixture;
        }
    }
}
