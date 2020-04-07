namespace Graph
{
    /// <summary>
    /// 并查集
    /// 并查集，在一些有N个元素的集合应用问题中，我们通常是在开始时让每个元素构成一个单元素的集合，
    /// 然后按一定顺序将属于同一组的元素所在的集合合并，其间要反复查找一个元素在哪个集合中。
    /// *操作:1.把每个点所在集合初始化为其自身。
    ///通常来说，这个步骤在每次使用该数据结构时只需要执行一次，无论何种实现方式，时间复杂度均为O(N)。
    ///2.查找
    ///查找元素所在的集合，即根节点。
    ///3.合并
    ///将两个元素所在的集合合并为一个集合。
    ///通常来说，合并之前，应先判断两个元素是否属于同一集合，这可用上面的“查找”操作实现。
    /// </summary>
    public class UnionFindAggregate
    {
        private int[] _ElementAggregates;

        private int[] _Size;

        public UnionFindAggregate(int v)
        {
            _ElementAggregates=new int[v];
            _Size=new int[v];
            for (int i = 0; i < v; i++)
            {
                _ElementAggregates[i] = i;
                _Size[i] = 1;
            }
        }

        /// <summary>
        /// 查找属于第几个集合
        /// </summary>
        /// <returns></returns>
        public int Find(int v)
        {
            if (v != _ElementAggregates[v])
                _ElementAggregates[v] = Find(_ElementAggregates[v]);
            return _ElementAggregates[v];
        }

        /// <summary>
        /// 两个顶点是否联通
        /// </summary>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public bool IsConnected(int v,int w)
        {
            return Find(w) == Find(v);
        }

        /// <summary>
        /// 合并两个集合
        /// </summary>
        public void Union(int v,int w)
        {
            int vRoot = Find(v);
            int wRoot = Find(w);
            if(vRoot==wRoot) return;
            _ElementAggregates[vRoot] = wRoot;
            _Size[wRoot] += _Size[vRoot];
        }

        /// <summary>
        /// 返回集合大小
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int Size(int v)
        {
            return _Size[Find(v)];
        }
    }
}