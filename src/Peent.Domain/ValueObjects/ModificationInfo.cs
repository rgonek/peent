using System;
using System.Collections.Generic;
using Peent.Common.Time;
using Peent.Domain.Entities;
using Peent.Domain.Infrastructure;

namespace Peent.Domain.ValueObjects
{
    public class ModificationInfo : ValueObject
    {
        protected ModificationInfo() { }

        public ModificationInfo(ApplicationUser modifiedBy)
        {
            LastModificationDate = Clock.UtcNow;
            LastModifiedBy = modifiedBy;
        }

        public ModificationInfo(string modifiedById)
        {
            LastModificationDate = Clock.UtcNow;
            LastModifiedById = modifiedById;
        }

        public virtual DateTime? LastModificationDate { get; protected internal set; }
        public virtual string LastModifiedById { get; protected set; }
        public virtual ApplicationUser LastModifiedBy { get; protected internal set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return LastModificationDate;
            yield return LastModifiedBy;
        }
    }
}
