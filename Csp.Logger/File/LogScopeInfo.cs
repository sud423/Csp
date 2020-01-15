using System.Collections.Generic;

namespace Csp.Logger.File
{
    /// <summary>
    /// 有关日志条目的范围信息
    /// </summary>
    public class LogScopeInfo
    {

        /// <summary>
        /// 当Scope只是字符串类型时使用，否则为null。
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 当范围是类似于字典的对象时使用
        /// </summary>
        public Dictionary<string, object> Properties { get; set; }
    }
}
