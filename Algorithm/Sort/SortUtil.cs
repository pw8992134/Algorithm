using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Sort
{
    public static class SortUtil
    {
        /// <summary>
        /// 插入排序
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int[] InsertionSort(int[] args)
        {
            for (int i = 1; i < args.Length; i++)
            {
                int key = args[i];
                int j = i - 1;
                while (j >= 0 && key < args[j])
                {
                    args[j + 1] = args[j];
                    j--;
                }

                args[j + 1] = key;
            }

            return args;
        }

        /// <summary>
        /// 选择排序
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int[] SelectionSort(int[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                int key = args[i];
                int minIndex = i;
                for (int j = i + 1; j < args.Length; j++)
                {
                    if (args[j] < key)
                    {
                        key = args[j];
                        minIndex = j;
                    }
                }

                Swap(args, i, minIndex);
            }

            return args;
        }

        /// <summary>
        /// 归并排序
        /// </summary>
        /// <returns></returns>
        public static void MergeSort(int[] args, int p, int r)
        {
            if (p < r)
            {
                int q = (int)Math.Floor((double)(p + r) / 2);
                MergeSort(args, p, q);
                MergeSort(args, q + 1, r);
                Merge(args, p, q, r);
            }
        }

        /// <summary>
        /// 合并过程
        /// </summary>
        /// <param name="args"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        public static void Merge(int[] args, int p, int q, int r)
        {
            int n1 = q - p + 1;
            int n2 = r - q;
            int[] nums1 = new int[n1];
            int[] nums2 = new int[n2];
            for (int x = 0; x < n1; x++)
            {
                nums1[x] = args[p + x];
            }

            for (int x = 0; x < n2; x++)
            {
                nums2[x] = args[q + x + 1];
            }

            int i = 0, j = 0;
            int final = p;
            for (int k = 0; k < n1 + n2; k++)
            {
                if (i >= n1 || j >= n2)
                {
                    final = p + k;
                    break;
                }
                if (nums1[i] < nums2[j])
                    args[p + k] = nums1[i++];
                else
                    args[p + k] = nums2[j++];
            }

            if (i >= n1)
            {
                for (int k = j; k < n2; k++)
                {
                    args[final++] = nums2[k];
                }
            }

            if (j >= n2)
            {
                for (int k = i; k < n1; k++)
                {
                    args[final++] = nums1[k];
                }
            }
        }

        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <param name="args"></param>
        public static void BubbleSort(int[] args)
        {
            for (int k = 0; k < args.Length - 1; k++)
            {
                for (int i = 0; i < args.Length - 1; i++)
                {
                    for (int j = i + 1; j < args.Length; j++)
                    {
                        if (args[j] < args[j - 1])
                        {
                            Swap(args, j - 1, j);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 堆排序
        /// </summary>
        /// <param name="args"></param>
        public static int[] HeapSort(int[] args)
        {
            Heap heap = new Heap(args);
            int[] sorted = heap.HeapSort();
            return sorted;
        }

        /// <summary>
        /// 快速排序(n^2)
        /// </summary>
        public static void QuikSort(int[] args, int p, int r)
        {
            if (p < r)
            {
                int q = Partion(args, p, r);
                QuikSort(args, p, q - 1);
                QuikSort(args, q + 1, r);
            }
        }

        /// <summary>
        /// 原址重排
        /// </summary>
        /// <param name="args"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static int Partion(int[] args, int p, int r)
        {
            int x = args[r];
            int i = p - 1;

            for (int j = p; j <= r - 1; j++)
            {
                if (args[j] <= x)
                {
                    i++;
                    Swap(args, i, j);
                }
            }

            Swap(args, i + 1, r);
            return i + 1;
        }

        /// <summary>
        /// 交换两个数
        /// </summary>
        /// <param name="args"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public static void Swap(int[] args, int i, int j)
        {
            if (i < 0 || i > args.Length - 1 || j < 0 || j > args.Length - 1) throw new IndexOutOfRangeException();
            int temp = args[i];
            args[i] = args[j];
            args[j] = temp;
        }


        /// <summary>
        /// 计数排序(不支持负数,是一种桶排序)
        /// </summary>
        public static void CountingSort(int[] args, int[] sorted, int max)
        {
            int[] counting = new int[max + 1];
            foreach (var t in args)
            {
                counting[t]++;
            }

            for (int i = 1; i < max + 1; i++)
            {
                counting[i] = counting[i] + counting[i - 1];
            }

            for (int i = args.Length - 1; i >= 0; i--)
            {
                sorted[counting[args[i]] - 1] = args[i];
                counting[args[i]]--;
            }
        }

        /// <summary>
        /// 计数排序(支持负数,是一种桶排序))
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int[] CoutingSortSupportNegative(int[] args)
        {
            int[] rv = new int[args.Length];
            SortedDictionary<int, int> dictionary = new SortedDictionary<int, int>();
            foreach (var v in args)
            {
                if (!dictionary.ContainsKey(v)) dictionary.Add(v, 0);
                dictionary[v]++;
            }

            int[] keys = dictionary.Keys.ToArray();
            for (int i = 1; i < keys.Length; i++)
            {
                dictionary[keys[i]] = dictionary[keys[i - 1]] + dictionary[keys[i]];
            }

            for (int i = args.Length - 1; i >= 0; i--)
            {
                rv[dictionary[args[i]] - 1] = args[i];
                dictionary[args[i]]--;
            }

            return rv;
        }

        /// <summary>
        /// 基数排序(是一种桶排序,基于位,只支持非负整数)
        /// </summary>
        /// <returns></returns>
        public static int[] RadixSort(int[] args)
        {
            int maxPosition = MaxPosition(args);
            for (int i = 0; i < maxPosition; i++)
            {
                SortedDictionary<int, List<int>> dictionary = new SortedDictionary<int, List<int>>();
                for (int j = 0; j < 10; j++)
                {
                    dictionary.Add(j, new List<int>());
                }

                for (int j = 0; j < args.Length; j++)
                {
                    string str = args[j].ToString();
                    int curPosition = str.Length >= i + 1 ? int.Parse(str.Substring(str.Length - 1 - i, 1)) : 0;
                    dictionary[curPosition].Add(args[j]);
                }

                int curIndex = 0;
                foreach (var v in dictionary.Values)
                {
                    foreach (var w in v)
                    {
                        args[curIndex++] = w;
                    }
                }
            }

            return args;
        }

        /// <summary>
        /// 桶排序(分桶后插入排序收集)
        /// </summary>
        /// <returns></returns>
        public static int[] BucketSort(int[] args)
        {
            int max = args.Max();
            int[] bucket = new int[max + 1];
            bool isContainsNegative = args.FirstOrDefault(x => x < 0) != 0;
            int min;
            int[] negativeBucket = null;
            if (isContainsNegative)
            {
                min = args.Min();
                negativeBucket = new int[-min + 1];
            }
            foreach (var v in args)
            {
                if (v < 0) negativeBucket[-v]++;
                else bucket[v]++;
            }

            int index = 0;
            if (isContainsNegative)
            {
                for (int i = negativeBucket.Length - 1; i >= 0; i--)
                {
                    for (int j = 0; j < negativeBucket[i]; j++)
                    {
                        args[index++] = -i;
                    }
                }
            }

            for (int i = 0; i < bucket.Length; i++)
            {
                for (int j = 0; j < bucket[i]; j++)
                {
                    args[index++] = i;
                }
            }

            return args;
        }

        /// <summary>
        /// 希尔排序(间隔插入排序)
        /// </summary>
        /// <returns></returns>
        public static int[] ShellSort(int[] args)
        {
            int distance = args.Length;
            if (args.Length == 1) return args;
            while (true)
            {
                distance = distance / 2;
                for (int i = 0; i < distance; i++)
                {
                    for (int j = i + distance; j < args.Length; j += distance)
                    {
                        if (args[j] < args[j - distance])
                        {
                            int key = args[j];
                            int k = j - distance;
                            while (k >= i && args[k] > key)
                            {
                                args[k + distance] = args[k];
                                k -= distance;
                            }

                            args[k + distance] = key;
                        }
                    }
                }
                if (distance == 1) break;
            }

            return args;
        }

        /// <summary>
        /// 获取最高位
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int MaxPosition(int[] args)
        {
            int maxPosition = 0;
            for (int i = 0; i < args.Length; i++)
            {
                int curPosition = args[0].ToString().Length;
                if (curPosition > maxPosition)
                {
                    maxPosition = curPosition;
                }
            }

            return maxPosition;
        }
    }

    public class Heap
    {
        private readonly int[] _array;

        public int HeapSize { get; private set; }

        public Heap(int[] args)
        {
            _array = args;
            BuildHeap();
        }

        /// <summary>
        /// 建立堆
        /// </summary>
        public void BuildHeap()
        {
            HeapSize = _array.Length;
            int bound = (int)Math.Floor((double)_array.Length / 2);
            for (int i = bound - 1; i >= 0; i--)
            {
                MaintainHeap(i);
            }
        }

        /// <summary>
        /// 维护堆的性质
        /// </summary>
        /// <param name="i"></param>
        public void MaintainHeap(int i)
        {
            int l = Left(i);
            int r = Right(i);
            int min = i;
            if (l < HeapSize && _array[l] < _array[min])
                min = l;
            if (r < HeapSize && _array[r] < _array[min])
                min = r;
            if (min != i)
            {
                SortUtil.Swap(_array, i, min);
                MaintainHeap(min);
            }
        }

        /// <summary>
        /// 左孩子
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int Left(int i)
        {
            ValidNumber(i);
            return 2 * i;
        }

        /// <summary>
        /// 验证索引
        /// </summary>
        /// <param name="i"></param>
        private void ValidNumber(int i)
        {
            if (i < 0 && i > _array.Length) throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// 右孩子
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int Right(int i)
        {
            ValidNumber(i);
            return 2 * i + 1;
        }

        /// <summary>
        /// 父亲节点
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int Parent(int i)
        {
            ValidNumber(i);
            return (int)Math.Floor((decimal)i / 2);
        }

        /// <summary>
        /// 堆排序(nlgn)
        /// </summary>
        /// <returns></returns>
        public int[] HeapSort()
        {
            for (int i = _array.Length - 1; i > 0; i--)
            {
                SortUtil.Swap(args: _array, 0, i);
                HeapSize--;
                MaintainHeap(0);
            }

            return _array;
        }
    }
}