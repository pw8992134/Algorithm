using Algorithm.DynamicArray;

namespace Algorithm.Queue
{
    /// <summary>
    /// 基于动态数组的队列
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class ArrayQueue<E>:IQueue<E>
    {
        /// <summary>
        /// 队列
        /// </summary>
        private DynamicArray.Array<E> _queue;

        /// <summary>
        /// 队列实际大小
        /// </summary>
        public int Size => _queue.Size;

        /// <summary>
        /// 队列是否为空
        /// </summary>
        public bool IsEmpty => _queue.IsEmpty;

        /// <summary>
        /// 队列容量
        /// </summary>
        public int Capacity => _queue.Capacity;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity">容量</param>
        public ArrayQueue(int capacity)
        {
            _queue=new Array<E>(capacity);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ArrayQueue()
        {
            _queue=new Array<E>();
        }

        /// <summary>
        /// 获取队头元素 o(1)
        /// </summary>
        /// <returns></returns>
        public E GetFront()
        {
            return _queue.GetFirst();
        }

        /// <summary>
        /// 入队 O(1)
        /// </summary>
        /// <param name="e">入对的元素</param>
        public void Enqueue(E e)
        {
            _queue.AddLast(e);
        }

        /// <summary>
        /// 出队 O(n)
        /// </summary>
        /// <returns></returns>
        public E Dequeue()
        {
            return _queue.RemoveFirst();
        }
    }
}