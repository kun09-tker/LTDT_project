using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.VisualBasic;

namespace LTDT_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public struct LienThong_Paint
        {
            public int index;
            public int r;
            public int b;
            public int g;
        };
        LienThong_Paint[] paint = new LienThong_Paint[100] ;
        Point[] Dinh = new Point[100];
        string[] tenDinh = new string[100];
        int iDinh = 0;
        int SoDinhLienThong = 0;
        int SoMienLT = 0;
        int[,] Matran = new int [100,100];
        bool vertex = false;
        bool edge = false;
        string di="";
        string den="";
        string path="";
        //====================================Bổ trợ=======================
        bool LechMau(int m1,int m2)
        {
            if (Math.Abs(m1 - m2) < 30) return true;
            return false;
        }
        bool KtMau(int r,int b,int g)
        {
            if (LechMau(219, r) == true || LechMau(219, b) == true || LechMau(219, g) == true) return false;
            for (int i = 0; i < SoDinhLienThong; i++)
            {
                if (LechMau(paint[i].r, r)==true || LechMau(paint[i].b,b)==true || LechMau(paint[i].g,g)==true) return false;
            }
            return true;
        }
        void CapNhatDS_dinhdi()
        {
            comboBox1.Items.Clear();
            foreach(string i in listBox1.Items)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.Items.Add("");
        }
        void HienThiMaTran()
        {
            listView2.Items.Clear();
            listView2.Columns.Clear();
            listView2.Columns.Add("*");
            for(int i = 0; i < iDinh; i++)
            {
                listView2.Columns.Add(tenDinh[i]);
               // listView2.Columns[i].Width = 30;
            }
            ListViewItem matran;
            string[] a = new string[iDinh + 1];
            for(int i = 0; i < iDinh; i++)
            {
                a[0] = tenDinh[i];
                for(int j = 0;j < iDinh; j++)
                {
                    if (Matran[i, j] == 0) a[j + 1] = "oo";
                    else a[j + 1] = Matran[i, j].ToString();
                }
                matran = new ListViewItem(a);
                listView2.Items.Add(matran);
            }
        }
        bool khoangcachDinh(int x,int y)
        {
            for(int i = 0; i < iDinh; i++)
            {
                float kc = (float)(Math.Sqrt(Math.Pow(x - Dinh[i].X, 2) + Math.Pow(y - Dinh[i].Y, 2)));
               // MessageBox.Show(kc.ToString());
                if (kc < 30) return true;
            }
            return false;
        }
        void TaoDinhAuto(string s)
        {
            Random rnd = new Random();
            int randomX, randomY;
            while (true){
                randomX = rnd.Next(30, 461);
                randomY = rnd.Next(5, 251);
                if (!khoangcachDinh(randomX, randomY))
                {
                    Dinh[iDinh] = new Point(randomX, randomY);
                    tenDinh[iDinh] = s;
                    iDinh++;
                    break;
                }
            }
            //pictureBox1.Refresh();
        }
        int indexDinh(string s)
        {
            for(int i = 0; i < iDinh; i++)
            {
                if (tenDinh[i] == s)
                {
                    return i;
                }
            }
            return -1;
        }
        void LamMoi()
        {
            listBox1.Items.Clear();
            listView1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox1.Items.Clear();
            for(int i = 0; i < iDinh; i++)
            {
                for(int j = 0; j < iDinh; j++)
                {
                    Matran[i, j] = 0;
                }
            }
            iDinh = SoDinhLienThong = SoMienLT = 0;
            di = den = path = "";
            vertex = edge = false;
            pictureBox1.Refresh();
            HienThiMaTran();
        }
        void DocCanhMaTran(int soDinh)
        {
            listView1.Items.Clear();
            ListViewItem Canh;
            string[] a = new string[3];
            for(int i = 0; i < soDinh; i++)
            {
                for(int j= 0; j < soDinh; j++)
                {
                    if (Matran[i, j] != 0)
                    {
                        a[0] = tenDinh[i];
                        a[1] = tenDinh[j];
                        //MessageBox.Show($"{tenDinh[i]} , {tenDinh[j]}");
                        a[2] = Matran[i, j].ToString();
                        Canh = new ListViewItem(a);
                        listView1.Items.Add(Canh);
                    }
                }
            }
        }

     // ========================================Phần code chính=============
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                
                button3.Enabled = false;
            }
            else
            {
                
                button3.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            button3.Enabled = false;
        }




        private void button3_Click(object sender, EventArgs e)
        {
            if (vertex)
            {
                var confirmation = MessageBox.Show(
                        "Bạn chắc rằng muốn xóa ?", "Hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    );
                if (confirmation == DialogResult.Yes)
                {
                    bool duplicate = false;
                    foreach (var node in listBox1.Items)
                    {
                        if (node.ToString() == textBox1.Text.Substring(6))
                        {
                            int index = indexDinh(textBox1.Text.Substring(6));
                           // MessageBox.Show(index.ToString());
                            // MessageBox.Show(index.ToString());
                            duplicate = true;
                            for (int i = index; i < iDinh - 1; i++)
                            {
                                Dinh[i] = Dinh[i + 1];
                                tenDinh[i] = tenDinh[i + 1];
                            }
                            for(int i=index;i < iDinh; i++)
                            {
                                for(int j = 0; j < iDinh; j++)
                                {
                                    Matran[i, j] = Matran[i + 1, j];
                                    Matran[j, i] = Matran[j, i + 1];
                                }
                            }
                            iDinh--;
                            listBox1.Items.Remove(node);
                            break;
                        }
                    }
                    if (!duplicate)
                    {
                        MessageBox.Show("Đỉnh này không tìm thấy để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Xóa thất bại rồi :( ", "Hệ thống",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (edge)
            {
                if (listView1.SelectedItems != null)
                {
                    var confirmation = MessageBox.Show(
                        "Bạn chắc rằng muốn xóa ?","Hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    );

                    if (confirmation == DialogResult.Yes)
                    {
                        for (int i = 0; i < listView1.Items.Count; i++)
                        {
                            if (listView1.Items[i].Selected)
                            {
                                listView1.Items[i].Remove();
                                Matran[indexDinh(di), indexDinh(den)] = 0;
                                Matran[indexDinh(den), indexDinh(di)] = 0;
                                i--;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Xóa thất bại rồi :( ", "Hệ thống",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            pictureBox1.Refresh();
            DocCanhMaTran(iDinh);
            CapNhatDS_dinhdi();
            HienThiMaTran();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LamMoi();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //path = Path.GetFullPath(openFileDialog1.FileName);
                path = openFileDialog1.FileName;
                string[] line = File.ReadAllLines(path);
                int soDinh = int.Parse(line[0].Trim());
              //  iDinh = soDinh;
                for(int j =0;j < soDinh; j++)
                {
                    listBox1.Items.Add("L"+j.ToString());
                    //comboBox1.Items.Add("L" + j.ToString());
                    TaoDinhAuto("L"+j.ToString());
                    //pictureBox1.Refresh();
                }
                //string[] weight;
                for(int i = 0;i < soDinh; i++)
                {
                    string[] weight = line[i + 1].Split(' ');
                    for(int j = 0;j< soDinh; j++)
                    {
                        Matran[i, j] = int.Parse(weight[j]);
                    }
                }
            }
            XetDoThiHopLe xet = new XetDoThiHopLe();
            XetDoThiHopLe.DoThi doThi = new XetDoThiHopLe.DoThi();
            doThi.iSodinh = iDinh;
            doThi.iMaTran = Matran;
            if (xet.KiemTraDonDoThiDonVoHuong(doThi)){
                CapNhatDS_dinhdi();
                pictureBox1.Refresh();
                //LamMoi();
                DocCanhMaTran(iDinh);
                HienThiMaTran();
            }
            else
            {
                MessageBox.Show("Chỉ đọc được đồ thị đơn vô hướng", "Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            bool duplicate = false;
            bool kc = false;
            string ten = Interaction.InputBox("Nhập tên đỉnh", "Nhập liệu");
            ten = ten.Trim();
            //MessageBox.Show($"{e.X} , {e.Y}");
            if (ten!= "")
            {
                foreach (var node in listBox1.Items)
                {
                    if (node.ToString() == ten)
                    {
                        duplicate = true;
                    }
                    if (khoangcachDinh(e.X, e.Y)) kc = true;
                }
                if (!duplicate)
                {
                    if (!kc)
                    {
                        tenDinh[iDinh] = ten;
                        Dinh[iDinh] = new Point(e.X, e.Y);
                        listBox1.Items.Add(ten);
                        iDinh++;
                        for(int k = 0; k < iDinh; k++)
                        {
                            Matran[iDinh, k] = Matran[k, iDinh] = 0;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Quá gần !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
                else
                {
                    MessageBox.Show("Đỉnh này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên đỉnh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            CapNhatDS_dinhdi();
            pictureBox1.Refresh();
            HienThiMaTran();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g;
            SolidBrush maunenDinh = new SolidBrush(Color.Aqua);
            SolidBrush mauchuDinh = new SolidBrush(Color.Black);
            SolidBrush khoangtrong = new SolidBrush(Color.FromArgb(219, 219, 219));
            Pen line = new Pen(mauchuDinh);
            g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            iTextSharp.text.Font f1 = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.TIMES_ROMAN.ToString(), 25, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE);
            string exeFile = (new System.Uri(Assembly.GetEntryAssembly().CodeBase)).AbsolutePath;
            string exeDir = Path.GetDirectoryName(exeFile);
            string fullPath0 = Path.Combine(exeDir, @"VietFontsWeb1_ttf\vuTimesBold.ttf");
            System.Drawing.Font f2 = new System.Drawing.Font(fullPath0, 8);
            //MessageBox.Show(iDinh.ToString());
            if (SoDinhLienThong == 0)
            {
                for (int i = 0; i < iDinh; i++)
                {
                    g.FillEllipse(maunenDinh, Dinh[i].X - 5, Dinh[i].Y - 5, 10, 10);
                    g.DrawString(tenDinh[i], f2, mauchuDinh, Dinh[i].X - 5, Dinh[i].Y - 5);
                    //  MessageBox.Show($"{tenDinh[i]} , {Dinh[i].X} , {Dinh[i].Y}");
                }
                for (int i = 0; i < iDinh; i++)
                {
                    for (int j = 0; j <= i; j++)
                    {
                        if (Matran[i, j] != 0)
                        {
                            g.DrawLine(line, Dinh[i], Dinh[j]);
                            g.FillEllipse(khoangtrong, (Dinh[i].X + Dinh[j].X) / 2 - 5, (Dinh[i].Y + Dinh[j].Y) / 2 - 5, 10, 10);
                            g.DrawString(Matran[i, j].ToString(), f2, mauchuDinh, (Dinh[i].X + Dinh[j].X) / 2 - 5, (Dinh[i].Y + Dinh[j].Y) / 2 - 5);
                            //  g.DrawLine(line,(Dinh[i].X + Dinh[j].X) / 2 + 5, (Dinh[i].Y + Dinh[j].Y) / 2 + 5,Dinh[j].X,Dinh[j].Y);
                        }
                    }
                }
            }
            if (SoDinhLienThong > 0)
            {
               

                for (int i = 0; i < SoDinhLienThong; i++)
                {
                    SolidBrush maulienthong = new SolidBrush(Color.FromArgb(paint[i].r, paint[i].b, paint[i].g));
                   
                    g.FillEllipse(maulienthong, Dinh[paint[i].index].X - 5, Dinh[paint[i].index].Y - 5, 10, 10);
                    g.DrawString(tenDinh[paint[i].index], f2, mauchuDinh, Dinh[paint[i].index].X - 5, Dinh[paint[i].index].Y - 5);
                    for (int j = 0; j < SoDinhLienThong; j++)
                    {
                        if (paint[i].r == paint[j].r && paint[i].b == paint[j].b && paint[i].g == paint[j].g&&Matran[paint[i].index, paint[j].index] !=0)
                        {
                            maulienthong = new SolidBrush(Color.FromArgb(paint[i].r, paint[i].b, paint[i].g));
                            Pen paintLienThong = new Pen(maulienthong);
                            g.DrawLine(paintLienThong, Dinh[paint[i].index], Dinh[paint[j].index]);
                            g.FillEllipse(khoangtrong, (Dinh[paint[i].index].X + Dinh[paint[j].index].X) / 2 - 5, (Dinh[paint[i].index].Y + Dinh[paint[j].index].Y) / 2 - 5, 10, 10);
                            g.DrawString(Matran[paint[i].index, paint[j].index].ToString(), f2, maulienthong, (Dinh[paint[i].index].X + Dinh[paint[j].index].X) / 2 - 5, (Dinh[paint[i].index].Y + Dinh[paint[j].index].Y) / 2 - 5);
                        }
                    }
                }
            }
           // MessageBox.Show(iDinh.ToString());
        }

        private void pictureBox1_ParentChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            edge = false;
            vertex = true;
            try
            {
                textBox1.Text = "Đỉnh: " + listBox1.SelectedItem.ToString();
            }
            catch
            {
                textBox1.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            edge = true;
            vertex = false;
            try
            {
                for (int i = 0; i < listView1.Items.Count;i++) {
                    if (listView1.Items[i].Selected)
                    {
                         di = listView1.Items[i].Text;
                         den = listView1.Items[i].SubItems[1].Text;
                        string trongso = listView1.Items[i].SubItems[2].Text;
                        textBox1.Text = $"Cạnh : {di} -> {den} ({trongso})";
                    }
                }
            }
            catch
            {
                textBox1.Text = "";
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < iDinh; i++)
            {
                Random rnd = new Random();
                int randomX, randomY;
                while (true)
                {
                    randomX = rnd.Next(30, 461);
                    randomY = rnd.Next(5, 251);
                    if (!khoangcachDinh(randomX, randomY))
                    {
                        Dinh[i].X = randomX;
                        Dinh[i].Y = randomY;
                        //tenDinh[iDinh] = s;
                        //iDinh++;
                        break;
                    }
                }
            }
            pictureBox1.Refresh();
            DocCanhMaTran(iDinh);
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            foreach(string i in comboBox1.Items)
            {
                if (i != comboBox1.Text)
                {
                    comboBox2.Items.Add(i);
                }
            }
            comboBox2.Items.Add("");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox2.Text != ""&&numericUpDown1.Value > numericUpDown1.Minimum)
            {
                int x = indexDinh(comboBox1.Text);
                int y = indexDinh(comboBox2.Text);
                Matran[x, y] = Matran[y,x] = int.Parse(numericUpDown1.Value.ToString());
            }
            pictureBox1.Refresh();
            DocCanhMaTran(iDinh);
            HienThiMaTran();
        }

        private void lienthong_kt_Click(object sender, EventArgs e)
        {
            string[] tp_lienThong = new string[100];
            XetLienThong x = new XetLienThong();
            XetLienThong.DoThi doThi = new XetLienThong.DoThi();
            doThi.iMaTran = Matran;
            doThi.iSoDinh = iDinh;
            tp_lienThong = x.xuatMienLienThong(doThi);
            //MessageBox.Show(string.Join("\n",tp_lienThong));
            SoMienLT = int.Parse(tp_lienThong[0]);
            for (int i = 1;i <= SoMienLT; i++)
            {
                Random rnd = new Random();
                while (true)
                {
                    int r = rnd.Next(0, 256);
                    int b = rnd.Next(0, 256);
                    int  g = rnd.Next(0, 256);
                    if (r != b && r != g && g != r && KtMau(r, b, g) == true)
                    {
                        foreach (char dinhlienthong in tp_lienThong[i])
                        {
                            if (dinhlienthong != ' ')
                            {
                                paint[SoDinhLienThong].index = dinhlienthong - '0';
                                //MessageBox.Show($"{(dinhlienthong - '0').ToString()}");
                                paint[SoDinhLienThong].r = r;
                                paint[SoDinhLienThong].b = b;
                                paint[SoDinhLienThong].g = g;
                                SoDinhLienThong++;
                            }

                        }
                        break;
                    }
                    else continue;
                }
            }
            pictureBox1.Refresh();
            SoDinhLienThong = 0;
        }
    }
}
