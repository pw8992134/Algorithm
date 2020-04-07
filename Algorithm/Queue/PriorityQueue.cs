using System;
using Algorithm.Heap;

namespace Algorithm.Queue
{
    /// <summary>
    /// 优先队列
    /// </summary>
    public class PriorityQueue<E>:IQueue<E> where E:IComparable<E>
    {
        /// <summary>
        /// 最大堆
        /// </summary>
        private readonly MaxHeap<E> _maxHeap;

        /// <summary>
        /// 优先队列是否为空
        /// </summary>
        public bool IsEmpty => _maxHeap.IsEmpty;

        /// <summary>
        /// 优先队列大小
        /// </summary>
        public int Size => _maxHeap.Size;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity"></param>
        public PriorityQueue(int capacity)
        {
            _maxHeap=new MaxHeap<E>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PriorityQueue()
        {
            _maxHeap=new MaxHeap<E>();
        }

        /// <summary>
        /// 获取队列首元素
        /// </summary>
        /// <returns></returns>
        public E GetFront()
        {
            return _maxHeap.GetMax();
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="e"></param>
        public void Enqueue(E e)
        {
            _maxHeap.Add(e);
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <returns></returns>
        public E Dequeue()
        {
            return _maxHeap.Remove();
        }
    }
}