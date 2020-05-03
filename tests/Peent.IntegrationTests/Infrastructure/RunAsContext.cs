using Peent.Domain.Entities;

namespace Peent.IntegrationTests.Infrastructure
{
    public class RunAsContext
    {
        public ApplicationUser User { get; }
        public Workspace Workspace { get; }

        public RunAsContext(ApplicationUser user, Workspace workspace)
        {
            User = user;
            Workspace = workspace;
        }
    }
}
