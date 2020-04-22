using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Peent.Application;

namespace Peent.Api.Infrastructure
{
    public class UserAccessor : IUserAccessor
    {
        private IHttpContextAccessor _accessor;
        public UserAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public ClaimsPrincipal User => _accessor.HttpContext.User;
    }
}
