using EnsureThat;
using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public int Id { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int WorkspaceId { get; private set; }
        public Workspace Workspace { get; private set; }

        private Category() { }

        public Category(string name, int workspaceId)
        {
            Ensure.That(name, nameof(name)).IsNotNullOrWhiteSpace();
            Ensure.That(workspaceId, nameof(workspaceId)).IsPositive();

            Name = name;
            WorkspaceId = workspaceId;
        }

        public Category(string name, string description, int workspaceId)
            : this(name, workspaceId)
        {
            Description = description;
        }

        public void SetName(string name)
        {
            Ensure.That(name, nameof(name)).IsNotNullOrWhiteSpace();

            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }
    }
}
