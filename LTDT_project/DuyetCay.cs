using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_project
{
    class DuyetCay
    {
        public struct GRAPH
        {
            public int sodinh;
            public int[,] A;
            public int[] visited;
            public int[] LuuVet;
        };
        public struct QUEUE
        {
            public int size;
            public int[] a;
        };

        public static void KhoiTao(ref QUEUE q) { q.size = 0; }
        public static bool Them(int k,ref QUEUE q)
        {
            if (q.size + 1 > 100) return false;
            q.a[q.size] = k;
            q.size++;
            return true;
        }
        public static bool KiemTraRong(QUEUE q)
        {
            return (q.size == 0);
        }
        public static void Lay(ref int v, ref QUEUE q)
        { 
            v = q.a[0];
            for (int i = 0; i < q.size - 1; i++)
                q.a[i] = q.a[i + 1];
            q.size--;
        }
        public static void BFS(int s,ref GRAPH g)
        {
            QUEUE q = new QUEUE();
            KhoiTao(ref q);
            Them(s, ref q);
            g.visited[s] = 1;
            while (!KiemTraRong(q))
            {
                Lay(ref s,ref q);
                for (int i = 0; i < g.sodinh; i++)
                {
                    if (g.visited[i] == 0 && g.A[s,i] != int.MinValue)
                    {
                        g.visited[i] = 1;
                        Them(i,ref q);
                        g.LuuVet[i] = s;
                    }
                }
            }
        }
        public string duyetBFS(int s, int f, ref GRAPH g)
        {
            string str="";
            for (int i = 0; i < g.sodinh; i++)
            {
                g.visited[i] = 0;
                g.LuuVet[i] = -1;
            }
            BFS(s,ref g);
            if (g.visited[f] == 1)
            {
                int j = f;
                while (j != s)
                {
                    //cout << j;
                    str += j.ToString() + " ";
                    j = g.LuuVet[j];
                }
                //cout << s;
                str+=j.ToString();
            }
            else str= (-1).ToString();
            return str;
        }

        public string[] BFS(GRAPH g, int a)
        {

            string[] all = new string[100];
            int nall = 0;
            for (int i = 0; i < g.sodinh; i++)
            {
                if (i == a) continue;
                string nget = duyetBFS(a, i,ref g);
                for (int j = nget.Length-1; j >= 0; j--)
                {
                    all[nall] += nget[j];
                    //if (j != nget - 1) all[nall] += " ";
                }
                nall++;
            }
            return all;
        }
    }

}       


