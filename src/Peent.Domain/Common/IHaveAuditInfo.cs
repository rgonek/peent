using Peent.Domain.ValueObjects;

namespace Peent.Domain.Common
{
    public interface IHaveAuditInfo
    {
        AuditInfo Created { get; }
        AuditInfo LastModified { get; }
    }
}
