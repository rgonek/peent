using System;
using Peent.Domain.Entities;

namespace Peent.Domain.Common
{
    public class AuditableEntity : IHaveAuditInfo
    {
        public DateTime CreationDate { get; set; }
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public string LastModifiedById { get; set; }
        public ApplicationUser LastModifiedBy { get; set; }
        public DateTime? DeletionDate { get; set; }
        public string DeletedById { get; set; }
        public ApplicationUser DeletedBy { get; set; }
    }
}
