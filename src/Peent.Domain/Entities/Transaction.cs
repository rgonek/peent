using System;
using System.Collections.Generic;
using Peent.Domain.ValueObjects;

namespace Peent.Domain.Entities
{
    public class Transaction : IHaveAuditInfo
    {
        public Transaction()
        {
            Tags = new HashSet<Tag>();
            Entries = new HashSet<TransactionEntry>();
        }

        public long Id { get; set; }

        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Tag> Tags { get; }
        public ICollection<TransactionEntry> Entries { get; }
        public TransactionType Type { get; set; }

        public CreationInfo CreationInfo { get; set; }
        public ModificationInfo ModificationInfo { get; set; }
        public DeletionInfo DeletionInfo { get; set; }

        public void MarkAsDeleted(string deletedById)
        {
            DeletionInfo = new DeletionInfo(deletedById);
            foreach (var entry in Entries)
            {
                entry.DeletionInfo = new DeletionInfo(deletedById);
            }
        }

        public void MarkAsModified(string modifiedById)
        {
            ModificationInfo = new ModificationInfo(modifiedById);
            foreach (var entry in Entries)
            {
                entry.ModificationInfo = new ModificationInfo(modifiedById);
            }
        }
    }
}
