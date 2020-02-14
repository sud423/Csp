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
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information).AddFile();
        });


        public static IServiceCollection AddEF<TDbContext>(this IServiceCollection services, IConfiguration configuration) where TDbContext : DbContext
        {
            var appSettingsSection = configuration.GetSection("SqlConnection");

            var _options = appSettingsSection.Get<SqlOption>();
            services.AddDbContext<TDbContext>(options =>
            {
                options.UseLoggerFactory(MyLoggerFactory);
                switch (_options.SqlType)
                {
                    case SqlType.MySql:
                        options.UseMySql(_options.MySqlConnection);
                        break;
                    case SqlType.SqlServer:
                        options.UseSqlServer(_options.SqlServerConnection);
                        break;
                }
            });

            return services;
        }

    }
}
