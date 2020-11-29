using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_project
{
    class DuyetCay
    {
        public struct DO_THI
        {
            public int sodinh;
            public int[,] canh;
            public int[] LuuVet;
            public int[] visited;
            public void DFS(int s)
            {
                visited[s] = 1;
                for (int i = 0; i < sodinh; i++)
                {
                    if (visited[i] == 0 && canh[s, i] != 0)
                    {
                        LuuVet[i] = s;
                        DFS(i);
                    }
                }
            }
            public int[] duyetDFS(int s, int f, DO_THI dt)
            {
                for (int i = 0; i < dt.sodinh; i++)
                {
                    dt.visited[i] = 0;
                    dt.LuuVet[i] = -1;
                }
                DFS(s);
                return LuuVet;
            }
            public void BFS(int s)
            {
                Queue<int> q = new Queue<int>();
                q.Enqueue(s);
                visited[s] = 1;
                while (q.Count() != 0)
                {
                    s = q.Peek();
                    q.Dequeue();
                    for (int i = 0; i < sodinh; i++)
                    {
                        if (visited[i] == 0 && canh[s, i] != 0)
                        {
                            visited[i] = 1;
                            q.Enqueue(i);
                            LuuVet[i] = s;
                        }
                    }
                }
            }
            public int[] duyetBFS(int s, int f)
            {

                for (int i = 0; i < sodinh; i++)
                {
                    visited[i] = 0;
                    LuuVet[i] = -1;
                }
                BFS(s);
                return LuuVet;
            }
        };
    }
}

