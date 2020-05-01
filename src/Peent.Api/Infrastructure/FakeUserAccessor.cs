using System.Security.Claims;
using Peent.Application;
using Peent.Application.Infrastructure;

namespace Peent.Api.Infrastructure
{
    public class FakeUserAccessor : IUserAccessor
    {
        public ClaimsPrincipal User =>
            new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "test"),
                new Claim(ClaimTypes.NameIdentifier, "E78EECD3-87CA-4DBA-B5B2-861BC5A65F4A"),
                new Claim(KnownClaims.WorkspaceId, "1")
            }, "mock"));
    }
}
