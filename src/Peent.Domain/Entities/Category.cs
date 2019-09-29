namespace Peent.Domain.Entities
{
    public class Category : AuditInfoOwner
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }
    }
}
