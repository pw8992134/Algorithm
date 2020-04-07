using Algorithm.DynamicArray;

namespace Algorithm.Stack
{
    /// <summary>
    /// 基于动态数组的栈
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class ArrayStack<E>:IStack<E>
    {
        /// <summary>
        /// 数组栈
        /// </summary>
        private readonly DynamicArray.Array<E> _stack;

        /// <summary>
        /// 栈大小
        /// </summary>
        public int Size => _stack.Size;
        
        /// <summary>
        /// 栈是否为空
        /// </summary>
        public bool IsEmpty => _stack.IsEmpty;

        /// <summary>
        /// 栈容量
        /// </summary>
        public int Capacity => _stack.Capacity;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="capacity">容量</param>
        public ArrayStack(int capacity)
        {
            _stack=new Array<E>(capacity);
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public ArrayStack()
        {
            _stack=new Array<E>();
        }

        /// <summary>
        /// 入栈 O(1)
        /// </summary>
        /// <param name="e">入栈的元素</param>
        public void Push(E e)
        {
            _stack.AddLast(e);
        }

        /// <summary>
        /// 出栈 O(1)
        /// </summary>
        /// <returns></returns>
        public E Pop()
        {
            return _stack.RemoveLast();
        }

        /// <summary>
        /// 获取栈顶元素 O(1)
        /// </summary>
        /// <returns></returns>
        public E Peek()
        {
            return _stack.GetLast();
        }
    }
}