using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;

namespace TakeFramework.SemanticKernel;

/// <summary>
/// Extension methods for registering Semantic Kernel related services.
/// </summary>
public sealed class SemanticKernelProvider
{

    private readonly IKernelBuilder _builderChat;
    // private readonly KernelBuilder _builderPlanner;

    public SemanticKernelProvider(IServiceProvider serviceProvider, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        this._builderChat = InitializeCompletionKernel(serviceProvider, configuration, httpClientFactory);
    }

    /// <summary>
    /// Produce semantic-kernel with only completion services for chat.
    /// </summary>
    public Kernel GetCompletionKernel()
    {
        return _builderChat.Build();
    }

    // /// <summary>
    // /// Produce semantic-kernel with only completion services for planner.
    // /// </summary>
    // public IKernel GetPlannerKernel() => this._builderPlanner.Build();


    private static IKernelBuilder InitializeCompletionKernel(
        IServiceProvider serviceProvider,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        var builder = Kernel.CreateBuilder();
        //builder.WithLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>());

        var semanticKernelOptions = serviceProvider.GetRequiredService<IOptions<SemanticKernelOptions>>().Value;

        switch (semanticKernelOptions.TextGeneratorType)
        {
            case string x when x.Equals("AzureOpenAI", StringComparison.OrdinalIgnoreCase):
            case string y when y.Equals("AzureOpenAIText", StringComparison.OrdinalIgnoreCase):
                //var azureAIOptions = memoryOptions.GetServiceConfig<AzureOpenAIConfig>(configuration, "AzureOpenAIText");
#pragma warning disable CA2000 // No need to dispose of HttpClient instances from IHttpClientFactory
                builder.AddAzureOpenAIChatCompletion(semanticKernelOptions.Deployment,
                    semanticKernelOptions.ModelId,
                    semanticKernelOptions.Endpoint,
                    semanticKernelOptions.APIKey,
                    httpClient: httpClientFactory.CreateClient());
#pragma warning restore CA2000
                break;

            case string x when x.Equals("OpenAI", StringComparison.OrdinalIgnoreCase):
                //var openAIOptions = memoryOptions.GetServiceConfig<OpenAIConfig>(configuration, "OpenAI");
#pragma warning disable CA2000 // No need to dispose of HttpClient instances from IHttpClientFactory
                builder.AddOpenAIChatCompletion(semanticKernelOptions.TextModel,
                    semanticKernelOptions.APIKey,
                    httpClient: httpClientFactory.CreateClient());
#pragma warning restore CA2000
                break;

            default:
                throw new ArgumentException($"Invalid {nameof(semanticKernelOptions.TextGeneratorType)} value in 'KernelMemory' settings.");
        }

        return builder;
    }

}
