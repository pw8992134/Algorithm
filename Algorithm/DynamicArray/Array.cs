using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm.DynamicArray
{
    /// <summary>
    /// 动态数组
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class Array<E>
    {
        /// <summary>
        /// 内部静态数组
        /// </summary>
        private E[] _data;

        /// <summary>
        /// 存储有效元素的长度
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity => _data.Length;

        /// <summary>
        /// 数组是否为空
        /// </summary>
        public bool IsEmpty => Size == 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity">容量</param>
        public Array(int capacity)
        {
            _data = new E[capacity];
            Size = 0;
        }

        /// <summary>
        /// 默认容量为10的构造函数
        /// </summary>
        public Array():this(10)
        {
            
        }

        /// <summary>
        /// 在数组末尾添加元素 O(1)
        /// </summary>
        /// <param name="e">添加的元素</param>
        public void AddLast(E e)
        {
            Add(Size,e);
        }

        /// <summary>
        /// 在索引位置添加元素 O(N)
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="e">添加的元素</param>
        public void Add(int index,E e)
        {
            if (Size == _data.Length) Resize(2*Capacity);
            if(index<0 || index>=Size) throw new Exception("Require index>=0 and index<Size");
            for (int i = Size-1; i >=index; i--)
            {
                _data[i+1]=_data[i];
            }

            _data[index] = e;
            Size++;
        }

        /// <summary>
        /// 在数组头部添加元素 O(N)
        /// </summary>
        /// <param name="e">添加的元素</param>
        public void AddFirst(E e)
        {
            Add(0,e);
        }

        /// <summary>
        /// 删除索引位置的元素 O(N) {均摊了Resize}
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public E Remove(int index)
        {
            if (index < 0 || index >= Size) throw new Exception("Require index>=0 and index<Size");
            E cur = _data[index];
            for (int i = index+1; i < Size; i++)
            {
                _data[i - 1] = _data[i];
            }

            Size--;
            if(Size==Capacity/4 && Capacity/2!=0) Resize(Capacity/2);
            return cur;
        }

        /// <summary>
        /// 删除数组头部的元素 O(N)
        /// </summary>
        /// <returns></returns>
        public E RemoveFirst()
        {
            return Remove(0);
        }

        /// <summary>
        /// 删除数组尾部的元素 O(1)
        /// </summary>
        /// <returns></returns>
        public E RemoveLast()
        {
            return Remove(Size - 1);
        }

        /// <summary>
        /// 删除指定元素 O(N)
        /// </summary>
        /// <param name="e">待删除的元素</param>
        public void RemoveElement(E e)
        {
            int index = Find(e);
            if (index != -1)
                Remove(index);
        }

        /// <summary>
        /// 删除所有的待删除元素 O(N)
        /// </summary>
        /// <param name="e">待删除的元素</param>
        public void RemoveAll(E e)
        {
            int index = Find(e);
            if (index != -1)
            {
                Remove(index);
                RemoveAll(e);
            }
        }

        /// <summary>
        /// 修改指定索引元素为e O(1)
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="e">元素值</param>
        public void Set(int index,E e)
        {
            if (index < 0 || index >= Size) throw new Exception("Set failed ,Index is illegal");
            _data[index] = e;
        }

        /// <summary>
        /// 获取索引位置的元素 O(1)
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public E Get(int index)
        {
            if(index<0 || index>=Size) throw new Exception("Get failed ,Index is illegal");
            return _data[index];
        }

        /// <summary>
        /// 获取最后一个元素 O(1)
        /// </summary>
        /// <returns></returns>
        public E GetLast()
        {
            return Get(Size - 1);
        }

        /// <summary>
        /// 获取第一个元素 O(1)
        /// </summary>
        /// <returns></returns>
        public E GetFirst()
        {
            return Get(0);
        }

        /// <summary>
        /// 是否包含此元素 O(N)
        /// </summary>
        /// <param name="e">元素</param>
        /// <returns></returns>
        public bool Contains(E e)
        {
            for (int i = 0; i < Size; i++)
            {
                if (_data[i].Equals(e)) return true;
            }

            return false;
        }

        /// <summary>
        /// 查找第一个等于e的索引值 O(N)
        /// </summary>
        /// <param name="e">元素e</param>
        /// <returns></returns>
        public int Find(E e)
        {
            for (int i = 0; i < Size; i++)
            {
                if (_data[i].Equals(e)) return i;
            }

            return -1;
        }

        /// <summary>
        /// 查找所有等于元素e的索引 O(N)
        /// </summary>
        /// <param name="e">元素e</param>
        /// <returns></returns>
        public int[] FindAll(E e)
        {
            List<int> list=new List<int>();
            for (int i = 0; i < Size; i++)
            {
                if(_data[i].Equals(e)) list.Add(i);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 复写ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder res=new StringBuilder();
            res.AppendFormat("Array:Size={0},Capacity={1} \n",Size,Capacity);
            res.Append("[");
            for (int i = 0; i < Size; i++)
            {
                res.Append(_data[i]);
                if (i != Size - 1) res.Append(",");
            }

            res.Append("]");
            return res.ToString();
        }

        /// <summary>
        /// 调整数组的大小 O(N)
        /// </summary>
        /// <param name="length">数组新的容量</param>
        private void Resize(int length)
        {
            E[] newData=new E[length];
            for (int i = 0; i < Size; i++)
            {
                newData[i] = _data[i];
            }

            _data = newData;
        }

        /// <summary>
        /// 交换两个位置的元素
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void Swap(int i,int j)
        {
            if(i<0 || i>=Size || j < 0 || j >= Size) throw new Exception("index is illegal");
            E e = _data[i];
            _data[i] = _data[j];
            _data[j] = e;
        }
    }
}
