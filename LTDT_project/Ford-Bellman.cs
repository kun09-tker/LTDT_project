using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_project
{
    class Ford_Bellman
    {
        public struct DuLieu
        {
            public int sodinh;
            public int[,] mt;
            public int di, den;
        }
        public int[] TimDuong(DuLieu x,bool type)
        {
            x.den = x.den - 1;
            x.di = x.di - 1;
            int[] data = new int[100];
            int[,] maTran = x.mt;
            int[] P = new int[100];
            int[] L = new int[100];


            for (int i = 0; i < x.sodinh; i++)
            {
                for (int j = 0; j < x.sodinh; j++)
                {

                    if (maTran[i, j] == int.MinValue)
                    {
                        maTran[i, j] = int.MaxValue;
                    }
                    else
                    {
                        if (!type)
                        {
                            maTran[i, j] *= -1;
                        }
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
            int size = 2;
          

            while (den != x.di)
            {
                count++;
                den = P[den];
                // str = (den+1).ToString() + str;
                data[size++] = den + 1;
            }
            count++;
            data[size] = x.di + 1;
            data[0] = count + 2;
            data[1] = L[x.den];
            return data;
        }
    }
}