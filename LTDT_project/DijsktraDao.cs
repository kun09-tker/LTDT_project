using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_project
{
    class DijsktraDao
    {
        public struct DuLieu
        {
            public int sodinh;
            public int[,] mt;
            public int di, den;
        }



        public int[] TimDuong(DuLieu x)
        {
            int[] LastV = new int[100];
            int[] data = new int[100];
            int totalLength = 0;
            int count = 0;
            int[,] mts = new int[100, 100];
            for(int i = 0; i < x.sodinh; i++)
            {
                for(int j = 0; j < x.sodinh; j++)
                {
                     mts[i + 1, j + 1] = x.mt[i, j];
                }
            }
            Dijkstra(x.di, x.den, x.sodinh, ref totalLength, LastV, mts);
            if(totalLength!=int.MinValue) handle(x.di, x.den, ref count, LastV, data);
            if (totalLength == int.MinValue) data[0] = -1;
            else data[0] = count + 3;
            data[1] = totalLength;

            return data;
        }

        public void Dijkstra(int dinhDau, int dinhCuoi, int soDinh, ref int totalLength, int[] LastV, int[,] MaTran)
        {
            int[] Length = new int[100];
            bool[] ThuocT = new bool[100];
            int[] Last = LastV;

            int x = dinhDau;
            int y = dinhCuoi;

            for (int i = 1; i <= soDinh; i++)
            {
                ThuocT[i] = true;
                Length[i] = -1;
                Last[i] = -1;
            }

            Length[x] = 0;
            ThuocT[x] = false;
            Last[x] = x;

            int v = x;

            while (ThuocT[y])
            {
                for (int k = 1; k <= soDinh; k++)
                {
                    if (MaTran[v, k] != int.MinValue && ThuocT[k] == true && (Length[k] == -1 || Length[k] < Length[v] + MaTran[v, k]))
                    {
                        Length[k] = Length[v] + MaTran[v, k];
                        Last[k] = v;
                    }
                }


                v = -1;

                for (int i = 1; i <= soDinh; i++)
                {
                    if (i != y)
                    {
                        if (ThuocT[i] == true && Length[i] != -1)
                        {
                            if (v == -1 || Length[v] < Length[i]) v = i;
                        }
                    }
                }

                if (v == -1) v = y;
                ThuocT[v] = false;
            }

            v = y;

            while (v != x)
            {
                try
                {
                    totalLength += MaTran[v, Last[v]];
                    v = Last[v];
                }
                catch
                {
                    totalLength = int.MinValue;
                    break;
                }
            }
        }

        public void handle(int dinhDau, int dinhCuoi, ref int count, int[] LastV, int[] data)
        {
            int x = dinhDau;
            int y = dinhCuoi;
            int[] Last = LastV;
            int[] DuongDi = new int[100];
            int v = y, i;
            int id = 0;

            while (v != x)
            {
                DuongDi[id] = v;
                v = Last[v];
                id++;
            }

            DuongDi[id] = x;

            int index = 2;
            for (i = id; i > 0; i--)
            {
                count++;
                data[index++] = DuongDi[i];
            }
            data[index] = DuongDi[i];

        }

    }
}
