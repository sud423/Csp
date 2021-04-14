using Csp.Logger.File;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;

namespace Csp.Logger
{
    /// <summary>
    /// 基本记录器提供程序类。
    /// <para>记录器提供商本质上代表了用于保存日志信息的介质。</para>
    /// <para>此类可以用作编写文件或数据库记录程序提供程序的基类。</para>
    /// </summary>
    public abstract class LoggerProvider : IDisposable, ILoggerProvider, ISupportExternalScope
    {
        ConcurrentDictionary<string, FileLogger> loggers = new ConcurrentDictionary<string, FileLogger>();
        IExternalScopeProvider fScopeProvider;
        protected IDisposable _optionsChangeToken;

        /// <summary>
        /// 返回一个ILogger实例，以服务于指定的类别
        /// </summary>
        /// <param name="categoryName">类别通常是要求记录器的类别的完全限定类别名称，例如MyNamespace.MyClass</param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName,
            (category) => {
                return new FileLogger(this, category);
            });
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                try
                {
                    Dispose(true);
                }
                catch
                {
                }

                IsDisposed = true;
                GC.SuppressFinalize(this);  // 指示GC不要打扰调用析构函数   
            }
        }

        /// <summary>
        /// 配置选项更改代号。 IDisposable模式实现
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_optionsChangeToken != null)
            {
                _optionsChangeToken.Dispose();
                _optionsChangeToken = null;
            }
        }


        /// <summary>
        /// 析构函数   
        /// </summary>
        ~LoggerProvider()
        {
            if (!IsDisposed)
            {
                Dispose(false);
            }
        }

        /// <summary>
        /// 由日志记录框架调用，以便为日志记录提供程序设置外部范围信息源
        /// <para>ISupportExternalScope 实例</para>
        /// </summary>
        /// <param name="scopeProvider"></param>
        public void SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            fScopeProvider = scopeProvider;
        }

        /// <summary>
        /// 如果启用了指定的日志级别，则返回true
        /// <para>由此提供程序创建的记录器实例调用</para>
        /// </summary>
        public abstract bool IsEnabled(LogLevel logLevel);

        /// <summary>
        /// 记录器实际上并不以任何介质记录信息
        /// 而是调用其提供程序WriteLog()方法，传递收集的日志信息
        /// </summary>
        public abstract void WriteLog(LogMessage message);

        /// <summary>
        /// 返回范围提供者
        /// <para>由此提供程序创建的记录器实例调用</para>
        /// </summary>
        internal IExternalScopeProvider ScopeProvider
        {
            get
            {
                if (fScopeProvider == null)
                    fScopeProvider = new LoggerExternalScopeProvider();
                return fScopeProvider;
            }
        }
        /// <summary>
        /// 释放此实例时，返回true
        /// </summary> 
        public bool IsDisposed { get; protected set; }
    }
}
