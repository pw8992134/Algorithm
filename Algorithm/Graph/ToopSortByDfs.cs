using System.Collections.Generic;

namespace Graph
{
    public class ToopSortByDfs
    {
        private readonly IAdjacency _iAdjacency;

        private readonly bool[] _visited;

        public List<int> Result;

        public ToopSortByDfs(IAdjacency iAdjacency)
        {
            _iAdjacency = iAdjacency;
            _visited=new bool[iAdjacency.V];
            Result = new List<int>();

            for (int v = 0; v < iAdjacency.V; v++)
            {
                if(!_visited[v]) 
                    Dfs(v);
            }
            Result.Reverse();
        }

        public void Dfs(int v)
        {
            _visited[v] = true;
            foreach (var w in _iAdjacency.GetAllContiguousEdge(v))
            {
                if(!_visited[w]) Dfs(w);
            }
            Result.Add(v);
        }
    }
}