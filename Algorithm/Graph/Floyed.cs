using System;
using System.Collections.Generic;

namespace Graph
{
    public class Floyed
    {
        private WeightGraph _weightGraph;

        private int[][] _dirs;

        private bool IsHaveNegativeCircle = false;

        private int[,] _pre;

        public Floyed(WeightGraph weightGraph)
        {
            this._weightGraph = weightGraph;
            _dirs=new int[_weightGraph.V][];
            _pre=new int[_weightGraph.V, _weightGraph.V];
            for (int i = 0; i < _weightGraph.V; i++)
            {
                for (int j = 0; j < _weightGraph.V; j++)
                {
                    _pre[i, j] = i;
                }
            }

            for (int i = 0; i < _weightGraph.V; i++)
            {
                _dirs[i]=new int[_weightGraph.V];
                Array.Fill(_dirs[i],int.MaxValue);
            }

            for (int v = 0; v < _weightGraph.V; v++)
            {
                foreach (var w in _weightGraph.GetAllContiguousEdge(v))
                {
                    _dirs[v][w] = _weightGraph.GetWeight(v, w);
                }
            }

            //Floyed
            for (int t = 0; t < _weightGraph.V; t++)
            {
                for (int v = 0; v < _weightGraph.V; v++)
                {
                    for (int w = 0; w < _weightGraph.V; w++)
                    {
                        if (_dirs[v][t]!=Int32.MaxValue && _dirs[t][w]!=Int32.MaxValue && _dirs[v][t] + _dirs[t][w] < _dirs[v][w])
                        {
                            _dirs[v][w] = _dirs[v][t] + _dirs[t][w];
                            _pre[v, w] = _pre[t, w];
                        }
                    }
                }
            }

            for (int i = 0; i < _weightGraph.V; i++)
            {
                if (_dirs[i][i] < 0) IsHaveNegativeCircle = true;
            }
        }

        public bool IsNeagetiveCircle() => IsHaveNegativeCircle;

        public bool IsConnected(int v, int w)
        {
            _weightGraph.ValidateNumber(v);
            _weightGraph.ValidateNumber(w);
            return _dirs[v][w] != Int32.MaxValue;
        }

        public int ShortedLength(int v,int w)
        {
            if(!IsConnected(v,w) || IsHaveNegativeCircle) throw new Exception("exists negative circle");
            return _dirs[v][w];
        }

        public List<int> Path(int v,int w)
        {
            if (!IsConnected(v, w) || IsHaveNegativeCircle) throw new Exception("exists negative circle");
            List<int> list=new List<int>();
            int cur = w;
            while (cur!=v)
            {
                list.Add(cur);
                cur = _pre[v, cur];
            }
            list.Add(v);
            list.Reverse();
            return list;
        }
    }
}