using System;
using System.Collections.Generic;
using EnsureThat;
using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class Tag : AuditableEntity
    {
        public int Id { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime? Date { get; private set; }
        public int WorkspaceId { get; private set; }
        public Workspace Workspace { get; private set; }

        private readonly List<TransactionTag> _transactionTags;
        public IReadOnlyCollection<TransactionTag> TransactionTags => _transactionTags.AsReadOnly();

        #region Ctors
        private Tag() { }

        public Tag(string name, int workspaceId)
            : this(name, null, null, workspaceId)
        {
        }

        public Tag(string name, string description, int workspaceId)
            : this(name, description, null, workspaceId)
        {
        }

        public Tag(string name, DateTime? date, int workspaceId)
            : this(name, null, date, workspaceId)
        {
        }

        #endregion

        public Tag(string name, string description, DateTime? date, int workspaceId)
        {
            Ensure.That(name, nameof(name)).IsNotNullOrWhiteSpace();
            Ensure.That(workspaceId, nameof(workspaceId)).IsPositive();

            Name = name;
            Description = description;
            Date = date;
            WorkspaceId = workspaceId;

            _transactionTags = new List<TransactionTag>();
        }

        public void SetName(string name)
        {
            Ensure.That(name, nameof(name)).IsNotNullOrWhiteSpace();

            Name = name;
        }

        public void SetDescription(string description) => Description = description;

        public void SetDate(DateTime? date) => Date = date;
    }
}
