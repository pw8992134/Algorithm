using System;
using System.Collections.Generic;

namespace Graph
{
    public class Bellman_Ford
    {
        private WeightGraph _weightGraph;

        private int _s;

        private int[] _dirs;

        private bool IsHaveCircle = false;

        private int[] _pre;

        public Bellman_Ford(WeightGraph weightGraph,int s)
        {
            this._weightGraph = weightGraph;
            this._s = s;
            _pre=new int[_weightGraph.V];

            _dirs=new int[_weightGraph.V];
            for (int i = 0; i < _weightGraph.V; i++)
            {
                _dirs[i]=Int32.MaxValue;
            }
            _dirs[s] = 0;

            for (int i = 0; i < _weightGraph.V-1; i++)
            {
                for (int j = 0; j < _weightGraph.V; j++)
                {
                    foreach (var w in _weightGraph.GetAllContiguousEdge(j))
                    {
                        if (_dirs[j] != int.MaxValue && _dirs[j] + _weightGraph.GetWeight(j, w) < _dirs[w])
                        {
                            _dirs[w] = _dirs[j] + _weightGraph.GetWeight(j, w);
                            _pre[w] = j;
                        }
                    }
                }
            }

            for (int j = 0; j < _weightGraph.V; j++)
            {
                foreach (var w in _weightGraph.GetAllContiguousEdge(j))
                {
                    if (_dirs[j] != int.MaxValue && _dirs[j] + _weightGraph.GetWeight(j, w) < _dirs[w])
                        IsHaveCircle = true;
                }
            }
        }

        public bool IsCircle()
        {
            return IsHaveCircle;
        }

        public int Distance(int v)
        {
            _weightGraph.ValidateNumber(v);
            if(_dirs[v]==int.MaxValue) throw new Exception("not exists path");
            return _dirs[v];
        }

        public List<int> Path(int v)
        {
            List<int> list=new List<int>();
            if (_dirs[v] == int.MaxValue) return list;
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