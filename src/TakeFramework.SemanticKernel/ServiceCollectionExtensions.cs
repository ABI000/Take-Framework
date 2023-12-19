using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace TakeFramework.SemanticKernel;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Delegate to register functions with a Semantic Kernel
    /// </summary>
    public delegate Task RegisterFunctionsWithKernel(IServiceProvider sp, Kernel kernel);

    /// <summary>
    /// Delegate for any complimentary setup of the kernel, i.e., registering custom plugins, etc.
    /// See webapi/README.md#Add-Custom-Setup-to-Chat-Copilot's-Kernel for more details.
    /// </summary>
    public delegate Task KernelSetupHook(IServiceProvider sp, Kernel kernel);
    public static IServiceCollection AddSemanticKernelServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.InitializeKernelProvider(configuration);
        services.Configure<SemanticKernelOptions>(configuration.GetSection(SemanticKernelOptions.Position));
        services.AddScoped(sp =>
        {
            var provider = sp.GetRequiredService<SemanticKernelProvider>();
            var kernel = provider.GetCompletionKernel();
            //sp.GetRequiredService<RegisterFunctionsWithKernel>()(sp, kernel);

            //// If KernelSetupHook is not null, invoke custom kernel setup.
            //sp.GetService<KernelSetupHook>()?.Invoke(sp, kernel);
            return kernel;
        });

        services.AddScoped<SemanticKernelService>();
        return services;
    }
    private static void InitializeKernelProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(sp => new SemanticKernelProvider(sp, configuration, sp.GetRequiredService<IHttpClientFactory>()));
    }
    /// <summary>
    /// Register custom hook for any complimentary setup of the kernel.
    /// </summary>
    /// <param name="hook">The delegate to perform any additional setup of the kernel.</param>
    public static IServiceCollection AddKernelSetupHook(this IServiceCollection services, KernelSetupHook hook)
    {
        // Add the hook to the service collection
        services.AddScoped(sp => hook);
        return services;
    }
}
