using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Csp.Jwt.Models
{
    public class TokenWithClaimsPrincipal
    {
        public Token AccessToken { get; internal set; }

        public ClaimsPrincipal ClaimsPrincipal { get; internal set; }

        public AuthenticationProperties AuthProperties { get; internal set; }
        
    }
}
