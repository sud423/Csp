using Microsoft.Extensions.Logging;
using System;

namespace Csp.Logger
{
    /// <summary>
    /// 表示一个处理日志信息的对象。
    /// <para>此类不会将日志信息保存在介质中。它的职责是创建日志信息，填充该日志信息的属性，然后将其传递给关联的记录器提供程序。</para>
    /// </summary>
    internal class Logger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }
    }
}
