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
    public partial class HeThong : Form
    {
        public HeThong()
        {
            InitializeComponent();
        }
        public bool ok = false;
        public string title = "Bạn có chắc muốn xóa ?";
        private void button1_Click(object sender, EventArgs e)
        {
            ok = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HeThong_Load(object sender, EventArgs e)
        {
            label1.Text = title;
        }
    }
}
