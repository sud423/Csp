using Csp.Wx.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Csp.Wx.Factories
{
    class TicketFactory
    {
        /// <summary>
        /// 获取微信授权信息
        /// </summary>
        /// <param name="client">httpclient</param>
        /// <param name="cache">memorycache</param>
        /// <param name="logger">日志记录器，记录返回信息</param>
        /// <param name="option">微信配置信息</param>
        /// <param name="code">微信授权码</param>
        /// <returns></returns>
        internal static async Task<string> GetTicketAsync()
        {
            var token = await AccessTokenFactory.GetTokenAsync();
            if (token == null)
                return string.Empty;

            string url = $"{WxConstants.JSAPI_TICKET_URL}?access_token={token.AccessToken}&type=jsapi";
            if (!WxClient.MemoryCache.TryGetValue(WxConstants.JS_API_TICKET_KEY, out WxTicket ticket))
            {
                ticket = await WxClient.GetAsync<WxTicket>(url);

                WxClient.MemoryCache.Set("wechat_jsapi_ticket", ticket.Ticket, TimeSpan.FromSeconds(ticket.ExpiresIn));
            }

            return ticket.Ticket;
        }
    }
}
