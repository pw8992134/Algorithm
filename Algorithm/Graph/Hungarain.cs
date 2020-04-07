using System;
using System.Collections.Generic;

namespace Graph
{
    public class Hungarain
    {
        private IAdjacency _iAdjacency;

        private int[] _match;

        public int MaxMatch;

        private bool[] _visited;

        public Hungarain(IAdjacency iAdjacency)
        {
            BinaryPartitionDetection binaryPartitionDetection=new BinaryPartitionDetection(iAdjacency);
            if(!binaryPartitionDetection.IsBinaryPartition) throw new Exception("must be two partite graph");
            _iAdjacency = iAdjacency;
            _match = new int[iAdjacency.V];
            _visited=new bool[iAdjacency.V];
            int[] colors = binaryPartitionDetection.Colors;

            Array.Fill(_match,-1);

            for (int v = 0; v < iAdjacency.V; v++)
            {
                if (_match[v] == -1 && colors[v] == 0)
                {
                    //if (Bfs(v)) MaxMatch++;
                    if (Dfs(v)) MaxMatch++;
                }
            }
        }

        private bool Dfs(int v)
        {
            _visited[v] = true;
            foreach (var w in _iAdjacency.GetAllContiguousEdge(v))
            {
                if (!_visited[w])
                {
                    _visited[w] = true;
                    if (_match[w] == -1 || Dfs(w))
                    {
                        _match[v] = w;
                        _match[w] = v;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool Bfs(int s)
        {
            Queue<int> queue=new Queue<int>();
            queue.Enqueue(s);
            int[] pre=new int[_iAdjacency.V];
            Array.Fill(pre,-1);
            pre[s] = s;
            while (queue.Count>0)
            {
                int v = queue.Dequeue();
                foreach (var w in _iAdjacency.GetAllContiguousEdge(v))
                {
                    if (pre[w]==-1)
                    {
                        if (_match[w] != -1)
                        {
                            pre[w] = v;
                            pre[_match[w]] = w;
                            queue.Enqueue(_match[w]);
                        }
                        else
                        {
                            pre[w] = v;
                            List<int> path = GetArgumentingPath(pre, v, w);
                            for (int i = 0; i < path.Count; i += 2)
                            {
                                _match[path[i]] = path[i + 1];
                                _match[path[i + 1]] = path[i];
                            }

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private List<int> GetArgumentingPath(int[] pre,int start,int end)
        {
            List<int> list=new List<int>();
            int cur = end;
            while (cur!=start)
            {
                list.Add(cur);
                cur = pre[cur];
            }
            list.Add(start);
            list.Reverse();
            return list;
        }
    }
}