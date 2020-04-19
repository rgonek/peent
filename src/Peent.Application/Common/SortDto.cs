using EnsureThat;

namespace Peent.Application.Common
{
    public class SortDto
    {
        public string Field { get; }
        public SortDirection Direction { get; }

        public SortDto(string field, SortDirection direction = SortDirection.Asc)
        {
            Ensure.That(field, nameof(field)).IsNotNullOrWhiteSpace();

            Field = field;
            Direction = direction;
        }
    }
}
