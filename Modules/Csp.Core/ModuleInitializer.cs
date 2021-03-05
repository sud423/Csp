using Csp.Core.Data;
using Csp.Extensions;
using Csp.Modules;
using Csp.Proxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Csp.Core
{
    public class ModuleInitializer : IModuleInitializer
    {

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
        }

        public void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<CoreDbContext>(options =>
                    options.UseMySql(configuration.GetConnectionString("DefaultConnection")))
                .AddScoped<ICoreDbContext>(provider => provider.GetService<CoreDbContext>())
                .DecorateWithDispatchProxy<ICoreDbContext, DbContextProxy<ICoreDbContext>>();
        }
    }
}
