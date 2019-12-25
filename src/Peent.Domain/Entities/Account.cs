using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class Account : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }

        public AccountType Type { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }
}
