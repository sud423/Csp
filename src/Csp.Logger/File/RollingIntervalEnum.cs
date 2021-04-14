namespace Csp.Logger.File
{
    /// <summary>
    /// 指定日志文件滚动的频率。
    /// </summary>
    public enum RollingIntervalEnum
    {
        /// <summary>
        /// 每年滚动。文件名的格式为<code>yyyy</code>后将附加一个四位数的年份。
        /// </summary>
        Year,

        /// <summary>
        /// 每个日历月滚动一次。文件名将附加<code>yyyyMM</code>
        /// </summary>
        Month,

        /// <summary>
        /// 每天滚动。文件名将附加<code>yyyyMMdd</code>。
        /// </summary>
        Day,

        /// <summary>
        /// 每小时滚动一次。文件名将附加<code>yyyyMMddHH</code>
        /// </summary>
        Hour,

        /// <summary>
        /// 每分钟滚动。文件名将附加<code> yyyyMMddHHmm </ code>。
        /// </summary>
        Minute
    }
}
