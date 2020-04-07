using System;
using Algorithm.DynamicArray;

namespace Algorithm.Heap
{
    /// <summary>
    /// 最大堆(使用动态数组)
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class MaxHeap<E> where E:IComparable<E>
    {
        /// <summary>
        /// 动态数组
        /// </summary>
        private readonly DynamicArray.Array<E> _array;

        /// <summary>
        /// 堆大小
        /// </summary>
        public int Size => _array.Size;

        /// <summary>
        /// 堆是否为空
        /// </summary>
        public bool IsEmpty => _array.IsEmpty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity"></param>
        public MaxHeap(int capacity)
        {
            _array=new Array<E>(capacity);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MaxHeap()
        {
            _array=new Array<E>();
        }

        /// <summary>
        /// 父节点
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int Parent(int i)
        {
            if(i<=0) throw new Exception("index is illegal");
            return (i - 1) / 2;
        }

        /// <summary>
        /// 左孩子
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int Left(int i)
        {
            return 2 * i+1;
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
        /// 上浮
        /// </summary>
        /// <param name="index"></param>
        private void SiftUp(int index)
        {
            while (index>0 && _array.Get(Parent(index)).CompareTo(_array.Get(index))<0)
            {
                _array.Swap(Parent(index),index);
                index = Parent(index);
            }
        }

        /// <summary>
        /// 向堆中添加元素
        /// </summary>
        /// <param name="e"></param>
        public void Add(E e)
        {
            _array.AddLast(e);
            SiftUp(Size-1);
        }

        /// <summary>
        /// 下沉
        /// </summary>
        private void SiftDown(int index)
        {
            while (Left(index)<Size)
            {
                int j = Left(index);
                if (j + 1 < Size && _array.Get(j).CompareTo(_array.Get(j + 1)) < 0)
                    j = j + 1;
                if(_array.Get(j).CompareTo(_array.Get(index))<=0) 
                    break;
                _array.Swap(j,index);
                index = j;
            }
        }

        /// <summary>
        /// 移除最大的元素
        /// </summary>
        /// <returns></returns>
        public E Remove()
        {
            E e = _array.Get(0);
            _array.Swap(0,Size-1);
            _array.RemoveLast();
            SiftDown(0);
            return e;
        }

        /// <summary>
        /// 获取最大元素
        /// </summary>
        /// <returns></returns>
        public E GetMax()
        {
            return _array.GetFirst();
        }
    }
}