using System;
using Algorithm.LinkedList;

namespace Algorithm.Queue
{
    /// <summary>
    /// 基于链表的队列
    /// 因为队列需要两头操作,若使用带有虚拟头节点的链表,会有一头的操作时间复杂度为O(n)
    /// 故使用带头尾指针的链表
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class LinkedListQueue<E>:IQueue<E>
    {
        /// <summary>
        /// 节点
        /// </summary>
        private class Node
        {
            /// <summary>
            /// 元素
            /// </summary>
            public E E;

            /// <summary>
            /// 下一个节点
            /// </summary>
            public Node Next;

            /// <summary>
            /// 构造函数
            /// </summary>
            public Node() : this(default(E), null)
            {
            }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="e"></param>
            public Node(E e) : this(e, null)
            {
            }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="e"></param>
            /// <param name="next"></param>
            public Node(E e, Node next)
            {
                E = e;
                Next = next;
            }
        }

        /// <summary>
        /// 头指针
        /// </summary>
        private Node _head;

        /// <summary>
        /// 尾指针
        /// </summary>
        private Node _tail;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LinkedListQueue()
        {
            _head = null;
            _tail = null;
            Size = 0;
        }

        /// <summary>
        /// 队列大小
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// 队列是否为空
        /// </summary>
        public bool IsEmpty => Size == 0;

        /// <summary>
        /// 获取队首元素
        /// </summary>
        /// <returns></returns>
        public E GetFront()
        {
            if(IsEmpty) throw new Exception("queue is empty!");
            return _head.E;
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="e"></param>
        public void Enqueue(E e)
        {
            if (_tail == null)
            {
                _tail=new Node(e);
                _head = _tail;
            }
            else
            {
                _tail.Next=new Node(e);
                _tail = _tail.Next;
            }

            Size++;
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <returns></returns>
        public E Dequeue()
        {
            if (IsEmpty) throw new Exception("queue is empty!");
            Node ret = _head;
            _head = _head.Next;
            if (_head == null)
            {
                _tail = null;
            }
            ret.Next = null;
            Size--;
            return ret.E;
        }
    }
}