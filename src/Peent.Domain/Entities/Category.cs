using EnsureThat;
using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class Category : AuditableEntity<int>, IHaveWorkspace
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Workspace Workspace { get; private set; }

        public Category(string name, string description = null)
        {
            SetName(name);
            Description = description;
        }

        public void SetWorkspace(Workspace workspace)
        {
            if (Workspace != null)
            {
//                throws
            }

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
