using System.Collections.Generic;

namespace Csp.Wx.Models
{
    /// <summary>
    /// 微信配置参数
    /// </summary>
    public class WxOptions
    {
        /// <summary>
        /// 开发者ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 开发者密码
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 令牌(Token)
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 需要使用的JS接口列表
        /// </summary>
        public IEnumerable<string> JsApiList { get; set; }
    }

    public struct WxConstants
    {
        public const string AUTH_URL = "https://open.weixin.qq.com/connect/oauth2/authorize";//网页授权获取code地址
        public const string SNSAPI_OAUTH2_URL = "https://api.weixin.qq.com/sns/oauth2/access_token"; //code换取网页授权access_token地址
        public const string SNSAPI_USERINFO_URL = "https://api.weixin.qq.com/sns/userinfo";//拉取用户信息(需scope为 snsapi_userinfo)

        public const string CGI_BIN_TOKEN_URL = "https://api.weixin.qq.com/cgi-bin/token";//获取access_token 地址
        public const string CGI_BIN_TEMPLATE_URL = "https://api.weixin.qq.com/cgi-bin/message/template/send";//发送模板消息
        public const string JSAPI_TICKET_URL = "https://api.weixin.qq.com/cgi-bin/ticket/getticket";//获取jsapi_ticket地址


        public const string ACCESS_TOKEN_KEY = "csp_wx_access_token";
        public const string JS_API_TICKET_KEY = "csp_wx_js_api_ticket";
    }
}
