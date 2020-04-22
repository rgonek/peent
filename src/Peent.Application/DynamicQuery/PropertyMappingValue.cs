using EnsureThat;

namespace Peent.Application.DynamicQuery
{
    public class PropertyMappingValue
    {
        public string DestinationProperty { get; }
        public bool RevertSort { get; }

        public PropertyMappingValue(string destinationProperty, bool revertSort = false)
        {
            Ensure.That(destinationProperty, nameof(destinationProperty)).IsNotNullOrWhiteSpace();

            DestinationProperty = destinationProperty;
            RevertSort = revertSort;
        }
    }
}
