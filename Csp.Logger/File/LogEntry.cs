using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Csp.Logger.File
{
    /// <summary>
    /// 日志条目的信息
    /// <para>记录器在调用其Log()方法时创建此类的实例，填充属性，然后将信息传递给调用WriteLog()的提供程序。</para>
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// 返回本地计算机的主机名
        /// </summary>
        static public readonly string StaticHostName = System.Net.Dns.GetHostName();

        /// <summary>
        /// 当前登录到操作系统的人员的用户名。
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// 本地计算机的主机名
        /// </summary>
        public string HostName { get { return StaticHostName; } }

        /// <summary>
        /// 此实例的创建时间的日期和时间
        /// </summary>
        public DateTime Timestamp { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// 该实例所属的类别
        /// <para>类别通常是要求记录器的类别的完全限定类别名称，例如MyNamespace.MyClass </para>
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 此信息的日志级别
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// 此信息的消息
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 此信息表示的异常（如果有），否则为null
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 此信息的EventId
        /// <para>ID设置为零的EventId，通常意味着没有EventId</para>
        /// </summary>
        public EventId EventId { get; set; }

        /// <summary>
        /// 状态对象。包含有关短信的信息
        /// <para>看起来它的类型始终是Microsoft.Extensions.Logging.Internal.FormattedLogValues </para>
        /// </summary>
        public object State { get; set; }

        /// <summary>
        /// 当State只是一个字符串类型时使用。到目前为止为空
        /// </summary>
        public string StateText { get; set; }

        /// <summary>
        /// 具有State属性的字典
        /// <para>当日志消息是具有格式值的消息模板时，例如<code> Logger.LogInformation（“客户{CustomerId}订单{OrderId}已完成”，CustomerId，OrderId）</ code></para>
        /// 该词典包含从消息中收集的条目，以简化任何结构化日志记录提供程序。
        /// </summary>
        public Dictionary<string, object> StateProperties { get; set; }

        /// <summary>
        /// 当前使用的范围（如果有）。最后一个范围是
        /// </summary>
        public List<LogScopeInfo> Scopes { get; set; }
    }
}
