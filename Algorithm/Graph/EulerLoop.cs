using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    /// <summary>
    /// 欧拉回路
    /// </summary>
    public class EulerLoop
    {
        private IAdjacency _iAdjacency;

        public EulerLoop(IAdjacency iAdjacency)
        {
            _iAdjacency = iAdjacency;
        }

        /// <summary>
        /// 是否存在欧拉回路
        /// </summary>
        /// <returns></returns>
        public bool IsExistsEulerLoop()
        {
            DfsGraph dfs=new DfsGraph(_iAdjacency);
            if (dfs.ConnectedComponentCount > 1) return false;

            for (int i = 0; i < _iAdjacency.V; i++)
            {
                if (_iAdjacency.Dgree(i) % 2 == 1) return false;
            }

            return true;
        }

        /// <summary>
        /// 欧拉路径(Hierholzer算法)
        /// </summary>
        /// <returns></returns>
        public List<int> EulerLoopPath()
        {
            List<int> path=new List<int>();
            if (!IsExistsEulerLoop()) return path;

            Stack<int> stack=new Stack<int>();
            IAdjacency adjacency = _iAdjacency.Clone();
            int cur = 0;
            stack.Push(cur);
            while (stack.Count>0)
            {
                if (adjacency.Dgree(cur) != 0)
                {
                    stack.Push(cur);
                    int w = adjacency.GetAllContiguousEdge(cur).First();
                    adjacency.RemoveEdge(cur,w);
                    cur = w;
                }
                else
                {
                    path.Add(cur);
                    cur = stack.Pop();
                }
            }

            return path;
        }
    }
}