using Csp.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Csp.EF
{
    public static class EFLoggerFactory
    {
        public static readonly ILoggerFactory SqlLoggerFactory = LoggerFactory.Create(builder => {
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Debug).AddDebug();
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information).AddConsole();
#if DEBUG
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information).AddFile();
#endif
        });
    }
}
