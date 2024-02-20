namespace TakeFramework.FileManger;

public class FileFactory(List<IFileProvider> fileProviders)
{
    private readonly Dictionary<string, IFileProvider> _fileProviderDic = fileProviders.ToDictionary(x => x.Key);

    public IFileProvider GetFileProvider(string? key = null)
    {
        key ??= "Local";
        return _fileProviderDic.TryGetValue(key, out IFileProvider output) ? output : throw new AggregateException($"不存在 {key} 文件提供器");
    }
}