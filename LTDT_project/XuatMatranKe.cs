using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTDT_project
{
    public partial class XuatMatranKe : Form
    {
        public XuatMatranKe()
        {
            InitializeComponent();
        }
        public int sodinh;
        public int[,] mt;
        public string[] tenDinh;
        private void XuatMatranKe_Load(object sender, EventArgs e)
        {
            int max = 0;
            for(int i = 0; i < sodinh; i++)
            {
                if (tenDinh[i].Length > max) max = tenDinh[i].Length;
            }
          //  max = (max + 2) * 9;
            string tmp = "  "; 
            for(int i = 0; i < max; i++)
            {
                tmp += " ";
            } 
            Size size = new Size((max)*(sodinh)*9,(max)*(sodinh)*9);
            /// listView1.Size = size;
            //this.Size = sizeForm;
            listView1.Size = size;
            this.AutoSize = true;
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("*"+tmp);
            for (int i = 0; i < sodinh; i++)
            {
                listView1.Columns.Add(tenDinh[i]);
                listView1.Columns[i].Width = -2;
            }
            ListViewItem matran;
            string[] a = new string[sodinh + 1];
            for (int i = 0; i < sodinh; i++)
            {
                a[0] = tenDinh[i];
                for (int j = 0; j < sodinh; j++)
                {
                    if (mt[i, j] == int.MinValue) a[j + 1] = "∞";
                    else a[j + 1] = mt[i, j].ToString();
                }
                matran = new ListViewItem(a);
                listView1.Items.Add(matran);
            }
            listView1.ShowItemToolTips = true;
        }
    }
}
