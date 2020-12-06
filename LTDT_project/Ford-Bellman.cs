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
        public int[] TimDuong(DuLieu x)
        {
            x.di = x.di - 1;
            x.den = x.den - 1;
            int[] data = new int[100];
            int[,] maTran = x.mt;
            int[,] maTran2 = new int[x.sodinh, x.sodinh];
            int[] P = new int[x.sodinh];
            int[] L = new int[x.sodinh];

            for (int f = 0; f < x.sodinh; f++)
            {
                for (int v = 0; v < x.sodinh; v++)
                {
                    maTran2[f, v] = maTran[f, v];
                }
            }


            for (int f = 0; f < x.sodinh; f++)
            {
                for (int v = 0; v < x.sodinh; v++)
                {
                    for (int u = 0; u < x.sodinh; u++)
                    {
                        if (maTran2[v, f] != int.MinValue && maTran2[f, u] != int.MinValue
                            && maTran2[v, f] + maTran2[f, u] < maTran2[v, u])
                        {
                            maTran2[v, u] = maTran2[v, f] + maTran2[f, u];
                        }
                    }

                    if (maTran2[v, v] < 0)
                    {
                        data[0] = -1;
                        return data;
                    }
                }
            }


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

                for (int i = 0; i < x.sodinh; i++)
                {
                    for (int j = 0; j < x.sodinh; j++)
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

                for (int i = 0; i < x.sodinh; i++)
                {
                    for (int j = 0; j < x.sodinh; j++)
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

                if (k > x.sodinh)
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

            while (den != x.di && den < x.sodinh && den != P[den])
            {
                count++;
                den = P[den];
                str = (den+1).ToString() + str;
            }

            count++;
            str += x.den;
            str = (count+2).ToString() + L[x.den].ToString() + str;

            data = new int[str.Length];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = int.Parse(str[i].ToString());
            }

            return data;
        }
    }
}