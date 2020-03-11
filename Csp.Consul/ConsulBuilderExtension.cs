using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Csp.Consul
{
    public static class ConsulBuilderExtension
    {
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app,IHostApplicationLifetime lifetime)
        {
            // Retrieve Consul client from DI
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var consulConfig = app.ApplicationServices
                                .GetRequiredService<IOptions<ConsulOptions>>();
            // Setup logger
            //var loggingFactory = app.ApplicationServices
            //                    .GetRequiredService<ILoggerFactory>();
            //var logger = loggingFactory.CreateLogger<IApplicationBuilder>();

            // Get server IP address   只要使用Kestrel在某个端口上托管服务，才能使用app.Properties["server.Features"]，否则address为null

            //var serverAddressesFeature = app.ServerFeatures.Get<IServerAddressesFeature>();

            //var features = app.Properties["server.Features"] as FeatureCollection;
            //var addresses = features.Get<IServerAddressesFeature>();
            //var address = serverAddressesFeature.Addresses.FirstOrDefault();

            // Register service with consul
            var uri = new Uri(consulConfig.Value.ApplicationUrl);
            var registration = new AgentServiceRegistration()
            {
                ID = $"{uri.Host}-{uri.Port}",
                Name = consulConfig.Value.Name,
                Address = uri.Host,
                Port = uri.Port,
                Tags = consulConfig.Value.Tags?.ToArray(),
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                    Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔
                    HTTP = uri.OriginalString,//健康检查地址
                    Timeout = TimeSpan.FromSeconds(5)
                }
            };
            
            //logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            consulClient.Agent.ServiceRegister(registration).Wait();

            lifetime.ApplicationStopping.Register(() => {
                //logger.LogInformation("Deregistering from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });


            return app;
        }
    }
}
