using Peent.Domain.ValueObjects;

namespace Peent.Domain.Entities
{
    public interface IHaveAuditInfo
    {
        CreationInfo CreationInfo { get; set; }
        ModificationInfo ModificationInfo { get; set; }
        DeletionInfo DeletionInfo { get; set; }
    }
}
