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
            int k = 0;//mặc định đỉnh a là 0(a=97(ascii)-49=>0) và dòng k=0
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
            cost[x.di] = 0;
            check[x.di] = true;
            ketqua[k, x.di] = "0*";
            // Xét từ từ dòng 1 trở đi
            for (int i = 0; i < x.sodinh; i++)
            {
                //Xét những đỉnh gần kề đỉnh x.di
                for (int j = 0; j < x.sodinh; j++)
                {
                    if (cost[j] < cost[x.di] + x.mt[x.di, j] && !check[j] && x.mt[x.di, j] != int.MinValue)
                    {
                        cost[j] = cost[x.di] + x.mt[x.di, j];
                        path[j] = x.di.ToString();
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
                        x.di = j;
                    }
                }
                check[x.di] = true;
                ketqua[k, x.di] += "*";
            }
            for (int i = 0; i < x.sodinh; i++)
            {
                ketqua[x.sodinh + 1, i] = "__";
            }
            //Lấy xét từ cột đỉnh kết thúc
            string temp = x.den.ToString();
            int[] ketqua_chinhthuc = new int[10000];
            ketqua_chinhthuc.Append(int.Parse(temp));
            int trongso = 0;
            //Cho tới khi temp chạm tới đỉnh x.di là ngừng
            while (temp != x.di.ToString())
            {
                for (int i = 0; i < x.sodinh; i++)
                {
                    //xét cột nào là đỉnh kết thúc
                    if (ketqua[0, i] == temp)
                    {
                        for (int j = 0; j < x.sodinh; j++)
                        {   //xét trong cột đỉnh kết thúc có đường đi dài nhất
                            if (ketqua[j, i].Contains("*"))
                            {
                                //tiếp tục đỉnh nối với đỉnh kết thúc có đường đi dài nhất sẽ là đỉnh kết thúc và chèn vào đỉnh đó vào kết quả chính thức
                                temp = ketqua[j, i].Substring(ketqua[j, i].IndexOf(",")+1 , ketqua[j, i].IndexOf(")") - ketqua[j, i].IndexOf(",")-1);
                                ketqua_chinhthuc.Append(int.Parse(temp));
                                string trongso_cua_dainhat = ketqua[j, i].Substring(ketqua[j, i].IndexOf("(")+1, ketqua[j, i].IndexOf(",") - ketqua[j, i].IndexOf("(")-1);
                                trongso += int.Parse(trongso_cua_dainhat);
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            ketqua_chinhthuc.Append(trongso);
            ketqua_chinhthuc.Append(ketqua_chinhthuc.Count());
            Array.Reverse(ketqua_chinhthuc);
            return ketqua_chinhthuc;
        }

    }
}