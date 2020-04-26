using Peent.Domain.ValueObjects;

namespace Peent.Domain.Common
{
    public interface IHaveAuditInfo
    {
        public CreationInfo CreationInfo { get; }
        public ModificationInfo LastModificationInfo { get; }
    }
}
