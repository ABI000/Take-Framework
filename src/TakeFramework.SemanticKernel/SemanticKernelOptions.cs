namespace TakeFramework.SemanticKernel;

public class SemanticKernelOptions
{
    public static readonly string Position = nameof(SemanticKernelOptions);

    public required string TextGeneratorType { get; set; }

    public required string APIKey { get; set; }
    /// <summary>
    /// azure open id
    /// </summary>
    public string Deployment { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public string ModelId { get; set; } = string.Empty;
    /// <summary>
    /// open ai
    /// </summary>
    public string TextModel { get; set; } = string.Empty;
}
