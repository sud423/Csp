using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace Csp.Logger.File
{
    /// <summary>
    /// 文件日志处理
    /// </summary>
    internal class FileLogger : ILogger
    {
        //创建此实例的记录器提供程序
        private readonly LoggerProvider _provider;

        //此实例服务的类别  
        private readonly string _category;

        public FileLogger(LoggerProvider loggerProvider, string category)
        {
            _provider = loggerProvider;
            _category = category;
        }


        /// <summary>
        /// 开始逻辑运算范围。返回一个IDisposable，该IDisposable结束逻辑处理范围
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return _provider.ScopeProvider.Push(state);
        }

        /// <summary>
        /// Checks if the given logLevel is enabled.
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return _provider.IsEnabled(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            DateTimeOffset timestamp = DateTimeOffset.Now;
            var builder = new StringBuilder();
            builder.Append(timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff zzz"));
            builder.Append(" [");
            builder.Append(logLevel.ToString());
            builder.Append("] ");
            builder.Append(_category);
            builder.Append(": ");
            builder.AppendLine(formatter(state, exception));

            if (exception != null)
            {
                builder.AppendLine(exception.ToString());
            }

            _provider.WriteLog(new LogMessage(timestamp,builder.ToString()));

            
        }
    }
}
