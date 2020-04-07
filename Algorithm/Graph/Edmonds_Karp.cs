using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Edmonds_Karp
    {
        private WeightGraph _weightGraph;

        private WeightGraph _residualQuantityGraph;

        private int _s;

        private int _t;

        public int MaxFlow { get; private set; }

        public Edmonds_Karp(WeightGraph weightGraph,int s,int t)
        {
            _weightGraph = weightGraph;
            _s = s;
            _t = t;
            if (!weightGraph.Directed) throw new Exception("directed graph can be supported");
            if (_weightGraph.V < 2) throw new Exception("vertex must be more than 2");
            _weightGraph.ValidateNumber(s);
            _weightGraph.ValidateNumber(t);
            if (s == t) throw new Exception("source point and sink point can't be same");
            _residualQuantityGraph =new WeightGraph(_weightGraph.V,true);

            for (int v = 0; v < _weightGraph.V; v++)
            {
                foreach (var w in _weightGraph.GetAllContiguousEdge(v))
                {
                    int weight = _weightGraph.GetWeight(v, w);
                    _residualQuantityGraph.AddEdge(v,w,weight);
                    _residualQuantityGraph.AddEdge(w,v,0);
                }
            }

            while (true)
            {
                List<int> path = GetArgumentingPath();
                if (path.Count == 0) break;
                int f = int.MaxValue;
                for (int i = 1; i < path.Count; i++)
                {
                    int v = path[i - 1];
                    int w = path[i];
                    f = Math.Min(f, _residualQuantityGraph.GetWeight(v, w));
                }
                MaxFlow += f;
                for (int i = 1; i < path.Count; i++)
                {
                    int v = path[i-1];
                    int w = path[i];
                    int weight = _residualQuantityGraph.GetWeight(v, w);
                    int residualQuantity = _residualQuantityGraph.GetWeight(w, v);
                    _residualQuantityGraph.SetWeight(v,w,weight-f);
                    _residualQuantityGraph.SetWeight(w,v,residualQuantity+f);
                }
            }
        }

        private List<int> GetArgumentingPath()
        {
            Queue<int> queue=new Queue<int>();
            queue.Enqueue(_s);

            int[] pre=new int[_residualQuantityGraph.V];

            for (int i = 0; i < _residualQuantityGraph.V; i++)
            {
                pre[i] = -1;
            }

            pre[_s] = _s;
            while (queue.Count>0)
            {
                int v = queue.Dequeue();
                foreach (var w in _residualQuantityGraph.GetAllContiguousEdge(v))
                {
                    if (pre[w] == -1 && _residualQuantityGraph.GetWeight(v, w) > 0)
                    {
                        pre[w] = v;
                        queue.Enqueue(w);
                    }
                }
            }

            List<int> path=new List<int>();
            if (pre[_t] == -1) return path;
            int cur = _t;
            while (cur!=_s)
            {
                path.Add(cur);
                cur = pre[cur];
            }
            path.Add(_s);
            path.Reverse();
            return path;
        }

        public int FlowInTwoVertex(int v,int w)
        {
            if(!_weightGraph.HasEdge(v,w)) throw new Exception("this edge is not exists!");
            return _residualQuantityGraph.GetWeight(w, v);
        }
    }
}