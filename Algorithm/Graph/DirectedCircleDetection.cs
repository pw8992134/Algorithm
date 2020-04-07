using System;

namespace Graph
{
    public class DirectedCircleDectection
    {
        private IAdjacency _iAdjacency;

        private bool[] _visited;

        private bool[] _onPath;

        public bool IsHaveCircle { get; private set; }

        public DirectedCircleDectection(IAdjacency iAdjacency)
        {
            if(!iAdjacency.Directed) throw new Exception("directed graph can be support");
            _iAdjacency = iAdjacency;
            _visited=new bool[iAdjacency.V];
            _onPath=new bool[iAdjacency.V];
            for (int i = 0; i < iAdjacency.V; i++)
            {
                if (!_visited[i])
                {
                    if (Dfs(i))
                    {
                        IsHaveCircle = true;
                        break;
                    }
                }
            }
        }

        private bool Dfs(int v)
        {
            _visited[v] = true;
            _onPath[v] = true;
            foreach (var w in _iAdjacency.GetAllContiguousEdge(v))
            {
                if(!_visited[w])
                    if (Dfs(w)) return true;
                else if (_onPath[w])
                        return true;
            }

            _onPath[v] = false;
            return false;
        }
    }
}