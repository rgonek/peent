using Peent.Domain.Entities;
using Peent.Domain.ValueObjects;

namespace Peent.Domain.Common
{
    public interface IHaveAuditInfo
    {
        AuditInfo Created { get; }
        AuditInfo LastModified { get; }

        void SetCreatedBy(ApplicationUser user);
        void SetModifiedBy(ApplicationUser user);
    }
}
