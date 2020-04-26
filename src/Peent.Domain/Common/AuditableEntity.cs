using System;
using Peent.Common.Time;
using Peent.Domain.Entities;
using Peent.Domain.ValueObjects;

namespace Peent.Domain.Common
{
    public class AuditableEntity : IHaveAuditInfo
    {
        public CreationInfo CreationInfo { get; private set; }
        public ModificationInfo LastModificationInfo { get; private set; }

        public void SetCreatedBy(string createdById)
        {
            if (CreationInfo != null)
            {
                //throw
            }
            CreationInfo = CreationInfo.For(createdById);
        }

        public void SetModifiedBy(string modifiedById)
        {
            if (CreationInfo == null)
            {
                //throw
            }
            LastModificationInfo = ModificationInfo.For(modifiedById);
        }
    }
}
