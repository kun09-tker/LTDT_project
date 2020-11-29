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
    public partial class Cập_nhật_trọng_số : Form
    {
        public Cập_nhật_trọng_số()
        {
            InitializeComponent();
        }
        public int trongso = -1;
        private void Cập_nhật_trọng_số_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            trongso = int.Parse(numericUpDown1.Value.ToString());
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
