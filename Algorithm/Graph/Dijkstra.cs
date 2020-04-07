using System;
using System.Collections.Generic;

namespace Graph
{
    public class Dijkstra
    {
        private WeightGraph _weightGraph;

        private int[] _pre;

        private int _s;

        private int[] _dirs;

        private bool[] _visited;

        public Dijkstra(WeightGraph weightGraph,int s)
        {
            this._weightGraph = weightGraph;
            this._s = s;
            _pre=new int[_weightGraph.V];
            _visited=new bool[_weightGraph.V];
            _dirs=new int[_weightGraph.V];

            for (int i = 0; i < _weightGraph.V; i++)
            {
                _dirs[i] = int.MaxValue;
            }

            _dirs[_s] = 0;

            while (true)
            {
                int curDis=Int32.MaxValue;
                int cur = -1;
                for (int i = 0; i < _weightGraph.V; i++)
                {
                    if (!_visited[i] && _dirs[i] < curDis)
                    {
                        curDis = _dirs[i];
                        cur = i;
                    }
                }

                if(cur==-1) break;

                _visited[cur] = true;

                foreach (var w in _weightGraph.GetAllContiguousEdge(cur))
                {
                    if (!_visited[w] && _dirs[w] > _dirs[cur] + _weightGraph.GetWeight(cur, w))
                    {
                        _dirs[w] = _dirs[cur] + _weightGraph.GetWeight(cur, w);
                        _pre[w] = cur;
                    }
                }
            }
        }

        public int Distance(int v)
        {
            _weightGraph.ValidateNumber(v);
            if(!IsConnected(v)) throw new Exception(" v not be visited");
            return _dirs[v];
        }

        public bool IsConnected(int v)
        {
            return _visited[v];
        }

        public List<int> Path(int v)
        {
            List<int> list=new List<int>();
            if (!IsConnected(v)) return list;

            int cur = v;
            while (cur!=_s)
            {
                list.Add(cur);
                cur = _pre[cur];
            }
            list.Add(_s);
            list.Reverse();
            return list;
        }
    }
}