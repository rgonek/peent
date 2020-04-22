using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class Workspace : AuditableEntity, IEntity<int>
    {
        public int Id { get; private set; }
    }
}
