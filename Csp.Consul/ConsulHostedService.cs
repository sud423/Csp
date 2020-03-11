using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Csp.Consul
{
    public class ConsulHostedService : IHostedService
    {
        private CancellationTokenSource _cts;
        private readonly IConsulClient _client;
        private readonly ConsulOptions _options;
        private string _registrationID;

        public ConsulHostedService(IConsulClient client, IOptions<ConsulOptions> options)
        {
            _options = options.Value;
            _client = client;

        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            var uri = new Uri(_options.ApplicationUrl);

            _registrationID = $"{uri.Host}-{uri.Port}";

            var registration = new AgentServiceRegistration()
            {
                ID = _registrationID,
                Name = _options.Name,
                Address = uri.Host,
                Port = uri.Port,
                Tags = _options.Tags?.ToArray(),
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                    Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔
                    HTTP = uri.OriginalString,//健康检查地址
                    Timeout = TimeSpan.FromSeconds(10)
                }
            };

            await _client.Agent.ServiceDeregister(registration.ID, _cts.Token);
            await _client.Agent.ServiceRegister(registration, _cts.Token);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _cts.Cancel();
            try
            {
                await _client.Agent.ServiceDeregister(_registrationID, cancellationToken);
            }
            catch
            {

            }
        }
    }
}
