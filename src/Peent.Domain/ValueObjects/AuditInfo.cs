using System;
using System.Collections.Generic;
using Peent.Common.Time;
using Peent.Domain.Common;
using Peent.Domain.Entities;

namespace Peent.Domain.ValueObjects
{
    public class AuditInfo : ValueObject
    {
        private AuditInfo() { }

        public static AuditInfo For(ApplicationUser user)
        {
            return new AuditInfo
            {
                By = user,
                On = Clock.UtcNow
            };
        }

        public virtual DateTime On { get; private set; }
        public virtual ApplicationUser By { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return On;
            yield return By;
        }
    }
}
