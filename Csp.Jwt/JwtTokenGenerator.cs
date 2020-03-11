using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Csp.Jwt.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Csp.Jwt
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtTokenOptions _tokenOptions;

        public JwtTokenGenerator(IOptions<JwtTokenOptions> options)
        {
            _tokenOptions = options.Value;
        }

        public TokenWithClaimsPrincipal GenerateAccessTokenWithClaimsPrincipal(string userName, IEnumerable<Claim> userClaims)
        {

            var claims = MergeUserClaimsWithDefaultClaims(userClaims);

            var accessToken = GenerateAccessToken(claims);

            return new TokenWithClaimsPrincipal()
            {
                AccessToken = new Token(accessToken, _tokenOptions.Expires),
                ClaimsPrincipal = ClaimsPrincipalFactory.CreatePrincipal(claims),
                AuthProperties = CreateAuthProperties(accessToken)
            };
        }

        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userClaims">用户身份标识列表</param>
        /// <returns></returns>
        private string GenerateAccessToken(IEnumerable<Claim> userClaims)
        {
            var expiration = TimeSpan.FromSeconds(_tokenOptions.Expires);
            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: userClaims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.Add(expiration),
                signingCredentials: new SigningCredentials(_tokenOptions.Key, SecurityAlgorithms.HmacSha256)
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            return accessToken;
        }


        private IEnumerable<Claim> MergeUserClaimsWithDefaultClaims(IEnumerable<Claim> userClaims)
        {
            var claims = new List<Claim>(userClaims)
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),ClaimValueTypes.Integer64)
            };

            return claims;
        }

        private AuthenticationProperties CreateAuthProperties(string accessToken)
        {
            var authProps = new AuthenticationProperties();
            authProps.StoreTokens(
                new[]
                {
                    new AuthenticationToken()
                    {
                        Name = TokenConstants.TokenName,
                        Value = accessToken
                    }
                });

            return authProps;
        }
    }
}
