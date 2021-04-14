using Csp.Extensions;
using Csp.Wx.Extensions;
using System;
using System.Collections.Generic;

namespace Csp.Wx.Models
{
    public class WxConfig
    {
        private readonly string _url;
        private readonly string _jsapi_ticket;

        /// <summary>
        /// 开发者ID，公众号的唯一标识
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 生成签名的时间戳
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// 生成签名的随机串
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature
        {
            get
            {
                return SHA1Crypto.Signature(_jsapi_ticket, NonceStr, Timestamp, _url);
            }
        }

        /// <summary>
        /// 需要使用的JS接口列表
        /// </summary>
        public IEnumerable<string> JsApiList { get; set; }

        public WxConfig(string jstickte,string url)
        {
            Timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            NonceStr = Guid.NewGuid().ToString("N");
            _url = url;
            _jsapi_ticket = jstickte;
        }

        public static WxConfig Create(WxOptions options, string jsapi_ticket, string url)
        {
            return new WxConfig(jsapi_ticket, url)
            {
                AppId = options.AppId,
                JsApiList = options.JsApiList
            };
        }


        public override string ToString()
        {
            return this.ToJsonString();
        }
    }
}
