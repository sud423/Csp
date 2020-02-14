using Csp.Jwt.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Csp.Jwt.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static JwtTokenOption jwtTokenOptions;

        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("Jwt");
            services.Configure<JwtTokenOption>(appSettingsSection);
            jwtTokenOptions = appSettingsSection.Get<JwtTokenOption>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = jwtTokenOptions.ToTokenValidationParams();
            });


            return services;
        }

        public static IServiceCollection AddMvcJwt(this IServiceCollection services, IConfiguration configuration, string applicationDiscriminator = null)
        {
            var appSettingsSection = configuration.GetSection("Jwt");
            services.Configure<JwtTokenOption>(appSettingsSection);
            jwtTokenOptions = appSettingsSection.Get<JwtTokenOption>();

            var appSettings = configuration.GetSection("AuthUrl");
            AuthUrlOption authUrlOptions = appSettingsSection.Get<AuthUrlOption>();

            var hostingEnvironment = services.BuildServiceProvider().GetService<IWebHostEnvironment>();

            //添加数据保护机制
            services.AddDataProtection(options =>
            options.ApplicationDiscriminator =
                $"{applicationDiscriminator ?? hostingEnvironment.ApplicationName}")
                .SetApplicationName(
                $"{applicationDiscriminator ?? hostingEnvironment.ApplicationName}")
                //windows、Linux、macOS 下可以使用此种方式 保存到文件系统
                //也扩展写到redis
                .PersistKeysToFileSystem(new System.IO.DirectoryInfo(jwtTokenOptions.KeyStoragePath));

            services.AddScoped<IDataSerializer<AuthenticationTicket>, TicketSerializer>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Name = $"{hostingEnvironment.ApplicationName}";
                    options.ExpireTimeSpan = TimeSpan.FromSeconds(jwtTokenOptions.Expires);  //cookie有效期2小时,
                    options.TicketDataFormat = new JwtAuthTicketFormat(
                    jwtTokenOptions.ToTokenValidationParams(),
                    services.BuildServiceProvider()
                        .GetService<IDataSerializer<AuthenticationTicket>>(),
                    services.BuildServiceProvider()
                        .GetDataProtector(new[]
                        {
                            $"{applicationDiscriminator ?? hostingEnvironment.ApplicationName}.Auth"
                        }));
                    options.LoginPath = !string.IsNullOrWhiteSpace(authUrlOptions.LoginPath) ? new PathString(authUrlOptions.LoginPath) : new PathString("/Account/Index");
                    options.LogoutPath = !string.IsNullOrWhiteSpace(authUrlOptions.LogoutPath) ? new PathString(authUrlOptions.LogoutPath) : new PathString("/Account/Logout");
                    options.AccessDeniedPath = options.LoginPath;
                    options.ReturnUrlParameter = authUrlOptions?.ReturnUrlParameter ?? "returnUrl";
                    options.SlidingExpiration = true;
                });


            return services;
        }
    }
}
