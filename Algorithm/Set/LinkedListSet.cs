using Algorithm.LinkedList;

namespace Algorithm.Set
{
    /// <summary>
    /// 基于链表的集合
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class LinkedListSet<E>:ISet<E>
    {
        /// <summary>
        /// 链表
        /// </summary>
        private readonly LinkedList.LinkedList<E> _linkedList;

        /// <summary>
        /// 构造方法
        /// </summary>
        public LinkedListSet()
        {
            _linkedList=new LinkedList<E>();
        }

        /// <summary>
        /// 添加 O(n)
        /// </summary>
        /// <param name="e"></param>
        public void Add(E e)
        {
            if(!Contains(e)) _linkedList.AddFirst(e);
        }

        /// <summary>
        /// 删除 O(n)
        /// </summary>
        /// <param name="e"></param>
        public void Remove(E e)
        {
            _linkedList.RemoveElement(e);
        }

        /// <summary>
        /// 是否包含某个元素
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool Contains(E e)
        {
            return _linkedList.Contains(e);
        }

        /// <summary>
        /// 集合大小
        /// </summary>
        public int Size => _linkedList.Size;

        /// <summary>
        /// 集合是否为空
        /// </summary>
        public bool IsEmpty => _linkedList.IsEmpty;
    }
}