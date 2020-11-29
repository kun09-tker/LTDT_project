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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sodinh = int.Parse(numericUpDown1.Value.ToString());
           // tableLayoutPanel1.RowCount = sodinh;
            //tableLayoutPanel1.ColumnCount = sodinh;
            for(int i = 0; i < sodinh; i++)
            {
                //tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                //tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Contact No" }, 2, 0);
                //tableLayoutPanel1.
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
