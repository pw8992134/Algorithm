using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// 图的深度优先搜索遍历(前后序)
    /// </summary>
    public class DfsGraph
    {
        /// <summary>
        /// 输入的图的数据结构
        /// </summary>
        private readonly IAdjacency _adjacency;

        /// <summary>
        /// 记录节点是否被遍历过的数组(初始全部为-1,相同连通分量的值一样)
        /// </summary>
        private readonly int[] _visited;

        /// <summary>
        /// 联通分量个数
        /// </summary>
        public int ConnectedComponentCount { get; }

        /// <summary>
        /// 前序遍历结果数组
        /// </summary>
        public List<int> PreOrder { get; }=new List<int>();

        /// <summary>
        /// 后序遍历结果数组
        /// </summary>
        public List<int> PostOrder { get; }=new List<int>();

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="iAdjacency">输入的图的数据结构</param>
        public DfsGraph(IAdjacency iAdjacency)
        {
            _adjacency = iAdjacency;
            _visited=new int[iAdjacency.V];
            for (int i = 0; i < _visited.Length; i++)
            {
                _visited[i] = -1;
            }
            for (int i = 0; i < iAdjacency.V; i++)
            {
                if (_visited[i]==-1)
                {
                    Dfs(i, ConnectedComponentCount++);
                }
            }
        }

        /// <summary>
        /// 深度优先遍历
        /// </summary>
        /// <param name="v"></param>
        /// <param name="connectedComponentIndex">第几个联通分量的索引</param>
        private void Dfs(int v,int connectedComponentIndex)
        {
            _visited[v] = connectedComponentIndex;
            PreOrder.Add(v);
            foreach (var w in _adjacency.GetAllContiguousEdge(v))
            {
                if(_visited[w]==-1) Dfs(w,connectedComponentIndex);
            }
            PostOrder.Add(v);
        }

        /// <summary>
        /// 两个顶点是否属于同一个连通分量
        /// </summary>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public bool IsConnected(int v,int w)
        {
            _adjacency.ValidateNumber(v);
            _adjacency.ValidateNumber(w);
            return _visited[v] == _visited[w];
        }

        /// <summary>
        /// 各个联通分量数组
        /// </summary>
        /// <returns></returns>
        public List<int>[] ConnectedComponents()
        {
            List<int>[] connectedComponents=new List<int>[ConnectedComponentCount];
            for (int i = 0; i < connectedComponents.Length; i++)
            {
                connectedComponents[i]=new List<int>();
            }
            for (int i = 0; i < _adjacency.V; i++)
            {
                connectedComponents[_visited[i]].Add(i);
            }

            return connectedComponents;
        }
    }
}