using System;
using System.Collections.Generic;
using EnsureThat;
using Peent.Domain.Common;
using Peent.Domain.Entities.TransactionAggregate;

namespace Peent.Domain.Entities
{
    public class Tag : AuditableEntity<int>, IHaveWorkspace
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Workspace Workspace { get; private set; }

        private readonly List<TransactionTag> _transactionTags = new List<TransactionTag>();
        public IReadOnlyCollection<TransactionTag> TransactionTags => _transactionTags.AsReadOnly();

        #region Ctors

        private Tag()
        {
        }

        public Tag(string name, Workspace workspace)
            : this(name, null, workspace)
        {
        }

        #endregion

        public Tag(string name, string description, Workspace workspace)
        {
            Ensure.That(workspace, nameof(workspace)).IsNotNull();

            SetName(name);
            SetDescription(description);
            Workspace = workspace;
        }

        public void SetName(string name)
        {
            Ensure.That(name, nameof(name)).IsNotNullOrWhiteSpace();

            Name = name;
        }

        public void SetDescription(string description) => Description = description;
    }
}