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
    public partial class DFS_BFS : Form
    {
        public DFS_BFS()
        {
            InitializeComponent();
        }
        public bool DFS = false;
        public bool BFS = false;
        private void button1_Click(object sender, EventArgs e)
        {
            DFS = radioButton1.Checked;
            BFS = radioButton2.Checked;
            this.Close();
        }

        private void DFS_BFS_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton2.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
