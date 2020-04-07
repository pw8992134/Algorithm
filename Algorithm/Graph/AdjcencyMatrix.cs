using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Graph
{
    /// <summary>
    /// 图的邻接矩阵的实现方式
    /// </summary>
    public class AdjacencyMatrix: IAdjacency
    {
        /// <summary>
        /// 顶点(这里以0~i的顶点做实例,即表示个数也表示索引值)
        /// </summary>
        public int V { get; }

        /// <summary>
        /// 边的个数
        /// </summary>
        public int E { get; private set; }

        public bool Directed { get; }

        private int[] _inDgree;

        private int[] _outDegree;

        public int InDegree(int v)
        {
            if (!Directed) throw new Exception("direcred graph can be support!");
            ValidateNumber(v);
            return _inDgree[v];
        }

        public int OutDegree(int v)
        {
            if (!Directed) throw new Exception("direcred graph can be support!");
            ValidateNumber(v);
            return _outDegree[v];
        }

        /// <summary>
        /// 矩阵存储(二维数组)
        /// </summary>
        private int[,] Adj { get; set; }

        /// <summary>
        /// 构造方法(传入文件名解析图)
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="isDirected"></param>
        public AdjacencyMatrix(string fileName,bool isDirected)
        {
            try
            {
                using FileStream fs=File.OpenRead(fileName);
                using StreamReader reader=new StreamReader(fs);
                string line = reader.ReadLine();
                SplitTwoNumber(line,out var v,out var e);
                if(v<0) throw new ArgumentOutOfRangeException("V must be non-negative");
                V = v;
                if (e < 0) throw new ArgumentOutOfRangeException("E must be non-negative");
                E = e;
                Adj=new int[v,v];
                Directed = isDirected;
                _inDgree=new int[V];
                _outDegree=new int[V];
                for (int i = 0; i <= e; i++)
                {
                    if(reader.EndOfStream) break;
                    string numbeReadLine = reader.ReadLine();
                    SplitTwoNumber(numbeReadLine, out int v1, out var v2);
                    ValidateNumber(v1);
                    ValidateNumber(v2);
                    if (v1 == v2) throw new Exception("Self loop is exists");
                    if (Adj[v1, v2] == 1) throw new Exception("Parallel edge is exists");
                    Adj[v1, v2] = 1;
                    if (!Directed)
                        Adj[v2, v1] = 1;
                    if (Directed)
                    {
                        _inDgree[v2]++;
                        _outDegree[v1]++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public AdjacencyMatrix(string fileName):this(fileName,false)
        {
            
        }

        /// <summary>
        /// 两个顶点是否有边
        /// </summary>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public bool HasEdge(int v,int w)
        {
            ValidateNumber(v);
            ValidateNumber(w);
            return Adj[v, w] == 1;
        }

        /// <summary>
        /// 返回一个顶点的相邻边的顶点
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public ICollection<int> GetAllContiguousEdge(int v)
        {
            ValidateNumber(v);
            List<int> list=new List<int>();
            for (int i = 0; i < V; i++)
            {
                if(Adj[v,i]==1) list.Add(i);
            }

            return list;
        }

        /// <summary>
        /// 顶点的度(这里只讨论了无向图,故度等于顶点相邻边数)
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int Dgree(int v)
        {
            if(Directed) throw new Exception("directed graph can be support!");
            return GetAllContiguousEdge(v).Count;
        }

        /// <summary>
        /// 将从文件中读取的一行拆分成两个数字
        /// </summary>
        /// <param name="line"></param>
        /// <param name="v"></param>
        /// <param name="e"></param>
        private void SplitTwoNumber(string line,out int v,out int e)
        {
            if (string.IsNullOrEmpty(line)) throw new NullReferenceException("Line is null");
            try
            {
                if(!line.Contains(" ")) throw new VerificationException("Don't contains space char");
                string[] numbers = line.Split(' ');
                if(numbers.Length!=2) throw new ArgumentOutOfRangeException("Number less than two");
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
            if(v<0|| v>=V) throw new ArgumentOutOfRangeException("Vertex is out of range");
        }

        public void RemoveEdge(int v, int w)
        {
            ValidateNumber(v);
            ValidateNumber(w);
            if (Adj[v, w] == 1)
            {
                E--;
                Adj[v, w] = 0;
                if(!Directed) Adj[w, v] = 0;
                if (Directed)
                {
                    _outDegree[v]--;
                    _inDgree[w]--;
                }
            }
        }

        public void AddEdge(int v, int w)
        {
            ValidateNumber(v);
            ValidateNumber(w);
            Adj[v, w] = 1;
            if(!Directed) Adj[w, v] = 1;
            if (Directed)
            {
                _outDegree[v]++;
                _inDgree[w]++;
            }
        }

        public IAdjacency Clone()
        {
            AdjacencyMatrix adjacencyMatrix = (AdjacencyMatrix) this.MemberwiseClone();
            adjacencyMatrix.Adj=new int[V,V];
            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                {
                    adjacencyMatrix.Adj[i,j] = this.Adj[i, j];
                }
            }

            return adjacencyMatrix;
        }

        /// <summary>
        /// 求一个图的反图
        /// </summary>
        /// <returns></returns>
        public IAdjacency Reverse()
        {
            AdjacencyMatrix reverse = (AdjacencyMatrix) this.Clone();
            reverse._inDgree=new int[V];
            reverse._outDegree=new int[V];
            reverse.Adj=new int[V,V];

            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                {
                    if (this.Adj[i, j] == 1)
                    {
                        reverse.Adj[j, i] = 1;
                        reverse._inDgree[i]++;
                        reverse._outDegree[j]++;
                    }
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
            StringBuilder s=new StringBuilder("V:"+V+" ; E:"+E);
            for (int i = 0; i < V; i++)
            {
                s.Append("\r\n");
                for (int j = 0; j < V; j++)
                {
                    s.Append(Adj[i, j] + " ");
                }
            }

            return s.ToString();
        }
    }
}