namespace Graph
{
    /// <summary>
    /// 二分图检测(顶点分为两个集合,一个红色,一个绿色,每条边的两个顶点分别属于这两个集合)
    /// </summary>
    public class BinaryPartitionDetection
    {
        /// <summary>
        /// 图的表示
        /// </summary>
        private readonly IAdjacency _adjacency;

        /// <summary>
        /// 记录节点是否被访问过
        /// </summary>
        private readonly bool[] _visited;

        private readonly int[] _colors;

        /// <summary>
        /// 返回二分的数组
        /// </summary>
        public int[] Colors => _colors;

        /// <summary>
        /// 是否是二分图
        /// </summary>
        public bool IsBinaryPartition { get; } = true;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="iAdjacency"></param>
        public BinaryPartitionDetection(IAdjacency iAdjacency)
        {
            _adjacency = iAdjacency;
            _visited = new bool[_adjacency.V];
            _colors=new int[_adjacency.V];
            for (int i = 0; i < _colors.Length; i++)
            {
                _colors[i] = -1;
            }
            for (int i = 0; i < _adjacency.V; i++)
            {
                if (!_visited[i])
                    if (!Dfs(i, 0))
                    {
                        IsBinaryPartition = false;
                        break;
                    }
            }
        }

        /// <summary>
        /// 深度优先遍历并且检测二分图冲突
        /// </summary>
        /// <param name="v"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool Dfs(int v, int color)
        {
            _visited[v] = true;
            _colors[v] = color;
            foreach (var w in _adjacency.GetAllContiguousEdge(v))
            {
                if (!_visited[w])
                {
                    if (!Dfs(w, 1 - color))
                        return false;
                }
                else if (_colors[v] == _colors[w]) 
                        return false;
            }

            return true;
        }
    }
}