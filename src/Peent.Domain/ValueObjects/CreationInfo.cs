using System;
using System.Collections.Generic;
using Peent.Common.Time;
using Peent.Domain.Entities;
using Peent.Domain.Infrastructure;

namespace Peent.Domain.ValueObjects
{
    public class CreationInfo : ValueObject
    {
        protected CreationInfo() { }

        public CreationInfo(ApplicationUser createdBy)
        {
            CreationDate = Clock.UtcNow;
            CreatedBy = createdBy;
        }

        public virtual DateTime CreationDate { get; protected set; }
        public virtual string CreatedById { get; protected set; }
        public virtual ApplicationUser CreatedBy { get; protected set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CreationDate;
            yield return CreatedBy;
        }
    }
}
