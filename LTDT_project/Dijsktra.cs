using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_project
{
    class Dijsktra
    {
        public struct DuLieu
        {
            public int sodinh;
            public int di;
            public int den;
            public int[,] mt;
        };
       
        public int[] TimDuong(DuLieu x)
        {
            int min;
            int[] nhan = new int[100];
            int[] daDuyet = new int[100];
            int[] kq = new int[100];
            int dem = 0;
            int[,] mts = new int[100,100];
            int[] s = new int[1000];
            for(int i = 1; i <= x.sodinh; i++)
            {
                for(int j = 1; j <= x.sodinh; j++)
                {
                    mts[i, j] = x.mt[i - 1, j - 1];
                }
            }
            for (int i = 1; i <= x.sodinh; i++)
            {
                nhan[i] = 0;
                kq[i] = 1000000;
                daDuyet[i] = 0;
            }
            int vet = x.di;
            nhan[vet] = 1;
            kq[vet] = 0;
            while (vet != x.den && dem <= x.sodinh)
            {
                for(int i = 1; i <= x.sodinh; i++)
                {
                    if (mts[vet, i] > 0 && kq[vet] + mts[vet, i] < kq[i] && nhan[i] == 0)
                    {
                        kq[i] = kq[vet] + mts[vet, i];
                        daDuyet[i] = vet;
                    }
                }
                min = 1000000;
                for(int j = 1; j <= x.sodinh; j++)
                {
                    if (min > kq[j] && nhan[j] == 0)
                    {
                        min = kq[j];
                        vet = j;
                    }
                }
                nhan[vet] = 1;
                dem++;
            }
            if (dem < x.sodinh)
            {
                s[1] = kq[x.den];
                s[2] = x.den;
                int index = 3;
                int tmp = daDuyet[x.den];
                
                while (tmp != x.di)
                {
                   // if (tmp == 0) break;
                    s[index] = tmp;
                    tmp = daDuyet[tmp];
                    index++;
                    //if(tmp==0) break; 
                }
                s[index++] = x.di;
                s[0] = index;
                //s[3] = dem;
            }
            else s[0] = -1;
            return s;
        }
    }
}
