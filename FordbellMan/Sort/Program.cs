using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            FordBellMan.DuLieu x = new FordBellMan.DuLieu();
            string[] temp = File.ReadAllLines("D:/DoThi.txt");
            x.sodinh = int.Parse(temp[0]);
            int dong = 0;
            int[,] MaTran = new int[100, 100];
            for (int i = 1; i <= x.sodinh; i++)
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
            x.mt = MaTran;

            x.di = 0;
            x.den = 2;

            FordBellMan g = new FordBellMan();
            int[] a = g.TimDuong(x);
            for (int i = 0; i < a[0] + 2; i++)
            {
                Console.Write(a[i].ToString());
            }

            Console.ReadKey();

        }

    }

    public class FordBellMan
    {

        public struct DuLieu
        {
            public int sodinh;
            public int[,] mt;
            public int di, den;
        }

        public int[] TimDuong(DuLieu x)
        {
            int[,] maTran = x.mt;
            int[] Truoc = new int[100];
            int[] khoangCach = new int[100];
            int[] duongDi = new int[100];
            int count = 0;
            int[] data = new int[100];

            for (int k = 0; k < x.sodinh; k++)
            {
                for (int l = 0; l < x.sodinh; l++)
                {
                    if (maTran[k, l] == 0)
                    {
                        maTran[k, l] = 999999;
                    }
                }
            }

            for (int k = 0; k < x.sodinh; k++)
            {
                khoangCach[k] = maTran[x.di, k];
                Truoc[k] = x.di;
            }

            for (int k = 0; k < x.sodinh - 2; k++)
            {
                for (int u = 0; u < x.sodinh; u++)
                {
                    for (int v = 0; v < x.sodinh; v++)
                    {
                        int tmp = khoangCach[u] + maTran[u, v];
                        if (khoangCach[v] > tmp)
                        {
                            khoangCach[v] = tmp;
                            Truoc[v] = u;
                        }
                    }
                }
            }

           

            int i, j = 0;
            duongDi[j] = x.den;
            i = Truoc[x.den];

            while (i != x.di)
            {
                duongDi[++j] = i;
                i = Truoc[i];
            }

            duongDi[j + 1] = x.di;
            int size = 2;
            for (int k = j + 1; k >= 0; k--)
            {
                count++;
                data[size++] = duongDi[k];
            }

            data[0] = count;
            if (khoangCach[x.den] == 999999)
            {
                data[0] = -1;
            }

            data[1] = khoangCach[x.den];

            return data;
        }
    }
}



