namespace Algorithm.SegmentTree
{
    /// <summary>
    /// 线段树元素支持合并操作的接口
    /// </summary>
    public interface IMerger<E>
    {
        /// <summary>
        /// 合并过程
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        E Merge(E a,E b);
    }
}