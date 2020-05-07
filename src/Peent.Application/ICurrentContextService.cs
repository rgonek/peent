using System.Security.Claims;
using Peent.Domain.Entities;

namespace Peent.Application
{
    public interface ICurrentContextService
    {
        ClaimsPrincipal Claims { get; }
        ApplicationUser User { get; }
        Workspace Workspace { get; }
    }
}
