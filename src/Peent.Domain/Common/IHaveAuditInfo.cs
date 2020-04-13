using System;
using Peent.Domain.Entities;

namespace Peent.Domain.Common
{
    public interface IHaveAuditInfo
    {
        DateTime CreationDate { get; }
        string CreatedById { get; }
        ApplicationUser CreatedBy { get; }

        DateTime? LastModificationDate { get; }
        string LastModifiedById { get; }
        ApplicationUser LastModifiedBy { get; }
    }
}
