using System;
using System.Collections.Generic;
using Peent.Common.Time;
using Peent.Domain.Common;
using Peent.Domain.Entities;

namespace Peent.Domain.ValueObjects
{
    public class CreationInfo : ValueObject
    {
        private CreationInfo() { }

        public static CreationInfo For(string userId)
        {
            return new CreationInfo
            {
                CreatedById = userId,
                CreationDate = Clock.UtcNow
            };
        }

        public virtual DateTime CreationDate { get; private set; }
        public virtual string CreatedById { get; private set; }
        //public virtual ApplicationUser CreatedBy { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CreationDate;
            yield return CreatedById;
        }
    }
}
