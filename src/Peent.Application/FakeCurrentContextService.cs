using System;
using System.Security.Claims;
using Peent.Application.Common;
using Peent.Domain.Entities;

namespace Peent.Application
{
    public class FakeCurrentContextService : ICurrentContextService
    {
        public ApplicationUser User { get; private set; }

        public Workspace Workspace { get; private set; }

        public ClaimsPrincipal Claims =>
            new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "test"),
                new Claim(ClaimTypes.NameIdentifier, User is null ? "" : User.Id.ToString()),
                new Claim(KnownClaims.WorkspaceId, Workspace is null ? "" : Workspace.Id.ToString())
            }, "mock"));
        
        public FakeCurrentContextService(){}

        public FakeCurrentContextService(Guid userId, int workspaceId)
        {
            User = ApplicationUser.FromId(userId);
            Workspace = Workspace.FromId(workspaceId);
        }

        public void SetCurrentContext(ApplicationUser user, Workspace workspace)
        {
            User = user == null ? null : ApplicationUser.FromId(user.Id);
            Workspace = workspace == null ? null : Workspace.FromId(workspace.Id);
        }

        public void Reset()
        {
            User = null;
            Workspace = null;
        }
    }
}