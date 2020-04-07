using System;
using System.Collections.Generic;

namespace Algorithm.AvlTree
{
    /// <summary>
    /// 平衡二叉树 每个节点的左右子树高度差不超过1
    /// </summary>
    public class AvlTree<E,V> where E:IComparable<E>
    {
        /// <summary>
        /// 树节点的定义
        /// </summary>
        private class Node
        {
            public E Key { get; set; }

            public V Value { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }

            public Node Parent { get; set; }

            public int Height { get; set; }

            public Node(E key,V value)
            {
                this.Key = key;
                this.Value = value;
                Left = null;
                Right = null;
                Parent = null;
                Height = 1;
            }

            public Node(E key, V value,Node parent)
            {
                this.Key = key;
                this.Value = value;
                Left = null;
                Right = null;
                Parent = parent;
                Height = 1;
            }
        }

        /// <summary>
        /// 根节点
        /// </summary>
        private Node _root;

        /// <summary>
        /// 树的节点数量
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 树是否为空
        /// </summary>
        public bool IsEmpty { get; set; }

        /// <summary>
        /// 前序遍历结果
        /// </summary>
        public List<E> PreOrder { get; }

        /// <summary>
        /// 中序遍历结果
        /// </summary>
        public List<E> SequentialOrder { get; }

        /// <summary>
        /// 后序遍历结果
        /// </summary>
        public List<E> PostOrder { get; }

        /// <summary>
        /// 层序遍历结果
        /// </summary>
        public List<E> LevelOrder { get; }

