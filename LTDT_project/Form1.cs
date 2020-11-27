using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
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
        Color color = Color.Black;
        string[] tenDinh = new string[100];
        int iDinh = 0;
        int SoDinhLienThong = 0;
        int tmp = 0;
        int imin = int.MinValue;
        int[,] Matran = new int [100,100];
        bool[,] luuVetcanh = new bool[100, 100];
        bool[] luuVetdinh = new bool[100];
        bool vertex = false;
        bool edge = false;
        bool dij = false;
        bool OnlyPath = false;
        string di="";
        string den="";
        string path="";
        //====================================Bổ trợ=======================
        void DinhDangLaiMaTran()
        {
            for(int i = 0; i < iDinh; i++)
            {
                Matran[i, i] = int.MinValue;
            }
        }
        bool LechMau(int m1,int m2)
        {
            if (Math.Abs(m1 - m2) < 50) return true;
            return false;
        }
        bool KtMau(int r,int b,int g)
        {
            int dem = 0;
            if (r+g+b <=370)
            {
                if (SoDinhLienThong == 0) return true;
                for (int i = 0; i < SoDinhLienThong; i++)
                {
                    if (LechMau(paint[i].r, r) == false || LechMau(paint[i].b, b) == false || LechMau(paint[i].g, g) == false) dem++;
                }
                return dem == SoDinhLienThong;
            }
            return false;
        }
        void CapNhatDS_dinhdi()
        {
            listBox1.Items.Clear();
            comboBox1.Items.Clear();
            Dijsktra_di.Items.Clear();
            for(int i = 0;i<iDinh;i++)
            {
                listBox1.Items.Add(tenDinh[i]);
                comboBox1.Items.Add(tenDinh[i]);
                Dijsktra_di.Items.Add(tenDinh[i]);
            }
            comboBox1.Items.Add("");
            Dijsktra_di.Items.Add("");
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
                randomX = rnd.Next(12, 885);
                randomY = rnd.Next(12, 400);
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
            Dijsktra_di.Items.Clear();
            Dijstra_den.Items.Clear();
            richTextBox1.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
            for(int i = 0; i < iDinh; i++)
            {
                luuVetdinh[i] = false;
                for(int j = 0; j < iDinh; j++)
                {
                    Matran[i, j] = int.MinValue;
                    luuVetcanh[i, j] = false;
                }
            }
            iDinh = SoDinhLienThong = tmp = 0;
            di = den = path = "";
            vertex = edge = dij = OnlyPath = false;
            pictureBox1.Refresh();
             
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
                    if (Matran[i, j] != int.MinValue)
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
            radioButton1.Checked = true;
            radioButton2.Checked = false;
           // button3.Enabled = false;
            //MessageBox.Show(int.MinValue.ToString());
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
              
                //string[] weight;
                for(int i = 0;i < soDinh; i++)
                {
                    string[] weight = line[i + 1].Split(' ');
                    for(int j = 0;j< soDinh; j++)
                    {
                        if (weight[j] == "∞") Matran[i, j] = int.MinValue;
                       // else if (int.Parse(weight[j]) == 0) Matran[i, j] = int.MinValue;
                        else Matran[i, j] = int.Parse(weight[j]);
                    }
                }
                for (int j = 0; j < soDinh; j++)
                {
                    string ten;
                    try
                    {
                        ten = line[soDinh + 1 + j].Trim();
                    }
                    catch
                    {
                        ten = $"L{j}";
                    }
                    listBox1.Items.Add(ten);
                    //comboBox1.Items.Add("L" + j.ToString());
                    TaoDinhAuto(ten);
                    //pictureBox1.Refresh();
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
                        MessageBox.Show($"{e.X} {e.Y}");
                        Dinh[iDinh] = new Point(e.X, e.Y);
                        listBox1.Items.Add(ten);
                        for(int k = 0; k <=iDinh; k++)
                        {
                            Matran[iDinh, k] = Matran[k, iDinh] = imin;
                        }
                        iDinh++;
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
            DinhDangLaiMaTran();
            CapNhatDS_dinhdi();
            pictureBox1.Refresh();
             
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g;
            SolidBrush maunenDinh = new SolidBrush(Color.BurlyWood);
            SolidBrush mauchuDinh = new SolidBrush(Color.Black);
            SolidBrush khoangtrong = new SolidBrush(Color.FromArgb(224, 224, 224));
            Pen line = new Pen(maunenDinh);
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
                        if (Matran[i, j] != int.MinValue)
                        {
                            g.DrawLine(line, Dinh[i], Dinh[j]);
                            g.FillEllipse(khoangtrong, (Dinh[i].X + Dinh[j].X) / 2 - 5, (Dinh[i].Y + Dinh[j].Y) / 2 - 5, 10, 10);
                            g.DrawString(Matran[i, j].ToString(), f2, mauchuDinh, (Dinh[i].X + Dinh[j].X) / 2 - 5, (Dinh[i].Y + Dinh[j].Y) / 2 - 5);
                            //  g.DrawLine(line,(Dinh[i].X + Dinh[j].X) / 2 + 5, (Dinh[i].Y + Dinh[j].Y) / 2 + 5,Dinh[j].X,Dinh[j].Y);
                        }
                    }
                }
            }
            if (SoDinhLienThong > 0&&dij==false)
            {
               

                for (int i = 0; i < SoDinhLienThong; i++)
                {
                    SolidBrush maulienthong = new SolidBrush(Color.FromArgb(paint[i].r, paint[i].b, paint[i].g));
                   
                    g.FillEllipse(maulienthong, Dinh[paint[i].index].X - 5, Dinh[paint[i].index].Y - 5, 10, 10);
                    g.DrawString(tenDinh[paint[i].index], f2, mauchuDinh, Dinh[paint[i].index].X - 5, Dinh[paint[i].index].Y - 5);
                    for (int j = 0; j < SoDinhLienThong; j++)
                    {
                        if (paint[i].r == paint[j].r && paint[i].b == paint[j].b && paint[i].g == paint[j].g&&Matran[paint[i].index, paint[j].index] !=int.MinValue)
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
            if (dij)
            {
                for (int i = 0; i < SoDinhLienThong - 1; i++)
                {
                    SolidBrush maulienthong = new SolidBrush(Color.FromArgb(paint[i].r, paint[i].b, paint[i].g));
                    color = Color.FromArgb(paint[i].r, paint[i].b, paint[i].g);
                    richTextBox1.SelectionColor = Color.FromArgb(paint[i].r, paint[i].b, paint[i].g);
                    richTextBox1.SelectedText = tenDinh[paint[i].index];
                    richTextBox1.SelectionColor = color;
                    richTextBox1.SelectedText = " -> ";
                    luuVetdinh[paint[i].index] = true;
                    luuVetcanh[paint[i].index, paint[i + 1].index] = luuVetcanh[paint[i + 1].index, paint[i].index] = true;
                    g.FillEllipse(maulienthong, Dinh[paint[i].index].X - 5, Dinh[paint[i].index].Y - 5, 10, 10);
                    g.DrawString(tenDinh[paint[i].index], f2, mauchuDinh, Dinh[paint[i].index].X - 5, Dinh[paint[i].index].Y - 5);
                    Pen paintLienThong = new Pen(maulienthong);
                    g.DrawLine(paintLienThong, Dinh[paint[i].index], Dinh[paint[i + 1].index]);
                    g.FillEllipse(khoangtrong, (Dinh[paint[i].index].X + Dinh[paint[i + 1].index].X) / 2 - 5, (Dinh[paint[i].index].Y + Dinh[paint[i + 1].index].Y) / 2 - 5, 10, 10);
                    g.DrawString(Matran[paint[i].index, paint[i + 1].index].ToString(), f2, maulienthong, (Dinh[paint[i].index].X + Dinh[paint[i + 1].index].X) / 2 - 5, (Dinh[paint[i].index].Y + Dinh[paint[i + 1].index].Y) / 2 - 5);
                }
                luuVetdinh[paint[SoDinhLienThong - 1].index] = true;
                richTextBox1.SelectionColor = Color.FromArgb(paint[SoDinhLienThong - 1].r, paint[SoDinhLienThong - 1].b, paint[SoDinhLienThong - 1].g);
                richTextBox1.SelectedText = tenDinh[paint[SoDinhLienThong - 1].index];
                SolidBrush mau = new SolidBrush(Color.FromArgb(paint[SoDinhLienThong - 1].r, paint[SoDinhLienThong - 1].b, paint[SoDinhLienThong - 1].g));
                g.FillEllipse(mau, Dinh[paint[SoDinhLienThong - 1].index].X - 5, Dinh[paint[SoDinhLienThong - 1].index].Y - 5, 10, 10);
                g.DrawString(tenDinh[paint[SoDinhLienThong - 1].index], f2, mauchuDinh, Dinh[paint[SoDinhLienThong - 1].index].X - 5, Dinh[paint[SoDinhLienThong - 1].index].Y - 5);
                if (OnlyPath == false)
                {
                    for (int i = 0; i < iDinh; i++)
                    {
                        if (!luuVetdinh[i])
                        {
                            g.FillEllipse(maunenDinh, Dinh[i].X - 5, Dinh[i].Y - 5, 10, 10);
                            g.DrawString(tenDinh[i], f2, mauchuDinh, Dinh[i].X - 5, Dinh[i].Y - 5);
                            //  MessageBox.Show($"{tenDinh[i]} , {Dinh[i].X} , {Dinh[i].Y}");
                        }
                    }
                    for (int i = 0; i < iDinh; i++)
                    {
                        for (int j = 0; j <= i; j++)
                        {
                            if (Matran[i, j] != int.MinValue && !luuVetcanh[i, j])
                            {
                                g.DrawLine(line, Dinh[i], Dinh[j]);
                                g.FillEllipse(khoangtrong, (Dinh[i].X + Dinh[j].X) / 2 - 5, (Dinh[i].Y + Dinh[j].Y) / 2 - 5, 10, 10);
                                g.DrawString(Matran[i, j].ToString(), f2, mauchuDinh, (Dinh[i].X + Dinh[j].X) / 2 - 5, (Dinh[i].Y + Dinh[j].Y) / 2 - 5);
                                //  g.DrawLine(line,(Dinh[i].X + Dinh[j].X) / 2 + 5, (Dinh[i].Y + Dinh[j].Y) / 2 + 5,Dinh[j].X,Dinh[j].Y);
                            }
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
                    randomX = rnd.Next(12,885 );
                    randomY = rnd.Next(12, 400);
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
             
        }

        private void lienthong_kt_Click(object sender, EventArgs e)
        {
            string[] tp_lienThong = new string[100];
            XetLienThong x = new XetLienThong();
            XetLienThong.DoThi doThi = new XetLienThong.DoThi();
            doThi.iMaTran = Matran;
            doThi.iSoDinh = iDinh;
            tp_lienThong = x.xuatMienLienThong(doThi);
            //MessageBox.Show(tenDinh[10].ToString());
            int SoMienLT = int.Parse(tp_lienThong[0]);
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
                        string[] tachtp = tp_lienThong[i].Split(' ');
                       // MessageBox.Show(string.Join("\n",tachtp));
                        foreach (string dinhlienthong in tachtp)
                        {
                            if (dinhlienthong != "")
                            {
                                // int a = int.Parse(dinhlienthong);
                                //MessageBox.Show(dinhlienthong);
                                paint[SoDinhLienThong].index = int.Parse(dinhlienthong);
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

        private void thoi_kiemlienthong_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void Dijsktra_di_SelectedValueChanged(object sender, EventArgs e)
        {
            Dijstra_den.Items.Clear();
            foreach (string i in Dijsktra_di.Items)
            {
                if (i != Dijsktra_di.Text)
                {
                    Dijstra_den.Items.Add(i);
                }
            }
            Dijstra_den.Items.Add("");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            textBox2.Text = "";
            if (Dijstra_den.Text != "" && Dijsktra_di.Text != "")
            {
                for (int i = 0; i < iDinh; i++)
                {
                    luuVetdinh[i] = false;
                    for (int j = 0; j < iDinh; j++)
                    {
                        luuVetcanh[i, j] = false;
                    }
                }
                int[] duongdi;
                if (radioButton1.Checked)
                {
                    Dijsktra.DuLieu x = new Dijsktra.DuLieu();
                    Dijsktra dijsktra = new Dijsktra();
                    x.sodinh = iDinh;
                    x.mt = Matran;
                    x.di = indexDinh(Dijsktra_di.Text) + 1;
                    x.den = indexDinh(Dijstra_den.Text) + 1;
                    // MessageBox.Show(x.den.ToString());
                    duongdi = dijsktra.TimDuong(x);
                }
                else
                {
                    DijsktraDao.DuLieu x = new DijsktraDao.DuLieu();
                    DijsktraDao dijsktraDao = new DijsktraDao();
                    x.sodinh = iDinh;
                    x.mt = Matran;
                    x.di = indexDinh(Dijsktra_di.Text) + 1;
                    x.den = indexDinh(Dijstra_den.Text) + 1;
                    // MessageBox.Show(x.den.ToString());
                    duongdi = dijsktraDao.TimDuong(x);
                    Array.Reverse(duongdi, 2, duongdi[0] - 2);
                }
                int n = duongdi[0];
                // MessageBox.Show(string.Join("\n",duongdi));
                if (n > 0)
                {
                    dij = true;
                    SoDinhLienThong = 0;
                    for (int i = n - 1; i > 1; i--)
                    {
                        //   MessageBox.Show((duongdi[i] - 1).ToString());
                        Random rnd = new Random();
                        while (true)
                        {
                            int r = rnd.Next(0, 256);
                            int b = rnd.Next(0, 256);
                            int g = rnd.Next(0, 256);
                            if (r != b && r != g && g != r && KtMau(r, b, g) == true)
                            {
                                paint[SoDinhLienThong].index = duongdi[i] - 1;
                                // MessageBox.Show($"{(duongdi[i] - 1)}");
                                paint[SoDinhLienThong].r = r;
                                paint[SoDinhLienThong].b = b;
                                paint[SoDinhLienThong].g = g;
                                SoDinhLienThong++;
                                break;
                            }
                            // else continue;
                        }
                    }
                    tmp = SoDinhLienThong;
                    pictureBox1.Refresh();
                    textBox2.Text = duongdi[1].ToString();
                    dij = false;
                    SoDinhLienThong = 0;
                }
                else MessageBox.Show("Không tìm thấy đường đi", "Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else pictureBox1.Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (tmp > 0)
            {
                richTextBox1.Text = "";
                SoDinhLienThong = tmp;
                dij = OnlyPath = true;
                pictureBox1.Refresh();
                dij = OnlyPath = false;
                SoDinhLienThong = 0;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string save = iDinh.ToString() + "\n";
                    for(int i = 0; i < iDinh; i++)
                    {
                        for(int j = 0; j < iDinh; j++)
                        {
                            if (Matran[i, j] == int.MinValue) save += "∞ ";
                            else save += Matran[i, j].ToString() + " ";
                        }
                        save += "\n";
                    }
                    for(int i = 0; i < iDinh; i++)
                    {
                        save+=tenDinh[i];
                        if (i < iDinh - 1) save += "\n";
                    }
                    StreamWriter stream = new StreamWriter(saveFileDialog1.FileName);
                    stream.WriteLine(save);
                    stream.Close();
                }
            }
        }



        private void button3_Click_1(object sender, EventArgs e)
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
                            for (int i = index; i < iDinh; i++)
                            {
                                for (int j = 0; j < iDinh; j++)
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
                        "Bạn chắc rằng muốn xóa ?", "Hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    );

                    if (confirmation == DialogResult.Yes)
                    {
                        for (int i = 0; i < listView1.Items.Count; i++)
                        {
                            if (listView1.Items[i].Selected)
                            {
                                listView1.Items[i].Remove();
                                Matran[indexDinh(di), indexDinh(den)] = int.MinValue;
                                Matran[indexDinh(den), indexDinh(di)] = int.MinValue;
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
            DinhDangLaiMaTran();
            pictureBox1.Refresh();
            DocCanhMaTran(iDinh);
            CapNhatDS_dinhdi();
             
        }

        private void DoitenDinh_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text[0] == 'Đ')
                {
                    string rename = Interaction.InputBox("Nhập tên mới", "Đổi tên đỉnh");
                    string name = textBox1.Text.Substring(6);
                    tenDinh[indexDinh(name)] = rename;
                    CapNhatDS_dinhdi();
                     
                    DocCanhMaTran(iDinh);
                    pictureBox1.Refresh();
                }
                else
                {
                    MessageBox.Show("Chỉ được đổi tên của đối tượng đỉnh", "Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            XuatMatranKe matranKe = new XuatMatranKe();
            matranKe.mt = Matran;
            matranKe.sodinh = iDinh;
            matranKe.tenDinh = tenDinh;
            //MessageBox.Show(tenDinh[2].Length.ToString());
            matranKe.ShowDialog();
        }
    }
}
