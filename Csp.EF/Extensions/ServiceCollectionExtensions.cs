using Csp.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Csp.EF.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => {
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Debug).AddDebug();
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information).AddConsole();
#if DEBUG
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information).AddFile();
#endif
        });


        public static IServiceCollection AddEF<TDbContext>(this IServiceCollection services, IConfiguration configuration) where TDbContext : DbContext
        {
            var appSettingsSection = configuration.GetSection("ConnectionStrings:DefaultConnectionString");

            var _options = appSettingsSection.Get<SqlOptions>();
            services.AddDbContext<TDbContext>(options =>
            {
                options.UseLoggerFactory(MyLoggerFactory);

                if ("MySql.Data.MySqlClient".ToLower().Equals(_options.ProviderName.ToLower()))
                {
                    options.UseMySql(_options.ConnectionString);
                }

                if ("System.Data.SqlClient".ToLower().Equals(_options.ProviderName.ToLower()))
                {
                    options.UseSqlServer(_options.ConnectionString);
                }
            });

            return services;
        }

    }
}
