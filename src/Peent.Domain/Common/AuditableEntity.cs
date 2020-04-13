using System;
using Peent.Common.Time;
using Peent.Domain.Entities;

namespace Peent.Domain.Common
{
    public class AuditableEntity : IHaveAuditInfo
    {
        public DateTime CreationDate { get; private set; }
        public string CreatedById { get; private set; }
        public ApplicationUser CreatedBy { get; private set; }

        public DateTime? LastModificationDate { get; private set; }
        public string LastModifiedById { get; private set; }
        public ApplicationUser LastModifiedBy { get; private set; }

        public void SetCreatedBy(string createdById)
        {
            CreatedById = createdById;
            CreationDate = Clock.UtcNow;
        }

        public void SetModifiedBy(string modifiedById)
        {
            LastModifiedById = modifiedById;
            LastModificationDate = Clock.UtcNow;
        }
    }
}
