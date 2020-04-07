using System;

namespace Graph
{
    /// <summary>
    /// 环检测(访问一个顶点v的连接顶点w,若此节点已经被访问且v的上个顶点不是w,则有环)
    /// </summary>
    public class CircleDetection
    {
        /// <summary>
        /// 图的表示
        /// </summary>
        private readonly IAdjacency _adjacency;

        /// <summary>
        /// 记录节点是否被访问过
        /// </summary>
        private readonly bool[] _visited;

        /// <summary>
        /// 是否有环
        /// </summary>
        public bool IsHaveCircle { get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="iAdjacency"></param>
        public CircleDetection(IAdjacency iAdjacency)
        {
            if(iAdjacency.Directed) throw new Exception("direced graph can not be support!");
            _adjacency = iAdjacency;
            _visited=new bool[_adjacency.V];

            for (int i = 0; i < _adjacency.V; i++)
            {
                if (!_visited[i])
                    if (Dfs(i, i))
                    {
                        IsHaveCircle = true;
                        break;
                    }
            }
        }

        /// <summary>
        /// 深度优先遍历并且记录是否有环
        /// </summary>
        /// <param name="v"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private bool Dfs(int v,int parent)
        {
            _visited[v] = true;
            foreach (var w in _adjacency.GetAllContiguousEdge(v))
            {
                if(!_visited[w])
                    if (Dfs(w, v)) return true;
                else if (w != parent) return true;
            }

            return false;
        }
    }
}