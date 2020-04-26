using System;
using System.Collections.Generic;
using Peent.Common.Time;
using Peent.Domain.Common;
using Peent.Domain.Entities;

namespace Peent.Domain.ValueObjects
{
    public class ModificationInfo : ValueObject
    {
        private ModificationInfo() { }

        public static ModificationInfo For(string userId)
        {
            return new ModificationInfo
            {
                LastModifiedById = userId,
                LastModificationDate = Clock.UtcNow
            };
        }

        public virtual DateTime? LastModificationDate { get; private set; }
        public virtual string LastModifiedById { get; private set; }
        //public virtual ApplicationUser LastModifiedBy { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return LastModificationDate;
            yield return LastModifiedById;
        }
    }
}
