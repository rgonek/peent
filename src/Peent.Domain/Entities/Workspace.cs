using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class Workspace : AuditableEntity
    {
        public int Id { get; private set; }

        public Workspace()
        {
        }
    }
}
