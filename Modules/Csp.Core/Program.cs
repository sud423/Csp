using Csp.Core.Data;
using Csp.Core.Services;
using Csp.Data;
using Csp.Extensions;
using Csp.Proxy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Csp.Core
{
    class Program
    {
        static async Task Main()
        {
            var serviceCollection = new ServiceCollection();
            await using var sp = GetServiceProviderWithClassicDecorator(serviceCollection);

            var consumer = sp.GetRequiredService<ExampleConsumer>();

            var result=await consumer.Run();

            Console.WriteLine(result);


            Console.ReadKey();
        }


        //static ServiceProvider GetServiceProviderWithDispatchProxyDecorator(IServiceCollection serviceCollection)
        //{
        //    serviceCollection.AddDbContext<CoreDbContext>(options =>
        //            options.UseMySql("Server=139.196.145.162;Port=3306;Database=flyorder;Uid=root;Pwd=Lghl2Mcl3*#;"))
        //        //.AddScoped<DbContext>(provider => provider.GetService<CoreDbContext>())
        //        .AddScoped<IDbContext>(provider => provider.GetService<CoreDbContext>())
        //        .AddScoped<ICurrentUserService,CurrentUserService>()
        //        .DecorateWithDispatchProxy<IDbContext, DbContextProxy<IDbContext>>()
        //        .AddSingleton<ExampleConsumer>();

        //    return serviceCollection.BuildServiceProvider();
        //}

        static ServiceProvider GetServiceProviderWithClassicDecorator(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<CoreDbContext>(options =>
                    options.UseMySql("Server=139.196.145.162;Port=3306;Database=flyorder;Uid=root;Pwd=Lghl2Mcl3*#;"))
                .AddScoped<ICoreDbContext>(provider => provider.GetService<CoreDbContext>())
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .DecorateWithDispatchProxy<ICoreDbContext, DbContextProxy<ICoreDbContext>>()
                .AddSingleton<ExampleConsumer>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
