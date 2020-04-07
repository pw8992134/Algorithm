using System;
using Algorithm.DynamicArray;

namespace Algorithm.Queue
{
    /// <summary>
    /// 动态数组实现循环队列
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class LoopQueue<E>:IQueue<E>
    {
        /// <summary>
        /// 队列数组
        /// </summary>
        private E[] _data;

        /// <summary>
        /// 队列大小
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// 队列是否为空
        /// </summary>
        public bool IsEmpty => _front==_tail;

        /// <summary>
        /// 队列容量
        /// </summary>
        public int Capacity => _data.Length - 1;

        /// <summary>
        /// 队头元素索引
        /// </summary>
        private int _front;

        /// <summary>
        /// 队尾元素索引
        /// </summary>
        private int _tail;

        /// <summary>
        /// 使用容量构造队列
        /// </summary>
        /// <param name="capacity">容量</param>
        public LoopQueue(int capacity)
        {
            _data=new E[capacity+1];
            _front = 0;
            _tail = 0;
            Size = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoopQueue():this(10)
        {
            
        }

        /// <summary>
        /// 获取队头元素 O(1)
        /// </summary>
        /// <returns></returns>
        public E GetFront()
        {
            if (IsEmpty) throw new Exception("queue is empty!");
            return _data[_front];
        }

        /// <summary>
        /// 入队 O(1)
        /// </summary>
        /// <param name="e">入队的元素</param>
        public void Enqueue(E e)
        {
            if ((_tail + 1) % _data.Length == _front) Resize(Capacity*2);
            _data[_tail] = e;
            _tail = (_tail + 1) % _data.Length;
            Size++;
        }

        /// <summary>
        /// 出队 O(1)
        /// </summary>
        /// <returns></returns>
        public E Dequeue()
        {
            if(IsEmpty) throw new Exception("queue is empty!");
            E cur = _data[_front];
            _data[_front] = default(E);
            _front = (_front + 1) % _data.Length;
            Size--;
            if(Size==Capacity/4 && Capacity/2!=0) Resize(Capacity/2);
            return cur;
        }

        /// <summary>
        /// 调整动态数组的大小 O(N)
        /// </summary>
        /// <param name="capacity"></param>
        private void Resize(int capacity)
        {
            E[] newData=new E[capacity+1];
            for (int i = 0; i < Size; i++)
            {
                newData[i] = _data[(i + _front)%_data.Length];
            }

            _data = newData;
            _front = 0;
            _tail = Size;
        }
    }
}