using System;
using System.Collections.Generic;

namespace Graph
{
    public class ToopSort
    {
        private IAdjacency _iAdjacency;

        public bool IsHaveCircle { get; private set; }

        public List<int> Result { get; private set; }

        public ToopSort(IAdjacency iAdjacency)
        {
            _iAdjacency = iAdjacency;
            if(!iAdjacency.Directed) throw new Exception("directed graph can be support");
            Result=new List<int>();
            Queue<int> queue=new Queue<int>();
            int[] indgree=new int[iAdjacency.V];
            for (int i = 0; i < iAdjacency.V; i++)
            {
                indgree[i] = iAdjacency.InDegree(i);
                if(iAdjacency.InDegree(i)==0) 
                    queue.Enqueue(i);
            }

            while (queue.Count>0)
            {
                int v = queue.Dequeue();
                Result.Add(v);
                foreach (var w in _iAdjacency.GetAllContiguousEdge(v))
                {
                    indgree[w]--;
                    if(indgree[w]==0)
                        queue.Enqueue(w);
                }
            }

            if (Result.Count != iAdjacency.V)
            {
                IsHaveCircle = true;
                Result.Clear();
            }
        }
    }
}