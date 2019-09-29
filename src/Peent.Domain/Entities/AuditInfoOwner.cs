using System;
using Peent.Common.Time;

namespace Peent.Domain.Entities
{
    public abstract class AuditInfoOwner
    {
        public DateTime CreationDate { get; private set; }
        public string CreatedById { get; private set; }
        public ApplicationUser CreatedBy { get; private set; }

        public void SetCreatedBy(ApplicationUser createdBy)
        {
            CreationDate = Clock.UtcNow;
            CreatedBy = createdBy;
        }

        public void SetCreatedBy(string createdById)
        {
            CreationDate = Clock.UtcNow;
            CreatedById = createdById;
        }

        public DateTime? LastModificationDate { get; private set; }
        public string LastModifiedById { get; private set; }
        public ApplicationUser LastModifiedBy { get; private set; }

        public void SetModifiedBy(ApplicationUser modifiedBy)
        {
            LastModificationDate = Clock.UtcNow;
            LastModifiedBy = modifiedBy;
        }

        public void SetModifiedBy(string modifiedById)
        {
            LastModificationDate = Clock.UtcNow;
            LastModifiedById = modifiedById;
        }

        public DateTime? DeletionDate { get; private set; }
        public string DeletedById { get; private set; }
        public ApplicationUser DeletedBy { get; private set; }

        public void SetDeletedBy(ApplicationUser deletedBy)
        {
            DeletionDate = Clock.UtcNow;
            DeletedBy = deletedBy;
        }

        public void SetDeletedBy(string deletedById)
        {
            DeletionDate = Clock.UtcNow;
            DeletedById = deletedById;
        }
    }
}
