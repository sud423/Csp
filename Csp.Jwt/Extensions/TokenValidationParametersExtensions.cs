using Csp.Jwt.Models;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Csp.Jwt.Extensions
{
    static class TokenValidationParametersExtensions
    {
        internal static TokenValidationParameters ToTokenValidationParams(this JwtTokenOptions tokenOptions) =>
            new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,

                ValidateAudience = true,
                ValidAudience = tokenOptions.Audience,

                ValidateIssuer = true,
                ValidIssuer = tokenOptions.Issuer,

                IssuerSigningKey = tokenOptions.Key,
                ValidateIssuerSigningKey = true,

                RequireExpirationTime = true,
                ValidateLifetime = true
            };
    }
}
