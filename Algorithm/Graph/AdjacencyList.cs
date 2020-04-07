using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;

namespace Graph
{
    /// <summary>
    /// 图的数据结构的邻接表方式
    /// </summary>
    public class AdjacencyList: IAdjacency
    {
        /// <summary>
        /// 顶点(这里以0~i的顶点做实例,即表示个数也表示索引值)
        /// </summary>
        public int V { get; }

        /// <summary>
        /// 边的个数
        /// </summary>
        public int E { get; private set; }

        /// <summary>
        /// 是否有向图
        /// </summary>
        public bool Directed { get; }

        /// <summary>
        /// 顶点的入度
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int InDegree(int v)
        {
            if(!Directed) throw new Exception("Directed Graph Can Be Support!");
            ValidateNumber(v);
            return _indgree[v];
        }

        private int[] _indgree;

        private int[] _outDgree;

        /// <summary>
        /// 顶点的出度
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int OutDegree(int v)
        {
            if (!Directed) throw new Exception("Directed Graph Can Be Support!");
            ValidateNumber(v);
            return _outDgree[v];
        }

        /// <summary>
        /// 链表存储方式
        /// </summary>
        private LinkedList<int>[] Adj { get; set; }

        /// <summary>
        /// 构造方法(传入文件名解析图)
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="isDirected"></param>
        public AdjacencyList(string fileName,bool isDirected)
        {
            try
            {
                using FileStream fs = File.OpenRead(fileName);
                using StreamReader reader = new StreamReader(fs);
                string line = reader.ReadLine();
                SplitTwoNumber(line, out var v, out var e);
                if (v < 0) throw new ArgumentOutOfRangeException("V must be non-negative");
                V = v;
                if (e < 0) throw new ArgumentOutOfRangeException("E must be non-negative");
                E = e;
                Adj = new LinkedList<int>[V];
                for (int i = 0; i < V; i++)
                {
                    Adj[i]=new LinkedList<int>();
                }
                _indgree=new int[V];
                _outDgree=new int[V];
                Directed = isDirected;
                for (int i = 0; i <= e; i++)
                {
                    if (reader.EndOfStream) break;
                    string numbeReadLine = reader.ReadLine();
                    SplitTwoNumber(numbeReadLine, out int v1, out var v2);
                    ValidateNumber(v1);
                    ValidateNumber(v2);
                    if (v1 == v2) throw new Exception("Self loop is exists");
                    if (Adj[v1].Contains(v2)) throw new Exception("Parallel edge is exists");
                    Adj[v1].AddLast(v2);
                    if(!isDirected)
                        Adj[v2].AddLast(v1);
                    if (Directed)
                    {
                        _outDgree[v1]++;
                        _indgree[v2]++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public AdjacencyList(int v,bool directed)
        {
            V = v;
            E = 0;
            Directed = directed;
            Adj=new LinkedList<int>[v];
            for (int i = 0; i < v; i++)
            {
                Adj[i]=new LinkedList<int>();
            }
            _indgree=new int[v];
            _outDgree=new int[v];
        }

        public AdjacencyList(string fileName):this(fileName,false)
        {
            
        }

        /// <summary>
        /// 两个顶点是否有边
        /// </summary>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public bool HasEdge(int v, int w)
        {
            ValidateNumber(v);
            ValidateNumber(w);
            return Adj[v].Contains(w);
        }

        /// <summary>
        /// 返回一个顶点的相邻边的顶点
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public ICollection<int> GetAllContiguousEdge(int v)
        {
            ValidateNumber(v);
            return Adj[v];
        }

        /// <summary>
        /// 顶点的度(这里只讨论了无向图,故度等于顶点相邻边数)
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int Dgree(int v)
        {
            if(Directed) throw new Exception("this is a not directed graph!");
            return GetAllContiguousEdge(v).Count;
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

        /// <summary>
        /// 验证顶点索引是否合法
        /// </summary>
        /// <param name="v"></param>
        public void ValidateNumber(int v)
        {
            if (v < 0 || v >= V) throw new ArgumentOutOfRangeException("Vertex is out of range");
        }

        public void RemoveEdge(int v, int w)
        {
            ValidateNumber(v);
            ValidateNumber(w);
            if (Adj[v].Contains(w))
            {
                E--;
                Adj[v].Remove(w);
                if(!Directed) 
                  Adj[w].Remove(v);
                if (Directed)
                {
                    _outDgree[v]--;
                    _indgree[v]--;
                }
            }
        }

        public void AddEdge(int v, int w)
        {
            ValidateNumber(v);
            ValidateNumber(w);
            if(!Adj[v].Contains(w)) Adj[v].AddLast(w);
            if (!Adj[w].Contains(v)&& !Directed) Adj[w].AddLast(v);
            if (Directed)
            {
                _outDgree[v]++;
                _indgree[w]++;
            }
        }

        public IAdjacency Clone()
        {
            AdjacencyList clone = (AdjacencyList)this.MemberwiseClone();
            clone.Adj=new LinkedList<int>[V];
            for (int i = 0; i < this.Adj.Length; i++)
            {
                clone.Adj[i]=new LinkedList<int>();
                foreach (var v in this.Adj[i])
                {
                    clone.Adj[i].AddLast(v);
                }
            }

            return clone;
        }

        /// <summary>
        /// 求一个图的反图
        /// </summary>
        /// <returns></returns>
        public IAdjacency Reverse()
        {
            if(!Directed) throw new Exception("directed graph can be support!");
            AdjacencyList reverse = (AdjacencyList)this.Clone();
            reverse.Adj=new LinkedList<int>[V];
            for (int i = 0; i < V; i++)
            {
                reverse.Adj[i]=new LinkedList<int>();
            }
            reverse._indgree=new int[V];
            reverse._outDgree=new int[V];
            for (int i = 0; i < V; i++)
            {
                foreach (var w in this.Adj[i])
                {
                    reverse.Adj[w].AddLast(i);
                    reverse._indgree[i]++;
                    reverse._outDgree[w]++;
                }
            }

            return reverse;
        }


        /// <summary>
        /// 复写ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder s = new StringBuilder("V:" + V + " ; E:" + E);
            for (int i = 0; i < V; i++)
            {
                s.Append("\r\n"+i+": ");
                foreach (var x in Adj[i])
                {
                    s.Append(x + " ");
                }
            }

            return s.ToString();
        }
    }
}