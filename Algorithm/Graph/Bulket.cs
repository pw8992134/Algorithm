using System;
using System.Collections;
using System.Collections.Generic;

namespace Graph
{
    //一个水桶能装5升水,一个水桶能装3升水,怎么得到4升水
    public class Bulket
    {
        private bool[] _visited;

        private int[] _pre;

        private int _end=-1;

        public Bulket WaterPullze()
        {
            _visited=new bool[100];
            _pre=new int[5*10+3+1];
            Queue<int[]> queue=new Queue<int[]>();
            queue.Enqueue(new int[]{0,0});
            _visited[0] = true;
            _pre[0] = 0;
            while (queue.Count>0)
            {
                int[] state = queue.Dequeue();
                List<int[]> list = GetNextStates(state[0], state[1]);
                foreach (var v in list)
                {
                    if (!_visited[v[0] * 10 + v[1]])
                    {
                        _visited[v[0] * 10 + v[1]]=true;
                        queue.Enqueue(v);
                        _pre[v[0] * 10 + v[1]] = state[0] * 10 + state[1];
                        if (v[0] == 4)
                        {
                            _end = v[0] * 10 + v[1];
                            break;
                        }
                    }
                }
            }

            return this;
        }


        private List<int[]> GetNextStates(int x,int y)
        {
            List<int[]> list=new List<int[]>();
            list.Add(new int[]{0,y});
            list.Add(new int[]{x,0});
            int nextX = x - Math.Min(x, 3 - y);
            int nextY = y + Math.Min(x, 3 - y);
            list.Add(new int[]{nextX,nextY});
            nextX = x + Math.Min(5-x, y);
            nextY = y - Math.Min(5-x, y);
            list.Add(new int[]{nextX,nextY});
            list.Add(new int[]{5,y});
            list.Add(new int[]{x,3});
            return list;
        }

        public List<int[]> Path()
        {
            List<int[]> list=new List<int[]>();
            if (_end == -1) return list;
            int cur = _end;
            while (cur!=0)
            {
                int x = cur / 10;
                int y = cur % 10;
                list.Add(new int[]{x,y});
                cur = _pre[cur];
            }
            list.Add(new int[]{0,0});
            list.Reverse();
            return list;
        }
    }
}