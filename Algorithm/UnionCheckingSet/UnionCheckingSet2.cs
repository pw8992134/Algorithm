using System;

namespace Algorithm.UnionCheckingSet
{
    /// <summary>
    /// 基于树的并查集实现 Quick Union
    /// 合并两个集合,即使一个集合的根节点指向另一个集合的根节点
    /// </summary>
    public class UnionCheckingSet2
    {
        /// <summary>
        /// 并查集数组
        /// </summary>
        private readonly int[] _parent;

        /// <summary>
        /// 大小
        /// </summary>
        public int Size => _parent.Length;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="size"></param>
        public UnionCheckingSet2(int size)
        {
            _parent = new int[size];
            for (int i = 0; i < size; i++)
            {
                _parent[i] = i;
            }
        }

        /// <summary>
        /// 查找元素所属于集合的索引 O (h)
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private int Find(int p)
        {
            if (p < 0 || p >= _parent.Length) throw new Exception("index is illegal");
            while (p!=_parent[p])
            {
                p = _parent[p];
            }

            return p;
        }

        /// <summary>
        /// 两个顶点是否联通 O (h)
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public bool IsConnected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        /// <summary>
        /// 合并两个元素到一个集合 O (h)
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        public void UnionElements(int p, int q)
        {
            int pId = Find(p);
            int qId = Find(q);
            if (pId == qId) return;
            _parent[p] = qId;
        }
    }
}