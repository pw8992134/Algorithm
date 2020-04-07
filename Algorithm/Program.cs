using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Algorithm.AvlTree;
using Algorithm.BinarySeachTree;
using Algorithm.DynamicArray;
using Algorithm.HashTable;
using Algorithm.Map;
using Algorithm.Queue;
using Algorithm.RedBlackTree;
using Algorithm.SegmentTree;
using Algorithm.Set;
using Algorithm.Stack;
using Algorithm.UnionCheckingSet;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestDynamicArray();
            //TestQueue();
            //TestBinarySearchTree();
            //TestSet();
            //TestDictionary();
            //int[] nums1=new int[]{ 1, 2, 2, 1 };
            //int[] nums2=new int[]{ 2, 2 };
            //int[] a=Intersect(nums1,nums2);
            //TestPriorityQueue();
            //TestSegmentTree();
            //TestTrie();
            //TestUnionCheckingSet();
            //TestAvlTree();
            //TestRedBlackTree();
            TestHashTable();
        }

        #region test dynamic array

        public static void TestDynamicArray()
        {
            DynamicArray.Array<int> array = new DynamicArray.Array<int>(10);
            for (int i = 0; i < 10; i++)
            {
                array.AddLast(i);
            }

            Console.WriteLine(array);
            array.Add(1, 100);
            Console.WriteLine(array);
            array.AddFirst(-1);
            Console.WriteLine(array);
            array.Remove(2);
            Console.WriteLine(array);
            array.RemoveFirst();
            Console.WriteLine(array);
            array.RemoveLast();
            Console.WriteLine(array);
            array.AddFirst(5);
            array.AddFirst(5);
            Console.WriteLine(array);
            Console.WriteLine(array.FindAll(5));
            array.RemoveAll(5);
            Console.WriteLine(array);
            array.Remove(3);
            Console.WriteLine(array);
            Console.WriteLine(array.Contains(8));
            array.Set(5, 789);
            Console.WriteLine(array);
            Console.WriteLine(array.Get(5));

        }

        #endregion

        #region test stack



        #endregion

        #region test queue

        public static void TestQueue()
        {
            ArrayQueue<int> q1 = new ArrayQueue<int>();
            long t1 = GetSeconds(q1);
            Console.WriteLine(t1);

            LoopQueue<int> q2 = new LoopQueue<int>();
            long t2 = GetSeconds(q2);
            Console.WriteLine(t2);
        }

        public static long GetSeconds(IQueue<int> queue)
        {
            int count = 100000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Random r = new Random();
            for (int i = 0; i < count; i++)
            {
                queue.Enqueue(r.Next(int.MaxValue));
            }

            for (int i = 0; i < count; i++)
            {
                queue.Dequeue();
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        #endregion

        #region test linkedlist


        public class ListNode
        {
            public int val;
            public ListNode next;

            public ListNode(int x)
            {
                val = x;
            }
        }

        public ListNode RemoveElements(ListNode head, int val)
        {
            if (head == null) return null;
            if (head.val == val)
            {
                return RemoveElements(head.next, val);
            }
            else
            {
                head.next = RemoveElements(head.next, val);
                return head;
            }
        }

        #endregion

        #region test binary search tree

        public static void TestBinarySearchTree()
        {
            BinarySeachTree.BinarySearchTree<int> binarySearchTree=new BinarySearchTree<int>();
            binarySearchTree.AddByRecursion(12);
            binarySearchTree.AddByRecursion(1);
            binarySearchTree.AddByRecursion(58);
            binarySearchTree.AddByRecursion(64); 
            binarySearchTree.AddByRecursion(76);
            binarySearchTree.AddByRecursion(21); 
            binarySearchTree.AddByRecursion(148);
            Console.Write(binarySearchTree.ContainsByRecursion(12));
            Console.Write(binarySearchTree.ContainsByRecursion(24));
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            binarySearchTree.PreOrderTraversal();
            Console.WriteLine(binarySearchTree.PreOrder.Translate());
            binarySearchTree.SequentialTraversal();
            Console.WriteLine(binarySearchTree.Sequential.Translate());
            binarySearchTree.PostOrderTraversal();
            Console.WriteLine(binarySearchTree.PostOrder.Translate());
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            binarySearchTree.PreOrderTraversalByRecursion();
            Console.WriteLine(binarySearchTree.PreOrder.Translate());
            binarySearchTree.SequentialTraversalByRecursion();
            Console.WriteLine(binarySearchTree.Sequential.Translate());
            binarySearchTree.PostOrderTraversalByRecursion();
            Console.WriteLine(binarySearchTree.PostOrder.Translate());
            binarySearchTree.LevelTraversal();
            Console.WriteLine(binarySearchTree.LevelOrder.Translate());
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            //Console.WriteLine(binarySearchTree.RemoveMinInTree());
            //Console.WriteLine(binarySearchTree.RemoveMinInTree());
            //Console.WriteLine(binarySearchTree.RemoveMinInTree());
            //Console.WriteLine(binarySearchTree.RemoveMinInTree());
            //Console.WriteLine(binarySearchTree.RemoveMinInTree());
            //Console.WriteLine(binarySearchTree.RemoveMinInTree());
            //Console.WriteLine(binarySearchTree.RemoveMinInTree());
            //Console.WriteLine(binarySearchTree.RemoveMinInTree());
            //Console.WriteLine(binarySearchTree.RemoveMinInTreeByRecursion());
            //Console.WriteLine(binarySearchTree.RemoveMinInTreeByRecursion());
            //Console.WriteLine(binarySearchTree.RemoveMinInTreeByRecursion());
            //Console.WriteLine(binarySearchTree.RemoveMinInTreeByRecursion());
            //Console.WriteLine(binarySearchTree.RemoveMinInTreeByRecursion());
            //Console.WriteLine(binarySearchTree.RemoveMinInTreeByRecursion());
            //Console.WriteLine(binarySearchTree.RemoveMinInTreeByRecursion());
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            //Console.WriteLine(binarySearchTree.MinInTreeByRecursion());
            //Console.WriteLine(binarySearchTree.MaxInTreeByRecursion());
            //Console.WriteLine(binarySearchTree.RemoveMaxInTree());
            //Console.WriteLine(binarySearchTree.RemoveMaxInTree());
            //Console.WriteLine(binarySearchTree.RemoveMaxInTree());
            //Console.WriteLine(binarySearchTree.RemoveMaxInTree());
            //Console.WriteLine(binarySearchTree.RemoveMaxInTree());
            //Console.WriteLine(binarySearchTree.RemoveMaxInTree());
            //Console.WriteLine(binarySearchTree.RemoveMaxInTree());
            //binarySearchTree.Remove(12);
            //Console.WriteLine(binarySearchTree.Contains(12));
            //binarySearchTree.Remove(1);
            //Console.WriteLine(binarySearchTree.Contains(1));
            //binarySearchTree.Remove(64);
            //Console.WriteLine(binarySearchTree.Contains(64));
            //binarySearchTree.Remove(148);
            //Console.WriteLine(binarySearchTree.Contains(148));
            //binarySearchTree.Remove(21);
            //Console.WriteLine(binarySearchTree.Contains(21));
            binarySearchTree.RemoveByRecursion(12);
            Console.WriteLine(binarySearchTree.Contains(12));
            binarySearchTree.RemoveByRecursion(1);
            Console.WriteLine(binarySearchTree.Contains(1));
            binarySearchTree.RemoveByRecursion(64);
            Console.WriteLine(binarySearchTree.Contains(64));
            binarySearchTree.RemoveByRecursion(148);
            Console.WriteLine(binarySearchTree.Contains(148));
            binarySearchTree.RemoveByRecursion(21);
            Console.WriteLine(binarySearchTree.Contains(21));
            binarySearchTree.RemoveByRecursion(58);
            Console.WriteLine(binarySearchTree.Contains(58));
            binarySearchTree.RemoveByRecursion(76);
            Console.WriteLine(binarySearchTree.Contains(76));
        }

        #endregion

        #region test set

        public static void TestSet()
        {
            string[] s=new string[]{"ss","we","rng","edg","ig","ss"};
            LinkedListSet<string> linkedListSet=new LinkedListSet<string>();
            foreach (var v in s)
            {
                linkedListSet.Add(v);
            }
            Console.WriteLine(linkedListSet.Size);

            BinarySearchTree<string> binarySearchTree=new BinarySearchTree<string>();
            foreach (var v in s)
            {
                binarySearchTree.Add(v);
            }
            Console.WriteLine(binarySearchTree.Size);
        }

        #endregion

        #region test dictionary

        /// <summary>
        /// 
        /// </summary>
        public static void TestDictionary()
        {
            LinkedListDictionary<string,int> dic=new LinkedListDictionary<string, int>();
            dic.Add("ss",12);
            dic.Add("rng",58);
            dic.Remove("ss");

            BinarySearchTreeDictionary<string,int> dictionary=new BinarySearchTreeDictionary<string, int>();
            dictionary.Add("edg",156);
            dictionary.Add("vg",456);
            dictionary.Set("vg",789);
            dictionary.Remove("edg");
        }

        //804
        public int UniqueMorseRepresentations(string[] words)
        {
            string[] letters=new string[]{".-","-...","-.-.","-..",".","..-.","--.","....","..",".---","-.-",".-..","--","-.","---",".--.","--.-",".-.","...","-","..-","...-",".--","-..-","-.--","--.."};
            BinarySearchTreeSet<string> binarySearchTreeSet=new BinarySearchTreeSet<string>();
            foreach (string word in words)
            {
                StringBuilder builder=new StringBuilder();
                foreach (char c in word)
                {
                    builder.Append(letters[c - 'a']);
                }
                binarySearchTreeSet.Add(builder.ToString());
            }

            return binarySearchTreeSet.Size;
        }

        //349
        public static int[] Intersection(int[] nums1, int[] nums2)
        {
            BinarySearchTreeSet<int> binarySearchTreeSet = new BinarySearchTreeSet<int>();
            foreach (var num in nums1)
            {
                binarySearchTreeSet.Add(num);
            }
            List<int> list=new List<int>();
            foreach (var num in nums2)
            {
                if (binarySearchTreeSet.Contains(num))
                {
                    list.Add(num);
                    binarySearchTreeSet.Remove(num);
                }
            }

            return list.ToArray();
        }

        public static int[] Intersect(int[] nums1, int[] nums2)
        {
            BinarySearchTreeDictionary<int,int> dictionary=new BinarySearchTreeDictionary<int, int>();
            foreach (var num in nums1)
            {
                if(!dictionary.Contains(num)) dictionary.Add(num,1);
                else
                {
                    dictionary.Set(num,dictionary.Get(num)+1);
                }
            }
            List<int> list=new List<int>();
            foreach (var num in nums2)
            {
                if (dictionary.Contains(num))
                {
                    list.Add(num);
                    dictionary.Set(num,dictionary.Get(num)-1);
                    if (dictionary.Get(num) == 0) dictionary.Remove(num);
                }
            }

            return list.ToArray();
        }

        #endregion

        #region test priority queue

        public static void TestPriorityQueue()
        {
            int[] nums=new int[]{ 1, 1, 1, 2, 2, 3 };
            Program p=new Program();
            IList<int> list =p.TopKFrequent(nums,2);
        }

        public class Node:IComparable<Node>, IComparable
        {
            public int key { get;set; }

            public int Value { get; set; }

            public Node(int key, int value)
            {
                this.key = key;
                Value = value;
            }

            public int CompareTo(Node other)
            {
                return -(this.Value - other.Value);
            }

            public int CompareTo(object obj)
            {
                if (ReferenceEquals(null, obj)) return 1;
                if (ReferenceEquals(this, obj)) return 0;
                return obj is Node other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Node)}");
            }
        }

        public IList<int> TopKFrequent(int[] nums, int k)
        {
            Dictionary<int,int> dic=new Dictionary<int, int>();
            foreach (var num in nums)
            {
                if(!dic.ContainsKey(num)) dic.Add(num,1);
                else
                {
                    dic[num] = dic[num] + 1;
                }
            }
            List<KeyValuePair<int,int>> list= dic.ToList();
            list.Sort((s,v)=>-s.Value.CompareTo(v.Value));
            List<int> ret=new List<int>();
            foreach (var kv in list.Take(k))
            {
                ret.Add(kv.Key);
            }

            return ret;
        }

        #endregion

        #region test segment tree

        private class SumMerger:IMerger<int>
        {
            public int Merge(int a, int b)
            {
                return a + b;
            }
        }

        public static void TestSegmentTree()
        {
            int[] nums=new int[]{ -2, 0, 3, -5, 2, -1 };
            SegmentTree.SegmentTree<int> segmentTree = new SegmentTree<int>(nums,new SumMerger());
            segmentTree.Update(2,-3);
            Console.WriteLine(segmentTree.Query(0,3));
        }

        public class NumArray
        {
            private readonly SegmentTree<int> _segmentTree;

            private readonly int[] _data;

            private class SumMerger : IMerger<int>
            {
                public int Merge(int a, int b)
                {
                    return a + b;
                }
            }

            public NumArray(int[] nums)
            {
                if (nums.Length != 0)
                {
                    _data = nums;
                    _segmentTree = new SegmentTree<int>(nums, new SumMerger());
                }
            }

            public void Update(int i, int val)
            {
                if(_segmentTree==null) throw new Exception("length is 0");
                if(i<0 || i>=_data.Length) throw new Exception("index is illegal");
                _segmentTree.Update(i,val);
            }

            public int SumRange(int i, int j)
            {
                if(_segmentTree==null) throw new Exception("length is 0");
                return _segmentTree.Query(i, j);
            }
        }

        #endregion

        #region test trie

        public static void TestTrie()
        {
            //["WordDictionary","addWord","addWord","addWord","addWord","search","search","addWord","search","search","search","search","search","search"]
            //[[],["at"],["and"],["an"],["add"],["a"],[".at"],["bat"],[".at"],["an."],["a.d."],["b."],["a.d"],["."]]

            //["WordDictionary","addWord","addWord","search","search","search","search","search","search","search","search"]
            //[[],["a"],["ab"],["a"],["a."],["ab"],[".a"],[".b"],["ab."],["."],[".."]]

            Trie.Trie trie=new Trie.Trie();
            //trie.Add("at");
            //trie.Add("and");
            //trie.Add("an");
            //trie.Add("add");
            //Console.WriteLine(trie.PatternMatch("a"));//false
            //Console.WriteLine(trie.PatternMatch(".at"));//false
            //trie.Add("bat");
            //Console.WriteLine(trie.PatternMatch(".at"));//true
            //Console.WriteLine(trie.PatternMatch("an."));//true
            //Console.WriteLine(trie.PatternMatch("a.d."));//false
            //Console.WriteLine(trie.PatternMatch("b."));//false
            //Console.WriteLine(trie.PatternMatch("a.d"));//true
            //Console.WriteLine(trie.PatternMatch("."));//false

            trie.Add("a");
            trie.Add("ab");
            Console.WriteLine(trie.PatternMatch("a"));//true .
            Console.WriteLine(trie.PatternMatch("a."));//true
            Console.WriteLine(trie.PatternMatch("ab"));//true
            Console.WriteLine(trie.PatternMatch(".a"));//false
            Console.WriteLine(trie.PatternMatch(".b"));//true
            Console.WriteLine(trie.PatternMatch("ab."));//false
            Console.WriteLine(trie.PatternMatch("."));//true .
            Console.WriteLine(trie.PatternMatch(".."));//true
        }

        #endregion

        #region test union checking set

        public static void TestUnionCheckingSet()
        {
            int size = 100000;
            int m = 10000000;
            Stopwatch stopwatch=new Stopwatch();
            Random r = new Random();
            //stopwatch.Start();
            //UnionCheckingSet.UnionCheckingSet1 u1=new UnionCheckingSet1(size);
            //for (int i = 0; i < m; i++)
            //{
            //    int a = r.Next(size);
            //    int b = r.Next(size);
            //    u1.UnionElements(a,b);
            //    u1.IsConnected(a, b);
            //}
            //stopwatch.Stop();
            //Console.WriteLine("u1 time :"+stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            UnionCheckingSet.UnionCheckingSet2 u2 = new UnionCheckingSet2(size);
            for (int i = 0; i < m; i++)
            {
                int a = r.Next(size);
                int b = r.Next(size);
                u2.UnionElements(a, b);
                u2.IsConnected(a, b);
            }
            stopwatch.Stop();
            Console.WriteLine("u2 time :" + stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            UnionCheckingSet.UnionCheckingSet3 u3 = new UnionCheckingSet3(size);
            for (int i = 0; i < m; i++)
            {
                int a = r.Next(size);
                int b = r.Next(size);
                u3.UnionElements(a, b);
                u3.IsConnected(a, b);
            }
            stopwatch.Stop();
            Console.WriteLine("u3 time :" + stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            UnionCheckingSet.UnionCheckingSet4 u4 = new UnionCheckingSet4(size);
            for (int i = 0; i < m; i++)
            {
                int a = r.Next(size);
                int b = r.Next(size);
                u4.UnionElements(a, b);
                u4.IsConnected(a, b);
            }
            stopwatch.Stop();
            Console.WriteLine("u4 time :" + stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            UnionCheckingSet.UnionCheckingSet5 u5 = new UnionCheckingSet5(size);
            for (int i = 0; i < m; i++)
            {
                int a = r.Next(size);
                int b = r.Next(size);
                u5.UnionElements(a, b);
                u5.IsConnected(a, b);
            }
            stopwatch.Stop();
            Console.WriteLine("u5 time :" + stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            UnionCheckingSet.UnionCheckingSet6 u6 = new UnionCheckingSet6(size);
            for (int i = 0; i < m; i++)
            {
                int a = r.Next(size);
                int b = r.Next(size);
                u6.UnionElements(a, b);
                u6.IsConnected(a, b);
            }
            stopwatch.Stop();
            Console.WriteLine("u6 time :" + stopwatch.ElapsedMilliseconds);
        }

        #endregion

        #region test avl tree

        public static void TestAvlTree()
        {
            AvlTree<int,int> avl=new AvlTree<int, int>();
            avl.Add(10, 10);
            avl.Add(9, 9);
            avl.Add(8, 8);
            avl.Add(7, 7);
            avl.Add(6, 6);
            avl.Add(5, 5);
            avl.Add(4, 4);
            avl.Add(3, 3);
            avl.Add(2, 2);
            avl.Add(1, 1);
            //avl.AddByRecursion(10, 10);
            //avl.AddByRecursion(9, 9);
            //avl.AddByRecursion(8, 8);
            //avl.AddByRecursion(7, 7);
            //avl.AddByRecursion(6, 6);
            //avl.AddByRecursion(5, 5);
            //avl.AddByRecursion(4, 4);
            //avl.AddByRecursion(3, 3);
            //avl.AddByRecursion(2, 2);
            //avl.AddByRecursion(1, 1);
            Console.WriteLine("avl size:"+avl.Size);
            Console.WriteLine("avl IsEmpty:" + avl.IsEmpty);
            Console.WriteLine("avl IsBalanceBinaryTree:" + avl.IsBalanceBinaryTree());
            Console.WriteLine("avl IsBinarySearchTree:" + avl.IsBinarySearchTree());
            Console.WriteLine("avl Contains 4:" + avl.Contains(4));
            Console.WriteLine("avl ContainsByRecursion 4:" + avl.ContainsByRecursion(4));
            avl.PreOrderTraversal();
            Console.WriteLine(avl.PreOrder.ToList().Translate());
            avl.PreOrderTraversalByRecursion();
            Console.WriteLine(avl.PreOrder.ToList().Translate());

            avl.SequentialTraversal();
            Console.WriteLine(avl.SequentialOrder.ToList().Translate());
            avl.SequentialTraversalByRecursion();
            Console.WriteLine(avl.SequentialOrder.ToList().Translate());

            avl.PostOrderTraversal();
            Console.WriteLine(avl.PostOrder.ToList().Translate());
            avl.PostOrderTraversalByRecursion();
            Console.WriteLine(avl.PostOrder.ToList().Translate());

            avl.LevelOrderTraversal();
            Console.WriteLine(avl.LevelOrder.ToList().Translate());
            avl.LevelOrderTraversalByRecursion();
            Console.WriteLine(avl.LevelOrder.ToList().Translate());
            //avl.Remove(4);
            //avl.Remove(6);
            //avl.Remove(7);
            //avl.Remove(9);
            //avl.RemoveByRecursion(4);
            //avl.RemoveByRecursion(6);
            //avl.RemoveByRecursion(7);
            //avl.RemoveByRecursion(9);
            avl.RemoveMax();
            avl.RemoveMin();
            avl.RemoveMaxByRecursion();
            avl.RemoveMinByRecursion();
            Console.WriteLine("avl size:" + avl.Size);
            Console.WriteLine("avl IsEmpty:" + avl.IsEmpty);
            Console.WriteLine("avl IsBalanceBinaryTree:" + avl.IsBalanceBinaryTree());
            Console.WriteLine("avl IsBinarySearchTree:" + avl.IsBinarySearchTree());
            Console.WriteLine("avl Contains 4:" + avl.Contains(4));
            Console.WriteLine("avl ContainsByRecursion 4:" + avl.ContainsByRecursion(4));
        }

        #endregion

        #region test redblack tree

        public static void TestRedBlackTree()
        {
            RedBlackTree<int,int> redBlackTree = new RedBlackTree<int,int>();
            redBlackTree.AddByRecursion(10,10);
            redBlackTree.AddByRecursion(9, 9);
            redBlackTree.AddByRecursion(8, 8);
            redBlackTree.AddByRecursion(7, 7);
            redBlackTree.AddByRecursion(6, 6); 
            redBlackTree.AddByRecursion(5, 5);
            redBlackTree.AddByRecursion(4, 4);
            redBlackTree.AddByRecursion(3, 3);
            redBlackTree.AddByRecursion(2, 2);
            redBlackTree.AddByRecursion(1, 1);
        }

        #endregion

        #region test hash table

        public static void TestHashTable()
        {
            Student student=new Student(15,"bobo","liu");
            Console.WriteLine(student.GetHashCode());
        }

        #endregion
    }

    static class Extendsion
    {
        public static string Translate(this List<int> list)
        {
            string s = "";
            list.ForEach(t=>s+=t+",");
            if (s.Contains(",")) s.TrimEnd(',');
            return s;
        }
    }
}
