using System;
using Algorithm.BinarySeachTree;

namespace Algorithm.Set
{
    /// <summary>
    /// 基于二分搜索树的集合
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class BinarySearchTreeSet<E>:ISet<E> where E :IComparable<E>
    {
        /// <summary>
        /// 二分搜索树
        /// </summary>
        private readonly BinarySearchTree<E> _binarySearchTree;

        /// <summary>
        /// 构造函数
        /// </summary>
        public BinarySearchTreeSet()
        {
            _binarySearchTree=new BinarySearchTree<E>();
        }

        /// <summary>
        /// 添加 O(lgn)
        /// </summary>
        /// <param name="e"></param>
        public void Add(E e)
        {
            _binarySearchTree.Add(e);
        }

        /// <summary>
        /// 删除 O(lgn)
        /// </summary>
        /// <param name="e"></param>
        public void Remove(E e)
        {
            _binarySearchTree.Remove(e);
        }

        /// <summary>
        /// 是否包含某个元素 O(lgn)
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool Contains(E e)
        {
            return _binarySearchTree.Contains(e);
        }

        /// <summary>
        /// 集合大小
        /// </summary>
        public int Size => _binarySearchTree.Size;

        /// <summary>
        /// 集合是否为空
        /// </summary>
        public bool IsEmpty => _binarySearchTree.IsEmpty;
    }
}