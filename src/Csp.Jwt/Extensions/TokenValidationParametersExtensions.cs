using Csp.Jwt.Models;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Csp.Jwt.Extensions
{
    static class TokenValidationParametersExtensions
    {
        internal static TokenValidationParameters ToTokenValidationParams(this JwtTokenOptions tokenOptions) =>
            new()
            {
                ClockSkew = TimeSpan.Zero,

                ValidateAudience = false,
                //ValidAudience = tokenOptions.Audience,

                ValidateIssuer = true,
                ValidIssuer = tokenOptions.Issuer,

                IssuerSigningKey = tokenOptions.SigningKey,
                ValidateIssuerSigningKey = true,

                RequireExpirationTime = true,
                ValidateLifetime = true
            };
    }
}
