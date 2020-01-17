using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Csp.Logger.File
{
    /// <summary>
    /// 将日志条目写入文本文件的记录程序提供程序
    /// “文件”是此提供程序的提供程序别名，可以在appsettings.json的“日志记录”部分中使用
    /// </summary>
    [ProviderAlias("File")]
    public class FileLoggerProvider : LoggerProvider
    {
        FileLoggerOption _settings;

        bool terminated;
        int counter = 0;
        string filePath;

        ConcurrentQueue<LogMessage> infoQueue = new ConcurrentQueue<LogMessage>();


        public FileLoggerProvider(FileLoggerOption settings)
        {

            _settings = settings;


            //日志文件保留策略
            ApplyRetainPolicy();


            ThreadProc();

            
        }

        /// <summary>
        /// 根据选项应用日志文件保留策略
        /// </summary>
        void ApplyRetainPolicy()
        {
            FileInfo fileInfo;
            try
            {
                var ext = Path.GetExtension(_settings.Path);
                var fullPath = Path.GetFullPath(_settings.Path);
                List<FileInfo> files = new DirectoryInfo(Path.GetDirectoryName(fullPath))
                .GetFiles($"*.{ext}", SearchOption.TopDirectoryOnly)
                .OrderBy(fi => fi.CreationTime)
                .ToList();

                while (_settings.RetainedFileCountLimit.HasValue && files.Count >= _settings.RetainedFileCountLimit)
                {
                    fileInfo = files.First();
                    fileInfo.Delete();
                    files.Remove(fileInfo);
                }
            }
            catch
            {
            }
        }
        
        /// <summary>
        /// 将一行文本写入当前文件
        /// 如果文件达到大小限制，请创建一个新文件并使用该新文件。
        /// </summary>
        void WriteLine(string text)
        {
            // 写入100次后检查文件大小
            counter++;
            if (counter % 100 == 0)
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (_settings.FileSizeLimit.HasValue && fileInfo.Length > (1024 * 1024 * _settings.FileSizeLimit.Value))
                {
                    return;
                }
            }

            System.IO.File.AppendAllText(filePath, text);
        }

        /// <summary>
        /// Pops a log info instance from the stack, prepares the text line, and writes the line to the text file.
        /// </summary>
        void WriteLogLine()
        {
            if (infoQueue.TryDequeue(out LogMessage message))
            {
                var fullPath = Path.GetFullPath(_settings.Path);

                var directory = Path.GetDirectoryName(fullPath);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var format = _settings.RollingInterval.GetFormat();
                var ext = Path.GetExtension(fullPath);
                filePath = fullPath.Replace(ext, $"{message.Timestamp.ToString(format)}{ext}");
                
                WriteLine(message.Message);
            }

        }
        
        void ThreadProc()
        {
            Task.Run(() => {

                while (!terminated)
                {
                    try
                    {
                        WriteLogLine();
                        Thread.Sleep(100);
                    }
                    catch // (Exception ex)
                    {
                    }
                }

            });
        }

        /// <summary>
        /// 配置选项更改代号。 IDisposable模式实现。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            terminated = true;
            base.Dispose(disposing);
        }

        /// <summary>
        /// IOptionsMonitor提供OnChange()方法，当用户更改appsettings.json文件中此提供程序的设置时，将调用此方法。
        /// </summary>
        public FileLoggerProvider(IOptionsMonitor<FileLoggerOption> settings) : this(settings.CurrentValue)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/change-tokens
            _optionsChangeToken = settings.OnChange(setting => {
                _settings = setting;
            });
        }

        public override bool IsEnabled(LogLevel logLevel)
        {
            bool result = logLevel != LogLevel.None
               && _settings.LogLevel != LogLevel.None
               && Convert.ToInt32(logLevel) >= Convert.ToInt32(_settings.LogLevel);

            return result;
        }

        public override void WriteLog(LogMessage message)
        {
            infoQueue.Enqueue(message);
        }
    }
}