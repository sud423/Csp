using Csp.Jwt.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Csp.Jwt
{
    /// <summary>
    /// token生成接口
    /// </summary>
    public interface ITokenGenerator
    {
        /// <summary>
        /// 根据用户名生成token和身份标识
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userClaims">身份标识</param>
        /// <returns></returns>
        TokenWithClaimsPrincipal GenerateAccessTokenWithClaimsPrincipal(string userName, IEnumerable<Claim> userClaims);
    }
}
