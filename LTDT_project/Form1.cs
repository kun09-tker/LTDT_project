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
        LienThong_Paint[] paint = new LienThong_Paint[100];
        Point[] Dinh = new Point[100];
        Color color = Color.Black;
        string[] tenDinh = new string[100];
        string[] Duyet = new string[100];
        public int iDinh = 0;
        int SoDinhLienThong = 0;
        int tmp = 0;
        int demNode = 0;
        int demTim = 0;
        int imin = int.MinValue;
        //  int start = -1;
        int x = -1, y = -1;
        public int[,] Matran = new int[100, 100];
        bool[,] luuVetcanh = new bool[100, 100];
        bool[] luuVetdinh = new bool[100];
        bool[] luuVetdinh1 = new bool[100];
        //  bool[] luuVetdinh2 = new bool[100];
        bool vertex = false;
        bool edge = false;
        bool dij = false;
        bool OnlyPath = false;
        bool am, nghivan = false;
        string di = "";
        string den = "";
        string path = "";
        //====================================Bổ trợ=======================
        int XetToaDo(int x, int y)
        {
            for (int i = 0; i < iDinh; i++)
            {
                if (Math.Abs(Dinh[i].X - x) < 8 && Math.Abs(Dinh[i].Y - y) < 8) return i;
            }
            return -1;
        }
        void TaoDoDuongDi(int x, int y)
        {
            if (XetToaDo(x, y) != -1)
            {
                demNode++;
                luuVetdinh1[XetToaDo(x, y)] = true;
                if (demNode == 1)
                {
                    vt1.Location = new Point(Dinh[XetToaDo(x, y)].X, Dinh[XetToaDo(x, y)].Y);
                    vt1.Visible = true;
                }
                else if (demNode == 2)
                {
                    vt2.Location = new Point(Dinh[XetToaDo(x, y)].X, Dinh[XetToaDo(x, y)].Y);
                    vt2.Visible = true;
                }
            }
            // errorProvider1.SetError(, "");

        }
        void Mess(string title, string text, System.Drawing.Image anh, Icon icon)
        {
            PictureBox pictureE = new PictureBox();
            pictureE.Image = anh;
            pictureE.Location = new Point(12, 12);
            pictureE.SizeMode = PictureBoxSizeMode.AutoSize;
            Label labelLoi = new Label();
            labelLoi.Font = new System.Drawing.Font("Palatino Linotype", 10, FontStyle.Bold);
            labelLoi.Text = title;
            labelLoi.Location = new Point(100, 50);
            labelLoi.AutoSize = true;
            Form Loi = new Form();
            Loi.Text = text;
            Loi.Icon = icon;
            Loi.Controls.Add(pictureE);
            Loi.Controls.Add(labelLoi);
            Loi.BackColor = Color.White;
            Loi.Height = 180;
            Loi.AutoSize = true;
            Loi.StartPosition = FormStartPosition.CenterScreen;
            Loi.MinimizeBox = false;
            Loi.MaximizeBox = false;
            //Loi.FormBorderStyle = FormBorderStyle.None;
            Loi.ShowDialog();

        }
        void DinhDangLaiMaTran()
        {
            for (int i = 0; i < iDinh; i++)
            {
                Matran[i, i] = int.MinValue;
            }
        }
        bool LechMau(int m1, int m2)
        {
            if (Math.Abs(m1 - m2) < 50) return true;
            return false;
        }
        bool KtMau(int r, int b, int g)
        {
            int dem = 0;
            if (r + g + b <= 370)
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
            //  comboBox1.Items.Clear();
            // Dijsktra_di.Items.Clear();
            for (int i = 0; i < iDinh; i++)
            {
                listBox1.Items.Add(tenDinh[i]);
                // comboBox1.Items.Add(tenDinh[i]);
                // Dijsktra_di.Items.Add(tenDinh[i]);
            }
            // comboBox1.Items.Add("");
            // Dijsktra_di.Items.Add("");
        }
        bool khoangcachDinh(int x, int y)
        {
            for (int i = 0; i < iDinh; i++)
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
            while (true)
            {
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
            for (int i = 0; i < iDinh; i++)
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
            // comboBox2.Items.Clear();
            // comboBox1.Items.Clear();
            // Dijsktra_di.Items.Clear();
            //  Dijstra_den.Items.Clear();
            richTextBox1.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
            for (int i = 0; i < iDinh; i++)
            {
                luuVetdinh[i] = false;
                luuVetdinh1[i] = false;
                // luuVetdinh2[i] = false;
                for (int j = 0; j < iDinh; j++)
                {
                    Matran[i, j] = int.MinValue;
                    luuVetcanh[i, j] = false;
                }
            }
            iDinh = SoDinhLienThong = tmp = demNode = demTim = 0;
            //  start = -1;
            di = den = path = "";
            vertex = edge = dij = OnlyPath = xuatphat.Visible = dich.Visible = vt1.Visible = vt2.Visible = Tree.Visible = am = nghivan =false;
            Duyet = null;
            pictureBox1.Image = null;
            pictureBox1.Refresh();

        }
        void DocCanhMaTran(int soDinh)
        {
            listView1.Items.Clear();
            ListViewItem Canh;
            string[] a = new string[3];
            for (int i = 0; i < soDinh; i++)
            {
                for (int j = 0; j < soDinh; j++)
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



        public string xuLy(ref string s)
        {
            s.TrimEnd();
            s.TrimStart();
            while (s.IndexOf("  ") != -1)
            {
                s = s.Replace("  ", " ");
            }

            return s;
        }
        public string[] PhanTang(string[] str)
        {
            int level = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == null) break;
                else
                {
                    string[] tmp = str[i].Split(' ');
                    if (tmp.Length > level) level = tmp.Length;
                }
            }
           // MessageBox.Show(level.ToString());
            string[] result = new string[level];

            List<string>[] s = new List<string>[level];

            for (int i = 0; i < level; i++)
            {
                s[i] = new List<string>();
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == null) break;
                else
                {
                    string[] strOne = str[i].Split(' ');
                    for (int j = 0; j < strOne.Length; j++)
                    {
                        string charOne = strOne[j];

                        if (s[j] == null)
                        {
                            s[j].Add(charOne);
                        }
                        else if (!s[j].Contains(charOne))
                        {
                            s[j].Add(charOne);
                        }
                    }
                }
            }

            for (int j = 0; j < level; j++)
            {
                string tmp = "";

                for (int i = 0; i < s[j].Count; i++)
                {
                    if (i < s[j].Count - 1)
                    {
                        tmp += s[j][i] + " ";
                    }
                    else
                    {
                        tmp += s[j][i];
                    }
                }

                result[j] += tmp;
            }

            return result;
        }
        // ========================================Phần code chính=============
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
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
            Duyet = null;
            vt1.Visible = vt2.Visible = xuatphat.Visible = dich.Visible = Tree.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            LamMoi();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                string[] line = File.ReadAllLines(path);
                int soDinh = int.Parse(line[0].Trim());
                for (int i = 0; i < soDinh; i++)
                {
                    string[] weight = line[i + 1].Split(' ');
                    for (int j = 0; j < soDinh; j++)
                    {
                        if (weight[j] == "∞") Matran[i, j] = int.MinValue;
                        else
                        {
                            if (int.Parse(weight[j]) == 0) nghivan = true;
                            if (int.Parse(weight[j]) < 0) am = true;
                            Matran[i, j] = int.Parse(weight[j]);
                        }
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
                        ten = $"N{j}";
                    }
                    listBox1.Items.Add(ten);
                    TaoDinhAuto(ten);
                }
            }
            XetDoThiHopLe xet = new XetDoThiHopLe();
            XetDoThiHopLe.DoThi doThi = new XetDoThiHopLe.DoThi();
            doThi.iSodinh = iDinh;
            doThi.iMaTran = Matran;
            if (nghivan)
            {
                System.Media.SystemSounds.Exclamation.Play();
                NghiVan nghiVan = new NghiVan();
                nghiVan.ShowDialog();
                if (!nghiVan.ok)
                {
                    for (int i = 0; i < iDinh; i++)
                    {
                        Matran[i, i] = int.MinValue;
                    }
                    nghivan = false;
                }
                else
                {
                    for (int i = 0; i < iDinh; i++)
                    {
                        for (int j = 0; j < iDinh; j++)
                        {
                            if (Matran[i, j] == 0) Matran[i, j] = int.MinValue;
                        }
                    }
                    nghivan = false;
                }

            }
            if (!nghivan && xet.KiemTraDonDoThiDonVoHuong(doThi))
            {
                if (path != "")
                {
                    if (am)
                    {
                        System.Media.SystemSounds.Exclamation.Play();
                        Mess("Hệ thống nhạy cảm với số âm :(", "Hệ thống", global::LTDT_project.Properties.Resources.warning, SystemIcons.Warning);
                        LamMoi();
                    }
                    else
                    {
                        System.Media.SystemSounds.Asterisk.Play();
                        Mess("Đọc file thành công", "Hệ thống", global::LTDT_project.Properties.Resources.Tick, SystemIcons.Information);
                        CapNhatDS_dinhdi();
                        pictureBox1.Refresh();
                        DocCanhMaTran(iDinh);
                    }
                }
            }
            else
            {
                System.Media.SystemSounds.Exclamation.Play();
                Mess("Chỉ đọc được đồ thị đơn vô hướng thôi !!! ", "Hệ thống", global::LTDT_project.Properties.Resources.warning, System.Drawing.SystemIcons.Warning);
                LamMoi();
            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            tmp = 0;
            richTextBox1.Text = "";
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        Duyet = null;
                        xuatphat.Visible = dich.Visible = Tree.Visible = false ;
                        demTim = 0;
                        int xx = -1, yy = -1;
                        TaoDoDuongDi(e.X, e.Y);
                        if (demNode == 2)
                        {
                            for (int i = 0; i < iDinh; i++)
                            {
                                if (luuVetdinh1[i] == true)
                                {
                                    if (xx == -1)
                                    {
                                        xx = i;
                                    }
                                    else if (xx != -1 && yy == -1 && i != xx)
                                    {
                                        yy = i;
                                    }
                                }
                                luuVetdinh1[i] = false;
                            }
                            if (xx != -1 && yy != -1)
                            {

                                Cập_nhật_trọng_số cập_Nhật_Trọng_Số = new Cập_nhật_trọng_số();
                                cập_Nhật_Trọng_Số.ShowDialog();
                                if (cập_Nhật_Trọng_Số.trongso != int.MinValue) Matran[xx, yy] = Matran[yy, xx] = cập_Nhật_Trọng_Số.trongso;

                            }
                            demNode = 0;
                            vt1.Visible = vt2.Visible = false;
                            pictureBox1.Refresh();
                            DocCanhMaTran(iDinh);
                           // break;
                        }
                        break;
                    }
                case MouseButtons.Middle:
                    {
                        Duyet = null;
                        vt1.Visible = vt2.Visible = Tree.Visible = false;
                        demNode = 0;
                        for (int i = 0; i < iDinh; i++)
                        {
                            luuVetdinh1[i] = false;
                        }
                        if (demTim == 0 && XetToaDo(e.X, e.Y) != -1)
                        {
                            x = XetToaDo(e.X, e.Y);
                            xuatphat.Location = new Point(Dinh[x].X, Dinh[x].Y);
                            xuatphat.Visible = true;
                            demTim++;
                        }
                        if (demTim == 1 && x != XetToaDo(e.X, e.Y) && XetToaDo(e.X, e.Y) != -1)
                        {
                            y = XetToaDo(e.X, e.Y);
                            dich.Location = new Point(Dinh[y].X, Dinh[y].Y);
                            dich.Visible = true;
                            demTim++;
                        }
                        if (x != -1 && y != -1)
                        {
                            Timduongdi timduongdi = new Timduongdi();
                            timduongdi.ShowDialog();
                            richTextBox1.Text = "";
                            textBox2.Text = "";
                            for (int i = 0; i < iDinh; i++)
                            {
                                luuVetdinh[i] = false;
                                for (int j = 0; j < iDinh; j++)
                                {
                                    luuVetcanh[i, j] = false;
                                }
                            }
                            int[] duongdi;
                            int n = -1;
                            if (timduongdi.Ngan)
                            {
                                Dijsktra.DuLieu a = new Dijsktra.DuLieu();
                                Dijsktra dijsktra = new Dijsktra();
                                a.sodinh = iDinh;
                                a.mt = Matran;
                                a.di = x + 1;
                                a.den = y + 1;
                                duongdi = dijsktra.TimDuong(a);
                            }
                            else if (timduongdi.Dai)
                            {
                                DeQuy.DuLieu a = new DeQuy.DuLieu();
                                DeQuy dq = new DeQuy();
                                a.sodinh = iDinh;
                                a.mt = Matran;
                                a.di = x + 1;
                                a.den = y + 1;
                                duongdi = dq.TimDuong(a);
                                
                                if(duongdi[0] > 2) Array.Reverse(duongdi, 2, duongdi[0]-2);
                                //MessageBox.Show(string.Join(" ", duongdi));
                            }
                            else
                            {
                                duongdi = null;
                            }


                            if (duongdi != null)
                            {
                                n = duongdi[0];
                            }
                            //MessageBox.Show(string.Join(" ",duongdi));
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
                                if (timduongdi.Ngan)
                                {
                                    var xacnhan = MessageBox.Show("Bạn có muốn xem lời giải", "Hệ thống", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                    if (xacnhan == DialogResult.OK)
                                    {
                                        string[,] loigiai;

                                        Write_Dijsktra_in_paper in_Paper = new Write_Dijsktra_in_paper();

                                        loigiai = in_Paper.Dijsktra(Matran, iDinh, x, tenDinh);

                                        // MessageBox.Show(iDinh.ToString());
                                        Loigiai loigiai1 = new Loigiai();
                                        loigiai1.loigiai = loigiai;
                                        loigiai1.iDinh = iDinh;
                                        loigiai1.ShowDialog();
                                    }
                                }
                            }
                            else
                            {
                                if ((timduongdi.Ngan == true || timduongdi.Dai == true))
                                {
                                    System.Media.SystemSounds.Hand.Play();
                                    Mess("Hmm... Chắc không tìm thấy đường đi đâu", "Hệ thống", global::LTDT_project.Properties.Resources.Error, SystemIcons.Error);
                                }
                            }
                            x = y = -1;
                        }
                        if (demTim == 2)
                        {
                            demTim = 0;
                            xuatphat.Visible = dich.Visible = false;
                        }
                    }
                    break;
                case MouseButtons.Right:
                    {
                        XetLienThong xetLienThong = new XetLienThong();
                        XetLienThong.DoThi doThi = new XetLienThong.DoThi();
                        doThi.iMaTran = Matran;
                        doThi.iSoDinh = iDinh;
                        bool kt = xetLienThong.xetLienThong(doThi);
                        if (kt)
                        {
                            int sDc;
                            Duyet = null;
                            vt1.Visible = vt2.Visible = xuatphat.Visible = dich.Visible = false;
                            demTim = demTim = 0;
                            for (int i = 0; i < iDinh; i++)
                            {
                                luuVetdinh1[i] = false;
                            }
                            if (XetToaDo(e.X, e.Y) != -1)
                            {
                                sDc = XetToaDo(e.X, e.Y);
                                Tree.Location = new Point(Dinh[sDc].X, Dinh[sDc].Y);
                                Tree.Visible = true;
                                DFS_BFS dFS_BFS = new DFS_BFS();
                                dFS_BFS.ShowDialog();
                                if (dFS_BFS.BFS)
                                {

                                    DuyetCay.GRAPH gRAPH = new DuyetCay.GRAPH();
                                    // DuyetCay.QUEUE qUEUE = new DuyetCay.QUEUE();
                                    gRAPH.A = Matran;
                                    gRAPH.sodinh = iDinh;
                                    //qUEUE.size = 0;
                                    //qUEUE.a.DefaultIfEmpty(0);
                                    int[] visit = new int[101];
                                    int[] luuvet = new int[101];
                                    for (int i = 0; i < 100; i++)
                                    {
                                        visit[i] = 0;
                                        luuvet[i] = -1;
                                    }
                                    gRAPH.LuuVet = luuvet;
                                    gRAPH.visited = visit;
                                    //qUEUE.a = visit;
                                    // qUEUE.a.DefaultIfEmpty(0);
                                    DuyetCay duyetCay = new DuyetCay();
                                    Duyet = duyetCay.BFS(gRAPH, sDc);

                                    // MessageBox.Show(string.Join("\n", Duyet));
                                }
                                else if (dFS_BFS.DFS)
                                {
                                    DuyetCay.DO_THI dO_THI = new DuyetCay.DO_THI();
                                    dO_THI.canh = Matran;
                                    dO_THI.sodinh = iDinh;
                                    DuyetCay duyetCay = new DuyetCay();
                                    Duyet = duyetCay.DFSstr(sDc, dO_THI);
                                    // MessageBox.Show(string.Join("\n", Duyet));
                                    //MessageBox.Show(Duyet[0]);

                                    /* string s = "";
                                     for (int i = 0; i < Duyet.Length - 1; i++)
                                     {
                                         s += Duyet[i].Trim() + ".";
                                     }
                                     s += Duyet[Duyet.Length - 1];
                                     MessageBox.Show($"{s}\n{Duyet.Length}");*/
                                }
                                //Duyet = PhanTang(Duyet);
                                pictureBox1.Refresh();
                                Duyet = null;
                                Tree.Visible = false;
                            }
                        }
                        else
                        {
                            System.Media.SystemSounds.Exclamation.Play();
                            Mess("Không duyệt DFS - BFS được vì đồ thị không liên thông ", "Hệ thống", global::LTDT_project.Properties.Resources.warning, SystemIcons.Warning);
                            DinhDangLaiMaTran();
                            pictureBox1.Refresh();
                            DocCanhMaTran(iDinh);
                            CapNhatDS_dinhdi();
                        }
                        break;
                    }
            }

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
            if (SoDinhLienThong == 0&&Duyet==null)
            {
                for (int i = 0; i < iDinh; i++)
                {
                    g.FillEllipse(maunenDinh, Dinh[i].X - 7, Dinh[i].Y - 7, 15, 15);
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
            if (SoDinhLienThong > 0 && dij == false)
            {


                for (int i = 0; i < SoDinhLienThong; i++)
                {
                    SolidBrush maulienthong = new SolidBrush(Color.FromArgb(paint[i].r, paint[i].b, paint[i].g));

                    g.FillEllipse(maulienthong, Dinh[paint[i].index].X - 7, Dinh[paint[i].index].Y - 7, 15, 15);
                    g.DrawString(tenDinh[paint[i].index], f2, mauchuDinh, Dinh[paint[i].index].X - 5, Dinh[paint[i].index].Y - 5);
                    for (int j = 0; j < SoDinhLienThong; j++)
                    {
                        if (paint[i].r == paint[j].r && paint[i].b == paint[j].b && paint[i].g == paint[j].g && Matran[paint[i].index, paint[j].index] != int.MinValue)
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
                    g.FillEllipse(maulienthong, Dinh[paint[i].index].X - 7, Dinh[paint[i].index].Y - 7, 15, 15);
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
                g.FillEllipse(mau, Dinh[paint[SoDinhLienThong - 1].index].X - 7, Dinh[paint[SoDinhLienThong - 1].index].Y - 7, 15, 15);
                g.DrawString(tenDinh[paint[SoDinhLienThong - 1].index], f2, mauchuDinh, Dinh[paint[SoDinhLienThong - 1].index].X - 5, Dinh[paint[SoDinhLienThong - 1].index].Y - 5);
                if (OnlyPath == false)
                {
                    for (int i = 0; i < iDinh; i++)
                    {
                        if (!luuVetdinh[i])
                        {
                            g.FillEllipse(maunenDinh, Dinh[i].X - 7, Dinh[i].Y - 7, 15, 15);
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
            if (Duyet != null)
            {
                string[] Pt = PhanTang(Duyet);
                int kcngang = 900 / Pt.Length;
                int batdaux = 20;
                // Ve Dinh
                for (int i = 0; i < Pt.Length; i++)
                {
                    string[] Node = Pt[i].Split(' ');
                    int batdauy = 450 / (Node.Length+1);
                    int kcdoc = batdauy;
                    for (int j = 0; j < Node.Length; j++)
                    {
                        int id = int.Parse(Node[j]);
                        Dinh[id].X = batdaux;
                        Dinh[id].Y = batdauy;
                        g.FillEllipse(maunenDinh, Dinh[id].X - 7, Dinh[id].Y - 7, 15, 15);
                        g.DrawString(tenDinh[id], f2, mauchuDinh, Dinh[id].X - 5, Dinh[id].Y - 5);
                        batdauy += kcdoc;
                    }
                    batdaux += kcngang;
                   
                }
                // Ve canh
                bool[,] Try = new bool[100, 100];
                for (int l = 0; l < 100; l++)
                {
                    for (int r = 0; r < 100; r++)
                    {
                        Try[l, r] = false;
                    }
                }
                for (int i = 0; i < Duyet.Length; i++)
                {
                    
                    if (Duyet[i] == null) break;
                    else
                    {
                        string[] a = Duyet[i].Split(' ');
                        for (int j = 0; j < a.Length - 1; j++)
                        {
                            
                            int id1 = int.Parse(a[j]);
                            int id2 = int.Parse(a[j + 1]);
                           

                            if (Matran[id1, id2] != int.MinValue && Try[id1,id2]==false)
                            {
                                g.DrawLine(line, Dinh[id1], Dinh[id2]);
                                g.FillEllipse(khoangtrong, (Dinh[id1].X + Dinh[id2].X) / 2 - 5, (Dinh[id1].Y + Dinh[id2].Y) / 2 - 5, 10, 10);
                                g.DrawString(Matran[id1, id2].ToString(), f2, mauchuDinh, (Dinh[id1].X + Dinh[id2].X) / 2 - 5, (Dinh[id1].Y + Dinh[id2].Y) / 2 - 5);
                                Try[id1,id2] =Try[id2,id1] =  true;
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
            System.Media.SystemSounds.Question.Play();
            HeThong heThong = new HeThong();
            heThong.title = "Bạn có chắc muốn làm mới ?";
            heThong.ShowDialog();
            if (heThong.ok)
            {
                LamMoi();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            edge = true;
            vertex = false;
            try
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {
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
            xuatphat.Visible = dich.Visible = vt1.Visible = vt2.Visible =Tree.Visible= false;
            demTim = demNode = 0;
            for (int i = 0; i < iDinh; i++)
            {
                Random rnd = new Random();
                int randomX, randomY;
                while (true)
                {
                    randomX = rnd.Next(12, 885);
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
        private void button5_Click(object sender, EventArgs e)
        {
            /*
                int x = indexDinh(comboBox1.Text);
                int y = indexDinh(comboBox2.Text);
                Matran[x, y] = Matran[y,x] = int.Parse(numericUpDown1.Value.ToString());

            pictureBox1.Refresh();
            DocCanhMaTran(iDinh);
             */
        }

        private void lienthong_kt_Click(object sender, EventArgs e)
        {
            vt1.Visible = vt2.Visible = xuatphat.Visible = dich.Visible = Tree.Visible = false;
            demNode = demTim = 0;
            tmp = 0;
            richTextBox1.Text = "";
            string[] tp_lienThong = new string[100];
            XetLienThong x = new XetLienThong();
            XetLienThong.DoThi doThi = new XetLienThong.DoThi();
            doThi.iMaTran = Matran;
            doThi.iSoDinh = iDinh;
            tp_lienThong = x.xuatMienLienThong(doThi);
            //MessageBox.Show(tenDinh[10].ToString());
            int SoMienLT = int.Parse(tp_lienThong[0]);
            for (int i = 1; i <= SoMienLT; i++)
            {
                Random rnd = new Random();
                while (true)
                {
                    int r = rnd.Next(0, 256);
                    int b = rnd.Next(0, 256);
                    int g = rnd.Next(0, 256);
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


        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            vt1.Visible = vt2.Visible = xuatphat.Visible = dich.Visible = Tree.Visible = false;
            demNode = demTim = 0;
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
                    for (int i = 0; i < iDinh; i++)
                    {
                        for (int j = 0; j < iDinh; j++)
                        {
                            if (Matran[i, j] == int.MinValue) save += "∞ ";
                            else save += Matran[i, j].ToString() + " ";
                        }
                        save += "\n";
                    }
                    for (int i = 0; i < iDinh; i++)
                    {
                        save += tenDinh[i];
                        if (i < iDinh - 1) save += "\n";
                    }
                    StreamWriter stream = new StreamWriter(saveFileDialog1.FileName);
                    stream.WriteLine(save);
                    stream.Close();
                    System.Media.SystemSounds.Asterisk.Play();
                    Mess("Lưu file thành công", "Hệ thống", global::LTDT_project.Properties.Resources.Tick, System.Drawing.SystemIcons.Information);
                }
            }
        }



        private void button3_Click_1(object sender, EventArgs e)
        {
            vt1.Visible = vt2.Visible = xuatphat.Visible = dich.Visible = Tree.Visible = false;
            demNode = demTim = 0;
            tmp = 0;
            richTextBox1.Text = "";
            if (textBox1.Text != "")
            {
                if (vertex)
                {
                    System.Media.SystemSounds.Question.Play();
                    HeThong heThong = new HeThong();
                    heThong.ShowDialog();
                    if (!heThong.ok)
                    {
                        DinhDangLaiMaTran();
                        pictureBox1.Refresh();
                        DocCanhMaTran(iDinh);
                        CapNhatDS_dinhdi();

                        //Loi.Close();
                    }
                    else
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
                        DinhDangLaiMaTran();
                        pictureBox1.Refresh();
                        DocCanhMaTran(iDinh);
                        CapNhatDS_dinhdi();

                    }
                }
                else if (edge)
                {
                    System.Media.SystemSounds.Question.Play();
                    HeThong heThong = new HeThong();
                    heThong.ShowDialog();
                    if (!heThong.ok)
                    {
                        DinhDangLaiMaTran();
                        pictureBox1.Refresh();
                        DocCanhMaTran(iDinh);
                        CapNhatDS_dinhdi();

                        //Loi.Close();
                    }
                    else
                    {
                        try
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
                        catch
                        {

                        }
                        DinhDangLaiMaTran();
                        pictureBox1.Refresh();
                        DocCanhMaTran(iDinh);
                        CapNhatDS_dinhdi();

                        //Loi.Close();
                    };
                }
            }

        }

        private void DoitenDinh_Click_1(object sender, EventArgs e)
        {
            vt1.Visible = vt2.Visible = xuatphat.Visible = dich.Visible = Tree.Visible = false;
            demNode = demTim = 0;
            tmp = 0;
            richTextBox1.Text = "";
            try
            {
                if (textBox1.Text[0] == 'Đ')
                {
                    string rename = Interaction.InputBox("Nhập tên mới", "Đổi tên đỉnh");
                    string name = textBox1.Text.Substring(6);
                    rename = rename.Trim();
                    if (rename != "")
                    {
                        bool duyet = false;
                        foreach (var node in listBox1.Items)
                        {
                            if (rename == node.ToString()) duyet = true;
                        }
                        if (!duyet)
                        {
                            tenDinh[indexDinh(name)] = rename;
                        }
                        else
                        {
                            System.Media.SystemSounds.Exclamation.Play();
                            Mess("Đỉnh này đã tồn tại hoặc têm không hợp hệ", "Hệ thống", global::LTDT_project.Properties.Resources.warning, SystemIcons.Warning);
                        }
                    }
                }
                else
                {
                    System.Media.SystemSounds.Hand.Play();
                    Mess("Chỉ được đổi tên của đối tượng đỉnh thôi !!!", "Hệ thống", global::LTDT_project.Properties.Resources.Error, SystemIcons.Error);
                }
                CapNhatDS_dinhdi();
                DocCanhMaTran(iDinh);
                pictureBox1.Refresh();
            }
            catch
            {

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            vt1.Visible = vt2.Visible = xuatphat.Visible = dich.Visible = Tree.Visible = false;
            demNode = demTim = 0;
            XuatMatranKe matranKe = new XuatMatranKe();
            matranKe.mt = Matran;
            matranKe.sodinh = iDinh;
            matranKe.tenDinh = tenDinh;
            //MessageBox.Show(tenDinh[2].Length.ToString());
            matranKe.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            LamMoi();
            Form2 form2 = new Form2();
            form2.ShowDialog();
            iDinh = form2.Sodinh;
            Matran = form2.mt;
            int sodinh = iDinh;
            iDinh = 0;
            for (int i = 0; i < sodinh; i++)
            {
                tenDinh[i] = $"N{i}";
                TaoDinhAuto(tenDinh[i]);
            }

            pictureBox1.Refresh();
            CapNhatDS_dinhdi();
            DocCanhMaTran(iDinh);
            DinhDangLaiMaTran();
        }
        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tmp = 0;
            richTextBox1.Text = "";
            bool duplicate = false;
            bool kc = false;
            string ten = Interaction.InputBox("Nhập tên đỉnh", "Nhập liệu");
            ten = ten.Trim();
            //MessageBox.Show($"{e.X} , {e.Y}");
            if (ten != "")
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
                        //MessageBox.Show($"{e.X} {e.Y}");
                        Dinh[iDinh] = new Point(e.X, e.Y);
                        listBox1.Items.Add(ten);
                        for (int k = 0; k <= iDinh; k++)
                        {
                            Matran[iDinh, k] = Matran[k, iDinh] = imin;
                        }
                        iDinh++;
                    }
                    else
                    {
                        System.Media.SystemSounds.Exclamation.Play();
                        Mess("Quá gần !!!", "Thông báo", global::LTDT_project.Properties.Resources.warning, SystemIcons.Warning);

                    }
                }
                else
                {
                    // MessageBox.Show("Đỉnh này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Media.SystemSounds.Hand.Play();
                    Mess("Đỉnh này đã tồn tại", "Hệ thống", global::LTDT_project.Properties.Resources.Error, SystemIcons.Error);
                }
            }
            DinhDangLaiMaTran();
            CapNhatDS_dinhdi();
            pictureBox1.Refresh();

        }
    }
}
