using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Csp.Consul
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConsul(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("Consul");
            services.Configure<ConsulOption>(appSettingsSection);
            //var options= appSettingsSection.Get<ConsulOption>(); 
            //services.AddSingleton<IHostedService, ConsulHostedService>();
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri(configuration.GetValue<string>("Consul:address"));
            }));

            return services;
        }
    }
}
