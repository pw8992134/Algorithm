using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;

namespace Graph
{
    public class Lcp4
    {
        private IAdjacency _iAdjacency;

        public int domino(int n, int m, int[][] broken)
        {
            int[,] array=new int[n,m];
            foreach (var index in broken)
            {
                array[index[0], index[1]] = 1;
            }
            _iAdjacency=new AdjacencyList(n*m,true);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (array[i, j] == 0 && j + 1 < m && array[i, j + 1] != 1)
                        _iAdjacency.AddEdge(i * m + j, i * m + j + 1);
                    if (array[i, j] == 0 && i + 1 < n && array[i + 1, j] != 1)
                        _iAdjacency.AddEdge(i * m + j, (i + 1) * m + j);
                }
            }
            BinaryPartGraphMatch binaryPartGraphMatch=new BinaryPartGraphMatch(_iAdjacency);
            return binaryPartGraphMatch.MaxMatch;
        }
    }
}