using System.Collections.Generic;

namespace Graph
{
    public class HamiltonPath
    {
        private IAdjacency _iAdjacency;

        private int _s;

        private bool[] _visited;

        private int _end;

        private int[] _pre;

        public HamiltonPath(IAdjacency iAdjacency,int s)
        {
            _iAdjacency = iAdjacency;
            _s = s;
            _visited=new bool[_iAdjacency.V];
            _end = -1;
            _pre=new int[_iAdjacency.V];

            Dfs(s, s, _iAdjacency.V);
        }

        private bool Dfs(int v,int parent,int left)
        {
            _visited[v] = true;
            _pre[v] = parent;
            left--;
            foreach (var w in _iAdjacency.GetAllContiguousEdge(v))
            {
                if (!_visited[w])
                {
                    if (Dfs(w, v, left)) return true;
                }
                else if (left == 0)
                {
                    _end = v;
                    return true;
                }
            }

            _visited[v] = false;
            left++;
            return false;
        }

        public List<int> Path()
        {
            List<int> path = new List<int>();
            if (_end == -1) return path;
            int cur = _end;
            while (cur != 0)
            {
                path.Add(cur);
                cur = _pre[cur];
            }
            path.Add(_s);
            path.Reverse();
            return path;
        }
    }
}