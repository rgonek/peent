﻿using EnsureThat;
using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class Category : AuditableEntity<int>, IHaveWorkspace
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Workspace Workspace { get; private set; }

        #region Ctors

        private Category() { }

        public Category(string name, Workspace workspace)
            : this(name, null, workspace)
        {
        }

        #endregion

        public Category(string name, string description, Workspace workspace)
        {
            Ensure.That(workspace, nameof(workspace)).IsNotNull();

            SetName(name);
            Description = description;
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
