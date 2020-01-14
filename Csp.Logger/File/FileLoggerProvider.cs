using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Csp.Logger.File
{
    [ProviderAlias("File")]
    public class FileLoggerProvider : ILoggerProvider
    {
        public bool IsEnabled { get; private set; }

        private readonly FileLoggerOptions _options;

        public FileLoggerProvider(IOptionsMonitor<FileLoggerOptions> options)
        {
            _options = options.CurrentValue;
        }

        /* public */
        /// <summary>
        /// Checks if the given logLevel is enabled. It is called by the Logger.
        /// </summary>
        //public override bool IsEnabled(LogLevel logLevel)
        //{
        //    bool Result = logLevel != LogLevel.None
        //       && this.Settings.LogLevel != LogLevel.None
        //       && Convert.ToInt32(logLevel) >= Convert.ToInt32(this.Settings.LogLevel);

        //    return Result;
        //}

        internal void WriteMessages(LogMessage message)
        {
            Directory.CreateDirectory(_options.LogDirectory);

            //foreach (var group in messages.GroupBy(GetGrouping))
            //{
                var fullName = Path.Combine(_options.LogDirectory, _options.FileName + GetGrouping(message) + ".log");
                var fileInfo = new FileInfo(fullName);
                if (_options.FileSizeLimit > 0 && fileInfo.Exists && fileInfo.Length > _options.FileSizeLimit)
                {
                    return;
                }
                using (var streamWriter = System.IO.File.AppendText(fullName))
                {
                    //foreach (var item in group)
                    //{
                        streamWriter.Write(message.Message);
                    //}
                    //await streamWriter.FlushAsync();
                }
            //}

            RollFiles();
        }

        protected string GetGrouping(LogMessage message)
        {
            return message.Timestamp.ToString(_options.RollingInterval.GetFormat());
        }

        protected void RollFiles()
        {
            if (_options.RetainedFileCountLimit > 0)
            {
                var files = new DirectoryInfo(_options.LogDirectory)
                    .GetFiles(_options.FileName + "*")
                    .OrderByDescending(f => f.Name)
                    .Skip(_options.RetainedFileCountLimit.Value);

                foreach (var item in files)
                {
                    item.Delete();
                }
            }
        }

        public ILogger CreateLogger(string categoryName)
        {
            //return new finle(this, categoryName);
            return null;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}