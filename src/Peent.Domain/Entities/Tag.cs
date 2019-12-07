using System;
using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class Tag : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }
    }
}
