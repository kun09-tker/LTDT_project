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
    public partial class Loigiai : Form
    {
        public Loigiai()
        {
            InitializeComponent();
        }
        public string[,] loigiai;
        public int iDinh;
        private void Loigiai_Load(object sender, EventArgs e)
        {
            ListViewItem listViewItem;
            // int[] max = new int[iDinh];
            string[] a = new string[iDinh];
            for (int i = 0; i < iDinh; i++)
            {
                listView1.Columns.Add(loigiai[0, i]);
                listView1.Columns[i].Width = -2;
                listView1.Columns[i].TextAlign = HorizontalAlignment.Center;
            }
            for (int i = 1; i <=iDinh+1; i++)
            {
                //a[0] = tenDinh[i];
                for (int j = 0; j <iDinh; j++)
                {
                    a[j] = loigiai[i, j];
                }
                listViewItem = new ListViewItem(a);
                listView1.Items.Add(listViewItem);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            int headerSize = 0;
            foreach (ColumnHeader header in listView1.Columns)
            {
                headerSize += header.Width;
            }
            listView1.Width = headerSize + 7;
            int height = 22;
            for (int l = 0; l < listView1.Items.Count; l++)
            {
                height += listView1.GetItemRect(l).Height;
            }
            listView1.Height = height + 7;
            this.AutoSize = true;

        }
    }
}
