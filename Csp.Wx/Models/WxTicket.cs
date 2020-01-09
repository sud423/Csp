using Newtonsoft.Json;

namespace Csp.Wx.Models
{
    class WxTicket
    {
        /// <summary>
        /// jsapi_ticket
        /// </summary>
        public string Ticket { get; set; }

        /// <summary>
        /// 有效期7200秒
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
