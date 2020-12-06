using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_project
{
    class DijsktraDao
    {
        const int negative_INF = -100000;
        public struct DuLieu
        {
            public int sodinh;
            public int[,] mt;
            public int di, den;
        }

        //
        //Dijktra Đảo
        //Trả về mảng 1 chiều
        public int[] Dijsktra_Dao(DuLieu x)
        {
            x.den = x.den - 1;
            x.di = x.di - 1;
            XetLienThong cmd = new XetLienThong();
            XetLienThong.DoThi X;
            X.iMaTran = x.mt;
            X.iSoDinh = x.sodinh;
            if (!cmd.xetLienThong(X))
            {
                int[] a = { -1 };
                return a;
            }

            string[,] ketqua = new string[10000, 10000];//kết quả cuối cùng
            bool[] check = new bool[x.sodinh];//đã có dấu * hay chưa
            int[] cost = new int[x.sodinh];//tổng trọng số đi mặc định âm vô cực
            string[] path = new string[x.sodinh];//đỉnh phải đi qua đầu tiên trong Dijsktra
            int k = 0,A=x.di,B=x.den;//mặc định đỉnh a là 0(a=97(ascii)-49=>0) và dòng k=0
                      //Chỉnh dòng 0 đầu tiên
            for (int i = 0; i < x.sodinh; i++)
            {

                ketqua[k, i] = i.ToString();
            }
            k++;
            for (int i = 0; i < x.sodinh; i++)
            {
                check[i] = false;
                cost[i] = negative_INF;
                path[i] = "__";
                ketqua[k, i] = "(oo,__)";
            }
            cost[A] = 0;
            check[A] = true;
            ketqua[k, A] = "0*";
            // Xét từ từ dòng 1 trở đi
            for (int i = 0; i < x.sodinh; i++)
            {
                //Xét những đỉnh gần kề đỉnh x.di
                for (int j = 0; j < x.sodinh; j++)
                {
                    if (cost[j] < cost[A] + x.mt[A, j] && !check[j] && x.mt[A, j] != int.MinValue)
                    {
                        cost[j] = cost[A] + x.mt[A, j];
                        path[j] = A.ToString();
                    }
                }
                //Bỏ dòng k vào ma trận kết quả
                k++;
                for (int j = 0; j < x.sodinh; j++)
                {
                    if (check[j])
                    {
                        ketqua[k, j] = "__";
                    }
                    else
                    {
                        if (cost[j] != negative_INF)
                        {
                            ketqua[k, j] = $"({cost[j]},{path[j]})";
                        }
                        else
                        {
                            ketqua[k, j] = "(oo,__)";
                        }
                    }
                }
                //Tìm đỉnh lớn nhất tiếp theo
                int MAX = negative_INF;
                for (int j = 0; j < x.sodinh; j++)
                {
                    if (MAX < cost[j] && !check[j])
                    {
                        MAX = cost[j];
                        A = j;
                    }
                }
                check[A] = true;
                ketqua[k, A] += "*";
            }
            for (int i = 0; i < x.sodinh; i++)
            {
                ketqua[x.sodinh + 1, i] = "__";
            }
            int dem1 = 1, dem2 = 3,C=x.den,trongso=cost[C];
            int[] ketqua_chinhthuc = new int[1000];
            ketqua_chinhthuc[2] = C;
            while (C != x.di)
            {
                C = Int32.Parse(path[C]);
                ketqua_chinhthuc[dem2] = C;
                dem2++;
                dem1++;
            }
            ketqua_chinhthuc[0] = dem1+2;
            ketqua_chinhthuc[1] = trongso;
            Array.Reverse(ketqua_chinhthuc, 2, dem1);
            return ketqua_chinhthuc;
        }

    }
}