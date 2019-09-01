using System;
using System.Collections.Generic;
using Peent.Common.Time;
using Peent.Domain.Entities;
using Peent.Domain.Infrastructure;

namespace Peent.Domain.ValueObjects
{
    public class DeletionInfo : ValueObject
    {
        protected DeletionInfo() { }

        public DeletionInfo(ApplicationUser lastModifiedBy)
        {
            DeletionDate = Clock.UtcNow;
            DeletedBy = lastModifiedBy;
        }

        public DeletionInfo(string lastModifiedById)
        {
            DeletionDate = Clock.UtcNow;
            DeletedById = lastModifiedById;
        }

        public virtual DateTime? DeletionDate { get; protected internal set; }
        public virtual string DeletedById { get; protected set; }
        public virtual ApplicationUser DeletedBy { get; protected internal set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return DeletionDate;
            yield return DeletedBy;
        }
    }
}
