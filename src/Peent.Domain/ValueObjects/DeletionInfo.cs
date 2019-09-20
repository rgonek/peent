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

        public DeletionInfo(ApplicationUser deletedBy)
        {
            DeletionDate = Clock.UtcNow;
            DeletedBy = deletedBy;
        }

        public DeletionInfo(string deletedById)
        {
            DeletionDate = Clock.UtcNow;
            DeletedById = deletedById;
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
