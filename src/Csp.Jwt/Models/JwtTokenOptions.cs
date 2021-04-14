using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Csp.Jwt.Models
{
    public sealed class JwtTokenOptions
    {
        /// <summary>
        /// Token发布者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Token接受者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 签名秘钥
        /// </summary>
        public string Key { get; set; }

        public SecurityKey SigningKey
        {
            get
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
            }
        }


        public string LoginPath { get; set; } = "/account/login";

        public string LogoutPath { get; set; } = "/account/logout";

        public string AccessDeniedPath { get; set; }

        public string ReturnUrlParameter { get; set; } = "returnUrl";

        /// <summary>
        /// 过期时间，单位为秒
        /// </summary>
        public int Expires { get; set; }
    }

    public struct TokenConstants
    {
        public const string TokenName = "access_token";
    }
}
