using System;
using Peent.Common.Time;
using Peent.Domain.Entities;
using Peent.Domain.ValueObjects;

namespace Peent.Domain.Common
{
    public class AuditableEntity<T> : Entity<T>, IHaveAuditInfo
    {
        public AuditInfo Created { get; private set; }
        public AuditInfo LastModified { get; private set; }

        public void SetCreatedBy(ApplicationUser createdBy)
        {
            if (Created != null)
            {
                //throw
            }
            Created = AuditInfo.For(createdBy);
        }

        public void SetModifiedBy(ApplicationUser modifiedBy)
        {
            if (LastModified == null)
            {
                //throw
            }
            LastModified = AuditInfo.For(modifiedBy);
        }
    }
}
