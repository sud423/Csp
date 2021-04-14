using Csp.Wx.Models;
using System.Threading.Tasks;

namespace Csp.Wx
{
    public interface IWxService
    {
        /// <summary>
        /// 获取用户同意授权地址
        /// </summary>
        /// <param name="path">微信同意授权回调的地址</param>
        /// <param name="state">状态码，为空时值为：csp</param>
        /// <returns></returns>
        string GetAuthUrl(string path, string state = null);
        
        /// <summary>
        /// 获取页面配置信息
        /// </summary>
        /// <param name="url">页面地址</param>
        /// <returns></returns>
        Task<WxConfig> GetConfig(string url);
        
        /// <summary>
        /// 拉取用户信息(需scope为 snsapi_userinfo)
        /// </summary>
        /// <param name="code">同意确认后返回的授权，使用后无效</param>
        Task<WxUser> GetSnsApiUserInfo(string code);

        /// <summary>
        /// 发送模板消息
        /// eg:
        /// var data = new
        /// {
        ///     first = new {value="1"},
        ///     keyword1=new {value="2"},
        ///     keyword2 =new {value="3"},
        ///     keyword3 =new {value="4"},
        ///     keyword4 =new {value="5"},
        ///     remark=new {value="remark"}
        /// };
        /// </summary>
        /// <param name="toUser">接收者openid</param>
        /// <param name="tempId">模板ID</param>
        /// <param name="data">模板数据</param>
        /// <param name="url">模板跳转链接</param>
        /// <param name="data">模板内容字体颜色，不填默认为黑色</param>
        /// <returns></returns>
        Task SendTempMsg(string toUser, string tempId, object data, string url = "", string color = null);
    }
}
