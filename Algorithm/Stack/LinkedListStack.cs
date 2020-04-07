using Algorithm.LinkedList;

namespace Algorithm.Stack
{
    /// <summary>
    /// 基于链表实现的栈
    /// 由于链表头的操作时间复杂度都为O(1),故链表很适合实现栈
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class LinkedListStack<E> :IStack<E>
    {
        /// <summary>
        /// 链表
        /// </summary>
        private readonly LinkedList.LinkedList<E> _data;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LinkedListStack()
        {
            _data=new LinkedList<E>();
        }

        /// <summary>
        /// 是否为空栈
        /// </summary>
        public bool IsEmpty => _data.IsEmpty;

        /// <summary>
        /// 栈大小
        /// </summary>
        public int Size => _data.Size;

        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="e"></param>
        public void Push(E e)
        {
            _data.AddFirst(e);
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <returns></returns>
        public E Pop()
        {
            return _data.RemoveFirst();
        }

        /// <summary>
        /// 获取栈顶元素
        /// </summary>
        /// <returns></returns>
        public E Peek()
        {
            return _data.GetFirst();
        }
    }
}