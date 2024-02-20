namespace TakeFramework.FileManger;

public interface IFileProvider
{
    public string Key { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bucketName"></param>
    /// <param name="fileName"></param>
    /// <param name="filePath"></param>
    void UploadFile(string bucketName, string fileName, string filePath, Stream stream);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bucketName"></param>
    /// <param name="fileName"></param>
    /// <param name="filePath"></param>
    Stream DownloadFile(string bucketName, string fileName, string filePath);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bucketName"></param>
    /// <param name="fileName"></param>
    /// <param name="filePath"></param>
    /// <param name="stream"></param>
    void DeleteFile(string bucketName, string fileName, string filePath);
}
