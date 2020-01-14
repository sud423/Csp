using System;

namespace Csp.Logger.Batching
{
    class BatchingLoggerOptions
    {
        private int? _batchSize = 32;
        private int? _backgroundQueueSize = 1000;
        private TimeSpan _flushPeriod = TimeSpan.FromSeconds(1);

        /// <summary>
        /// 获取或设置将日志刷新到存储区之前的时间。
        /// </summary>
        public TimeSpan FlushPeriod
        {
            get { return _flushPeriod; }
            set
            {
                if (value <= TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(FlushPeriod)}不能为空");
                }
                _flushPeriod = value;
            }
        }

        /// <summary>
        /// 获取或设置后台日志消息队列的最大大小；如果没有限制，则为null。
        /// 达到最大队列大小后，日志事件接收器将开始阻塞。
        /// 默认为 <c>1000</c>.
        /// </summary>
        public int? BackgroundQueueSize
        {
            get { return _backgroundQueueSize; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(BackgroundQueueSize)} must be non-negative.");
                }
                _backgroundQueueSize = value;
            }
        }

        /// <summary>
        /// 获取或设置要包含在单个批处理中的最大事件数，如果没有限制则为null。
        /// </summary>
        public int? BatchSize
        {
            get { return _batchSize; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(BatchSize)} must be positive.");
                }
                _batchSize = value;
            }
        }

        /// <summary>
        /// 获取或设置值，该值指示记录器是否接受写入并将其排队。
        /// </summary>
        public bool IsEnabled { get; set; } = true;
    }
}
