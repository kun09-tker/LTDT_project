using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VisioForge.Shared.MediaFoundation.OPM;

namespace FordWriteOnPaper
{
    public class DuyetCay
    {
        public struct DO_THI
        {
            public int sodinh;
            public int[,] canh;
            public int[] LuuVet;
            public int[] visited;
        }
        public void DFS(int s, ref DO_THI g)
            {
                g.visited[s] = 1;
                for (int i = 0; i < g.sodinh; i++)
                {
                    if (g.visited[i] == 0 && g.canh[s, i] != 0)
                    {
                        g.LuuVet[i] = s;
                        DFS(i,ref g);
                    }
                }
            }
            public int[] duyetDFS(int s, int f, ref DO_THI dt)
            {
                for (int i = 0; i < dt.sodinh; i++)
                {
                    dt.visited[i] = 0;
                    dt.LuuVet[i] = -1;
                }
                DFS(s,ref dt);
                return dt.LuuVet;
            }
            public void BFS(int s, DO_THI g)
            {
                Queue<int> q = new Queue<int>();
                q.Enqueue(s);
                g.visited[s] = 1;
                while (q.Count() != 0)
                {
                    s = q.Peek();
                    q.Dequeue();
                    for (int i = 0; i < g.sodinh; i++)
                    {
                        if (g.visited[i] == 0 && g.canh[s, i] != 0)
                        {
                            g.visited[i] = 1;
                            q.Enqueue(i);
                            g.LuuVet[i] = s;
                        }
                    }
                }
            }
            public int[] duyetBFS(int s, int f, DO_THI g)
            {

                for (int i = 0; i < g.sodinh; i++)
                {
                    g.visited[i] = 0;
                    g.LuuVet[i] = -1;
                }
                BFS(s,g);
                return g.LuuVet;
            }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            DuyetCay.DO_THI g = new DuyetCay.DO_THI();
            string[] temp = File.ReadAllLines("D:/LTDT_project/DoThi3.txt");
            g.sodinh = int.Parse(temp[0]);
            //Console.WriteLine(g.sodinh);
            int dong = 0;
            //int cot = 0;
            int[, ] MaTran = new int[100, 100];
            for (int i = 1; i <= g.sodinh; i++)
            {
                int cot = 0;
                string[] tempTwo = temp[i].Split(' ');

                foreach (string value in tempTwo)
                {
                    MaTran[dong, cot] = int.Parse(value);
                    cot++;
                }
                dong++;
            }
            g.canh = MaTran;
    
            //Console.WriteLine(MaTran[0,1]);

            DuyetCay x = new DuyetCay();
            int[] a = x.duyetDFS(0, 4, ref g);
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
            }
            foreach (string s in temp)
            {
                Console.WriteLine(s);
            }
            Console.ReadLine();
        }
    }
}