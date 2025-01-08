using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Extensions.Logging;

namespace TakeFramework.Serilog
{
    public static class ServiceCollectionExtensions
    {
        public static IHostBuilder UseSerilog(this IHostBuilder builder, ILogger logger = null, bool dispose = false, LoggerProviderCollection providers = null)
        {

            return builder.UseSerilog(logger, dispose, providers);
        }
        public static IHostBuilder UseSerilog(this IHostBuilder builder, Action<HostBuilderContext, LoggerConfiguration> configureLogger, bool preserveStaticLogger = false, bool writeToProviders = false)
        {
            return builder.UseSerilog(configureLogger, preserveStaticLogger, writeToProviders);
        }

        public static IHostBuilder UseSerilog(this IHostBuilder builder, Action<HostBuilderContext, IServiceProvider, LoggerConfiguration> configureLogger, bool preserveStaticLogger = false, bool writeToProviders = false)
        {
            return builder.UseSerilog(configureLogger, preserveStaticLogger, writeToProviders);
        }
    }
}