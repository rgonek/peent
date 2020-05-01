using System;
using System.Security.Claims;
using Peent.Domain.Entities;

namespace Peent.Application.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static int GetWorkspaceId(this ClaimsPrincipal principal)
        {
            return int.Parse(principal.FindFirst(KnownClaims.WorkspaceId)?.Value ??
                             throw new InvalidOperationException());
        }

        public static Workspace GetWorkspace(this ClaimsPrincipal principal)
        {
            return Workspace.FromId(principal.GetWorkspaceId());
        }
    }
}
