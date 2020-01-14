using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Csp.Logger
{
    /// <summary>
    /// 基本记录器提供程序类。
    /// <para>记录器提供商本质上代表了用于保存日志信息的介质。</para>
    /// <para>此类可以用作编写文件或数据库记录程序提供程序的基类。</para>
    /// </summary>
    public abstract class LoggerProvider : IDisposable, ILoggerProvider, ISupportExternalScope
    {
        ConcurrentDictionary<string, Logger> loggers = new ConcurrentDictionary<string, Logger>();
        IExternalScopeProvider fScopeProvider;
        protected IDisposable SettingsChangeToken;

        public ILogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            throw new NotImplementedException();
        }
    }
}
