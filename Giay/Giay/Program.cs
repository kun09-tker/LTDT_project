using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giay
{
    class Program
    {
        public static int MAX = 100;

        public static void Main(string[] args)
        {
            GRAPH mt = new GRAPH();
            nhapMaTranKe("D:/text.txt", ref mt);

            int start;
            while (true)
            {
                start = int.Parse(Console.ReadLine());
                if (start <= mt.sodinh && start > 0)
                {
                    break;
                }
            }

            changeVertexStart(ref mt, start);
            duongDi[] dd = new duongDi[1000]; // ma trận chứa đường đi từ đỉnh x đến xx 
            int ndd = 0;

            for (int i = 1; i <= mt.sodinh; i++)
            {
                for (int j = 1; j <= mt.sodinh; j++)
                {
                    if (mt.a[i, j] != 0)
                    { //nếu trọng số khác 0 thì đẩy vào đường đi 
                        ndd++;
                        dd[ndd].begin = i;
                        dd[ndd].end = j;
                        dd[ndd].w = mt.a[i, j];
                    }
                }
            }

            duongDi[] lineNow = new duongDi[100];
            duongDi[] linePre = new duongDi[100];

            bool[] vertexAvailable = new bool[100]; //kiểm tra xem đỉnh có xét được hay không 
            bool[] vertexAvailable1 = new bool[100]; //kiểm tra xem đỉnh có xét được hay không 
                                                     //kiểm tra xem đỉnh có xét được hay không 
            int nlN = mt.sodinh, nlP = mt.sodinh; // số lượng kí tự 2 mảng 
                                                  // phần tử đầu là 0 còn lại là vô cực 
            for (int i = 1; i <= nlP; i++)
            {
                if (i == 1) linePre[i].w = 0;
                else linePre[i].w = 0;
            }

            lineNow[1].w = 0;

            // đỉnh đầu sử dụng được 
            for (int i = 1; i <= mt.sodinh; i++)
            {
                if (i == 1) vertexAvailable[i] = true;
                else vertexAvailable[i] = false;
            }
            for (int i = 1; i <= mt.sodinh; i++)
            {
                if (i == 1) vertexAvailable1[i] = true;
                else vertexAvailable1[i] = false;
            }

            bool stop = false;
            int k = -1;

            Console.Write("k    ");
            for (int i = 1; i <= mt.sodinh; i++)
            {
                Console.Write(i.ToString() + " ");
            }
            Console.WriteLine();

            while (!stop)
            {
                if (k == mt.sodinh)
                {

                    break;
                }
                k++;

                Console.Write(k.ToString() + "   ");

                //dòng 2
                if (k == 0)
                {
                    Console.Write(" 0   ");
                    for (int i = 2; i <= mt.sodinh; i++)
                    {
                        Console.Write("oo   ");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.Write(" 0   ");
                    for (int i = 2; i <= mt.sodinh; i++)
                    {
                        // Console.WriteLine(i.ToString(),dd.ToString(), vertexAvailable.ToString(), mt.sodinh.ToString(),);

                        duongDi minStreet = layTrongSo(i, dd, ndd, vertexAvailable, mt.sodinh, linePre, i);
                        //trước đó là vô cực và có đường đi đến nó 
                        if (linePre[i].w == 0 && minStreet.w != 0)
                        {
                            // Console.Write("duong di la" +minStreet.begin.ToString() + minStreet.end.ToString()+ minStreet.w.ToString() + "++");
                            vertexAvailable1[i] = true;
                            lineNow[i].w = linePre[minStreet.begin].w + minStreet.w;
                            lineNow[i].begin = minStreet.begin;
                            lineNow[i].end = i;
                            Console.Write(lineNow[i].w.ToString() + "," + lineNow[i].begin.ToString() + "   ");
                            // Console.Write("da chay vo da");
                        }
                        //trước đó là vô cực và không có đường đi đến nó 
                        else if (linePre[i].w == 0 && minStreet.w == 0)
                        {
                            lineNow[i] = linePre[i];
                            Console.Write("oo   ");
                        }
                        //trước đó có tồn tại số và đường đi đến nó nhỏ hơn lúc trước
                        else if (linePre[i].w != 0 && linePre[i].w != 0 && linePre[minStreet.begin].w + minStreet.w <= linePre[i].w)
                        {
                            //Console.WriteLine("da chay ");
                            vertexAvailable1[i] = true;
                            lineNow[i].w = linePre[minStreet.begin].w + minStreet.w;
                            lineNow[i].begin = minStreet.begin;
                            Console.Write(lineNow[i].w.ToString() + "," + lineNow[i].begin.ToString() + "   ");
                        }
                        //trước đó có tồn tại số và đường đi đến nó lớn hơn lúc trước
                        else if (linePre[i].w != 0 && linePre[i].w != 0 && linePre[minStreet.begin].w + minStreet.w > linePre[i].w)
                        {
                            // Console.WriteLine("da chay ");
                            lineNow[i].begin = linePre[i].begin;
                            lineNow[i].end = linePre[i].end;
                            lineNow[i].w = linePre[i].w;
                            Console.Write(lineNow[i].w.ToString() + "," + lineNow[i].begin.ToString() + "   ");
                        }
                        //không tồn tại đỉnh nào có thể đi đến nó
                        else if (minStreet.w == 0)
                        {
                            //lineNow[i] = linePre[i];
                            lineNow[i].begin = linePre[i].begin;
                            lineNow[i].end = linePre[i].end;
                            lineNow[i].w = linePre[i].w;
                            Console.Write("oo   ");
                        }
                    }
                    if (giongNhau(lineNow, linePre, mt.sodinh))
                    {
                        break;
                    }
                    for (int i = 2; i <= mt.sodinh; i++)
                    {
                        //linePre[i] = lineNow[i];
                        linePre[i].begin = lineNow[i].begin;
                        linePre[i].end = lineNow[i].end;
                        linePre[i].w = lineNow[i].w;
                    }
                    for (int i = 1; i <= mt.sodinh; i++)
                    {
                        vertexAvailable[i] = vertexAvailable1[i];
                    }
                     Console.WriteLine("da chay het");
                    Console.ReadKey();
                }
            }
        }

        public struct duongDi
        {
            public int begin;
            public int end;
            public int w;
        }

        public struct GRAPH
        {
            public int sodinh;
            public int[,] a;
        }

        public static void nhapMaTranKe(string duongdan, ref GRAPH dt)
        {
            dt.a = new int[100, 100];

            string[] temp = File.ReadAllLines(duongdan);

            dt.sodinh = int.Parse(temp[0]);


            int dong = 1;

            for (int i = 1; i <= dt.sodinh; i++)
            {
                int cot = 1;
                string[] tempTwo = temp[i].Split(' ');

                foreach (string value in tempTwo)
                {
                    dt.a[dong, cot] = int.Parse(value);
                    cot++;
                }
                dong++;
            }
        }

        public static bool checkAvailable(bool[] vA, int x, int soDinh)
        {
            for (int i = 1; i <= soDinh; i++)
            {
                if (x == i)
                {
                    return vA[i];
                }
            }
            return false;
        }

        public static duongDi layTrongSo(int dinhB, duongDi[] dd, int ndd, bool[] vA, int soLuongDinh, duongDi[] linePre, int z)
        {
            duongDi[] tt = new duongDi[1000];
            int ntt = 0;

            for (int i = 1; i <= ndd; i++)
            {
                if (dd[i].end == dinhB && checkAvailable(vA, dd[i].begin, soLuongDinh))
                {
                    tt[ntt].begin = dd[i].begin;
                    tt[ntt].end = dd[i].end;
                    tt[ntt].w = dd[i].w;
                    ntt++;
                }
            }

            duongDi min = tt[0];

            for (int i = 0; i < ntt; i++)
            {
                for (int j = 0; j < ntt; j++)
                {

                    if (tt[i].w < tt[j].w)
                    {
                        duongDi tmp = tt[j];
                        tt[j] = tt[i];
                        tt[i] = tmp;
                    }

                }
            }

            if (ntt != 0)
            {
                for (int i = 0; i < ntt; i++)
                {
                    tt[i].w += linePre[tt[i].begin].w;
                }
                //sort(tt, tt + ntt);
                for (int i = 0; i < ntt; i++)
                {
                    for (int j = 0; j < ntt; j++)
                    {

                        duongDi tmp = tt[j];
                        tt[j] = tt[i];
                        tt[i] = tmp;
                    }
                }
                tt[0].w -= linePre[tt[0].begin].w;
                return tt[0];
            }


            duongDi ddNULL = new duongDi();
            ddNULL.begin = 0;
            ddNULL.w = 0;
            ddNULL.end = 0;
            return ddNULL;

        }

        public static bool giongNhau(duongDi[] lineNow, duongDi[] linePre, int soDinh)
        {
            for (int i = 0; i < soDinh; i++)
            {
                if (lineNow[i].w != linePre[i].w) return false;
            }
            return true;
        }

        public static void changeVertexStart(ref GRAPH mt, int start)
        {
            duongDi[] dd = new duongDi[10000];

            int ndd = 0;

            for (int i = 1; i <= mt.sodinh; i++)
            {
                for (int j = 1; j <= mt.sodinh; j++)
                {
                    ndd++;
                    dd[ndd].begin = i;
                    dd[ndd].end = j;
                    dd[ndd].w = mt.a[i, j];
                }
            }

            int[] dinh = new int[100];
            dinh[1] = start;
            int tt = 2;
            for (int i = 1; i <= mt.sodinh; i++)
            {
                if (i != start)
                {
                    dinh[tt] = i;
                    tt++;
                }
            }

            for (int i = 1; i <= mt.sodinh; i++)
            {
                for (int j = 1; j <= mt.sodinh; j++)
                {
                    for (int l = 1; l <= ndd; l++)
                    {
                        if (dinh[i] == dd[l].begin && dinh[j] == dd[l].end)
                            mt.a[i, j] = dd[l].w;
                    }
                }

            }

        }
    }
}
