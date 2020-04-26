using Peent.Domain.ValueObjects;

namespace Peent.Domain.Common
{
    public interface IHaveAuditInfo
    {
        public AuditInfo Created { get; }
        public AuditInfo LastModified { get; }
    }
}
