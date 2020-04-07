using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;

namespace Graph
{
    /// <summary>
    /// 带权图
    /// </summary>
    public class WeightGraph
    {
        public int V { get; }
        public int E { get; private set; }

        public bool Directed { get; private set; }

        private readonly int[] _inDgree;

        private readonly int[] _outDgree;

        private Dictionary<int, int>[] Adj;

        public WeightGraph(string fileName,bool isDirected)
        {
            try
            {
                using FileStream fs=File.OpenRead(fileName);
                using StreamReader reader=new StreamReader(fs);
                string text = reader.ReadLine();
                SplitTwoNumber(text,out int v,out int e);
                if(v<0 || e<0) throw new Exception("v or e must be non-negative ");
                V=v;
                E = e;
                Adj=new Dictionary<int, int>[v];
                for (int i = 0; i < V; i++)
                {
                    Adj[i]=new Dictionary<int, int>();
                }

                Directed = isDirected;
                _inDgree=new int[V];
                _outDgree=new int[V];
                for (int i = 0; i < e; i++)
                {
                    if(reader.EndOfStream) break;
                    string line = reader.ReadLine();
                    SplitTwoNumber1(line,out int number1,out int number2,out int number3);
                    ValidateNumber(number1);
                    ValidateNumber(number2);
                    if(number1==number2) throw new Exception("self loop edge is detected");
                    if(Adj[number1].ContainsKey(number2)) throw new Exception("parallel edge is detected");
                    Adj[number1].Add(number2,number3);
                    if(!Directed) Adj[number2].Add(number1, number3);
                    if (Directed)
                    {
                        _inDgree[number2]++;
                        _outDgree[number1]++;
                    }
                }
            }
            catch (Exception va)
            {
                Console.WriteLine(va);
                throw;
            }
        }

        public WeightGraph(string fileName):this(fileName,false)
        {
            
        }

        public WeightGraph(int v,bool directed)
        {
            V = v;
            Directed = directed;
            E = 0;
            _inDgree=new int[v];
            _outDgree=new int[v];
            Adj=new Dictionary<int, int>[v];
            for (int i = 0; i < v; i++)
            {
                Adj[i]=new Dictionary<int, int>();
            }
        }

        public int InDgree(int v)
        {
            if(!Directed) throw new Exception("direcred graph can be support!");
            ValidateNumber(v);
            return _inDgree[v];
        }

        public int OutDgree(int v)
        {
            if (!Directed) throw new Exception("direcred graph can be support!");
            ValidateNumber(v);
            return _outDgree[v];
        }

        /// <summary>
        /// 将从文件中读取的一行拆分成两个数字
        /// </summary>
        /// <param name="line"></param>
        /// <param name="v"></param>
        /// <param name="e"></param>
        /// <param name="weight"></param>
        private void SplitTwoNumber1(string line, out int v, out int e,out int weight)
        {
            if (string.IsNullOrEmpty(line)) throw new NullReferenceException("Line is null");
            try
            {
                if (!line.Contains(" ")) throw new VerificationException("Don't contains space char");
                string[] numbers = line.Split(' ');
                if (numbers.Length != 3) throw new ArgumentOutOfRangeException("Number less than two");
                v = Int32.Parse(numbers[0]);
                e = Int32.Parse(numbers[1]);
                weight= Int32.Parse(numbers[2]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// 将从文件中读取的一行拆分成两个数字
        /// </summary>
        /// <param name="line"></param>
        /// <param name="v"></param>
        /// <param name="e"></param>
        private void SplitTwoNumber(string line, out int v, out int e)
        {
            if (string.IsNullOrEmpty(line)) throw new NullReferenceException("Line is null");
            try
            {
                if (!line.Contains(" ")) throw new VerificationException("Don't contains space char");
                string[] numbers = line.Split(' ');
                if (numbers.Length != 2) throw new ArgumentOutOfRangeException("Number less than two");
                v = Int32.Parse(numbers[0]);
                e = Int32.Parse(numbers[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public bool HasEdge(int v, int w)
        {
            ValidateNumber(v);
            ValidateNumber(w);
            return Adj[v].ContainsKey(w);
        }

        public ICollection<int> GetAllContiguousEdge(int v)
        {
            ValidateNumber(v);
            return Adj[v].Keys;
        }

        public int Dgree(int v)
        {
            if(Directed) throw new Exception("directed graph is not be support!");
            return GetAllContiguousEdge(v).Count;
        }

        public void ValidateNumber(int v)
        {
            if(v < 0 || v >= V) throw new Exception("v is not correct!");
        }

        public void RemoveEdge(int v, int w)
        {
            ValidateNumber(v);
            ValidateNumber(w);
            if (Adj[v].ContainsKey(w)) Adj[v].Remove(w);
            if (Adj[w].ContainsKey(v)&& !Directed) Adj[w].Remove(v);
            if (Directed)
            {
                _inDgree[w]--;
                _outDgree[v]--;
            }

            E--;
        }

        public void AddEdge(int v, int w,int weight)
        {
            ValidateNumber(v);
            ValidateNumber(w);
            if (!Adj[v].ContainsKey(w)) Adj[v].Add(w,weight);
            if (!Adj[w].ContainsKey(v)&&!Directed) Adj[w].Add(v, weight);
            if (Directed)
            {
                _inDgree[w]++;
                _outDgree[v]++;
            }

            E++;
        }

        public WeightGraph Clone()
        {
            WeightGraph g = (WeightGraph) this.MemberwiseClone();
            g.Adj=new Dictionary<int, int>[V];
            for (int i = 0; i < V; i++)
            {
                g.Adj[i]=new Dictionary<int, int>();
                foreach (var kv in this.Adj[i])
                {
                    g.Adj[i].Add(kv.Key,kv.Value);
                }
            }

            return g;
        }

        public int GetWeight(int v,int w)
        {
            ValidateNumber(v);
            ValidateNumber(w);
            if(!Adj[v].ContainsKey(w)) throw new Exception("not contains v-w edge");
            return Adj[v][w];
        }

        public void SetWeight(int v, int w,int weight)
        {
            ValidateNumber(v);
            ValidateNumber(w);
            if (!Adj[v].ContainsKey(w)) throw new Exception("not contains v-w edge");
            Adj[v][w] = weight;
        }
    }
}