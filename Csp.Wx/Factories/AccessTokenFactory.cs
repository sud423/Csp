using Csp.Wx.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Csp.Wx.Factories
{
    class AccessTokenFactory
    {
        /// <summary>
        /// 获取微信授权信息
        /// </summary>
        /// <param name="code">微信授权码</param>
        /// <returns></returns>
        internal static async Task<WxToken> GetTokenAsync(string code = null)
        {
            string url = $"{WxConstants.CGI_BIN_TOKEN_URL}?grant_type=client_credential&appid={WxClient.WxOption.AppId}&secret={WxClient.WxOption.AppSecret}";

            if (!string.IsNullOrEmpty(code))
            {
                url = $"{WxConstants.SNSAPI_OAUTH2_URL}?appid={WxClient.WxOption.AppId}&secret={WxClient.WxOption.AppSecret}&code={code}&grant_type=authorization_code";

                return await WxClient.GetAsync<WxToken>(url);
            }

            if (!WxClient.MemoryCache.TryGetValue(WxConstants.ACCESS_TOKEN_KEY, out WxToken token))
            {
                token = await WxClient.GetAsync<WxToken>(url);
                WxClient.MemoryCache.Set(WxConstants.ACCESS_TOKEN_KEY, token, TimeSpan.FromSeconds(token.ExpiresIn));
            }

            return token;
        }

    }
}
