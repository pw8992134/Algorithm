using System;

namespace Graph
{
    public class BinaryPartGraphMatch
    {
        private IAdjacency _iAdjacency;

        public int MaxMatch { get; }

        public bool IsPerfectMatch { get; }

        public BinaryPartGraphMatch(IAdjacency iAdjacency)
        {
            _iAdjacency = iAdjacency;
            BinaryPartitionDetection binaryPartitionDetection=new BinaryPartitionDetection(iAdjacency);
            if(!binaryPartitionDetection.IsBinaryPartition) throw new Exception("you must use two partite graph");
            int[] colors = binaryPartitionDetection.Colors;
            WeightGraph weightGraph=new WeightGraph(iAdjacency.V+2,true);
            for (int v = 0; v < iAdjacency.V; v++)
            {
                if(colors[v]==0) weightGraph.AddEdge(iAdjacency.V,v,1);
                if(colors[v]==1) weightGraph.AddEdge(v,iAdjacency.V+1,1);
                foreach (var w in iAdjacency.GetAllContiguousEdge(v))
                {
                    if(colors[v]==0) weightGraph.AddEdge(v,w,1);
                    else if(colors[v]==1) weightGraph.AddEdge(w,v,1);
                }
            }
            Edmonds_Karp edmondsKarp=new Edmonds_Karp(weightGraph,iAdjacency.V,iAdjacency.V+1);
            MaxMatch = edmondsKarp.MaxFlow;
            IsPerfectMatch = MaxMatch * 2 == iAdjacency.V;
        }
    }
}