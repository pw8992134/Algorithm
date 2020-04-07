using System;

namespace Algorithm.SegmentTree
{
    /// <summary>
    /// 线段树(区间树)
    /// </summary>
    public class SegmentTree<E>
    {
        /// <summary>
        /// 创建线段树的原始数组
        /// </summary>
        private readonly E[] _data;

        /// <summary>
        /// 线段树数组
        /// </summary>
        private readonly E[] _segmentTree;

        /// <summary>
        /// 合并接口
        /// </summary>
        private readonly IMerger<E> _merger;

        /// <summary>
        /// 区间树的大小
        /// </summary>
        public int Size => _data.Length;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="merger"></param>
        public SegmentTree(E[] data,IMerger<E> merger)
        {
            _data = data;
            _segmentTree=new E[4*_data.Length];
            _merger = merger;

            BuildSegmentTree(0,0,_data.Length-1);
        }

        /// <summary>
        /// 左孩子
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int Left(int i)
        {
            return 2 * i + 1;
        }

        /// <summary>
        /// 右孩子
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int Right(int i)
        {
            return 2 * i + 2;
        }

        /// <summary>
        /// 获取索引位置元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public E Get(int index)
        {
            if (index < 0 || index >= _data.Length) throw new Exception("index is illegal");
            return _data[index];
        }

        /// <summary>
        /// 建立线段树 O(lgn)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private void BuildSegmentTree(int index,int left,int right)
        {
            if (left == right)
            {
                _segmentTree[index] = _data[left];
                return;
            }

            int leftIndex = Left(index);
            int rightIndex = Right(index);
            int mid = left + (right - left) / 2;
            BuildSegmentTree(leftIndex,left,mid);
            BuildSegmentTree(rightIndex,mid+1,right);
            _segmentTree[index] = _merger.Merge(_segmentTree[leftIndex], _segmentTree[rightIndex]);
        }

        /// <summary>
        /// 更新线段树 O(lgn)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="e"></param>
        public void Update(int index,E e)
        {
            if(index<0 || index>=_data.Length) throw new Exception("index is illegal");
            _data[index] = e;

            Update(0,0,_data.Length-1,index,e);
        }

        /// <summary>
        /// 更新线段树 O(lgn)
        /// </summary>
        /// <param name="index">当前区间树索引</param>
        /// <param name="left">代表区间左边界</param>
        /// <param name="right">代表区间右边界</param>
        /// <param name="cur">更新的索引</param>
        /// <param name="e">更新的值</param>
        private void Update(int index,int left,int right,int cur,E e)
        {
            if (left == right && right == cur)
            {
                _segmentTree[index] = e;
                return;
            }
            int leftIndex = Left(index);
            int rightIndex = Right(index);
            int mid = left + (right - left) / 2;

            if(cur>=mid+1) Update(rightIndex,mid+1,right,cur,e);
            else if(cur<=mid) Update(leftIndex,left,mid,cur,e);
            _segmentTree[index] = _merger.Merge(_segmentTree[leftIndex], _segmentTree[rightIndex]);
        }

        /// <summary>
        /// 区间查询 O(lgn)
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public E Query(int left,int right)
        {
            if(left<0 || left>=_data.Length || right<0 || right>=_data.Length || left>right) throw new Exception("index is illegal");
            return Query(0,0,_data.Length-1,left,right);
        }

        /// <summary>
        /// 区间查询 O(lgn)
        /// </summary>
        /// <param name="index">当前区间树索引</param>
        /// <param name="left">当前区间树索引所对应区间左边界</param>
        /// <param name="right">当前区间树索引所对应区间右边界</param>
        /// <param name="queryLeft">查询的左区间</param>
        /// <param name="queryRight">查询的右区间</param>
        /// <returns></returns>
        private E Query(int index,int left,int right,int queryLeft,int queryRight)
        {
            if (left == queryLeft && right == queryRight)
            {
                return _segmentTree[index];
            }

            int leftIndex = Left(index);
            int rightIndex = Right(index);
            int mid = left + (right - left) / 2;

            if (mid + 1 <= queryLeft) return Query(rightIndex, mid + 1, right, queryLeft, queryRight);
            else if (queryRight <= mid) return Query(leftIndex, left, mid, queryLeft, queryRight);

            return _merger.Merge(Query(leftIndex,left,mid,queryLeft,mid),Query(rightIndex,mid+1,right,mid+1,queryRight));
        }
    }
}