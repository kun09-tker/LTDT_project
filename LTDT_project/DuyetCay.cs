﻿using System;
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
        /*public struct QUEUE
        {
            public int size;
            public int[] a;
        };*/

        public int[] a = new int[100];
        public int size = 0;
        public bool Them(int k,int[] a,ref int size)
        {
            if (size + 1 > 100) return false;
            a[size] = k;
            size++;
            return true;
        }
        public  bool KiemTraRong(int size)
        {
            return (size == 0);
        }
        public  void Lay(ref int v, int[] a,ref int size)
        { 
            v = a[0];
            for (int i = 0; i < size - 1; i++)
                a[i] = a[i + 1];
            size--;
        }
        public void BFS(int s,ref GRAPH g)
        {
            // QUEUE q = new QUEUE();
            Them(s, a, ref size);
            g.visited[s] = 1;
            while (!KiemTraRong(size))
            {
                Lay(ref s,a,ref size);
                for (int i = 0; i < g.sodinh; i++)
                {
                    if (g.visited[i] == 0 && g.A[s,i] != int.MinValue)
                    {
                        g.visited[i] = 1;
                        Them(i,a,ref size);
                        g.LuuVet[i] = s;
                    }
                }
            }
        }
        public List<int> duyetBFS(int s, int f, ref GRAPH g)
        {
            List<int> str = new List<int>();
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
                    str.Add(j);
                    j = g.LuuVet[j];
                }
                //cout << s;
                str.Add(j);
            }
            else str.Add(-1);
            return str;
        }

        public string[] BFS(GRAPH g, int a)
        {

            string[] all = new string[100];
            int nall = 0;
            for (int i = 0; i < g.sodinh; i++)
            {
                if (i == a) continue;
                List<int> nget = duyetBFS(a, i,ref g);
                for (int j = nget.Count-1; j > 0; j--)
                {
                    all[nall] += nget[j].ToString()+" ";
                    //if (j != nget - 1) all[nall] += " ";
                }
                all[nall] += nget[0].ToString();
                nall++;
            }
            return all;
        }


        public struct DO_THI
        {
            public int sodinh;
            public int[,] canh;
        }
        public int[] LuuVet = new int[100];
        public int[] visited = new int[100];
        public void DFS(int s, ref DO_THI g)
        {
            visited[s] = 1;
            for (int i = 0; i < g.sodinh; i++)
            {
                if (visited[i] == 0 && g.canh[s, i] != int.MinValue)
                {
                    LuuVet[i] = s;
                    DFS(i, ref g);
                }
            }
        }
        public List<int> duyetDFS(int s, int f, ref DO_THI dt)
        {
            List<int> str = new List<int>();
            for (int i = 0; i < dt.sodinh; i++)
            {
                visited[i] = 0;   /// chu f t co thay su dung au ?? coi lai di

                LuuVet[i] = -1;
            }
            DFS(s, ref dt);
            if (visited[f] == 1)
            {
                int j = f;
                while (j != s)
                {
                    str.Add(j);
                    j = LuuVet[j];
                }
                str.Add(s);
            }
            else
            {
                str.Add(-1);
            }
            return str;
        }
        public string[] DFSstr(int s, DO_THI g)
        {
            string[] str = new string[100];
            int index = 0;
            for (int i = 0; i < g.sodinh; i++)
            {
                if (i == s) continue;
                else
                {
                    List<int> a = duyetDFS(s, i, ref g);
                    for (int j = a.Count-1; j > 0; j--)
                    {
                        str[index] += a[j].ToString()+" ";
                    }
                    str[index] += a[0].ToString();
                    index++;
                }
            }
            return str;
        }
    }

}       


