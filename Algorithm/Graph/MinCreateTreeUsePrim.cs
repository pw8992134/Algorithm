using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// 最小生成树(使用prim算法)
    /// </summary>
    public class MinCreateTreeUsePrim
    {
        private WeightGraph _weightGraph;

        private List<WeightEdge> _result;

        public MinCreateTreeUsePrim(WeightGraph weightGraph)
        {
            this._weightGraph = weightGraph;
            _result=new List<WeightEdge>();

            //cc 的联通分量个数==1

            bool[] visited=new bool[_weightGraph.V];
            visited[0] = true;
            for (int i = 0; i < _weightGraph.V-1; i++)
            {
                WeightEdge min = new WeightEdge(-1, -1, int.MaxValue);
                for (int j = 0; j < visited.Length; j++)
                {
                    if(visited[j]==false) continue;
                    foreach (var w in _weightGraph.GetAllContiguousEdge(j))
                    {
                        if(visited[w])continue;
                        int weight = _weightGraph.GetWeight(j, w);
                        WeightEdge cur=new WeightEdge(j,w,weight);
                        if (cur.CompareTo(min) < 0) min = cur;
                    }
                }
                _result.Add(min);
                visited[min.W] = true;
                visited[min.V] = true;
            }
        }

        public List<WeightEdge> Result()
        {
            return _result;
        }
    }
}