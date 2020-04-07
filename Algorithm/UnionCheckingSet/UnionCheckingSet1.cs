using System;

namespace Algorithm.UnionCheckingSet
{
    /// <summary>
    /// 基于普通的数组实现并查集 Quick Find
    /// </summary>
    public class UnionCheckingSet1
    {
        /// <summary>
        /// 并查集数组
        /// </summary>
        private readonly int[] _data;

        /// <summary>
        /// 大小
        /// </summary>
        public int Size => _data.Length;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="size"></param>
        public UnionCheckingSet1(int size)
        {
            _data=new int[size];
            for (int i = 0; i < size; i++)
            {
                _data[i] = i;
            }
        }

        /// <summary>
        /// 查找元素所属于集合的索引 O (1)
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private int Find(int p)
        {
            if(p<0 || p>=_data.Length) throw new Exception("index is illegal");
            return _data[p];
        }

        /// <summary>
        /// 两个顶点是否联通 O (1)
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public bool IsConnected(int p,int q)
        {
            return Find(p)==Find(q);
        }

        /// <summary>
        /// 合并两个元素到一个集合 O (n)
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        public void UnionElements(int p,int q)
        {
            int pId = Find(p);
            int qId = Find(q);
            if(pId==qId) return;
            for (int i = 0; i < Size; i++)
            {
                if (_data[i] == pId) _data[i] = qId;
            }
        }
    }
}