using Peent.Domain.ValueObjects;

namespace Peent.Domain.Entities
{
    public class Workspace : IHaveAuditInfo
    {
        public int Id { get; set; }

        public CreationInfo CreationInfo { get; set; }
        public ModificationInfo ModificationInfo { get; set; }
        public DeletionInfo DeletionInfo { get; set; }
    }
}