        /// <summary>
        /// 层序遍历辅助list
        /// </summary>
        private List<List<E>> levelOrderList { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AvlTree()
        {
            _root = null;
            Size = 0;
            PreOrder=new List<E>();
            SequentialOrder=new List<E>();
            PostOrder=new List<E>();
            LevelOrder=new List<E>();
            levelOrderList=new List<List<E>>();
        }

        /// <summary>
        /// 是否包含某个元素
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool Contains(E e)
        {
            Node cur = _root;
            while (cur!=null)
            {
                if (cur.Key.CompareTo(e) == 0) return true;
                else if (cur.Key.CompareTo(e) < 0) cur = cur.Right;
                else cur = cur.Left;
            }

            return false;
        }

        /// <summary>
        /// 是否包含某个元素(递归)
        /// </summary>
        /// <returns></returns>
        public bool ContainsByRecursion(E e)
        {
            return ContainsByRecursion(_root, e);
        }

        /// <summary>
        /// 是否包含某个元素(递归)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool ContainsByRecursion(Node node,E e)
        {
            if (node == null) return false;
            if (node.Key.CompareTo(e) == 0) return true;
            else if (node.Key.CompareTo(e) < 0) return ContainsByRecursion(node.Right, e);
            else return ContainsByRecursion(node.Left,e);
        }

        /// <summary>
        /// 前序遍历
        /// </summary>
        public void PreOrderTraversal()
        {
            PreOrder.Clear();
            if (_root == null) return;
            Stack<Node> stack=new Stack<Node>();
            stack.Push(_root);
            while (stack.Count>0)
            {
                Node cur=stack.Pop();
                PreOrder.Add(cur.Key);
                if(cur.Right!=null) stack.Push(cur.Right);
                if(cur.Left!=null) stack.Push(cur.Left);
            }
        }

        /// <summary>
        /// 前序遍历(递归)
        /// </summary>
        public void PreOrderTraversalByRecursion()
        {
            PreOrder.Clear();
            PreOrderTraversalByRecursion(_root);
        }

        /// <summary>
        /// 前序遍历(递归)
        /// </summary>
        /// <param name="node"></param>
        private void PreOrderTraversalByRecursion(Node node)
        {
            if (node != null)
            {
                PreOrder.Add(node.Key);
                PreOrderTraversalByRecursion(node.Left);
                PreOrderTraversalByRecursion(node.Right);
            }
        }

        /// <summary>
        /// 中序遍历
        /// </summary>
        public void SequentialTraversal()
        {
            SequentialOrder.Clear();
            Stack<Node> stack=new Stack<Node>();
            Node cur = _root;
            while (stack.Count > 0 || cur != null)
            {
                while (cur != null)
                {
                    stack.Push(cur);
                    cur = cur.Left;
                }

                Node node = stack.Pop();
                SequentialOrder.Add(node.Key);
                cur = node.Right;
            }
        }

        /// <summary>
        /// 中序遍历(递归)
        /// </summary>
        public void SequentialTraversalByRecursion()
        {
            SequentialOrder.Clear();
            SequentialTraversalByRecursion(_root);
        }

        /// <summary>
        /// 中序遍历(递归)
        /// </summary>
        /// <param name="node"></param>
        private void SequentialTraversalByRecursion(Node node)
        {
            if (node != null)
            {
                SequentialTraversalByRecursion(node.Left);
                SequentialOrder.Add(node.Key);
                SequentialTraversalByRecursion(node.Right);
            }
        }

        /// <summary>
        /// 后序遍历
        /// </summary>
        public void PostOrderTraversal()
        {
            PostOrder.Clear();
            Stack<Node> stack=new Stack<Node>();
            Node cur = _root;
            Node prev = null;
            while (stack.Count>0 || cur!=null)
            {
                while (cur!=null)
                {
                    stack.Push(cur);
                    cur = cur.Left;
                }

                Node node = stack.Peek();
                if (node.Right == null || node.Right == prev)
                {
                    Node del = stack.Pop();
                    PostOrder.Add(del.Key);
                    prev = del;
                }
                else cur = node.Right;
            }
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
            if (node != null)
            {
                PostOrderTraversalByRecursion(node.Left);
                PostOrderTraversalByRecursion(node.Right);
                PostOrder.Add(node.Key);
            }
        }

        /// <summary>
        /// 层序遍历
        /// </summary>
        public void LevelOrderTraversal()
        {
            LevelOrder.Clear();
            if(_root==null) return;
            Queue<Node> queue=new Queue<Node>();
            queue.Enqueue(_root);
            while (queue.Count>0)
            {
                Node cur = queue.Dequeue();
                LevelOrder.Add(cur.Key);
                if(cur.Left!=null) queue.Enqueue(cur.Left);
                if(cur.Right!=null) queue.Enqueue(cur.Right);
            }
        }

        /// <summary>
        /// 层序遍历的递归写法
        /// </summary>
        public void LevelOrderTraversalByRecursion()
        {
            levelOrderList.Clear();
            LevelOrder.Clear();
            LevelOrderTraversalByRecursion(_root,0);
            foreach (var list in levelOrderList)
            {
                foreach (var e in list)
                {
                    LevelOrder.Add(e);
                }
            }
        }

        /// <summary>
        /// 层序遍历的递归写法
        /// </summary>
        /// <param name="node"></param>
        /// <param name="level"></param>
        private void LevelOrderTraversalByRecursion(Node node,int level)
        {
            if (node == null) return;
            if(level>=levelOrderList.Count) levelOrderList.Add(new List<E>());

            levelOrderList[level].Add(node.Key);
            LevelOrderTraversalByRecursion(node.Left,level+1);
            LevelOrderTraversalByRecursion(node.Right,level+1);
        }

        /// <summary>
        /// 添加树节点
        /// </summary>
        public void Add(E key,V value)
        {
            Node cur = _root;
            Node parent = null;
            while (cur!=null)
            {
                parent = cur;
                if(cur.Key.CompareTo(key)==0) return;
                else if (cur.Key.CompareTo(key) < 0) cur = cur.Right;
                else cur = cur.Left;
            }

            if (parent == null) _root = new Node(key, value,null);
            else
            {
                if (parent.Key.CompareTo(key) < 0)  parent.Right = new Node(key, value, parent);
                else parent.Left = new Node(key, value, parent);
                while (parent!=null)
                {
                    parent.Height = Math.Max(GetHeight(parent.Left), GetHeight(parent.Right)) + 1;
                    int balance = GetBalance(parent);
                    if (balance > 1 && GetBalance(parent.Left) >= 0)
                    {
                        Node prev = parent.Parent;
                        Node right =RightRotate(parent);
                        if (prev == null)
                        {
                            _root = right;
                            if(_root!=null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = right;
                            if(prev.Right!=null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = right;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }
                    else if (balance<-1 && GetBalance(parent.Right)<=0)
                    {
                        Node prev = parent.Parent;
                        Node left = LeftRotate(parent);
                        if (prev == null)
                        {
                            _root = left;
                            if (_root != null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = left;
                            if (prev.Right != null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = left;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }
                    else if (balance >1 && GetBalance(parent.Left) < 0)
                    {
                        parent.Right=LeftRotate(parent.Right);
                        if (parent.Right != null) parent.Right.Parent = parent;
                        Node prev = parent.Parent;
                        Node right = RightRotate(parent);
                        if (prev == null)
                        {
                            _root = right;
                            if (_root != null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = right;
                            if (prev.Right != null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = right;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }
                    else if (balance < -1 && GetBalance(parent.Left) > 0)
                    {
                        parent.Left = RightRotate(parent.Right);
                        if (parent.Left != null) parent.Left.Parent = parent;
                        Node prev = parent.Parent;
                        Node left = LeftRotate(parent);
                        if (prev == null)
                        {
                            _root = left;
                            if (_root != null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = left;
                            if (prev.Right != null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = left;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }

                    parent = parent.Parent;
                }
            }
            Size++;
        }

        /// <summary>
        /// 添加树节点的递归算法
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddByRecursion(E key,V value)
        {
            _root=AddByRecursion(_root,key,value);
        }

        /// <summary>
        /// 添加树节点的递归算法
        /// </summary>
        /// <param name="node"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private Node AddByRecursion(Node node, E key, V value)
        {
            if (node == null)
            {
                Size++;
                return new Node(key,value);
            }

            if (node.Key.CompareTo(key) < 0)
            {
                node.Right = AddByRecursion(node.Right, key, value);
                if (node.Right != null) node.Right.Parent = node;
            }
            else if (node.Key.CompareTo(key) > 0)
            {
                node.Left = AddByRecursion(node.Left, key, value);
                if (node.Left != null) node.Left.Parent = node;
            }
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            if (GetBalance(node) > 1 && GetBalance(node.Left) >= 0) return RightRotate(node);
            if (GetBalance(node) < -1 && GetBalance(node.Right) <= 0) return LeftRotate(node);
            if (GetBalance(node) > 1 && GetBalance(node.Left) < 0)
            {
                node.Left = LeftRotate(node.Left);
                if (node.Left != null) node.Left.Parent = node;
                return RightRotate(node);
            }

            if (GetBalance(node) < -1 && GetBalance(node.Right) > 0)
            {
                node.Right = RightRotate(node.Right);
                if (node.Right != null) node.Right.Parent = node;
                return LeftRotate(node);
            }
            return node;
        }

        /// <summary>
        /// 获取树中最大元素
        /// </summary>
        /// <returns></returns>
        public E GetMax()
        {
            if (_root == null) throw new Exception("tree is empty");
            Node cur = _root;
            while (cur.Right!=null)
            {
                cur = cur.Right;
            }

            return cur.Key;
        }

        /// <summary>
        /// 获取树中的最大元素(递归)
        /// </summary>
        /// <returns></returns>
        public E GetMaxByRecursion()
        {
            if(_root==null) throw new Exception("tree is empty");
            return GetMaxByRecursion(_root).Key;
        }

        /// <summary>
        /// 获取树中的最大元素(递归)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node GetMaxByRecursion(Node node)
        {
            if (node.Right == null) return node;
            return GetMaxByRecursion(node.Right);
        }

        /// <summary>
        /// 获取树中最小元素
        /// </summary>
        /// <returns></returns>
        public E GetMin()
        {
            if(_root==null) throw new Exception("tree is empty");
            Node cur = _root;
            while (cur.Left!=null)
            {
                cur = cur.Left;
            }

            return cur.Key;
        }

        /// <summary>
        /// 获取树中最小元素(递归)
        /// </summary>
        /// <returns></returns>
        public E GetMinByRecursion()
        {
            if(_root==null) throw new Exception("tree is empty");
            return GetMinByRecursion(_root).Key;
        }

        /// <summary>
        /// 获取树中最小元素(递归)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node GetMinByRecursion(Node node)
        {
            if (node.Left == null) return node;
            return GetMinByRecursion(node.Left);
        }

        /// <summary>
        /// 移除最小元素
        /// </summary>
        /// <returns></returns>
        public E RemoveMin()
        {
            E e = GetMin();
            Node cur = _root;
            Node parent = null;
            while (cur.Left!=null)
            {
                parent = cur;
                cur = cur.Left;
            }

            if (parent == null)
            {
                _root = cur.Right;
                if(_root!=null) _root.Parent = null;
            }
            else
            {
                parent.Left = cur.Right;
                if(parent.Left!=null) parent.Left.Parent = parent;
                while (parent != null)
                {
                    parent.Height = Math.Max(GetHeight(parent.Left), GetHeight(parent.Right)) + 1;
                    int balance = GetBalance(parent);
                    if (balance > 1 && GetBalance(parent.Left) >= 0)
                    {
                        Node prev = parent.Parent;
                        Node right = RightRotate(parent);
                        if (prev == null)
                        {
                            _root = right;
                            if (_root != null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = right;
                            if (prev.Right != null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = right;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }
                    else if (balance < -1 && GetBalance(parent.Right) <= 0)
                    {
                        Node prev = parent.Parent;
                        Node left = LeftRotate(parent);
                        if (prev == null)
                        {
                            _root = left;
                            if (_root != null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = left;
                            if (prev.Right != null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = left;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }
                    else if (balance > 1 && GetBalance(parent.Left) < 0)
                    {
                        parent.Right = LeftRotate(parent.Right);
                        if (parent.Right != null) parent.Right.Parent = parent;
                        Node prev = parent.Parent;
                        Node right = RightRotate(parent);
                        if (prev == null)
                        {
                            _root = right;
                            if (_root != null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = right;
                            if (prev.Right != null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = right;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }
                    else if (balance < -1 && GetBalance(parent.Left) > 0)
                    {
                        parent.Left = RightRotate(parent.Right);
                        if (parent.Left != null) parent.Left.Parent = parent;
                        Node prev = parent.Parent;
                        Node left = LeftRotate(parent);
                        if (prev == null)
                        {
                            _root = left;
                            if (_root != null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = left;
                            if (prev.Right != null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = left;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }

                    parent = parent.Parent;
                }
            }
            cur = null;
            Size--;
            return e;
        }

        /// <summary>
        /// 移除最小元素(递归)
        /// </summary>
        /// <returns></returns>
        public E RemoveMinByRecursion()
        {
            E e = GetMin();
            _root = RemoveMinByRecursion(_root);
            return e;
        }

        /// <summary>
        /// 移除最小元素(递归)
        /// </summary>
        /// <returns></returns>
        private Node RemoveMinByRecursion(Node node)
        {
            if (node.Left == null)
            {
                Size--;
                Node right = node.Right;
                node = null;
                return right;
            }
            node.Left = RemoveMinByRecursion(node.Left);
            if (node.Left != null) node.Left.Parent = node;
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            if (GetBalance(node) > 1 && GetBalance(node.Left) >= 0) return RightRotate(node);
            if (GetBalance(node) < -1 && GetBalance(node.Right) <= 0) return LeftRotate(node);
            if (GetBalance(node) > 1 && GetBalance(node.Left) < 0)
            {
                node.Left = LeftRotate(node.Left);
                if (node.Left != null) node.Left.Parent = node; 
                return RightRotate(node);
            }

            if (GetBalance(node) < -1 && GetBalance(node.Right) > 0)
            {
                node.Right = RightRotate(node.Right);
                if (node.Right != null) node.Right.Parent = node;
                return LeftRotate(node);
            }
            return node;
        }

        /// <summary>
        /// 移除最大元素
        /// </summary>
        /// <returns></returns>
        public E RemoveMax()
        {
            E e = GetMax();
            Node cur = _root;
            Node parent = null;
            while (cur.Right!=null)
            {
                parent = cur;
                cur = cur.Right;
            }

            if (parent == null)
            {
                _root = cur.Left;
                if (_root != null) _root.Parent = null;
            }
            else
            {
                parent.Right = cur.Left;
                if (parent.Right != null) parent.Right.Parent = parent;
                while (parent != null)
                {
                    parent.Height = Math.Max(GetHeight(parent.Left), GetHeight(parent.Right)) + 1;
                    int balance = GetBalance(parent);
                    if (balance > 1 && GetBalance(parent.Left) >= 0)
                    {
                        Node prev = parent.Parent;
                        Node right = RightRotate(parent);
                        if (prev == null)
                        {
                            _root = right;
                            if (_root != null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = right;
                            if (prev.Right != null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = right;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }
                    else if (balance < -1 && GetBalance(parent.Right) <= 0)
                    {
                        Node prev = parent.Parent;
                        Node left = LeftRotate(parent);
                        if (prev == null)
                        {
                            _root = left;
                            if (_root != null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = left;
                            if (prev.Right != null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = left;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }
                    else if (balance > 1 && GetBalance(parent.Left) < 0)
                    {
                        parent.Right = LeftRotate(parent.Right);
                        if (parent.Right != null) parent.Right.Parent = parent;
                        Node prev = parent.Parent;
                        Node right = RightRotate(parent);
                        if (prev == null)
                        {
                            _root = right;
                            if (_root != null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = right;
                            if (prev.Right != null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = right;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }
                    else if (balance < -1 && GetBalance(parent.Left) > 0)
                    {
                        parent.Left = RightRotate(parent.Right);
                        if (parent.Left != null) parent.Left.Parent = parent;
                        Node prev = parent.Parent;
                        Node left = LeftRotate(parent);
                        if (prev == null)
                        {
                            _root = left;
                            if (_root != null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = left;
                            if (prev.Right != null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = left;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }

                    parent = parent.Parent;
                }
            }
            cur = null;
            Size--;
            return e;
        }

        /// <summary>
        /// 移除最大元素(递归)
        /// </summary>
        /// <returns></returns>
        public E RemoveMaxByRecursion()
        {
            E e = GetMax();
            _root = RemoveMaxByRecursion(_root);
            return e;
        }

        /// <summary>
        /// 移除node节点中最大元素 (递归)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node RemoveMaxByRecursion(Node node)
        {
            if (node.Right == null)
            {
                Node left = node.Left;
                node = null;
                Size--;
                return left;
            }

            node.Right = RemoveMaxByRecursion(node.Right);
            if (node.Right != null) node.Right.Parent = node;
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            if (GetBalance(node) > 1 && GetBalance(node.Left) >= 0) return RightRotate(node);
            if (GetBalance(node) < -1 && GetBalance(node.Right) <= 0) return LeftRotate(node);
            if (GetBalance(node) > 1 && GetBalance(node.Left) < 0)
            {
                node.Left = LeftRotate(node.Left);
                if (node.Left != null) node.Left.Parent = node;
                return RightRotate(node);
            }

            if (GetBalance(node) < -1 && GetBalance(node.Right) > 0)
            {
                node.Right = RightRotate(node.Right);
                if (node.Right != null) node.Right.Parent = node;
                return LeftRotate(node);
            }
            return node;
        }

        /// <summary>
        /// 通过后继删除节点
        /// </summary>
        /// <returns></returns>
        private Node SuccessorDelNode(Node node)
        {
            if (node?.Right == null) return node;
            Node successor = GetMinByRecursion(node.Right);
            successor.Right = RemoveMinByRecursion(node.Right);
            if (successor.Right != null) successor.Right.Parent = successor;
            successor.Left = node.Left;
            if (successor.Left != null) successor.Left.Parent = successor;
            node = null;
            return successor;
        }

        /// <summary>
        /// 通过前驱删除当前节点
        /// </summary>
        /// <returns></returns>
        private Node PreCursorDelNode(Node node)
        {
            if (node?.Left == null) return node;
            Node precursor = GetMaxByRecursion(node.Left);
            precursor.Left = RemoveMaxByRecursion(node.Left);
            if (precursor.Left != null) precursor.Left.Parent = precursor;
            precursor.Right = node.Right;
            if (precursor.Right != null) precursor.Right.Parent = precursor;
            node = null;
            return precursor;
        }

        /// <summary>
        /// 删除键值为e的节点
        /// </summary>
        /// <param name="e"></param>
        public void Remove(E e)
        {
            if(!Contains(e)) throw new Exception("this element is not exists");
            Node cur = _root;
            Node parent = null;
            while (cur.Key.CompareTo(e) != 0)
            {
                parent = cur;
                if (cur.Key.CompareTo(e) < 0) cur = cur.Right;
                else if (cur.Key.CompareTo(e) > 0) cur = cur.Left;
            }

            if (parent == null)
            {
                if (cur.Left == null || cur.Right == null)
                {
                    _root = cur.Left ?? cur.Right;
                    cur = null;
                    Size--;
                }
                else _root = SuccessorDelNode(_root);
                if (_root != null) _root.Parent = null;
            }
            else
            {
                if (parent.Left == cur)
                {
                    if (cur.Left == null || cur.Right == null)
                    {
                        parent.Left = cur.Left ?? cur.Right;
                        Size--;
                        cur = null;
                    }
                    else parent.Left = SuccessorDelNode(cur);

                    if (parent.Left != null) parent.Left.Parent = parent;
                }
                else
                {
                    if (cur.Left == null || cur.Right == null)
                    {
                        parent.Right = cur.Left ?? cur.Right;
                        Size--;
                        cur = null;
                    }
                    else parent.Right = SuccessorDelNode(cur);
                    if (parent.Right != null) parent.Right.Parent = parent;
                }
                while (parent != null)
                {
                    parent.Height = Math.Max(GetHeight(parent.Left), GetHeight(parent.Right)) + 1;
                    int balance = GetBalance(parent);
                    if (balance > 1 && GetBalance(parent.Left) >= 0)
                    {
                        Node prev = parent.Parent;
                        Node right = RightRotate(parent);
                        if (prev == null)
                        {
                            _root = right;
                            if (_root != null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = right;
                            if (prev.Right != null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = right;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }
                    else if (balance < -1 && GetBalance(parent.Right) <= 0)
                    {
                        Node prev = parent.Parent;
                        Node left = LeftRotate(parent);
                        if (prev == null)
                        {
                            _root = left;
                            if (_root != null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = left;
                            if (prev.Right != null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = left;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }
                    else if (balance > 1 && GetBalance(parent.Left) < 0)
                    {
                        parent.Right = LeftRotate(parent.Right);
                        if (parent.Right != null) parent.Right.Parent = parent;
                        Node prev = parent.Parent;
                        Node right = RightRotate(parent);
                        if (prev == null)
                        {
                            _root = right;
                            if (_root != null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = right;
                            if (prev.Right != null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = right;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }
                    else if (balance < -1 && GetBalance(parent.Left) > 0)
                    {
                        parent.Left = RightRotate(parent.Right);
                        if (parent.Left != null) parent.Left.Parent = parent;
                        Node prev = parent.Parent;
                        Node left = LeftRotate(parent);
                        if (prev == null)
                        {
                            _root = left;
                            if (_root != null) _root.Parent = null;
                        }
                        else if (prev.Key.CompareTo(parent.Key) < 0)
                        {
                            prev.Right = left;
                            if (prev.Right != null) prev.Right.Parent = prev;
                        }
                        else if (prev.Key.CompareTo(parent.Key) > 0)
                        {
                            prev.Left = left;
                            if (prev.Left != null) prev.Left.Parent = prev;
                        }
                    }

                    parent = parent.Parent;
                }
            }
        }

        /// <summary>
        /// 删除键值为e的节点(递归)
        /// </summary>
        /// <param name="e"></param>
        public void RemoveByRecursion(E e)
        {
            if(!Contains(e)) throw new Exception("this element is not exists");
            _root = RemoveByRecursion(_root, e);
        }

        /// <summary>
        /// 删除键值为e的节点(递归)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="e"></param>
        private Node RemoveByRecursion(Node node,E e)
        {
            if (node == null) return null;
            Node retNode = null;
            if (node.Key.CompareTo(e) > 0)
            {
                node.Left = RemoveByRecursion(node.Left,e);
                if (node.Left != null) node.Left.Parent = node;
                retNode= node;
            }
            else if (node.Key.CompareTo(e) < 0)
            {
                node.Right = RemoveByRecursion(node.Right, e);
                if (node.Right != null) node.Right.Parent = node;
                retNode = node;
            }
            else
            {
                if (node.Left == null || node.Right == null)
                {
                    retNode = node.Left ?? node.Right;
                    node = null;
                    Size--;
                }
                else
                {
                    retNode= SuccessorDelNode(node);
                }
            }

            if (retNode == null) return null;

            retNode.Height = Math.Max(GetHeight(retNode.Left), GetHeight(retNode.Right)) + 1;
            int balance = GetBalance(retNode);
            if (balance > 1 && GetBalance(retNode.Left) >= 0) return RightRotate(retNode);
            if (balance < -1 && GetBalance(retNode.Left) <= 0) return LeftRotate(retNode);
            if (balance > 1 && GetBalance(retNode.Left) < 0)
            {
                retNode.Left = LeftRotate(retNode.Left);
                if (retNode.Left != null) retNode.Left.Parent = retNode;
                return RightRotate(retNode);
            }

            if (balance < -1 && GetBalance(retNode.Left) > 0)
            {
                retNode.Right = RightRotate(retNode.Right);
                if (retNode.Right != null) retNode.Right.Parent = retNode;
                return LeftRotate(retNode);
            }

            return retNode;
        }

        /// <summary>
        /// 获取节点高度
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private int GetHeight(Node node)
        {
            if (node == null) return 0;
            return node.Height;
        }

        /// <summary>
        /// 计算平衡因子
        /// </summary>
        /// <returns></returns>
        private int GetBalance(Node node)
        {
            if (node == null) return 0;
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        /// <summary>
        /// 是否是二分搜索树
        /// </summary>
        /// <returns></returns>
        public bool IsBinarySearchTree()
        {
            SequentialTraversal();
            for (int i = 1; i < SequentialOrder.Count; i++)
            {
                if (SequentialOrder[i].CompareTo(SequentialOrder[i - 1]) < 0) return false;
            }

            return true;
        }

        /// <summary>
        /// 是否是平衡二叉树
        /// </summary>
        /// <returns></returns>
        public bool IsBalanceBinaryTree()
        {
            return IsBalanceBinaryTree(_root);
        }

        /// <summary>
        /// 是否是平衡二叉树
        /// </summary>
        /// <returns></returns>
        private bool IsBalanceBinaryTree(Node node)
        {
            if (node == null) return true;
            if (Math.Abs(GetBalance(node)) > 1) return false;
            return IsBalanceBinaryTree(node.Left) && IsBalanceBinaryTree(node.Right);
        }

        /// <summary>
        /// 左旋转
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node LeftRotate(Node node)
        {
            Node x = node.Right;
            Node z = x.Left;
            x.Left = node;
            node.Parent = x;
            node.Right = z;
            if(z!=null) z.Parent = node;
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
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
            Node z = x.Right;
            x.Right = node;
            node.Parent = x;
            node.Left = z;
            if (z != null) z.Parent = node;
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            return x;
        }
    }
}