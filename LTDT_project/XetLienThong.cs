using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_project
{
    class XetLienThong
    {
        int[] visited = new int[1000];
        int nSoMienLienThong;

        public struct DoThi
        {
            public int iSoDinh;
            public int[,] iMaTran;	
        };
		void visit(DoThi g, int i, int index)
		{
			int[] q = new int[100];
			int q_index = 0;
			int q_count = 1;
			q[0]=i;
			visited[i] = index;
			while (q_index < q_count)
			{
				int x = q[q_index];
				q_index++;
				//	cout << g.iMaTran[1][2] << endl;
				//cout << i <<endl;
				for (int j = 0; j < g.iSoDinh; j++)
				{
					if ((visited[j] == 0) && (g.iMaTran[x,j] != int.MinValue))
					{
						//cout << j <<endl;
						q[q_count]=j;
						q_count++;
						visited[j] = index;
					}
				}
			}
		}
		void LienThong(DoThi g)
		{
			for (int i = 0; i < g.iSoDinh; i++) visited[i] = 0;
			nSoMienLienThong = 0;
			for (int i = 0; i < g.iSoDinh; i++)
			{
				if (visited[i] == 0)
				{
					//	cout << i << endl;
					nSoMienLienThong++;
					visit(g, i, nSoMienLienThong);
				}
			}
		}
		public bool xetLienThong(DoThi g)
        {
			LienThong(g);
            if (nSoMienLienThong == 1)
            {
				return true;
            }
			return false;
        }
		public string[] xuatMienLienThong(DoThi g)
		{
			LienThong(g);
			string[] thanhphanlienthong = new string[nSoMienLienThong+1];
			//thanhphanlienthong[0] = nSoMienLienThong.ToString();
			for (int i = 1; i <= nSoMienLienThong; i++)
			{
				//cout << "Mien lien thong thu " << i << ": ";
				thanhphanlienthong[0] = nSoMienLienThong.ToString();
				for (int j = 0; j < g.iSoDinh; j++)
				{
					if (visited[j] == i) thanhphanlienthong[i] += j.ToString() + " ";
				}
			}
			return thanhphanlienthong;
		}
	}
}
