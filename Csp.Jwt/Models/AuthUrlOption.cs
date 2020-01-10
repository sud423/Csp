namespace Csp.Jwt.Models
{
    /// <summary>
    /// 一种简单的结构，用于存储配置的登录/注销
    /// 路径和返回URL参数的名称
    /// </summary>
    public sealed class AuthUrlOption
    {
        /// <summary>
        /// 在未经身份验证的请求的情况下将用户重定向到的登录路径。
        /// 默认值 "/Account/Login"
        /// </summary>
        public string LoginPath { get; set; }

        /// <summary>
        /// 用户注销后将其重定向到的路径。
        /// 默认值 "/Account/Logout"
        /// </summary>
        public string LogoutPath { get; set; }

        /// <summary>
        /// 成功尝试身份验证后将用户重定向到的路径。
        /// 默认值 "returnUrl"
        /// </summary>
        public string ReturnUrlParameter { get; set; }
    }
}
