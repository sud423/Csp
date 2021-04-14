using Csp.Logger.File;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace Csp.Logger
{
    /// <summary>
    /// 文件记录器扩展方法
    /// </summary>
    public static class FileLoggerExtension
    {
        /// <summary>
        /// 将文件记录程序提供程序（别名为“File”）以单例形式添加到可用服务中，并将文件记录程序选项类绑定到appsettings.json文件的“文件”部分
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<FileLoggerOptions>, FileLoggerOptionSetup>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<FileLoggerOptions>, LoggerProviderOptionsChangeTokenSource<FileLoggerOptions, FileLoggerProvider>>());
            return builder;
        }


        /// <summary>
        /// 将文件记录程序提供程序（别名为“File”）以单例形式添加到可用服务中，并将文件记录程序选项类绑定到appsettings.json文件的“文件”部分
        /// </summary>
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, Action<FileLoggerOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddFile();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
