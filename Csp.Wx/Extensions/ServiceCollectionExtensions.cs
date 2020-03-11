using Csp.Wx.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Csp.Wx.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWx(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("Wx");
            services.Configure<WxOptions>(appSettingsSection);

            services.AddTransient<WxRequestHeaderDelegatingHandler>();

            services.AddMemoryCache();
            services.AddHttpClient<IWxService, WxService>().AddHttpMessageHandler<WxRequestHeaderDelegatingHandler>();

            return services;
        }
    }
}
