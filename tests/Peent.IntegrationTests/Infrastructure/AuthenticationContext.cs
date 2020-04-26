using Peent.Domain.Entities;

namespace Peent.IntegrationTests.Infrastructure
{
    public class AuthenticationContext
    {
        public ApplicationUser User { get; }
        public Workspace Workspace { get; }

        public AuthenticationContext(ApplicationUser user, Workspace workspace)
        {
            User = user;
            Workspace = workspace;
        }
    }
}
