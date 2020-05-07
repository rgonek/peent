using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Peent.Application;
using Peent.Application.Common.Extensions;
using Peent.Domain.Entities;

namespace Peent.Api.Infrastructure
{
    public class UserAccessor : ICurrentContextService
    {
        private readonly IHttpContextAccessor _accessor;

        public UserAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public ClaimsPrincipal Claims => _accessor.HttpContext.User;
        public ApplicationUser User => ApplicationUser.FromId(Claims.GetUserId());
        public Workspace Workspace => Workspace.FromId(Claims.GetWorkspaceId());
    }
}