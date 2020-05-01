using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class Workspace : AuditableEntity<int>
    {
        public Workspace()
        {
        }
        
        private Workspace(int id)
            : base(id)
        {
        }

        public static Workspace FromId(int id)
        {
            return new Workspace(id);
        }
    }
}