using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class HamiltonLoop
    {
        private IAdjacency _iAdjacency;

        private bool[] _visited;

        private int _end;

        private int[] _pre;

        public void FindHamiltonPath(IAdjacency iAdjacency)
        {
            _iAdjacency = iAdjacency;
            _visited=new bool[_iAdjacency.V];
            _pre=new int[_iAdjacency.V];
            _end = -1;

            Dfs(0,0);
        }

        public bool Dfs(int v,int parent)
        {
            _visited[v] = true;
            _pre[v] = parent;
            foreach (var w in _iAdjacency.GetAllContiguousEdge(v))
            {
                if (!_visited[w])
                {
                    if (Dfs(w,v)) return true;
                }
                else if (w == 0 && AllVisited())
                {
                    _end = v;
                    return true;
                }
            }

            _visited[v] = false;
            return false;
        }

        private bool AllVisited()
        {
            return !_visited.Contains(false);
        }

        public List<int> Path()
        {
            List<int> path=new List<int>();
            if (_end == -1) return path;
            int cur = _end;
            while (cur!=0)
            {
                path.Add(cur);
                cur = _pre[cur];
            }
            path.Add(0);
            path.Reverse();
            return path;
        }
    }
}