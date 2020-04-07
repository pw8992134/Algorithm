using System.Collections.Generic;

namespace Graph
{
    //在一个 N × N 的方形网格中，每个单元格有两种状态：空（0）或者阻塞（1）。

    //一条从左上角到右下角、长度为 k 的畅通路径，由满足下述条件的单元格 C_1, C_2, ..., C_k 组成：

    //相邻单元格 C_i 和 C_ { i+1}
    //在八个方向之一上连通（此时，C_i 和C_{i+1}
    //不同且共享边或角）
    //C_1 位于(0, 0)（即，值为 grid[0][0]）
    //C_k 位于(N-1, N-1)（即，值为 grid[N - 1][N-1]）
    //如果 C_i 位于(r, c)，则 grid[r][c] 为空（即，grid[r][c] == 0）
    //返回这条从左上角到右下角的最短畅通路径的长度。如果不存在这样的路径，返回 -1 。

    //提示：

    //1 <= grid.length == grid[0].length <= 100
    //grid[i][j] 为 0 或 1
    public class Solution3
    {
        private bool[,] _visited;

        private int[,] _distance;

        private int[][] _grid;

        private int[,] _dirs=new int[8,2]{{-1,0},{1,0},{0,-1},{0,1},{-1,-1},{-1,1},{1,-1},{1,1}};

        private int _n;

        public int ShortestPathBinaryMatrix(int[][] grid)
        {
            _n = grid.Length;
            if (grid[0][0] == 1||grid[_n - 1][_n - 1]==1) return -1;
            if (_n == 1 && grid[0][0] == 0) return 1;
            _grid = grid;
            _visited=new bool[_n, _n];
            _distance=new int[_n, _n];
            Queue<int> queue=new Queue<int>();
            queue.Enqueue(0);
            _visited[0,0] = true;
            _distance[0, 0] = 1;
            while (queue.Count>0)
            {
                int v = queue.Dequeue();
                int x = v / _n;
                int y = v % _n;
                List<int> list = GetNextVetexs(x, y);
                foreach (var w in list)
                {
                    if (!_visited[w / _n, w % _n] && grid[w / _n][w % _n] == 0)
                    {
                        _visited[w / _n, w % _n] = true;
                        queue.Enqueue(w);
                        _distance[w / _n, w % _n] = _distance[x, y] + 1;
                        if (w / _n == _n - 1 && w % _n == _n - 1)
                            return _distance[_n-1, _n-1];
                    }
                }
            }

            return -1;
        }

        private List<int> GetNextVetexs(int x,int y)
        {
            List<int> list=new List<int>();

            for (int i = 0; i < 8; i++)
            {
                int nextX = x + _dirs[i, 0];
                int nextY = y + _dirs[i, 1];
                if(InArea(nextX,nextY) && _grid[nextX][nextY]==0)
                    list.Add(nextX*_n+nextY);
            }

            return list;
        }

        private bool InArea(int x,int y)
        {
            return x >= 0 && y >= 0 && x < _n && y < _n;
        }
    }
}