// ReSharper disable MemberCanBePrivate.Local

using System;

namespace Algorithm.LinkedList
{
    /// <summary>
    /// 链表
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class LinkedList<E>
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
            public Node():this(default(E),null)
            {
            }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="e"></param>
            public Node(E e):this(e,null)
            {
            }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="e"></param>
            /// <param name="next"></param>
            public Node(E e,Node next)
            {
                E = e;
                Next = next;
            }
        }

        /// <summary>
        /// 虚拟头节点
        /// </summary>
        private readonly Node _virtualHead;

        /// <summary>
        /// 链表大小
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// 链表是否为空
        /// </summary>
        public bool IsEmpty => Size == 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LinkedList()
        {
            _virtualHead = new Node(default,null);
            Size = 0;
        }

        //array translate to linkedlist constructor

        /// <summary>
        /// 链表头添加元素 O(1)
        /// </summary>
        /// <param name="e"></param>
        public void AddFirst(E e)
        {
            Add(0,e);
        }

        /// <summary>
        /// 在指定索引位置添加元素 O(n)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="e"></param>
        public void Add(int index,E e)
        {
            if(index<0 || index>Size) throw new Exception("Add failed,Illegal index");
            Node prev = _virtualHead;
            for (int i = 0; i < index; i++)
            {
                prev = prev.Next;
            }
            prev.Next = new Node(e, prev.Next);
            Size++;
        }

        /// <summary>
        /// 在链表尾部添加元素 O(n)
        /// </summary>
        /// <param name="e"></param>
        public void AddLast(E e)
        {
            Add(Size,e);
        }

        /// <summary>
        /// 获取指定索引位置元素 O(n)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public E Get(int index)
        {
            if (index < 0 || index > Size) throw new Exception("Add failed,Illegal index");
            Node cur = _virtualHead;
            for (int i = 0; i <= index; i++)
            {
                cur = cur.Next;
            }

            return cur.E;
        }

        /// <summary>
        /// 获取链表头元素 O(1)
        /// </summary>
        /// <returns></returns>
        public E GetFirst()
        {
            return Get(0);
        }

        /// <summary>
        /// 获取链表尾部元素 O(n)
        /// </summary>
        /// <returns></returns>
        public E GetLast()
        {
            return Get(Size-1);
        }

        /// <summary>
        /// 设置指定索引位置元素 O(n)
        /// </summary>
        /// <param name="e"></param>
        /// <param name="index"></param>
        public void Set(E e,int index)
        {
            if (index < 0 || index > Size) throw new Exception("Add failed,Illegal index");
            Node cur = _virtualHead.Next;
            for (int i = 0; i < index; i++)
            {
                cur = cur.Next;
            }

            cur.E = e;
        }

        /// <summary>
        /// 是否包含某个元素 O(n)
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool Contains(E e)
        {
            Node cur = _virtualHead.Next;
            while (cur!=null)
            {
                if (cur.E.Equals(e)) return true;
                cur = cur.Next;
            }

            return false;
        }

        /// <summary>
        /// 删除指定索引位置的元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public E Remove(int index)
        {
            if (index < 0 || index > Size) throw new Exception("Add failed,Illegal index");
            Node prev = _virtualHead;
            for (int i = 0; i < index; i++)
            {
                prev = prev.Next;
            }

            Node ret = prev.Next;
            prev.Next = ret.Next;
            ret.Next = null;
            Size--;
            return ret.E;
        }

        /// <summary>
        /// 删除链表头元素 O(1)
        /// </summary>
        /// <returns></returns>
        public E RemoveFirst()
        {
            return Remove(0);
        }

        /// <summary>
        /// 删除链表尾部的元素 O(n)
        /// </summary>
        /// <returns></returns>
        public E RemoveLast()
        {
            return Remove(Size - 1);
        }

        /// <summary>
        /// 删除指定元素
        /// </summary>
        /// <param name="e"></param>
        public void RemoveElement(E e)
        {
            int index = Find(e);
            if (index != -1) Remove(index);
        }

        /// <summary>
        /// 查找元素,返回索引
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int Find(E e)
        {
            for (int i = 0; i < Size; i++)
            {
                E cur = Get(i);
                if (cur.Equals(e)) return i;
            }

            return -1;
        }
    }
}