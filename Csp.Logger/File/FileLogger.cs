using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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
            if ((this as ILogger).IsEnabled(logLevel))
            {

                LogEntry info = new LogEntry();
                info.Category = _category;
                info.Level = logLevel;
                // 好吧，传递的默认格式化程序功能不考虑异常
                // SEE:  https://github.com/aspnet/Extensions/blob/master/src/Logging/Logging.Abstractions/src/LoggerExtensions.cs
                info.Text = exception?.Message ?? state.ToString(); // formatter(state, exception)
                info.Exception = exception;
                info.EventId = eventId;
                info.State = state;

                // 好吧，你永远不知道它到底是什么
                if (state is string)
                {
                    info.StateText = state.ToString();
                }
                // 如果我们需要一个消息模板，让我们获取键和值（针对结构化日志记录提供程序）
                // SEE: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging#log-message-template
                // SEE: https://softwareengineering.stackexchange.com/questions/312197/benefits-of-structured-logging-vs-basic-logging
                else if (state is IEnumerable<KeyValuePair<string, object>> Properties)
                {
                    info.StateProperties = new Dictionary<string, object>();

                    foreach (KeyValuePair<string, object> item in Properties)
                    {
                        info.StateProperties[item.Key] = item.Value;
                    }
                }

                // 收集有关范围的信息（如果有）
                if (_provider.ScopeProvider != null)
                {
                    _provider.ScopeProvider.ForEachScope((value, loggingProps) =>
                    {
                        if (info.Scopes == null)
                            info.Scopes = new List<LogScopeInfo>();

                        LogScopeInfo Scope = new LogScopeInfo();
                        info.Scopes.Add(Scope);

                        if (value is string)
                        {
                            Scope.Text = value.ToString();
                        }
                        else if (value is IEnumerable<KeyValuePair<string, object>> props)
                        {
                            if (Scope.Properties == null)
                                Scope.Properties = new Dictionary<string, object>();

                            foreach (var pair in props)
                            {
                                Scope.Properties[pair.Key] = pair.Value;
                            }
                        }
                    },
                    state);

                }

                _provider.WriteLog(info);

            }
        }
    }
}
