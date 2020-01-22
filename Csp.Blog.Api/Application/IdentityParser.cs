using Csp.Blog.Api.Models;
using Csp.Jwt;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Csp.Blog.Api.Application
{
    public class IdentityParser : IIdentityParser<AppUser>
    {
        private IHttpContextAccessor _httpContextAccessor;

        public IdentityParser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AppUser Parse()
        {
            if (_httpContextAccessor.HttpContext.User is ClaimsPrincipal claims)
            {
                return new AppUser
                {
                    Id = int.Parse(claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value ?? "0"),
                    TenantId = int.Parse(claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GroupSid)?.Value ?? "0")
                };
            }

            return null;
        }
    }
}
