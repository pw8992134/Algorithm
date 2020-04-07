using System;
using System.ComponentModel.DataAnnotations;

namespace Algorithm.Map
{
    public class LinkedListDictionary<K,V>:IDictionary<K,V>
    {
        private class Node
        {
            public K K { get; set; }

            public V V { get; set; }

            public Node Next { get; set; }

            public Node(K k = default, V v = default,Node next = null)
            {
                this.K = k;
                this.V = v;
                this.Next = next;
            }
        }

        private readonly Node _virtualHead;

        public int Size { get; private set; }

        public bool IsEmpty => Size == 0;

        public LinkedListDictionary()
        {
            _virtualHead=new Node();
            Size = 0;
        }

        private Node GetNode(K k)
        {
            Node cur = _virtualHead;
            while (cur.Next != null)
            {
                if (cur.Next.K.Equals(k))
                {
                    return cur.Next;
                }
                else cur = cur.Next;
            }

            return null;
        }

        public void Add(K k, V v)
        {
            Node cur = GetNode(k);
            if (cur == null)
            {
                _virtualHead.Next=new Node(k,v,_virtualHead.Next);
                Size++;
            }
            else throw new Exception("k is is exists");
        }

        public V Remove(K k)
        {
            Node cur = _virtualHead;
            while (cur.Next!=null)
            {
                if (cur.Next.K.Equals(k))
                {
                    Node delNode = cur.Next;
                    cur.Next = cur.Next.Next;
                    delNode.Next = null;
                    Size--;
                    return delNode.V;
                }
                else cur = cur.Next;
            }

            return default;
        }

        public bool Contains(K k)
        {
            return GetNode(k) != null;
        }

        public V Get(K k)
        {
            Node node = GetNode(k);
            return node == null ? default : node.V;
        }

        public void Set(K k, V v)
        {
            Node cur = GetNode(k);
            if (cur == null) throw new Exception("k is not exists ");
            else cur.V = v;
        }
    }
}