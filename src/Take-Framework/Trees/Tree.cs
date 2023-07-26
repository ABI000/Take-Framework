using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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
        public PrimaryKey Id { get; set; }
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
        public List<Tree<PrimaryKey>>? ChildList { get; set; }
        [NotNull]
        public string Name { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }



        public void AddChild(Tree<PrimaryKey> tree)
        {
            if (this.ChildList is null)
            {
                this.ChildList = new List<Tree<PrimaryKey>> { tree };
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
        public List<Tree<PrimaryKey>> GetAllParentList()
        {
            var output = new List<Tree<PrimaryKey>>();
            GetAllParentList(output, this);
            return output;
        }
        private void GetAllParentList(List<Tree<PrimaryKey>> output, Tree<PrimaryKey> node)
        {
            if (node.ParentId is not null)
            {
                output.Add(node.Parent);
                GetAllParentList(output, node.Parent);
            }
        }
        /// <summary>
        /// 获取所有子集
        /// </summary>
        /// <returns></returns>
        public List<Tree<PrimaryKey>> GetAllChildList()
        {
            var output = new List<Tree<PrimaryKey>>();
            GetAllChildList(output, this);
            return output;
        }
        private void GetAllChildList(List<Tree<PrimaryKey>> output, Tree<PrimaryKey> node)
        {
            if (node.ChildList is not null)
            {
                output.AddRange(node.ChildList);
                foreach (var child in node.ChildList)
                {
                    GetAllChildList(output, child);
                }
            }
        }

        /// <summary>
        /// 查找子级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tree<PrimaryKey>? FindNode(PrimaryKey id)
        {
            if (Id.Equals(id))
            {
                return this;
            }
            foreach (var child in ChildList)
            {
                var foundNode = child.FindNode(Id);
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
        public Tree<PrimaryKey>? FindParentNode(PrimaryKey id)
        {
            if (ParentId.Equals(id))
            {
                return Parent;
            }
            var foundNode = Parent.FindParentNode(Id);
            if (foundNode != null)
            {
                return foundNode;
            }
            return null;
        }

        public Tree<PrimaryKey> GenerateTree(IEnumerable<Tree<PrimaryKey>> source)
        {
            return TreeHelper.GenerateTree(source);
        }

    }


    public static class TreeHelper
    {
        /// <summary>
        /// 创建树
        /// </summary>
        /// <returns></returns>
        /// <exception cref="">存在多个根节点则会报错</exception>
        public static Tree<PrimaryKey> GenerateTree<PrimaryKey>(IEnumerable<Tree<PrimaryKey>> source)
        {
            Dictionary<PrimaryKey, Tree<PrimaryKey>> nodeLookup = source.ToDictionary(x => x.Id);
            if (source.Count(x => x.ParentId is null) > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(source), "多个根节点");
            }
            Tree<PrimaryKey> output = default;
            foreach (var nodeInfo in source)
            {
                if (nodeInfo.ParentId is not null)
                {
                    output = nodeInfo;
                }
                else
                {
                    if (nodeLookup.TryGetValue(nodeInfo.ParentId, out Tree<PrimaryKey>? parentNode))
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
        public static List<Tree<PrimaryKey>> GetAllChildList<PrimaryKey>(IEnumerable<Tree<PrimaryKey>> source, PrimaryKey id)
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
        public static List<Tree<PrimaryKey>> GetAllParentList<PrimaryKey>(IEnumerable<Tree<PrimaryKey>> source, PrimaryKey id)
        {
            var d = FindNode(source, id);
            d.GetAllParentList();
            return new List<Tree<PrimaryKey>>();
        }
        /// <summary>
        /// 获取节点
        /// </summary>
        /// <typeparam name="PrimaryKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Tree<PrimaryKey>? FindNode<PrimaryKey>(IEnumerable<Tree<PrimaryKey>> source, PrimaryKey id)
        {
            Dictionary<PrimaryKey, Tree<PrimaryKey>> nodeLookup = source.ToDictionary(x => x.Id);
            if (!nodeLookup.TryGetValue(id, out Tree<PrimaryKey>? output))
            {
                return null;
            }
            foreach (var nodeInfo in source)
            {
                if (nodeLookup.TryGetValue(nodeInfo.ParentId, out Tree<PrimaryKey>? parentNode))
                {
                    nodeInfo.Parent = parentNode;
                    parentNode.AddChild(nodeInfo);
                }
            }
            return output;
        }
    }
}
