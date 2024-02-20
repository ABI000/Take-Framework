using System.Collections.ObjectModel;

namespace TakeFramework.FileManger;

/// <summary>
/// 需要时用单例，避免出现并发更改，
/// 后期考虑改为同步队列处理
/// 不进行路径推理
/// </summary>
/// <param name="fileFactory"></param>
/// <param name="fileTreeProvider"></param>
public class DefaultFileManger(FileFactory fileFactory, IFileTreeProvider fileTreeProvider) : FileManger(fileFactory, fileTreeProvider), IFileManger
{

    public override void Copy(string targetPath, Collection<string> folders)
    {
        throw new NotImplementedException();
    }

    public override void Create(Stream fileStream, string targetPath, string fileName)
    {
        var fileProvider = fileFactory.GetFileProvider();
        fileProvider.UploadFile("", fileName, targetPath, fileStream);

    }

    public override void Delete(Collection<string> folders)
    {
        //根据文件/文件夹树进行删除
        List<FileTree> ss = _fileTree.GetAllChildList<FileTree>();
        ss.ToDictionary(x => x.FileProviderKey);

    }

    public override void Move(string targetPath, Collection<string> folders)
    {
        throw new NotImplementedException();
    }

    public override void Remove(Collection<string> folders)
    {
        throw new NotImplementedException();
    }
}
