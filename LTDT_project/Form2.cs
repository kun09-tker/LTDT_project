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
        public int Sodinh = 0;
        public int[,] mt = new int[100,100];
        int h = 20, m = 3;
        int[] tmp = new int[10000];
        int chisotmp = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            chisotmp = 0;
            int x = 3, y = 3;
            panel1.Controls.Clear();
            Sodinh = trackBar1.Value;
            for(int i = 0; i <= Sodinh; i++)
            {
               for(int j =0;j <= Sodinh;j++)
                {
                   // tmp[i + j] = int.MinValue;
                    mt[i, j] = int.MinValue;
                    TextBox a = new TextBox();
                    a.Multiline = true;
                    a.Width = 50; 
                    a.Height = h;
                    if (i == 0 && j == 0)
                    {
                        a.ReadOnly = true;
                        a.Text = $"*";
                    }
                    else
                    {
                        if (i == 0)
                        {
                            a.ReadOnly = true;
                            a.Text = $"N{j-1}";
                        }
                        if (j == 0)
                        {
                            a.ReadOnly = true;
                            a.Text = $"N{i-1}";
                        }
                        if (i == j)
                        {
                            a.ReadOnly = true;
                            a.Text = $"-1";
                        }
                        if (i> j && j !=0)
                        {
                            a.ReadOnly = true;
                            a.Text = "???";
                        }
                    }
                    a.Location = new Point(x, y);
                    panel1.Controls.Add(a);
                    x += m + 50;
                }
                x = 3;
                y += m + h;
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            sodinh.Text = "1";
        }
        Random rnd = new Random();
        private void button2_Click(object sender, EventArgs e)
        { 
            foreach(TextBox textBox in panel1.Controls)
            {
                if (!textBox.ReadOnly)
                {
                    int trongso = rnd.Next(-50, 51);
                    textBox.Text = trongso.ToString();
                   
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            bool kt = true;
            foreach(TextBox textBox in panel1.Controls)
            {
                if (!textBox.ReadOnly)
                {
                    try
                    {
                        int trongso = int.Parse(textBox.Text);
                        tmp[chisotmp++] = trongso;
                    }
                    catch
                    {
                        kt = false;
                        System.Media.SystemSounds.Hand.Play();
                        PictureBox pictureE = new PictureBox();
                        pictureE.Image = global::LTDT_project.Properties.Resources.Error;
                        pictureE.Location = new Point(12, 12);
                        pictureE.SizeMode = PictureBoxSizeMode.AutoSize;
                        Label labelLoi = new Label();
                        labelLoi.Font = new System.Drawing.Font("Palatino Linotype", 10, FontStyle.Bold);
                        labelLoi.Text = "Ma trận không phù hợp";
                        labelLoi.Location = new Point(100, 50);
                        labelLoi.AutoSize = true;
                        Form Loi = new Form();
                        Loi.Text = "Lỗi nhập liệu";
                        Loi.Icon = System.Drawing.SystemIcons.Error;
                        Loi.Controls.Add(pictureE);
                        Loi.Controls.Add(labelLoi);
                        Loi.BackColor = Color.White;
                        Loi.Height = 180;
                        Loi.AutoSize = true;
                        Loi.StartPosition = FormStartPosition.CenterScreen;
                        Loi.MinimizeBox = false;
                        Loi.MaximizeBox = false;
                        Button OkLoi = new Button();
                        OkLoi.Text = "OK";
                        OkLoi.TextAlign = ContentAlignment.MiddleCenter;
                        OkLoi.Location = new Point(Loi.Width - (Loi.Width / 3), 100);
                        Loi.Controls.Add(OkLoi);
                        Loi.Show();
                        OkLoi.Click += (sen, args) =>
                        {
                            Loi.Close();
                        };
                        break;
                    }
                }
            }
            if (kt)
            {
                chisotmp = 0;
                for(int i = 0; i < Sodinh-1; i++)
                {
                    for(int j = i+1; j < Sodinh; j++)
                    {
                        if (i != j)
                        {
                            if (tmp[chisotmp] < 0) mt[i, j] = mt[j, i] = int.MinValue;
                            else mt[i, j] = mt[j, i] = tmp[chisotmp];
                            chisotmp++;
                        }
                    }
                }
                chisotmp = 0;
                this.Close();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            sodinh.Text = trackBar1.Value.ToString();
        }

     
    }
}
