using System;

namespace Graph
{
    //给定一个包含了一些 0 和 1的非空二维数组 grid, 一个岛屿 是由四个方向(水平或垂直) 的 1 (代表土地) 构成的组合。你可以假设二维矩阵的四个边缘都被水包围着。

    //找到给定的二维数组中最大的岛屿面积。(如果没有岛屿，则返回面积为0。)

    //示例 1:

    //[[0,0,1,0,0,0,0,1,0,0,0,0,0],
    //[0,0,0,0,0,0,0,1,1,1,0,0,0],
    //[0,1,1,0,1,0,0,0,0,0,0,0,0],
    //[0,1,0,0,1,1,0,0,1,0,1,0,0],
    //[0,1,0,0,1,1,0,0,1,1,1,0,0],
    //[0,0,0,0,0,0,0,0,0,0,1,0,0],
    //[0,0,0,0,0,0,0,1,1,1,0,0,0],
    //[0,0,0,0,0,0,0,1,1,0,0,0,0]]
    //对于上面这个给定矩阵应返回 6。注意答案不应该是11，因为岛屿只能包含水平或垂直的四个方向的‘1’。

    //示例 2:

    //[[0,0,0,0,0,0,0,0]]
    //对于上面这个给定的矩阵, 返回 0。

    //注意: 给定的矩阵grid 的长度和宽度都不超过 50。

    public class Solution
    {
        private int[] g;

        private int r;

        private int c;

        private bool[] _visisted;

        private int[,] dirs = {{-1, 0}, {1, 0}, {0, -1}, {0, 1}};

        public int MaxAreaOfIsland(int[][] grid)
        {
            if (grid == null || grid.Length == 0 || grid[0].Length == 0) return 0;
            r = grid.Length;
            c = grid[0].Length;
            g = new int[r * c];
            _visisted = new bool[r * c];
            ConstructGraph(grid);
            int result = 0;
            for (int i = 0; i < g.Length; i++)
            {
                if (!_visisted[i] && g[i] == 1)
                    result = Math.Max(result, Dfs(i));
            }

            return result;
        }

        private int Dfs(int v)
        {
            _visisted[v] = true;
            int y = v / c;
            int x = v % c;
            int result = 1;
            for (int i = 0; i < 4; i++)
            {
                int nextX = x + dirs[i, 0];
                int nextY = y + dirs[i, 1];
                if (InArea(nextX, nextY) && !_visisted[nextY * c + nextX] && g[nextY * c + nextX] == 1)
                {
                    _visisted[nextY * c + nextX] = true;
                    result += Dfs(nextY * c + nextX);
                }
            }

            return result;
        }

        public bool InArea(int x, int y)
        {
            return x >= 0 && y >= 0 && x < c && y < r;
        }

        private void ConstructGraph(int[][] grid)
        {
            for (int i = 0; i < r * c; i++)
            {
                int x = i / c;
                int y = i % c;
                g[i] = grid[x][y];
            }
        }
    }
}