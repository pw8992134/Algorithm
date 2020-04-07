using System;
using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// 最小生成树(使用kruskal算法)
    /// </summary>
    public class MinCreateTreeUseKruskal
    {
        private WeightGraph _weightGraph;

        private List<WeightEdge> _result;

        public MinCreateTreeUseKruskal(WeightGraph weightGraph)
        {
            _weightGraph = weightGraph;
            _result=new List<WeightEdge>();

            //cc检测是否联通

            List<WeightEdge> list=new List<WeightEdge>();
            for (int i = 0; i < _weightGraph.V; i++)
            {
                foreach (var w in _weightGraph.GetAllContiguousEdge(i))
                {
                    if(i>w) continue; 
                    int weight = _weightGraph.GetWeight(i, w);
                    list.Add(new WeightEdge(i,w,weight));
                }
            }
            list.Sort();
            UnionFindAggregate uf = new UnionFindAggregate(_weightGraph.V);
            foreach (var e in list)
            {
                if(uf.IsConnected(e.V, e.W)) continue;
                _result.Add(e);
                uf.Union(e.V, e.W);
            }
        }

        public List<WeightEdge> Result()
        {
            return _result;
        }
    }
}