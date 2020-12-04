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
        public string[] Dijsktra_Dao(int[,] matran, int sodinh, int start,int end, string[] tendinh)
        {
            string[,] ketqua = new string[10000, 10000];//kết quả cuối cùng
            bool[] check = new bool[sodinh];//đã có dấu * hay chưa
            int[] cost = new int[sodinh];//tổng trọng số đi mặc định âm vô cực
            string[] path = new string[sodinh];//đỉnh phải đi qua đầu tiên trong Dijsktra
            int k = 0;//mặc định đỉnh a là 0(a=97(ascii)-49=>0) và dòng k=0
                      //Chỉnh dòng 0 đầu tiên
            for (int i = 0; i < sodinh; i++)
            {

                ketqua[k, i] = tendinh[i];
            }
            k++;
            for (int i = 0; i < sodinh; i++)
            {
                check[i] = false;
                cost[i] = negative_INF;
                path[i] = "__";
                ketqua[k, i] = "(oo,__)";
            }
            cost[start] = 0;
            check[start] = true;
            ketqua[k, start] = "0*";
            // Xét từ từ dòng 1 trở đi
            for (int i = 0; i < sodinh; i++)
            {
                //Xét những đỉnh gần kề đỉnh start
                for (int j = 0; j < sodinh; j++)
                {
                    if (cost[j] < cost[start] + matran[start, j] && !check[j] && matran[start, j] != int.MinValue)
                    {
                        cost[j] = cost[start] + matran[start, j];
                        path[j] = tendinh[start];
                    }
                }
                //Bỏ dòng k vào ma trận kết quả
                k++;
                for (int j = 0; j < sodinh; j++)
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
                for (int j = 0; j < sodinh; j++)
                {
                    if (MAX < cost[j] && !check[j])
                    {
                        MAX = cost[j];
                        start = j;
                    }
                }
                check[start] = true;
                ketqua[k, start] += "*";
            }
            for (int i = 0; i < sodinh; i++)
            {
                ketqua[sodinh + 1, i] = "__";
            }
            //Lấy xét từ cột đỉnh kết thúc
            string temp = tendinh[end];
            string[] ketqua_chinhthuc = new string[10000];
            ketqua_chinhthuc.Append(temp);
            //Cho tới khi temp chạm tới đỉnh start là ngừng
            while (temp != tendinh[start])
            {
                for (int i = 0; i < sodinh; i++)
                {
                    //xét cột nào là đỉnh kết thúc
                    if (ketqua[0, i] == temp)
                    {
                        for (int j = 0; j < sodinh; j++)
                        {   //xét trong cột đỉnh kết thúc có đường đi dài nhất
                            if (ketqua[j, i].Contains("*"))
                            {
                                //tiếp tục đỉnh nối với đỉnh kết thúc có đường đi dài nhất sẽ là đỉnh kết thúc và chèn vào đỉnh đó vào kết quả chính thức
                                temp = ketqua[j, i].Substring(ketqua[j, i].IndexOf(","), ketqua[j, i].IndexOf(")") - ketqua[j, i].IndexOf(","));
                                ketqua_chinhthuc.Append(temp);
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            //Sẽ trả về từ đỉnh end -> đỉnh start
            return ketqua_chinhthuc;
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
