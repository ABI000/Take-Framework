using System.Collections.ObjectModel;

namespace TakeFramework.Trees
{
    /// <summary>
    /// 使用多叉树实现方式进行处理
    /// </summary>
    /// <typeparam name="PrimaryKey"></typeparam>
    public class Tree<PrimaryKey> : ITree<PrimaryKey, Tree<PrimaryKey>>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public required PrimaryKey Id { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        public PrimaryKey? ParentId { get; set; }

        /// <summary>
        /// 父级主键
        /// </summary>
        public Tree<PrimaryKey>? Parent { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public Collection<Tree<PrimaryKey>>? ChildList { get; set; }
        public required string Name { get; set; } = "";
        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public required string Path { get; set; } = "";



        public void AddChild(Tree<PrimaryKey> tree)
        {
            if (this.ChildList is null)
            {
                this.ChildList = [tree];
            }
            else
            {
                this.ChildList.Add(tree);
            }
        }
        /// <summary>
        /// 获取所有父级
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllParentList<T>() where T : Tree<PrimaryKey>
        {
            var output = new List<T>();
            Tree<PrimaryKey>.GetAllParentList(output, this);
            return output;
        }

        private static void GetAllParentList<T>(List<T> output, Tree<PrimaryKey> node) where T : Tree<PrimaryKey>
        {
            if (node.ParentId is not null)
            {
                output.Add((T)node.Parent);
                Tree<PrimaryKey>.GetAllParentList(output, node.Parent);
            }
        }
        /// <summary>
        /// 获取所有子集
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllChildList<T>() where T : Tree<PrimaryKey>
        {
            var output = new List<T>();
            GetAllChildList(output, this);
            return output;
        }
        private static void GetAllChildList<T>(List<T> output, Tree<PrimaryKey> node) where T : Tree<PrimaryKey>
        {
            if (node.ChildList is not null)
            {
                foreach (var child in node.ChildList)
                {
                    output.Add((T)child);
                    Tree<PrimaryKey>.GetAllChildList(output, child);
                }
            }
        }

        /// <summary>
        /// 查找子级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T? FindChildNode<T>(PrimaryKey id) where T : Tree<PrimaryKey>
        {
            if (Id.Equals(id))
            {
                return (T)this;
            }
            foreach (var child in ChildList)
            {
                T? foundNode = child.FindChildNode<T>(Id);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }
            return null;
        }
        /// <summary>
        /// 查找父级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T? FindParentNode<T>(PrimaryKey id) where T : Tree<PrimaryKey>
        {
            if (ParentId.Equals(id))
            {
                return (T)Parent;
            }
            T? foundNode = Parent.FindParentNode<T>(Id);
            if (foundNode is not null)
            {
                return foundNode;
            }
            return null;
        }

        public static T? GenerateTree<T, PrimaryKey>(IEnumerable<T> source) where T : Tree<PrimaryKey>
        {
            return TreeHelper.GenerateTree<T, PrimaryKey>(source);
        }

    }


    public static class TreeHelper
    {
        /// <summary>
        /// 创建树
        /// </summary>
        /// <returns></returns>
        /// <exception cref="">存在多个根节点则会报错</exception>
        public static T? GenerateTree<T, PrimaryKey>(IEnumerable<T> source) where T : Tree<PrimaryKey>
        {
            Dictionary<PrimaryKey, T> nodeLookup = source.ToDictionary(x => x.Id);
            if (source.Count(x => x.ParentId is null) > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(source), "多个根节点");
            }
            T? output = default;
            foreach (var nodeInfo in source)
            {
                if (nodeInfo.ParentId is not null)
                {
                    output = nodeInfo;
                }
                else
                {
                    if (nodeLookup.TryGetValue(nodeInfo.ParentId, out T? parentNode))
                    {
                        nodeInfo.Parent = parentNode;
                        parentNode.AddChild(nodeInfo);
                    }
                }
            }
            return output;
        }

        /// <summary>
        /// 获取子级
        /// </summary>
        /// <returns></returns>
        public static Collection<Tree<PrimaryKey>> GetAllChildList<PrimaryKey>(IEnumerable<Tree<PrimaryKey>> source, PrimaryKey id)
        {
            Dictionary<PrimaryKey, Tree<PrimaryKey>> nodeLookup = source.ToDictionary(x => x.Id);
            if (source.Count(x => x.ParentId is null) > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(source), "多个根节点");
            }
            foreach (var nodeInfo in source)
            {
                if (nodeLookup.TryGetValue(nodeInfo.ParentId, out Tree<PrimaryKey>? parentNode))
                {
                    nodeInfo.Parent = parentNode;
                    parentNode.AddChild(nodeInfo);
                }
            }
            return nodeLookup[id].ChildList;
        }

        /// <summary>
        /// 获取父级
        /// </summary>
        /// <returns></returns>
        public static List<T> GetAllParentList<T, PrimaryKey>(IEnumerable<T> source, PrimaryKey id) where T : Tree<PrimaryKey>
        {
            var d = FindNode(source, id);
            return d.GetAllParentList<T>();
        }
        /// <summary>
        /// 获取节点
        /// </summary>
        /// <typeparam name="PrimaryKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static T? FindNode<T, PrimaryKey>(IEnumerable<T> source, PrimaryKey id) where T : Tree<PrimaryKey>
        {
            T? output = source.FirstOrDefault(x => x.Id.Equals(id));

            if (output is not null)
            {
                GenerateTree<T, PrimaryKey>(source);
                return output;
            }
            else
            {
                return null;
            }
        }
    }
}
