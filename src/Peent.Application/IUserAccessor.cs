using System.Security.Claims;

namespace Peent.Application
{
    public interface IUserAccessor
    {
        ClaimsPrincipal User { get; }
    }
}
