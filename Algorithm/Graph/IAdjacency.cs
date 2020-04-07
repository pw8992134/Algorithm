using System;
using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// 图数据结构实现相关接口
    /// </summary>
    public interface IAdjacency
    {
         /// <summary>
         /// 顶点(这里以0~i的顶点做实例,即表示个数也表示索引值)
         /// </summary>
         int V { get; }

         /// <summary>
         /// 边的个数
         /// </summary>
         int E { get; }

         /// <summary>
         /// 是否有向图
         /// </summary>
         bool Directed { get; }

         /// <summary>
         /// 顶点的入度
         /// </summary>
         /// <param name="v"></param>
         /// <returns></returns>
         int InDegree(int v);

         /// <summary>
         /// 顶点的出度
         /// </summary>
         /// <param name="v"></param>
         /// <returns></returns>
         int OutDegree(int v);

         /// <summary>
         /// 两个顶点是否有边
         /// </summary>
         /// <param name="v"></param>
         /// <param name="w"></param>
         /// <returns></returns>
         bool HasEdge(int v, int w);

         /// <summary>
         /// 返回一个顶点的相邻边的顶点
         /// </summary>
         /// <param name="v"></param>
         /// <returns></returns>
         ICollection<int> GetAllContiguousEdge(int v);

         /// <summary>
         /// 顶点的度(这里只讨论了无向图,故度等于顶点相邻边数)
         /// </summary>
         /// <param name="v"></param>
         /// <returns></returns>
         int Dgree(int v);

        /// <summary>
        /// 提供子接口复写ToString方法
        /// </summary>
        /// <returns></returns>
        string ToString();

        /// <summary>
        /// 验证顶点是否合法
        /// </summary>
        /// <param name="v"></param>
        void ValidateNumber(int v);

        /// <summary>
        /// 删除边
        /// </summary>
        /// <param name="v"></param>
        /// <param name="w"></param>
        void RemoveEdge(int v,int w);

        /// <summary>
        /// 增加边
        /// </summary>
        /// <param name="v"></param>
        /// <param name="w"></param>
        void AddEdge(int v,int w);

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        IAdjacency Clone();

        /// <summary>
        /// 求一个图的反图
        /// </summary>
        /// <returns></returns>
        IAdjacency Reverse();
    }
}