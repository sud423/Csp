using System;
using System.Security.Cryptography;

namespace Csp.Wx
{
    class SHA1Crypto
    {
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="jsapi_ticket">jsapi ticket</param>
        /// <param name="noncestr">随机字符串</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="url">当前网页的URL，不包含#及其后面部分(必须是调用JS接口页面的完整URL)</param>
        /// <returns></returns>
        internal static string Signature(string jsapi_ticket, string noncestr, long timestamp, string url)
        {
            if (string.IsNullOrEmpty(jsapi_ticket) || string.IsNullOrEmpty(noncestr) || timestamp <= 0 || string.IsNullOrEmpty(url))
                return null;

            var input = $"jsapi_ticket={jsapi_ticket}&noncestr={noncestr}&timestamp={timestamp}" +
                $"&url={(url.IndexOf("#") >= 0 ? url.Substring(0, url.IndexOf("#")) : url)}";

            return Sha1Sign(input);
        }

        /// <summary>
        /// Sha1加密签名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static string Sha1Sign(string input)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = System.Text.Encoding.Default.GetBytes(input);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string signature = BitConverter.ToString(bytes_sha1_out);
            signature = signature.Replace("-", "").ToLower();
            return signature;
        }
    }
}
