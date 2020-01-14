using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace Csp.Logger.File
{
    public class FileLoggerOptionsSetup : ConfigureFromConfigurationOptions<FileLoggerOptions>
    {
        public FileLoggerOptionsSetup(ILoggerProviderConfiguration<FileLoggerProvider> providerConfiguration)
            : base(providerConfiguration.Configuration)
        {

        }
    }
}
