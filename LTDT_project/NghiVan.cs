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
    public partial class NghiVan : Form
    {
        public NghiVan()
        {
            InitializeComponent();
        }
        public bool ok;
        private void button1_Click(object sender, EventArgs e)
        {
            ok = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ok = false;
            this.Close();
        }
    }
}
