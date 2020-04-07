using System;
using System.Collections.Generic;

namespace Graph
{
    public class Bridge
    {
        private IAdjacency _iaAdjacency;

        private bool[] _visited;

        private int[] _order;

        private int[] _low;

        private int _cnt;

        public List<Edge> BridgeEdge { get;private set; }

        public void FindBridge(IAdjacency iAdjacency)
        {
            _iaAdjacency = iAdjacency;
            _visited=new bool[_iaAdjacency.V];
            _order = new int[_iaAdjacency.V];
            _low=new int[_iaAdjacency.V];
            _cnt = 0;
            BridgeEdge=new List<Edge>();
            for (int i = 0; i < _iaAdjacency.V; i++)
            {
                if(!_visited[i])
                    Dfs(i,i);
            }
        }

        private void Dfs(int v,int parent)
        {
            _visited[v] = true;
            _order[v] = _cnt;
            _low[v] = _order[v];
            _cnt++;
            foreach (var w in _iaAdjacency.GetAllContiguousEdge(v))
            {
                if (!_visited[w])
                {
                    Dfs(w, v);
                    _low[v] = Math.Min(_low[v], _low[w]);
                    if(_low[w]>_order[v])
                        BridgeEdge.Add(new Edge(w,v));
                }
                else if (w != parent)
                    _low[v] = Math.Min(_low[v], _low[w]);
            }
        }
    }
}