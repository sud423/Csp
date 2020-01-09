using Csp.Wx.Extensions;
using Csp.Wx.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using System.Threading.Tasks;

namespace Csp.Wx
{
    class WxClient
    {
        static HttpClient _client;

        public static IMemoryCache MemoryCache { get; private set; }

        public static WxOption WxOption { get; private set; }

        internal static void Init(HttpClient client, IMemoryCache cache,WxOption option)
        {
            _client = client;
            MemoryCache = cache;
            WxOption = option;
        }

        /// <summary>
        /// 发送get请求
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="url">请求的url</param>
        /// <returns></returns>
        internal async static Task<T> GetAsync<T>(string url) where T : class
        {
            var response = await _client.GetAsync(url);
            return await DealResult<T>(response);
        }

        /// <summary>
        /// 发送get请求
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="url">请求的url</param>
        /// <returns></returns>
        internal async static Task GetAsync(string url)
        {
            await _client.GetAsync(url);
        }

        /// <summary>
        /// 发送post请求
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="url">请求的url</param>
        /// <param name="content">请求的参数</param>
        /// <returns></returns>
        internal async static Task<T> PostAsync<T>(string url,StringContent content) where T : class
        {
            var response = await _client.PostAsync(url, content);

            return await DealResult<T>(response);
        }

        /// <summary>
        /// 发送post请求
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="url">请求的url</param>
        /// <param name="content">请求的参数</param>
        /// <returns></returns>
        internal async static Task PostAsync(string url, StringContent content)
        {
            await _client.PostAsync(url, content);
        }

        /// <summary>
        /// 处理返回结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        static async Task<T> DealResult<T>(HttpResponseMessage response) where T:class
        {
            var message = await response.Content.ReadAsStringAsync();

            if (0 == message.GetValue<int>("errcode"))
                return message.FromJson<T>();

            return default;
        }
    }
}
