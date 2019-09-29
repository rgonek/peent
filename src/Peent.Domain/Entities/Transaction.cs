using System;
using System.Collections.Generic;

namespace Peent.Domain.Entities
{
    public class Transaction : AuditInfoOwner
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

        public void MarkAsDeleted(string deletedById)
        {
            SetDeletedBy(deletedById);
            foreach (var entry in Entries)
            {
                entry.SetDeletedBy(deletedById);
            }
        }

        public void MarkAsModified(string modifiedById)
        {
            SetModifiedBy(modifiedById);
            foreach (var entry in Entries)
            {
                entry.SetModifiedBy(modifiedById);
            }
        }
    }
}
