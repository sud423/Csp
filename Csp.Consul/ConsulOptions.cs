using System.Collections.Generic;

namespace Csp.Consul
{
    public class ConsulOptions
    {

        /// <summary>
        /// 应用程序启动地址
        /// </summary>
        public string ApplicationUrl { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// consul服务器地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
        /// </summary>
        public IEnumerable<string> Tags { get; set; }
    }
}
