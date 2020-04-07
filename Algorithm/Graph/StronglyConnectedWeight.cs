using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// 强连通分量
    /// </summary>
    public class StronglyConnectedWeight
    {
        private IAdjacency _iAdjacency;

        private IAdjacency _origion;

        private bool[] _visited;

        private int[] _visitedForConnectedWeight;

        private List<int> _post;

        public int StronglyConnectedWeightCount;

        public List<int>[] StronglyConnectedComponent;

        public StronglyConnectedWeight(IAdjacency iAdjacency)
        {
            _iAdjacency = iAdjacency.Reverse();
            _origion = iAdjacency;
            _post=new List<int>();
            _visited=new bool[iAdjacency.V];
            for (int i = 0; i < iAdjacency.V; i++)
            {
                if (!_visited[i])
                {
                    Dfs(i);
                }
            }
            CalculateStronglyConnectedWeight();
        }

        public void Dfs(int v)
        {
            _visited[v] = true;
            foreach (var w in _iAdjacency.GetAllContiguousEdge(v))
            {
                if(!_visited[w]) Dfs(w);
            }
            _post.Add(v);
        }

        public void CalculateStronglyConnectedWeight()
        {
            _post.Reverse();
            StronglyConnectedWeightCount = 0;
            _visitedForConnectedWeight=new int[_origion.V];
            for (int i = 0; i < _origion.V; i++)
            {
                _visitedForConnectedWeight[i] = -1;
            }
            foreach (var v in _post)
            {
                if(_visitedForConnectedWeight[v]==-1)
                    Dfs(v,++StronglyConnectedWeightCount);
            }

            StronglyConnectedComponent=new List<int>[StronglyConnectedWeightCount];
            for (int i = 0; i < StronglyConnectedWeightCount; i++)
            {
                StronglyConnectedComponent[i] = new List<int>();
                for (int j = 0; j < _visitedForConnectedWeight.Length; j++)
                {
                    if (_visitedForConnectedWeight[j] == i+1)
                    {
                        StronglyConnectedComponent[i].Add(j);
                    }
                }
            }
        }

        public void Dfs(int v,int count)
        {
            _visitedForConnectedWeight[v] = count;
            foreach (var w in _origion.GetAllContiguousEdge(v))
            {
                if(_visitedForConnectedWeight[w]==-1)
                    Dfs(w,count);
            }
        }
    }
}