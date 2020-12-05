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
            string[] temp = File.ReadAllLines("D:/graph.txt");
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
            x.den = 3;

            FordBellMan g = new FordBellMan();
            int[] a = g.TimDuong(x);


            for (int i = 0; i < a.Length; i++)
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
            int[] data;
            int[,] maTran = x.mt;
            int[] P = new int[100];
            int[] L = new int[100];


            for (int i = 0; i < x.sodinh; i++)
            {
                for (int j = 0; j < x.sodinh; j++)
                {

                    if (maTran[i, j] == 0)
                    {
                        maTran[i, j] = int.MaxValue;
                    }
                }
            }

            for (int i = 0; i < L.Length; i++)
            {
                L[i] = int.MaxValue;
            }

            L[x.di] = 0;

            bool stop = false;
            int k = 0;

            while (!stop)
            {
                stop = true;
                k = k + 1;

                for (int i = 0; i < L.Length; i++)
                {
                    for (int j = 0; j < L.Length; j++)
                    {
                        if (maTran[i, j] > 0 && maTran[i, j] < int.MaxValue)
                        {
                            if (L[j] > L[i] + maTran[i, j])
                            {
                                L[j] = L[i] + maTran[i, j];
                                P[j] = i;
                                stop = false;
                            }
                        }
                    }
                }

                if (k > 100)
                {
                    if (stop == false)
                    {
                        stop = true;
                    }
                }
            }

            int den = x.den;
            int count = 0;
            string str = "";

            while (den != x.di)
            {
                count++;
                den = P[den];
                str = den + str;
            }

            count++;
            str += x.den;
            str = count.ToString() + L[x.den].ToString() + str;

            data = new int[str.Length];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = int.Parse(str[i].ToString());
            }
            return data;
        }
    }
}



