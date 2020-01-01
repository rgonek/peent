using System;
using Peent.Domain.Entities;

namespace Peent.Domain.Common
{
    public interface IHaveAuditInfo
    {
        DateTime CreationDate { get; set; }
        string CreatedById { get; set; }
        ApplicationUser CreatedBy { get; set; }

        DateTime? LastModificationDate { get; set; }
        string LastModifiedById { get; set; }
        ApplicationUser LastModifiedBy { get; set; }
    }
}
