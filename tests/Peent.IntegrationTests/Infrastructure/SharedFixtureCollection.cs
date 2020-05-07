using Xunit;

namespace Peent.IntegrationTests.Infrastructure
{
    [CollectionDefinition(nameof(SharedFixture))]
    public class SharedFixtureCollection  : ICollectionFixture<SharedFixture>
    {
    }
}