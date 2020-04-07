using System;

namespace Graph
{
    public class Solution1
    {
        private bool[][] _visited;

        private int r;

        private int c;

        private readonly int[][] dirs=new int[4][]
        {
            new int[2] { -1, 0 }, 
            new int[2] { 1, 0 } ,
            new int[2] { 0, -1 } , 
            new int[2] { 0, 1 }
        };

        public int MaxAreaOfIsland(int[][] grid)
        {
            if (grid == null || grid.Length == 0 || grid[0].Length == 0) return 0;
            r = grid.Length;
            c = grid[0].Length;
            _visited=new bool[r][];
            for (int i = 0; i < r; i++)
            {
                _visited[i]=new bool[c];
            }
            int result = 0;
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    if (!_visited[i][j] && grid[i][j] == 1)
                        result = Math.Max(result, Dfs(i, j, grid));
                }
            }

            return result;
        }

        private int Dfs(int i,int j,int[][] grid)
        {
            _visited[i][j] = true;
            int result = 1;
            for (int k = 0; k < 4; k++)
            {
                int x = i + dirs[k][0];
                int y = j + dirs[k][1];
                if (InArea(x, y) && !_visited[x][y] && grid[x][y] == 1)
                {
                    _visited[x][y] = true;
                    result += Dfs(x, y, grid);
                }
            }
            return result;
        }

        private bool InArea(int x, int y) => x >= 0 && x < r && y >= 0 && y < c;
    }
}