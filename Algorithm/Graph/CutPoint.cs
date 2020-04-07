using System;
using System.Collections.Generic;

namespace Graph
{
    public class CutPoint
    {
        private IAdjacency _iAdjacency;

        private bool[] _visited;

        private int[] _order;

        private int[] _low;

        private int _cnt;

        public HashSet<int> CutPoints;

        public void FindCutPoint(IAdjacency iAdjacency)
        {
            _iAdjacency = iAdjacency;
            _visited=new bool[_iAdjacency.V];
            _order=new int[_iAdjacency.V];
            _low=new int[_iAdjacency.V];
            _cnt = 0;
            CutPoints=new HashSet<int>();

            for (int v = 0; v < iAdjacency.V; v++)
            {
                if(!_visited[v])
                    Dfs(v,v);
            }
        }

        private void Dfs(int v,int parent)
        {
            _visited[v] = true;
            _order[v] = _cnt;
            _low[v] = _order[v];
            _cnt++;
            int child = 0;
            foreach (var w in _iAdjacency.GetAllContiguousEdge(v))
            {
                if (!_visited[w])
                {
                    Dfs(w,v);
                    _low[v] = Math.Min(_low[v],_low[w]);
                    if (v!=parent && _low[w] >= _order[v])
                        CutPoints.Add(v);
                    child++;
                    if (v == parent && child > 1)
                        CutPoints.Add(v);
                }
                else if(w!=parent) 
                    _low[v]= Math.Min(_low[v], _low[w]);
            }
        }
    }
}