using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_project
{
    class DeQuy
    {
        public struct DuLieu
        {
            public int sodinh;
            public int di;
            public int den;
            public int[,] mt;
        };
        public int[] DanhDau = new int[100];
        public int[] L = new int[100];
        public int[] L1 = new int[100];
        public int Tong,Tong1 = 0;
        public int Canh1 = 0;
        void KhoiTao(ref DuLieu x)
        {
            x.den = x.den - 1;
            x.di = x.di - 1;
            for (int i = 0; i < x.sodinh; i++)
            {
                DanhDau[i] = 0;   //Các đỉnh chưa được đánh dấu
                L[i] = 0;
            }
            DanhDau[x.di] = 1;      //Dánh dấu đỉnh xuẩt phát
            L[0] = x.di;         //Đường đi đầu tiên qua đỉnh đầu
            Tong = 0;         //Trong số đường đi
            Tong1 = 0;         //Lưu lại trọng số lớn nhất của đường đi
        }
        int[] InDuongDi(DuLieu x)
        {
            int[] kq = new int[100];
            kq[0] = Canh1 + 2;
            kq[1] = Tong1;
            kq[2] = (x.di + 1);
            for (int i = 3; i < Canh1+2; i++)
                kq[i] = (L1[i-2] + 1);
            return kq;
        }
        void XuLy(int Canh)
        {
            if (Tong > Tong1)
            {
                Tong1 = Tong;      //Lưu lại trọng số tốt hơn
                Canh1 = Canh;      //Lưu lại số cạnh đã đi qua
                for (int i = 0; i < Canh; i++)
                    L1[i] = L[i];      //Lưu lại đường đi tốt hơn
            }
        }
        void TimKiem(int SoCanh,DuLieu x)
        {
            if (L[SoCanh - 1] == x.den) XuLy(SoCanh);
            else
            {
                for (int i = 0; i < x.sodinh; i++)
                    if (x.mt[L[SoCanh - 1],i] > 0 && DanhDau[i] == 0)
                    {
                        L[SoCanh] = i;
                        DanhDau[i] = 1;
                        Tong += x.mt[L[SoCanh - 1],i];
                        TimKiem(SoCanh + 1,x);
                        L[SoCanh] = 0;
                        Tong -=x.mt[L[SoCanh - 1],i];
                        DanhDau[i] = 0;
                    }
            }
        }
        public int[] TimDuong(DuLieu x)
        {
            int[] s = new int[100];
            KhoiTao(ref x);
            TimKiem(1, x);
            if (Tong1 == 0)
            {
                s[0] = -1;
                return s;
            }
            else
            {
                s = InDuongDi(x);
                return s;
            }
        }
    }
}