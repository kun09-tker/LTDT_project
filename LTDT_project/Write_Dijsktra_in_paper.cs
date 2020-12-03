using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_project
{
    class Write_Dijsktra_in_paper
    {
        const int INF = 100000;
        const int negative_INF = -100000;
        //Trả về ma trận string 2 chiều có chứa bảng viết tay
        public string[,] Dijsktra(int [,]matran,int sodinh,char c='a')
        {
            string[,] ketqua = new string[10000, 10000];//kết quả cuối cùng
            bool[] check=new bool[sodinh];//đã có dấu * hay chưa
            int[] cost=new int[sodinh];//tổng trọng số đi mặc định vô cực
            string[] path = new string[sodinh];//đỉnh phải đi qua đầu tiên trong Dijsktra
            int start = (int)c - 49, k = 0;//mặc định đỉnh a là 0(a=97(ascii)-49=>0) và dòng k=0
            //Chỉnh dòng 0 đầu tiên
            for (int i = 0; i < sodinh - 1; i++)
            {
                check[i] = false;
                cost[i] = INF;
                path[i] = "__";
                ketqua[k, i] = "(oo,__)";
            }
            cost[start] = 0;
            check[start] = true;
            ketqua[k, 0] = "0*";
            // Xét từ từ dòng 1 trở đi
            for(int i = 0; i < sodinh; i++)
            {
                //Xét những đỉnh gần kề đỉnh start
                for(int j = 0; j < sodinh; j++)
                {
                    if (cost[j] > cost[start] + matran[start, j]&&!check[j])
                    {
                        cost[j] = cost[start] + matran[start, j];
                        path[j] = ((char)(start+49)).ToString();
                    }
                }
                //Bỏ dòng k vào ma trận kết quả
                k++;
                for(int j = 0; j < sodinh; j++)
                {
                    if (check[j])
                    {
                        ketqua[k, j] = "__";
                    }
                    else
                    {
                        if (cost[j] != INF)
                        {
                            ketqua[k, j] = $"({cost[j]},{path[j]})";
                        }
                        else
                        {
                            ketqua[k, j] = "(oo,__)";
                        }
                    }
                }
                //Tìm đỉnh nhỏ nhất tiếp theo
                int MIN = INF;
                for (int j = 0; j < sodinh; j++)
                {
                    if (MIN > cost[j] && !check[j])
                    {
                        MIN = cost[j];
                        start = j;
                    }
                }
                check[start] = true;
                ketqua[k, start] += "*";
            }
            for(int i = 0; i < sodinh; i++)
            {
                ketqua[sodinh + 1, i] = "__";
            }
            return ketqua;
        }
        //
        //Dijktra Đảo
        //
        public string[,] Dijsktra_Dao(int[,] matran, int sodinh, char c = 'a')
        {
            string[,] ketqua = new string[10000, 10000];//kết quả cuối cùng
            bool[] check = new bool[sodinh];//đã có dấu * hay chưa
            int[] cost = new int[sodinh];//tổng trọng số đi mặc định âm vô cực
            string[] path = new string[sodinh];//đỉnh phải đi qua đầu tiên trong Dijsktra
            int start = (int)c - 49, k = 0;//mặc định đỉnh a là 0(a=97(ascii)-49=>0) và dòng k=0
            //Chỉnh dòng 0 đầu tiên
            for (int i = 0; i < sodinh - 1; i++)
            {
                check[i] = false;
                cost[i] = negative_INF;
                path[i] = "__";
                ketqua[k, i] = "(oo,__)";
            }
            cost[start] = 0;
            check[start] = true;
            ketqua[k, 0] = "0*";
            // Xét từ từ dòng 1 trở đi
            for (int i = 0; i < sodinh; i++)
            {
                //Xét những đỉnh gần kề đỉnh start
                for (int j = 0; j < sodinh; j++)
                {
                    if (cost[j] < cost[start] + matran[start, j] && !check[j])
                    {
                        cost[j] = cost[start] + matran[start, j];
                        path[j] = ((char)(start + 49)).ToString();
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
                int MAX =negative_INF;
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
            return ketqua;
        }
    }
}
