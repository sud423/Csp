using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Data;

namespace Csp.Dapper.Extensions
{
    public static class ServiceCollectionsExtensions
    {

        public static void AddMySqlDaaper(this IServiceCollection services)
        {
            services.AddScoped<IDbConnection, MySqlConnection>();
            services.AddScoped<IDapperRead, DapperRead>();
            services.AddScoped<IDapperWrite, DapperWrite>();
        }
    }
}
