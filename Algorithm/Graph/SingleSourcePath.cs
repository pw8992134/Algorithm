using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// 单源路径(找从源节点s到t的路径)
    /// </summary>
    public class SingleSourcePath
    {
        /// <summary>
        /// 图的表示
        /// </summary>
        private readonly IAdjacency _adjacency;

        /// <summary>
        /// 顶点s
        /// </summary>
        private int S { get;  }

        /// <summary>
        /// 顶点t
        /// </summary>
        private int T { get; }

        /// <summary>
        /// 记录节点是否被访问过
        /// </summary>
        private readonly bool[] _visited;//这个可以不要,PreNodes[i]的值若不是-1则已经被遍历过

        /// <summary>
        /// 记录遍历的节点的前一个节点
        /// </summary>
        private int[] PreNodes { get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="adjacency"></param>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public SingleSourcePath(IAdjacency adjacency,int s,int t)
        {
            _adjacency = adjacency;
            _adjacency.ValidateNumber(s);
            _adjacency.ValidateNumber(t);
            S = s;
            T = t;
            PreNodes=new int[_adjacency.V];
            _visited=new bool[_adjacency.V];
            for (int i = 0; i < PreNodes.Length; i++)
            {
                PreNodes[i] = -1;
            }

            Dfs(s, s);
        }

        /// <summary>
        /// 从s开始遍历记录父子关系到t为止
        /// </summary>
        /// <param name="s"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private bool Dfs(int s,int parent)
        {
            _visited[s] = true;
            PreNodes[s] = parent;
            if (s == T) return true;
            foreach (var v in _adjacency.GetAllContiguousEdge(s))
            {
                if(!_visited[v])
                    if (Dfs(v, s))
                        return true;
            }

            return false;
        }

        /// <summary>
        /// s到t是否有路径
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return _visited[T];
        }

        /// <summary>
        /// 找从s到t的路径
        /// </summary>
        /// <returns></returns>
        public ICollection<int> Path()
        {
            List<int> list=new List<int>();
            if (!IsConnected()) return list;
            int cur = T;
            while (cur!=S)
            {
                list.Add(cur);
                cur = PreNodes[cur];
            }
            list.Add(S); 
            list.Reverse();
            return list;
        }
    }
}