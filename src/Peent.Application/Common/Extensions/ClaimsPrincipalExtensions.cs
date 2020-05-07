using System;
using System.Security.Claims;

namespace Peent.Application.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
            => Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        public static string GetUserName(this ClaimsPrincipal principal)
            => principal.FindFirst(ClaimTypes.Name)?.Value;

        public static int GetWorkspaceId(this ClaimsPrincipal principal)
            => int.Parse(principal.FindFirst(KnownClaims.WorkspaceId)?.Value ??
                         throw new InvalidOperationException());
    }
}