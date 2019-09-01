using System.Security.Claims;

namespace Peent.Application.Interfaces
{
    public interface IUserAccessor
    {
        ClaimsPrincipal User { get; }
    }
}
