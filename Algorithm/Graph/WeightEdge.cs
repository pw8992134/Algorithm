using System;
using System.Diagnostics.CodeAnalysis;

namespace Graph
{
    public class WeightEdge:IComparable<WeightEdge>
    {
        public int V { get;private set; }

        public int W { get;private set; }

        public int Weight { get;private set; }

        public WeightEdge(int v, int w,int weight)
        {
            V = v;
            W = w;
            Weight = weight;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}", V, W,Weight);
        }

        public int CompareTo([AllowNull] WeightEdge other)
        {
            return this.Weight - other.Weight;
        }
    }
}