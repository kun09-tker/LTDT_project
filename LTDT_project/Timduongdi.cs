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
    public partial class Timduongdi : Form
    {
        public Timduongdi()
        {
            InitializeComponent();
        }
        public bool Ngan = false;
        public bool Dai = false;
        private void Timduongdi_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton2.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ngan = radioButton1.Checked;
            Dai = radioButton2.Checked;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
