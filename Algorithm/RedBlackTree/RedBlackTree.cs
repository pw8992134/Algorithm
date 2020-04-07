using System;
using System.Collections.Generic;
using Algorithm.Stack;

namespace Algorithm.RedBlackTree
{
    /// <summary>
    /// 红黑树
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class RedBlackTree<E,V> where E : IComparable<E>
    {
        /// <summary>
        /// 树节点
        /// </summary>
        private class Node
        {
            /// <summary>
            /// 元素
            /// </summary>
            public E E { get; }

            public V V { get; }

            /// <summary>
            /// 左孩子
            /// </summary>
            public Node Left { get; set; }

            /// <summary>
            /// 右孩子
            /// </summary>
            public Node Right { get; set; }

            /// <summary>
            /// 节点颜色
            /// </summary>
            public bool Color { get; set; }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="e"></param>
            /// <param name="v"></param>
            public Node(E e,V v)
            {
                E = e;
                V = v;
                Left = null;
                Right = null;
                Color = Red;
            }
        }

        /// <summary>
        /// 红色
        /// </summary>
        private static readonly bool Red = true;

        /// <summary>
        /// 黑色
        /// </summary>
        private static readonly bool Black = false;

        /// <summary>
        /// 前序遍历结果
        /// </summary>
        public List<E> PreOrder;

        /// <summary>
        /// 中序遍历结果
        /// </summary>
        public List<E> Sequential;

        /// <summary>
        /// 后序遍历结果
        /// </summary>
        public List<E> PostOrder;

        /// <summary>
        /// 层序遍历结果
        /// </summary>
        public List<E> LevelOrder;

        /// <summary>
        /// 树节点数量
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// 树是否为空
        /// </summary>
        public bool IsEmpty => Size == 0;

        /// <summary>
        /// 根节点
        /// </summary>
        private Node _root;

        /// <summary>
        /// 构造函数
        /// </summary>
        public RedBlackTree()
        {
            _root = null;
            Size = 0;

            PreOrder = new List<E>();
            Sequential = new List<E>();
            PostOrder = new List<E>();
            LevelOrder = new List<E>();
        }

        /// <summary>
        /// 判断节点是否为红色节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool IsRed(Node node)
        {
            if (node == null) return Black;
            return node.Color;
        }

        /// <summary>
        /// 填充颜色
        /// 3节点的第三种情况
        /// </summary>
        private void FillColor(Node node)
        {
            node.Color = Red;
            if (node.Left != null) node.Left.Color = Black;
            if (node.Right != null) node.Right.Color = Black;
        }

        /// <summary>
        /// 左旋转
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node LeftRotate(Node node)
        {
            Node x = node.Right;
            node.Right = x.Left;
            x.Left = node;
            x.Color = node.Color;
            node.Color = Red;
            return x;
        }

        /// <summary>
        /// 右旋转
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node RightRotate(Node node)
        {
            Node x = node.Left;
            node.Left = x.Right;
            x.Right = node;
            x.Color = node.Color;
            node.Color = Red;
            return x;
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="e"></param>
        /// <param name="v"></param>
        public void Add(E e,V v)
        {
            if (_root == null) _root = new Node(e,v);
            else
            {
                Node cur = _root;
                Node parent = _root;
                while (cur != null)
                {
                    parent = cur;
                    if (cur.E.CompareTo(e) < 0) cur = cur.Right;
                    else if (cur.E.CompareTo(e) > 0) cur = cur.Left;
                    else return;
                }
                if (parent.E.CompareTo(e) < 0) parent.Right = new Node(e,v);
                else if (parent.E.CompareTo(e) > 0) parent.Left = new Node(e,v);
            }

            Size++;
        }

        /// <summary>
        /// 添加节点的递归版本
        /// </summary>
        /// <param name="e"></param>
        /// <param name="v"></param>
        public void AddByRecursion(E e,V v)
        {
            _root = AddByRecursion(_root, e,v);
            _root.Color = Black;
        }

        /// <summary>
        /// 添加节点的递归版本
        /// </summary>
        /// <param name="node"></param>
        /// <param name="e"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private Node AddByRecursion(Node node, E e,V v)
        {
            if (node == null)
            {
                Size++;
                return new Node(e,v);
            }

            if (node.E.CompareTo(e) < 0) node.Right = AddByRecursion(node.Right, e,v);
            else if (node.E.CompareTo(e) > 0) node.Left = AddByRecursion(node.Left, e,v);

            if (!IsRed(node.Left) && IsRed(node.Right)) node = LeftRotate(node);

            if (IsRed(node.Left) && IsRed(node.Left.Left)) node = RightRotate(node);

            if(IsRed(node.Left) && IsRed(node.Right)) FillColor(node);

            return node;
        }

        /// <summary>
        /// 是否包含某个元素
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool Contains(E e)
        {
            Node cur = _root;
            while (cur != null)
            {
                if (cur.E.CompareTo(e) == 0) return true;
                else if (cur.E.CompareTo(e) < 0) cur = cur.Right;
                else cur = cur.Left;
            }

            return false;
        }

        /// <summary>
        /// 是否包含某个元素的递归版本
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool ContainsByRecursion(E e)
        {
            return ContainsByRecursion(_root, e);
        }

        /// <summary>
        /// 是否包含某个元素的递归版本
        /// </summary>
        /// <param name="node"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool ContainsByRecursion(Node node, E e)
        {
            if (node == null) return false;

            if (node.E.CompareTo(e) == 0) return true;
            else if (node.E.CompareTo(e) < 0) return ContainsByRecursion(node.Right, e);
            else return ContainsByRecursion(node.Left, e);
        }

        /// <summary>
        /// 前序遍历
        /// </summary>
        public void PreOrderTraversal()
        {
            PreOrder.Clear();
            ArrayStack<Node> stack = new ArrayStack<Node>();
            stack.Push(_root);
            while (!stack.IsEmpty)
            {
                Node cur = stack.Pop();
                PreOrder.Add(cur.E);
                if (cur.Right != null) stack.Push(cur.Right);
                if (cur.Left != null) stack.Push(cur.Left);
            }
        }

        /// <summary>
        /// 中序遍历
        /// </summary>
        public void SequentialTraversal()
        {
            Sequential.Clear();
            ArrayStack<Node> stack = new ArrayStack<Node>();
            Node cur = _root;
            while (cur != null || !stack.IsEmpty)
            {
                while (cur != null)
                {
                    stack.Push(cur);
                    cur = cur.Left;
                }

                Node pre = stack.Pop();
                Sequential.Add(pre.E);
                cur = pre.Right;
            }
        }

        /// <summary>
        /// 后序遍历
        /// </summary>
        public void PostOrderTraversal()
        {
            PostOrder.Clear();
            ArrayStack<Node> stack = new ArrayStack<Node>();
            Node cur = _root;
            Node prev = null;
            while (cur != null || !stack.IsEmpty)
            {
                while (cur != null)
                {
                    stack.Push(cur);
                    cur = cur.Left;
                }

                Node node = stack.Peek();
                if (node.Right == null || prev == node.Right)
                {
                    stack.Pop();
                    PostOrder.Add(node.E);
                    prev = node;
                }
                else cur = node.Right;
            }
        }

        /// <summary>
        /// 前序遍历的递归版本
        /// </summary>
        public void PreOrderTraversalByRecursion()
        {
            PreOrder.Clear();
            PreOrderTraversalByRecursion(_root);
        }

        /// <summary>
        /// 前序遍历的递归版本
        /// </summary>
        /// <param name="node"></param>
        private void PreOrderTraversalByRecursion(Node node)
        {
            if (node == null) return;
            PreOrder.Add(node.E);
            PreOrderTraversalByRecursion(node.Left);
            PreOrderTraversalByRecursion(node.Right);
        }

        /// <summary>
        /// 中序遍历的递归版本
        /// </summary>
        public void SequentialTraversalByRecursion()
        {
            Sequential.Clear();
            SequentialTraversalByRecursion(_root);
        }

        /// <summary>
        /// 中序遍历的递归版本
        /// </summary>
        /// <param name="node"></param>
        private void SequentialTraversalByRecursion(Node node)
        {
            if (node == null) return;
            SequentialTraversalByRecursion(node.Left);
            Sequential.Add(node.E);
            SequentialTraversalByRecursion(node.Right);
        }

        /// <summary>
        /// 后序遍历的递归版本
        /// </summary>
        public void PostOrderTraversalByRecursion()
        {
            PostOrder.Clear();
            PostOrderTraversalByRecursion(_root);
        }

        /// <summary>
        /// 后序遍历的递归版本
        /// </summary>
        /// <param name="node"></param>
        private void PostOrderTraversalByRecursion(Node node)
        {
            if (node == null) return;
            PostOrderTraversalByRecursion(node.Left);
            PostOrderTraversalByRecursion(node.Right);
            PostOrder.Add(node.E);
        }

        /// <summary>
        /// 层序遍历
        /// </summary>
        public void LevelTraversal()
        {
            Node node = _root;
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                Node cur = queue.Dequeue();
                LevelOrder.Add(cur.E);
                if (cur.Left != null) queue.Enqueue(cur.Left);
                if (cur.Right != null) queue.Enqueue(cur.Right);
            }
        }

        /// <summary>
        /// 移除树中最小的元素(非递归)
        /// </summary>
        public E RemoveMinInTree()
        {
            if (_root == null) return default(E);
            Node node = _root;
            Node prev = null;
            while (node.Left != null)
            {
                prev = node;
                node = node.Left;
            }

            if (prev == null)
            {
                _root = node.Right;
            }
            else prev.Left = node.Right;
            Size--;
            E e = node.E;
            node = null;
            return e;
        }

        /// <summary>
        /// 移除树中最小的元素(递归)
        /// </summary>
        /// <returns></returns>
        public E RemoveMinInTreeByRecursion()
        {
            E e = MinInTree();
            _root = RemoveMinInTreeByRecursion(_root);
            return e;
        }

        /// <summary>
        /// 移除树中最小的元素(递归)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node RemoveMinInTreeByRecursion(Node node)
        {
            if (node == null) return null;
            if (node.Left == null)
            {
                Size--;
                Node right = node.Right;
                node = null;
                return right;
            }

            node.Left = RemoveMinInTreeByRecursion(node.Left);
            return node;
        }

        /// <summary>
        /// 求树中最小的元素(非递归)
        /// </summary>
        /// <returns></returns>
        public E MinInTree()
        {
            if (Size == 0) return default;
            Node cur = _root;
            while (cur.Left != null)
            {
                cur = cur.Left;
            }

            return cur.E;
        }

        /// <summary>
        /// 求树中最小的元素(递归)
        /// </summary>
        /// <returns></returns>
        public E MinInTreeByRecursion()
        {
            return MinInTreeByRecursion(_root).E;
        }

        /// <summary>
        /// 求树中最小的元素(递归)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node MinInTreeByRecursion(Node node)
        {
            if (node == null) throw new Exception("not exists min element");
            if (node.Left == null) return node;
            return MinInTreeByRecursion(node.Left);
        }

        /// <summary>
        /// 求树中最大的元素(非递归)
        /// </summary>
        /// <returns></returns>
        public E MaxInTree()
        {
            if (_root == null) return default;
            Node cur = _root;
            while (cur.Right != null)
            {
                cur = cur.Right;
            }

            return cur.E;
        }

        /// <summary>
        /// 求树中最大的元素(递归)
        /// </summary>
        /// <returns></returns>
        public E MaxInTreeByRecursion()
        {
            return MaxInTreeByRecursion(_root).E;
        }

        /// <summary>
        /// 求树中最大的元素(递归)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node MaxInTreeByRecursion(Node node)
        {
            if (node == null) throw new Exception("");
            if (node.Right == null) return node;
            return MaxInTreeByRecursion(node.Right);
        }

        /// <summary>
        /// 删除树中最大元素
        /// </summary>
        /// <returns></returns>
        public E RemoveMaxInTree()
        {
            if (_root == null) return default;
            Node cur = _root;
            Node prev = null;
            while (cur.Right != null)
            {
                prev = cur;
                cur = cur.Right;
            }

            if (prev == null)
            {
                _root = cur.Left;
            }
            else prev.Right = cur.Left;

            E e = cur.E;
            cur = null;
            Size--;
            return e;
        }

        /// <summary>
        /// 删除树中最大的元素(递归)
        /// </summary>
        /// <returns></returns>
        public E RemoveMaxInTreeByRecursion()
        {
            E e = MaxInTree();
            _root = RemoveMaxInTreeByRecursion(_root);
            return e;
        }

        /// <summary>
        /// 删除树中最大的元素(递归)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node RemoveMaxInTreeByRecursion(Node node)
        {
            if (node == null) throw new Exception("not exists max node");
            if (node.Right == null)
            {
                Size--;
                Node left = node.Left;
                node = null;
                return left;
            }

            node.Right = RemoveMaxInTreeByRecursion(node.Right);
            return node;
        }

        /// <summary>
        /// 删除任意元素
        /// </summary>
        /// <param name="e"></param>
        public void Remove(E e)
        {
            if (!Contains(e)) throw new Exception("this element is not exists");
            Node cur = _root;
            Node prev = null;
            while (cur.E.CompareTo(e) != 0)
            {
                prev = cur;
                if (cur.E.CompareTo(e) < 0) cur = cur.Right;
                else if (cur.E.CompareTo(e) > 0) cur = cur.Left;
            }

            if (prev == null)
            {
                if (cur.Left == null || cur.Right == null) _root = cur.Left ?? cur.Right;
                else _root = GetSuccessor(cur);
            }
            else
            {
                if (prev.Left == cur)
                {
                    if (cur.Left == null || cur.Right == null)
                    {
                        prev.Left = cur.Right ?? cur.Left;
                        Size--;
                    }
                    else
                    {
                        prev.Left = GetSuccessor(cur);
                    }
                }
                else
                {
                    if (cur.Left == null || cur.Right == null)
                    {
                        prev.Right = cur.Right ?? cur.Left;
                        Size--;
                    }
                    else
                    {
                        prev.Right = GetSuccessor(cur);
                    }
                }
            }

            cur = null;
        }

        /// <summary>
        /// 递归删除任意节点
        /// </summary>
        /// <param name="e"></param>
        public void RemoveByRecursion(E e)
        {
            _root = RemoveByRecursion(_root, e);
        }

        /// <summary>
        /// 递归删除任意节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private Node RemoveByRecursion(Node node, E e)
        {
            if (node == null) throw new Exception("this element is not exists");
            if (node.E.CompareTo(e) == 0)
            {
                if (node.Right == null || node.Left == null)
                {
                    Size--;
                    return node.Left ?? node.Right;
                }
                else
                {
                    return GetSuccessor(node);
                }
            }
            else if (node.E.CompareTo(e) < 0) node.Right = RemoveByRecursion(node.Right, e);
            else node.Left = RemoveByRecursion(node.Left, e);

            return node;
        }

        /// <summary>
        /// 后继删除节点过程
        /// </summary>
        /// <param name="cur"></param>
        /// <returns></returns>
        private Node GetSuccessor(Node cur)
        {
            Node successor = MinInTreeByRecursion(cur.Right);
            successor.Right = RemoveMinInTreeByRecursion(cur.Right);
            successor.Left = cur.Left;
            cur = null;
            return successor;
        }

        /// <summary>
        /// 前驱删除节点过程
        /// </summary>
        /// <param name="cur"></param>
        /// <returns></returns>
        private Node GetPreCursor(Node cur)
        {
            Node precursor = MaxInTreeByRecursion(cur.Left);
            precursor.Left = RemoveMaxInTreeByRecursion(cur.Left);
            precursor.Right = cur.Right;
            cur = null;
            return precursor;
        }
    }
}