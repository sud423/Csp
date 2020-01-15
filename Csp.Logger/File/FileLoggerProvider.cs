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
        FileLoggerOptions _settings;


        bool terminated;
        int counter = 0;
        string filePath;

        Dictionary<string, int> lengths = new Dictionary<string, int>();

        ConcurrentQueue<LogEntry> infoQueue = new ConcurrentQueue<LogEntry>();


        public FileLoggerProvider(FileLoggerOptions settings)
        {
            PrepareLengths();
            _settings = settings;

            // 创建第一个文件
            BeginFile();

            ThreadProc();

            //IWebHostEnvironment
        }

        /// <summary>
        /// 根据选项应用日志文件保留策略
        /// </summary>
        void ApplyRetainPolicy()
        {
            FileInfo FI;
            try
            {
                var ext = Path.GetExtension(_settings.Path);
                var flode = Path.GetFullPath(_settings.Path);
                List<FileInfo> FileList = new DirectoryInfo(flode)
                .GetFiles($"*.{ext}", SearchOption.TopDirectoryOnly)
                .OrderBy(fi => fi.CreationTime)
                .ToList();

                while (FileList.Count >= _settings.RetainedFileCountLimit)
                {
                    FI = FileList.First();
                    FI.Delete();
                    FileList.Remove(FI);
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
                FileInfo FI = new FileInfo(filePath);
                if (FI.Length > (1024 * 1024 * _settings.FileSizeLimit))
                {
                    BeginFile();
                }
            }

            System.IO.File.AppendAllText(filePath, text);
        }

        /// <summary>
        /// 用空格填充字符串到最大长度。如果字符串超出限制，则将字符串截断为最大长度。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        string Pad(string text, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "".PadRight(maxLength);

            if (text.Length > maxLength)
                return text.Substring(0, maxLength);

            return text.PadRight(maxLength);
        }


        /// <summary>
        /// 准备日志文件中列的长度
        /// </summary>
        void PrepareLengths()
        {
            //准备长度表格
            lengths["Time"] = 24;
            lengths["Host"] = 16;
            lengths["User"] = 16;
            lengths["Level"] = 14;
            lengths["EventId"] = 32;
            lengths["Category"] = 92;
            lengths["Scope"] = 64;
        }
        /// <summary>
        /// Creates a new disk file and writes the column titles
        /// </summary>
        void BeginFile()
        {
            Directory.CreateDirectory(_settings.Path);
            //filePath = Path.Combine(_settings.Folder, LogEntry.StaticHostName + "-" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".log");

            // titles
            StringBuilder sb = new StringBuilder();
            sb.Append(Pad("Time", lengths["Time"]));
            sb.Append(Pad("Host", lengths["Host"]));
            sb.Append(Pad("User", lengths["User"]));
            sb.Append(Pad("Level", lengths["Level"]));
            sb.Append(Pad("EventId", lengths["EventId"]));
            sb.Append(Pad("Category", lengths["Category"]));
            sb.Append(Pad("Scope", lengths["Scope"]));
            sb.AppendLine("Text");

            System.IO.File.WriteAllText(filePath, sb.ToString());

            ApplyRetainPolicy();
        }
        /// <summary>
        /// Pops a log info instance from the stack, prepares the text line, and writes the line to the text file.
        /// </summary>
        void WriteLogLine()
        {
            LogEntry Info = null;
            if (infoQueue.TryDequeue(out Info))
            {
                string S;
                StringBuilder sb = new StringBuilder();
                sb.Append(Pad(Info.Timestamp.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.ff"), lengths["Time"]));
                sb.Append(Pad(Info.HostName, lengths["Host"]));
                sb.Append(Pad(Info.UserName, lengths["User"]));
                sb.Append(Pad(Info.Level.ToString(), lengths["Level"]));
                sb.Append(Pad(Info.EventId != null ? Info.EventId.ToString() : "", lengths["EventId"]));
                sb.Append(Pad(Info.Category, lengths["Category"]));

                S = "";
                if (Info.Scopes != null && Info.Scopes.Count > 0)
                {
                    LogScopeInfo SI = Info.Scopes.Last();
                    if (!string.IsNullOrWhiteSpace(SI.Text))
                    {
                        S = SI.Text;
                    }
                    else
                    {
                    }
                }
                sb.Append(Pad(S, lengths["Scope"]));

                string Text = Info.Text;

                /* writing properties is too much for a text file logger
                if (Info.StateProperties != null && Info.StateProperties.Count > 0)
                {
                    Text = Text + " Properties = " + Newtonsoft.Json.JsonConvert.SerializeObject(Info.StateProperties);
                }                 
                 */

                if (!string.IsNullOrWhiteSpace(Text))
                {
                    sb.Append(Text.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " "));
                }

                sb.AppendLine();
                WriteLine(sb.ToString());
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

        /* overrides */
        /// <summary>
        /// Disposes the options change toker. IDisposable pattern implementation.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            terminated = true;
            base.Dispose(disposing);
        }

        /* construction */
        /// <summary>
        /// Constructor.
        /// <para>The IOptionsMonitor provides the OnChange() method which is called when the user alters the settings of this provider in the appsettings.json file.</para>
        /// </summary>
        public FileLoggerProvider(IOptionsMonitor<FileLoggerOptions> settings)
            : this(settings.CurrentValue)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/change-tokens
            SettingsChangeToken = settings.OnChange(setting => {
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

        public override void WriteLog(LogEntry Info)
        {
            throw new System.NotImplementedException();
        }
    }
}