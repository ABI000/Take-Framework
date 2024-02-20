using System.Diagnostics.CodeAnalysis;

namespace TakeFramework.FileManger.Local;

public class LocalFileProvider : IFileProvider
{
    public string Key => "Local";


    public void DeleteFile(string bucketName, string fileName, string filePath)
    {
        File.Delete(Path.Combine(filePath, fileName));
    }

    public Stream DownloadFile(string bucketName, string fileName, string filePath)
    {
        return File.Exists(Path.Combine(filePath, fileName)) ? File.OpenRead(Path.Combine(filePath, fileName)) : throw new ArgumentException("文件不存在");
    }

    public void UploadFile(string bucketName, string fileName, string filePath, [NotNull] Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);
        using FileStream fs = File.Create(Path.Combine(filePath, fileName));
        stream.CopyTo(fs);
        stream.Seek(0, SeekOrigin.Begin);
    }
}
