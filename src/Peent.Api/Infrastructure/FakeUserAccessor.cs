using System.Security.Claims;
using Peent.Application;
using Peent.Application.Infrastructure;

namespace Peent.Api.Infrastructure
{
    public class FakeUserAccessor : IUserAccessor
    {
        public ClaimsPrincipal User
        {
            get
            {
                return new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "test"),
                    new Claim(ClaimTypes.NameIdentifier, "test"),
                    new Claim(KnownClaims.WorkspaceId, "1")
                }, "mock"));
            }
        }
    }
}
