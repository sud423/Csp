using Csp.Wx.Extensions;
using Csp.Wx.Factories;
using Csp.Wx.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Csp.Wx
{
    public class WxService : IWxService
    {
        private readonly WxOptions _options;
        
        public WxService(IOptions<WxOptions> options, HttpClient client, IMemoryCache cache)
        {
            _options = options.Value;

            WxClient.Init(client, cache, _options);
        }

        /// <summary>
        /// 获取微信授权地址
        /// </summary>
        /// <param name="path">微信同意授权回调的地址</param>
        /// <param name="state">状态码，为空时值为：csp</param>
        /// <returns></returns>
        public string GetAuthUrl(string path, string state = null)
        {
            if (string.IsNullOrEmpty(state))
                state = "csp";

            string redirect = $"{WxConstants.AUTH_URL}?appId={_options.AppId}&redirect_uri={HttpUtility.UrlEncode(path)}" +
                $"&response_type=code&scope=snsapi_userinfo&state={state}#wechat_redirect";

            return redirect;
        }

        public async Task<WxConfig> GetConfig(string url)
        {
            string ticket = await TicketFactory.GetTicketAsync();
            if (string.IsNullOrEmpty(ticket)) return null;
            return WxConfig.Create(_options, ticket, url);
        }

        public async Task<WxUser> GetSnsApiUserInfo(string code)
        {
            var token = await AccessTokenFactory.GetTokenAsync(code);
            if (token == null)
                return null;

            string url = $"{WxConstants.SNSAPI_USERINFO_URL}?access_token={token.AccessToken}&openid={token.OpenId}&lang=zh_CN";

            return await WxClient.GetAsync<WxUser>(url);
        }

        public async Task SendTempMsg(string toUser, string tempId, object data, string url = "", string color = null)
        {
            var token = await AccessTokenFactory.GetTokenAsync();
            if (token == null)
                return;

            var msg = new
            {
                touser = toUser,
                template_id = tempId,
                data,
                url,
                color
            };

            string uri = $"{WxConstants.CGI_BIN_TEMPLATE_URL}?access_token={token.AccessToken}";

            var content = new StringContent(msg.ToJson(), System.Text.Encoding.UTF8, "application/json");

            await WxClient.PostAsync(uri, content);
        }
    }
}
