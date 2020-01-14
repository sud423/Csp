using Microsoft.Extensions.Logging;
using System;

namespace Csp.Logger.File
{
    public class FileLoggerOptions
    {
        private long? _fileSizeLimit = 1L * 1024 * 1024 * 1024;
        private int? _retainedFileCountLimit = 31; // A long month of logs
        private string _logDirectory = "logs";
        private string _fileName = "log";

        /// <summary>
        /// 活动日志级别。默认为LogLevel.Information
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Information;

        /// <summary>
        /// 获取或设置指示日志写入目录的值
        /// </summary>
        public string LogDirectory
        {
            get { return _logDirectory; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(value));
                }
                _logDirectory = value;
            }
        }

        /// <summary>
        /// 获取或设置一个字符串，该字符串表示用于存储日志记录信息的文件名的前缀
        /// 在给定值之后，将以YYYYMMDD格式添加当前日期
        /// 默认为 <c>log</c>.
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(value));
                }
                _fileName = value;
            }
        }

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
                    throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(FileSizeLimit)}不能为null");
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
                    throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(RetainedFileCountLimit)}不能为null");
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
