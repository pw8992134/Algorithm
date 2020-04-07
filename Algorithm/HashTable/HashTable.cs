using System;
using System.Collections.Generic;

namespace Algorithm.HashTable
{
    /// <summary>
    /// 哈希表
    /// </summary>
    public class HashTable<K,V>
    {
        #region 常见映射方法

        //1.小范围正整数用它本身做hash值.
        //2.小范围的负整数向正整数做偏移
        //3.大范围整数取模,最好摸一个素数以减少hash冲突.
        //4.float,double在计算机的存储一般是32位或64位,使用表示其空间的整数32位或64位来表示其hash值
        //5.string也使用整数表示, c*26^x%m  ((c*26)%m)*26%m
        //6.自己创建的类,默认会使用类对象引用的地址来做hash值.可以复写GetHashCode和equals方法.当调用返回的hash值一样时,就会调用equals做真正的比较
        //7.解决hash冲突的方法链地址法.

        #endregion

        private Dictionary<K, V>[] _hashTable;

        private int _m;

        private int _size;

        private const int upper = 10;

        private const int lower = 2;

        private const int init = 7;

        public HashTable(int m)
        {
            _hashTable = new Dictionary<K, V>[m];
            Array.Fill(_hashTable,new Dictionary<K, V>());
            _m = m;
            _size = 0;
        }

        public HashTable():this(init)
        {
            
        }

        private int Hash(K key)
        {
            return (key.GetHashCode() & 0x7fffffff) % _m;
        }

        public int GetSize()
        {
            return _size;
        }

        public void Add(K k,V v)
        {
            int hash = Hash(k);
            if (_hashTable[hash].ContainsKey(k))
                _hashTable[hash][k] = v;
            else
            {
                _hashTable[hash].Add(k,v);
                _size++;
                if(_size>upper*_m) Resize(2*_m);
            }
        }

        public V Remove(K key)
        {
            Dictionary<K, V> dictionary = _hashTable[Hash(key)];
            V v = default;
            if (dictionary.ContainsKey(key))
            {
                v = dictionary[key];
                dictionary.Remove(key);
                _size--;
                if(_size<lower*_m && _m/2>init) Resize(_m/2);
            }

            return v;
        }

        public void Set(K k,V v)
        {
            Dictionary<K, V> dictionary = _hashTable[Hash(k)];
            if(!dictionary.ContainsKey(k)) throw new Exception("this key is not exists");
            dictionary[k] = v;
        }

        public bool Contains(K k)
        {
            return _hashTable[Hash(k)].ContainsKey(k);
        }

        public V Get(K k)
        {
            return _hashTable[Hash(k)][k];
        }

        private void Resize(int m)
        {
            Dictionary<K,V>[] dictionary=new Dictionary<K, V>[m];
            Array.Fill(dictionary,new Dictionary<K,V>());
            int oldM = _m;
            _m = m;
            for (int i = 0; i < _hashTable.Length; i++)
            {
                foreach (var kv in _hashTable[i])
                {
                    dictionary[Hash(kv.Key)].Add(kv.Key,kv.Value);
                }
            }

            _hashTable = dictionary;
        }
    }

    public class Student
    {
        public int Age { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public Student()
        {

        }

        public Student(int age, string firstName, string lastName)
        {
            Age = age;
            FirstName = firstName;
            LastName = lastName;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            hash = hash * 26 + Age;
            hash = hash * 26 + FirstName.ToLower().GetHashCode();
            hash = hash * 26 + LastName.ToLower().GetHashCode();
            return hash&0x7fffffff;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            if (this.GetType() != obj.GetType()) return false;
            return obj is Student student &&
                   (this.Age == student.Age && this.FirstName.ToLower().Equals(student.FirstName.ToLower())) &&
                   this.LastName.ToLower().Equals(student.LastName.ToLower());
        }
    }
}