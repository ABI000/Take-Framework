using System.Collections.Generic;

namespace TakeFramework.Tree
{
    /*
     * NodeId不为空，但是ParentId可为空
    *  由于引用类型和值类型的泛型约束无法共用，
    * string为引用类型，值类型本身可空
    * int为值类型，Nullable<T>是结构体，T只能使用值类型
     
     */

    /// <summary>
    /// 树形结构基本
    /// 请使用引用类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITree<T, U> : ITree
            where U : class
    {
        /// <summary>
        /// 节点id
        /// </summary>
        public U Id { get; set; }

        /// <summary>
        /// 父级节点id
        /// </summary>
        public U ParentId { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<T> ChildList { get; set; }
    }
    /// <summary>
    /// 请使用值类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public interface ITreeValueType<T, U> : ITree
            where U : struct
    {
        /// <summary>
        /// 节点id
        /// </summary>
        public U Id { get; set; }

        /// <summary>
        /// 父级节点id
        /// </summary>
        public U? ParentId { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<T> ChildList { get; set; }
    }
    public interface ITree
    {
        /// <summary>
        /// 节点名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 路径
        /// 示例：a，b，c
        /// </summary>
        public string Path { get; set; }
    }
}
