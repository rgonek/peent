using System;
using System.Collections.Generic;
using Peent.Common.Time;
using Peent.Domain.Common;
using Peent.Domain.Entities;

namespace Peent.Domain.ValueObjects
{
    public class AuditInfo : ValueObject
    {
        public DateTime On { get; private set; }
        public ApplicationUser By { get; private set; }
        
        private AuditInfo() { }

        public static AuditInfo For(ApplicationUser user)
        {
            return new AuditInfo
            {
                By = user,
                On = Clock.UtcNow
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return On;
            yield return By;
        }
    }
}
