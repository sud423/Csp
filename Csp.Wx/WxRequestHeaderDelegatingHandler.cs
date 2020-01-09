using Csp.Wx.Extensions;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Csp.Wx
{
    public class WxRequestHeaderDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger<WxRequestHeaderDelegatingHandler> _logger;

        public WxRequestHeaderDelegatingHandler(ILogger<WxRequestHeaderDelegatingHandler> logger)
        {
            _logger = logger;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response =await base.SendAsync(request, cancellationToken);

            var message = await response.Content.ReadAsStringAsync();
            //请求结果 不为0，记录返回结果 信息
            if (0 != message.GetValue<int>("errcode"))
            {
                _logger.LogError(message);
            }

            return response;
        }
    }
}
