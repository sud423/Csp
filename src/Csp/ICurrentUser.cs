namespace Csp
{
    public interface ICurrentUser
    {
        /// <summary>
        /// 判断是否身份登录
        /// </summary>
        bool IsAuthenticated { get; set; }

        /// <summary>
        /// 获取用户编号
        /// </summary>
        int UserId { get; set; }
    }
}
