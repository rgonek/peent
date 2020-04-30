﻿using EnsureThat;
using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class Category : AuditableEntity<int>, IHaveWorkspace
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int WorkspaceId { get; private set; }

        #region Ctors

        private Category() { }

        public Category(string name, int workspaceId)
            : this(name, null, workspaceId)
        {
        }

        #endregion

        public Category(string name, string description, int workspaceId)
        {
            Ensure.That(workspaceId, nameof(workspaceId)).IsPositive();

            SetName(name);
            Description = description;
            WorkspaceId = workspaceId;
        }

        public void SetName(string name)
        {
            Ensure.That(name, nameof(name)).IsNotNullOrWhiteSpace();

            Name = name;
        }

        public void SetDescription(string description) => Description = description;
    }
}
