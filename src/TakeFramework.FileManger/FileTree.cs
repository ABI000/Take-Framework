
using TakeFramework.Trees;

namespace TakeFramework.FileManger;

public class FileTree : Tree<long>, ITree<long, Tree<long>>
{
    public string FileProviderKey { get; set; }



    
}
