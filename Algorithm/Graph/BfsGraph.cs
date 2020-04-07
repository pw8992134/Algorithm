using System.Collections.Generic;

namespace Graph
{
    public class BfsGraph
    {
        private readonly IAdjacency _adjacency;

        private readonly bool[] _visited;

        public List<int> Order { get; }

        public BfsGraph(IAdjacency iAdjacency)
        {
            _adjacency = iAdjacency;
            _visited=new bool[_adjacency.V];
            Order=new List<int>();

            for (int i = 0; i < _adjacency.V; i++)
            {
                if(!_visited[i])  Bfs(i);
            }
        }

        private void Bfs(int v)
        {
            _visited[v] = true;
            Queue<int> queue=new Queue<int>();
            queue.Enqueue(v);
            while (queue.Count>0)
            {
                int s = queue.Dequeue();
                Order.Add(s);
                foreach (var w in _adjacency.GetAllContiguousEdge(s))
                {
                    if (!_visited[w])
                    {
                        queue.Enqueue(w);
                        _visited[w] = true;
                    }
                }
            }
        }
    }
}