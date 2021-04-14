using Csp.Jwt.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Csp.Jwt.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("Jwt");
            services.Configure<JwtTokenOptions>(appSettingsSection);
            var jwtTokenOptions = appSettingsSection.Get<JwtTokenOptions>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = jwtTokenOptions.ToTokenValidationParams();
                });


            return services;
        }

        public static IServiceCollection AddJwtAuthenticationWithCookie(this IServiceCollection services, IConfiguration configuration, string applicationDiscriminator = null)
        {
            var appSettingsSection = configuration.GetSection("Jwt");
            services.Configure<JwtTokenOptions>(appSettingsSection);
            var jwtTokenOptions = appSettingsSection.Get<JwtTokenOptions>();

            var hostingEnvironment = services.BuildServiceProvider().GetService<IWebHostEnvironment>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Name = $"{hostingEnvironment.ApplicationName}";
                    options.ExpireTimeSpan = TimeSpan.FromSeconds(jwtTokenOptions.Expires);  //cookie有效期2小时,

                    options.LoginPath = jwtTokenOptions.LoginPath;
                    options.LogoutPath = jwtTokenOptions.LogoutPath;
                    options.AccessDeniedPath = jwtTokenOptions.AccessDeniedPath;
                    options.ReturnUrlParameter = jwtTokenOptions.ReturnUrlParameter;
                    options.SlidingExpiration = true;
                }).AddJwtBearer(options=> {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = jwtTokenOptions.ToTokenValidationParams();
                });


            return services;
        }
    }
}
