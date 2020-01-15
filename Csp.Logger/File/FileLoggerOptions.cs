using Microsoft.Extensions.Logging;
using System;

namespace Csp.Logger.File
{
    public class FileLoggerOptions
    {
        private long? _fileSizeLimit; //每个文件的最大日志大小（以字节为单位）
        private int? _retainedFileCountLimit = 31; // 保留一个月的日志


        /// <summary>
        /// 活动日志级别。默认为LogLevel.Information
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Information;

        /// <summary>
        /// 获取或设置指示日志写入文件路径
        /// </summary>
        public string Path { get; set; } = "logs/log.log";


        /// <summary>
        /// 获取或设置一个严格的正值，该值表示最大日志大小（以字节为单位）或null（无限制）。
        /// 一旦日志已满，将不再附加任何消息。
        /// 默认为<c>1GB</c>.
        /// </summary>
        public long? FileSizeLimit
        {
            get { return _fileSizeLimit; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(FileSizeLimit)}不为负值");
                }
                _fileSizeLimit = value;
            }
        }

        /// <summary>
        /// 获取或设置一个严格的正值，该值表示最大保留文件数；如果没有限制，则为null。
        /// 默认为<c>31</c>.
        /// </summary>
        public int? RetainedFileCountLimit
        {
            get { return _retainedFileCountLimit; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(RetainedFileCountLimit)}不为负值");
                }
                _retainedFileCountLimit = value;
            }
        }

        /// <summary>
        /// 获取或设置日志文件滚动的频率。
        /// </summary>
        public RollingIntervalEnum RollingInterval { get; set; }
    }
}
