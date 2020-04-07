using System;
using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// 农夫过河
    /// 农夫有狼,羊,菜,过河,一条船.每次只能带一样东西过河,狼会吃羊,羊会吃菜.怎么样全部过河
    /// </summary>
    public class FarmerAcrossTheRiver
    {
        private List<int[]> _correctStates;

        private int[] _pre;

        private bool[] _visited;

        public int _end;

        public List<int[]> AcrossTheRiver()
        {
            _correctStates = CreateVertex();
            _visited=new bool[1*1000+1*100+1*10+1+1];
            _pre=new int[1 * 1000 + 1 * 100 + 1 * 10 + 1 + 1];
            Queue<int[]> queue=new Queue<int[]>();
            queue.Enqueue(new int[]{1,1,1,1});
            _visited[1 * 1000 + 1 * 100 + 1 * 10 + 1] = true;

            while (queue.Count>0)
            {
                int[] v = queue.Dequeue();
                List<int[]> nexts = GetNexts(v);

                foreach (var w in nexts)
                {
                    int index = GetIndex(w);
                    if (!_visited[index])
                    {
                        _visited[index] = true;
                        queue.Enqueue(w);
                        _pre[index] = GetIndex(v);
                        int i = _pre[index];
                        if (w[0] == 0 && w[1] == 0 && w[2] == 0 && w[3] == 0)
                        {
                            _end = GetIndex(w);
                            return Path();
                        }
                    }
                }
            }

            return new List<int[]>();
        }

        private int GetIndex(int[] w)
        {
            return w[0] * 1000 + w[1] * 100 + w[2] * 10 + w[3];
        }

        private List<int[]> GetNexts(int[] v)
        {
            List<int[]> transferArray = CreateTransferArray();
            List<int[]> list=new List<int[]>();
            foreach (var t in transferArray)
            {
                int[] w = SumTwoArray(v, t);
                bool isCorrectState = IsCorrectState(w);
                if (isCorrectState)
                    list.Add(w);
            }

            return list;
        }

        private int[] SumTwoArray(int[] a,int[] b)
        {
            int[] result=new int[4];
            for (int i = 0; i < 4; i++)
            {
                if (a[i] == 1 && b[i] == 1) result[i] = 0;
                else result[i] = a[i] + b[i];
            }

            return result;
        }

        private bool IsCorrectState(int[] s)
        {
            foreach (var v in _correctStates)
            {
                if (v[0] == s[0] && v[1] == s[1] && v[2] == s[2] && v[3] == s[3]) return true;
            }

            return false;
        }

        private List<int[]> CreateVertex()
        {
            List<int[]> list=new List<int[]>();
            list.Add(new int[] { 1, 1, 1, 0});
            list.Add(new int[] { 1, 1, 0, 1 });
            list.Add(new int[] { 1, 0, 1, 1 });
            list.Add(new int[] { 1, 0, 1, 0 });
            list.Add(new int[] { 0, 1, 0, 1 }); 
            list.Add(new int[] { 0, 1, 0, 0 });
            list.Add(new int[] { 0, 0, 1, 0 });
            list.Add(new int[] { 0, 0, 0, 1 });
            list.Add(new int[] { 0, 0, 0, 0 });
            list.Add(new int[] { 1, 1, 1, 1 });
            return list;
        }

        private int[] SplitNumber(int v)
        {
            int[] i=new int[4];
            int farmer = v / 1000;
            int wolf = v % 1000 / 100;
            int sheep= v % 1000 % 100/10;
            int vegetables = v % 1000 % 100 % 10;
            return new int[]{farmer,wolf,sheep,vegetables};
        }

        private List<int[]> CreateTransferArray()
        {
            List<int[]> list = new List<int[]>();
            list.Add(new int[] { 1, 1, 0, 0 });
            list.Add(new int[] { 1, 0, 1, 0 });
            list.Add(new int[] { 1, 0, 0, 1 });
            list.Add(new int[] { 1, 0, 0, 0 });
            return list;
        }

        public List<int[]> Path()
        {
            List<int[]> list=new List<int[]>();
            int cur = _end;
            int start = GetIndex(new int[] {1, 1, 1, 1});
            while (cur!=start)
            {
                list.Add(SplitNumber(cur));
                cur = _pre[cur];
            }
            list.Add(SplitNumber(start));
            list.Reverse();
            return list;
        }
    }
}