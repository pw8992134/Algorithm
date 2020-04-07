namespace Graph
{
    public class DiferencePath
    {
        //在二维网格 grid 上，有 4 种类型的方格：

        //1 表示起始方格。且只有一个起始方格。
        //2 表示结束方格，且只有一个结束方格。
        //0 表示我们可以走过的空方格。
        //-1 表示我们无法跨越的障碍。
        //返回在四个方向（上、下、左、右）上行走时，从起始方格到结束方格的不同路径的数目，每一个无障碍方格都要通过一次。

        private int[][] g;

        private int _visited;

        private int _r;

        private int _c;

        private int _start;

        private int _end;

        private int _left;

        private int[,] _momenry;

        private int[,] _dirs= { { -1,0},{ 1,0},{ 0,-1},{ 0,1} };

        public int UniquePathsIII(int[][] grid)
        {
            g = grid;
            _visited = 0;
            _r = grid.Length;
            _c = grid[0].Length;
            _left = _r * _c;

            _momenry=new int[1<<(_r * _c),_r * _c];
            for (int i = 0; i < 1 << (_r * _c); i++)
            {
                for (int j = 0; j < _r * _c; j++)
                {
                    _momenry[i,j] = -1;
                }
            }

            for (int i = 0; i < _r; i++)
            {
                for (int j = 0; j < _c; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        _start = i * _c + j;
                        grid[i][j] = 0;
                    }
                    else if (grid[i][j] == 2)
                    {
                        _end = i * _c + j;
                        grid[i][j] = 0;
                    }
                    else if (grid[i][j] == -1)
                        _left--;
                }
            }

            return Dfs(_visited,_start,_left);
        }

        private int Dfs(int visited,int v,int left)
        {
            if (_momenry[visited, v] != -1) return _momenry[visited, v];

            visited +=(1<<v);
            left--;
            if (left == 0 && v==_end)
            {
                visited -=(1<<v);
                _momenry[visited, v] = 1;
                return 1;
            }

            int x = v / _c;
            int y = v % _c;
            int res = 0;
            for (int i = 0; i < 4; i++)
            {
                int nextX = x + _dirs[i, 0];
                int nextY = y + _dirs[i, 1];
                int next = nextX * _c + nextY;
                if (InArea(nextX,nextY) && g[nextX][nextY]==0&& (visited&(1<<next))==0)
                {
                    res += Dfs(visited,next,left);
                }
            }
            visited -= (1 << v);
            _momenry[visited, v] = res;
            return res;
        }

        private bool InArea(int x,int y)
        {
            return x >= 0 && x < _r && y >= 0 && y < _c;
        }
    }
}