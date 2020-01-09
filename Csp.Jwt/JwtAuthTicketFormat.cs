using Csp.Jwt.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Csp.Jwt
{
    sealed class JwtAuthTicketFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private const string Algorithm = SecurityAlgorithms.HmacSha256;
        private readonly TokenValidationParameters validationParameters;
        private readonly IDataSerializer<AuthenticationTicket> ticketSerializer;
        private readonly IDataProtector dataProtector;

        public JwtAuthTicketFormat(TokenValidationParameters validationParameters,
                    IDataSerializer<AuthenticationTicket> ticketSerializer,
                    IDataProtector dataProtector)
        {
            this.validationParameters = validationParameters ??
                throw new ArgumentNullException($"{nameof(validationParameters)} 不能为null");
            this.ticketSerializer = ticketSerializer ??
                throw new ArgumentNullException($"{nameof(ticketSerializer)} 不能为null"); ;
            this.dataProtector = dataProtector ??
                throw new ArgumentNullException($"{nameof(dataProtector)} 不能为null");
        }

        public string Protect(AuthenticationTicket data) => Protect(data, null);

        public string Protect(AuthenticationTicket data, string purpose)
        {
            var array = ticketSerializer.Serialize(data);

            return Base64UrlTextEncoder.Encode(dataProtector.Protect(array));
        }

        public AuthenticationTicket Unprotect(string protectedText) => Unprotect(protectedText, null);

        public AuthenticationTicket Unprotect(string protectedText, string purpose)
        {
            var authTicket = ticketSerializer.Deserialize(
                dataProtector.Unprotect(
                    Base64UrlTextEncoder.Decode(protectedText)));

            var embeddedJwt = authTicket
                .Properties?
                .GetTokenValue(TokenConstants.TokenName);

            try
            {
                new JwtSecurityTokenHandler()
                    .ValidateToken(embeddedJwt, validationParameters, out var token);

                if (!(token is JwtSecurityToken jwt))
                {
                    throw new SecurityTokenValidationException("JWT令牌无效");
                }

                if (!jwt.Header.Alg.Equals(Algorithm, StringComparison.Ordinal))
                {
                    throw new ArgumentException($"算法必须是'{Algorithm}'");
                }
            }
            catch (Exception)
            {
                return null;
            }

            return authTicket;
        }
    }
}
