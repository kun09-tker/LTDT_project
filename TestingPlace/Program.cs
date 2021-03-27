using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingPlace
{
    class Program
    {
        static void Main(string[] args)
        {
            Edges[] A = new Edges[7];
            A[0]=new Edges(2, "1", "2");
            A[1]=new Edges(4, "0", "1");
            A[2]=new Edges(4, "0", "2");
            A[3]=new Edges(6, "0", "3");
            A[4]=new Edges(6, "0", "4");
            A[5]=new Edges(8, "2", "3");
            A[6]=new Edges(9, "3", "4");
            Kruskal B = new Kruskal(A);
            int Sum=B.Caculate();
            Console.WriteLine(Sum);
        }
    }

    public class Edges
    {
        public int weight;
        public string start, end;

        public Edges(int weight , string start, string end)
        {
            this.weight = weight;
            this.start = start;
            this.end = end;
        }

        public static int operator +(Edges a, Edges b)
        {
            int sum = a.weight + b.weight;
            return sum;
        }

        public static int operator -(Edges a, Edges b)
        {
            return a.weight - b.weight;
        }
        
        public static bool operator >(Edges a, Edges b)
        {
            if (a.weight > b.weight)
            {
                return true;
            }

            return false;
        }
        
        public static bool operator <(Edges a, Edges b)
        {
            if (a.weight < b.weight)
            {
                return true;
            }

            return false;
        }

        public static bool operator ==(Edges a, Edges b)
        {
            if (a.weight == b.weight)
            {
                return true;
            }

            return false;
        }
        
        public static bool operator !=(Edges a, Edges b)
        {
            if (a.weight != b.weight)
            {
                return true;
            }

            return false;
        }
        
    }
    class Kruskal
    {
        public List<Edges> mang;
        public List<string> cycle;
        public int sum;
        public Kruskal(Edges[] X)
        {
            this.mang = new List<Edges>(X);
            cycle = new List<string>();
        }
        
        public int Caculate()
        {
            sum = 0;
            int[] Answer=new int[this.mang.Count];
            this.mang=this.mang.OrderBy(o=>o.weight).ToList();
            this.cycle.Add(mang[0].start);
            this.cycle.Add(mang[0].end);
            sum += mang[0].weight;
            for (int i = 1; i < mang.Count; i++)
            {
                if (!cycle.Contains(mang[i].end))
                {
                    cycle.Add(mang[i].end);
                    sum += mang[i].weight;
                }else if (!cycle.Contains(mang[i].start))
                {
                    cycle.Add(mang[i].start);
                    sum += mang[i].weight;
                }
            }
            return sum;
        }
    }
}