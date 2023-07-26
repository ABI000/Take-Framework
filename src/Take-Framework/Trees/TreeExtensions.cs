using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeFramework.Trees
{
    public static class TreeExtensions
    {
        /// <summary>
        /// 仅返回当前需要的节点
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="PrimaryKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public static TSource? ToTreeFirstOrDefault<TSource, PrimaryKey>(this IEnumerable<TSource> source, PrimaryKey primaryKey)
            where TSource : Tree<PrimaryKey>
        {
            CheckTree<TSource, PrimaryKey>(source);
            var output = source.Where(x => x.Id!.Equals(primaryKey)).FirstOrDefault();
            if (output is null)
            {
                return null;
            }
            output.GenerateTree(source);
            return output;
        }
        /// <summary>
        /// 仅返入参中的根节点
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="PrimaryKey"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <exception cref=”System.Exception”>如果有多个根节点将会产生异常</exception>
        public static TSource? ToTree<TSource, PrimaryKey>(this IEnumerable<TSource> source)
            where TSource : Tree<PrimaryKey>
        {
            CheckTree<TSource, PrimaryKey>(source);
            var output = source.Where(x => x.ParentId == null).FirstOrDefault();
            if (output is null)
            {
                return null;
            }
            output.GenerateTree(source);
            return output;
        }

        private static void CheckTree<TSource, PrimaryKey>(IEnumerable<TSource> source) where TSource : Tree<PrimaryKey>
        {
            if (source.Where(x => x.ParentId == null).Count() > 1)
            {
                throw new ArgumentException("具有多个根节点");
            }
        }
    }
}
