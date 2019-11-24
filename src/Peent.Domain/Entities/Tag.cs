using System;
using Peent.Domain.ValueObjects;

namespace Peent.Domain.Entities
{
    public class Tag : IHaveAuditInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }

        public CreationInfo CreationInfo { get; set; }
        public ModificationInfo ModificationInfo { get; set; }
        public DeletionInfo DeletionInfo { get; set; }
    }
}
