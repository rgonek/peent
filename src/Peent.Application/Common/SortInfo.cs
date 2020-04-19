using EnsureThat;

namespace Peent.Application.Common
{
    public class SortInfo
    {
        public string Field { get; }
        public SortDirection Direction { get; }

        public SortInfo(string field, SortDirection direction = SortDirection.Asc)
        {
            Ensure.That(field, nameof(field)).IsNotNullOrWhiteSpace();

            Field = field;
            Direction = direction;
        }
    }
}
