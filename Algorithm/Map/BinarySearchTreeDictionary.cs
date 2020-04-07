using System;

namespace Algorithm.Map
{
    /// <summary>
    /// 基于二分搜索树的字典
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class BinarySearchTreeDictionary<K,V>:IDictionary<K,V> where K:IComparable<K>
    {
        /// <summary>
        /// 节点
        /// </summary>
        private class Node
        {
            public K K { get; set; }

            public V V { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }

            public Node(K k,V v)
            {
                this.K = k;
                this.V = v;
                Left = null;
                Right = null;
            }
        }

        /// <summary>
        /// 根节点
        /// </summary>
        private Node _root;

        /// <summary>
        /// 大小
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsEmpty => Size == 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public BinarySearchTreeDictionary()
        {
            _root = null;
            Size = 0;
        }

        /// <summary>
        /// 添加元素(递归) O(lgn)
        /// </summary>
        /// <param name="k"></param>
        /// <param name="v"></param>
        public void Add(K k, V v)
        {
            _root = Add(_root, k, v);
        }

        /// <summary>
        /// 添加元素(递归) O(lgn)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="k"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private Node Add(Node node,K k,V v)
        {
            if (node == null)
            {
                Size++;
                return new Node(k, v);
            }

            if (node.K.CompareTo(k) < 0) node.Right = Add(node.Right, k, v);
            else if (node.K.CompareTo(k) > 0) node.Left = Add(node.Left, k, v);
            //if equals ,update value or don't handle or throw exception
            return node;
        }

        /// <summary>
        /// 删除元素 O(lgn)
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public V Remove(K k)
        {
            Node node = GetNode(_root,k);
            if (node != null)
            {
                _root = Remove(_root, k);
                return node.V;
            }

            return default;
        }

        /// <summary>
        /// 删除元素 O(lgn)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private Node Remove(Node node,K k)
        {
            if (node == null) return null;

            if (node.K.CompareTo(k) == 0)
            {
                if (node.Left == null || node.Right == null)
                {
                    Size--;
                    return node.Left ?? node.Right;
                }
                else
                {
                    Node successor = MiNode(node.Right);
                    successor.Right = DeleteMinNode(node.Right);
                    successor.Left = node.Left;
                    node = null;
                    return successor;
                }
            }
            else if (node.K.CompareTo(k) < 0)
            {
                node.Right = Remove(node.Right, k);
                return node;
            }
            else
            {
                node.Left= Remove(node.Left, k);
                return node;
            }
        }

        /// <summary>
        /// 是否包含键为k的元素 O(lgn)
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public bool Contains(K k)
        {
            Node node = GetNode(_root, k);
            return node != null;
        }

        /// <summary>
        /// 获取关键字为k的值 O(lgn)
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public V Get(K k)
        {
            Node node = GetNode(_root,k);
            return node == null ? default : node.V;
        }

        /// <summary>
        /// 设置节点的值 O(lgn)
        /// </summary>
        /// <param name="k"></param>
        /// <param name="v"></param>
        public void Set(K k, V v)
        {
            Node node = GetNode(_root,k);
            if (node == null) throw new Exception("this key is not exists");
            else node.V = v;
        }

        /// <summary>
        /// 获取指定关键字的节点 O(lgn)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private Node GetNode(Node node,K key)
        {
            if (node == null) return null;

            if (node.K.CompareTo(key) == 0) return node;
            else if (node.K.CompareTo(key) < 0) return GetNode(node.Right, key);
            else return GetNode(node.Left, key);
        }

        /// <summary>
        /// 获取最小元素 O(lgn)
        /// </summary>
        /// <returns></returns>
        private Node MiNode(Node node)
        {
            if (node.Left == null) return node;
            return MiNode(node.Left);
        }

        /// <summary>
        /// 删除最小元素 O(lgn)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node DeleteMinNode(Node node)
        {
            if (node.Left == null)
            {
                Node right = node.Right;
                node.Right = null;
                Size--;
                return right;
            }

            node.Left = DeleteMinNode(node.Left);
            return node;
        }
    }
}