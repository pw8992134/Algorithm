namespace Graph
{
    public class Edge
    {
        public int V { get; set; }

        public int W { get; set; }

        public Edge(int v, int w)
        {
            V = v;
            W = w;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", V, W);
        }
    }
}