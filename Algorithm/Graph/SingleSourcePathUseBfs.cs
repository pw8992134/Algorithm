using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    /// <summary>
    /// 使用广度优先遍历求解单源最短路径问题
    /// 其实深度优先能解决的问题大多数使用广度优先也能解决
    /// </summary>
    public class SingleSourcePathUseBfs
    {
        /// <summary>
        /// 图的表示
        /// </summary>
        private readonly IAdjacency _iaAdjacency;

        /// <summary>
        /// 记录访问过的节点
        /// </summary>
        private readonly bool[] _visited;

        /// <summary>
        /// 记录一个节点的前一个节点
        /// </summary>
        private readonly int[] _pre;

        /// <summary>
        /// 记录每个节点到源节点的距离,因为广度优先是层序遍历,所以路径必然最短
        /// </summary>
        private readonly int[] _distance;

        /// <summary>
        /// 源节点s
        /// </summary>
        private readonly int _s;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="iaAdjacency"></param>
        /// <param name="s"></param>
        public SingleSourcePathUseBfs(IAdjacency iaAdjacency,int s)
        {
            _iaAdjacency = iaAdjacency;
            _visited=new bool[_iaAdjacency.V];
            _pre=new int[_iaAdjacency.V];
            _distance=new int[_iaAdjacency.V];
            _s = s;
            Bfs(s);
        }

        /// <summary>
        /// 广度优先遍历求路径与距离
        /// </summary>
        /// <param name="v"></param>
        private void Bfs(int v)
        {
            Queue<int> queue=new Queue<int>();
            _visited[v] = true;
            queue.Enqueue(v);
            _pre[v] = v;
            _distance[v] = 0;
            while (queue.Count>0)
            {
                int s = queue.Dequeue();
                foreach (var w in _iaAdjacency.GetAllContiguousEdge(s))
                {
                    if (!_visited[w])
                    {
                        _visited[w] = true;
                        queue.Enqueue(w);
                        _pre[w] = s;
                        _distance[w] = _distance[s] + 1;
                    }
                }
            }
        }

        /// <summary>
        /// 两个顶点是否连通
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool IsConnected(int t)
        {
            _iaAdjacency.ValidateNumber(t);
            return _visited[t];
        }

        /// <summary>
        /// 求s到t的路径
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public ICollection<int> Path(int t)
        {
            List<int> list=new List<int>();
            if (!IsConnected(t)) return list;
            int current = t;
            while (current!=_s)
            {
                list.Add(current);
                current = _pre[current];
            }
            list.Add(_s);
            list.Reverse();
            return list;
        }

        /// <summary>
        /// 最短距离
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int MinDistance(int t)
        {
            _iaAdjacency.ValidateNumber(t);
            return _distance[t];
        }
    }
}