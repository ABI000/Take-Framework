
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace TakeFramework.FileManger;
/// <summary>
/// 
/// </summary>
public abstract class FileManger : IFileManger
{
    /// <summary>
    /// 
    /// </summary>
    protected readonly FileTree _fileTree;
    /// <summary>
    /// 
    /// </summary>
    protected readonly FileFactory _fileFactory;
    /// <summary>
    /// 
    /// </summary>
    public FileManger(FileFactory fileFactory, [NotNull] IFileTreeProvider fileTreeProvider)
    {
        _fileFactory = fileFactory;
        _fileTree = CreateTree(fileTreeProvider.GetFileTrees()) ?? throw new ArgumentException("无法组装文件树");
    }
    private FileTree? CreateTree(List<FileTree> fileTrees)
    {
        return FileTree.GenerateTree<FileTree, long>(fileTrees);
    }

    /// <summary>
    /// 拷贝
    /// </summary>
    /// <param name="targetPath"></param>
    /// <param name="folders"></param>
    public abstract void Copy(string targetPath, Collection<string> folders);
    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="fileStream"></param>
    public abstract void Create(Stream fileStream, string targetPath, string fileName);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="folders"></param>
    public abstract void Delete(Collection<string> folders);
    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="targetPath"></param>
    /// <param name="folders"></param>
    public abstract void Move(string targetPath, Collection<string> folders);
    /// <summary>
    /// 抹除
    /// </summary>
    /// <param name="folders"></param>
    public abstract void Remove(Collection<string> folders);


}
