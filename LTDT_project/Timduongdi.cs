﻿using System;
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
        public bool cancel = false;
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
            Ngan = false;
            Dai = false;
            this.Close();
           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
