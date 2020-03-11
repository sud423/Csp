using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace Csp.Logger.File
{
    public class FileLoggerOptionSetup : ConfigureFromConfigurationOptions<FileLoggerOptions>
    {
        public FileLoggerOptionSetup(ILoggerProviderConfiguration<FileLoggerProvider> providerConfiguration)
            : base(providerConfiguration.Configuration)
        {

        }
    }
}
