using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ValorantKolayGiris_FormDesktop_.Entity;
using ValorantKolayGiris_FormDesktop_.forms;
using ValorantKolayGiris_FormDesktop_.Properties;
using Timer = System.Windows.Forms.Timer;

namespace ValorantKolayGiris_FormDesktop_.Classes
{
    public class Listele2
    {
        #region değişkenler
        private DB db = new DB();
        public Panel panel;
        private string uygulamaYolu;
        public FormWindowState FormWindowState { get; set; }
        private bool gir = true;
        private sifreTut girilicekHesap;
        private List<Timer> islemBitinceSilinecekler = new List<Timer>();
        #endregion

        #region sabitler
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const int KEYEVENTF_EXTENDEDKEY = 0x1;
        const int KEYEVENTF_KEYUP = 0x2;
        #endregion

        #region Dll
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        #endregion


        public Listele2(Panel panel, string uygulamaYolu)
        {
            this.panel = panel;
            this.uygulamaYolu = uygulamaYolu;
        }
        public void sifreCek()
        {
            int top = 5;
            int left = 10;
            List<sifreTut> sifreler = db.set<sifreTut>();
            foreach (var sifreTut in sifreler)
            {
                sifreTut.Sifre = Sifreleme.sifreCoz(sifreTut.Sifre);
                sifreEkle(top, left, sifreTut);
                top += 20;
            }
        }
        void sifreEkle(int top, int left, sifreTut sifreTut)
        {
            int fark = 5;
            TextBox nick = new TextBox();
            TextBox sifre = new TextBox();
            TextBox not = new TextBox();
            CheckBox check = new CheckBox();
            Button button = new Button();
            Button btnsil = new Button();
            Button btnDuzenle = new Button();
            panel.Controls.Add(nick);
            panel.Controls.Add(sifre);
            panel.Controls.Add(not);
            panel.Controls.Add(check);
            panel.Controls.Add(button);
            panel.Controls.Add(btnsil);
            panel.Controls.Add(btnDuzenle);
            //
            //nick
            //
            nick.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            nick.Size = new System.Drawing.Size(100, 34);
            nick.Top = top;
            nick.Left = left;
            nick.BorderStyle = BorderStyle.None;
            nick.ReadOnly = true;
            nick.Text = sifreTut.KullaniciAdi;
            nick.Click += new EventHandler(this.textBox_Click);
            nick.Tag = check;
            //
            //sifre
            //
            sifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            sifre.Size = new System.Drawing.Size(80, 34);
            sifre.Top = top;
            sifre.Left = nick.Width + nick.Left + fark;
            sifre.BorderStyle = BorderStyle.None;
            sifre.ReadOnly = true;
            sifre.Text = sifreTut.Sifre;
            sifre.PasswordChar = '*';
            sifre.Click += new EventHandler(this.textBox_Click);
            sifre.Tag = check;
            //
            //check
            //
            check.Width = 20;
            check.Height = 15;
            check.Top = top;
            check.Left = sifre.Width + sifre.Left + fark;
            check.Tag = sifre;
            check.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            //
            //not
            //
            not.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            not.Size = new System.Drawing.Size(80, 34);
            not.Top = top;
            not.Left = check.Width + check.Left + fark;
            not.BorderStyle = BorderStyle.None;
            not.ReadOnly = true;
            not.Text = sifreTut.Not;
            //
            //button
            //
            button.Top = top;
            button.Left = not.Width + not.Left + fark;
            button.Click += new EventHandler(this.button_Click);
            button.Text = "aç";
            button.Size = new Size(30, 20);
            button.Tag = sifreTut;
            //
            //btnsil
            //
            btnsil.Top = top;
            btnsil.Left = button.Width + button.Left + fark;
            btnsil.Click += new EventHandler(this.btnSil_Click);
            btnsil.Text = "sil";
            btnsil.Size = new Size(30, 20);
            btnsil.Tag = sifreTut.id;
            //
            //btnDuzenle
            //
            btnDuzenle.Top = top;
            btnDuzenle.Left = btnsil.Width + btnsil.Left + fark;
            btnDuzenle.Click += new EventHandler(this.btnDuzenle_Click);
            btnDuzenle.Text = "düzenle";
            btnDuzenle.Size = new Size(30, 20);
            btnDuzenle.Tag = sifreTut;
        }
        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            sifreTut btnDEleman = (sifreTut)((sender as Button).Tag);
            FrmSifreGuncelle frmSifreGuncelle = new FrmSifreGuncelle(btnDEleman);
            frmSifreGuncelle.ShowDialog();
            panel.Controls.Clear();
            FrmAnasayfa frmAnasayfa = (FrmAnasayfa)Application.OpenForms["FrmAnasayfa"];
            frmAnasayfa.listele();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            Baglanti baglanti = new Baglanti();
            int id = Convert.ToInt32((sender as Button).Tag);
            try
            {
                if (MessageBox.Show("emin misin", "silme", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    db.Delete(new sifreTut { id = id });
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            panel.Controls.Clear();
            FrmAnasayfa frmAnasayfa = (FrmAnasayfa)Application.OpenForms["FrmAnasayfa"];
            frmAnasayfa.listele();
        }
        private void button_Click(object sender, EventArgs e)
        {
            FormWindowState = FormWindowState.Minimized;
            girilicekHesap = (sifreTut)(sender as Button).Tag;
            Timer timer = new Timer();
            if (!Settings.Default.MakroKUllan)
            {
                timer.Interval = 1000;
            }
            
            timer.Tick += new EventHandler(this.SifreGir);
            timer.Start();
            islemBitinceSilinecekler.Add(timer);
            Process.Start(uygulamaYolu);
        }
        private void textBox_Click(object sender, EventArgs e)
        {
            TextBox textBox = (sender as TextBox);
            CheckBox checkBox = (CheckBox)textBox.Tag;
            if (checkBox.Checked)
            {
                Clipboard.SetText(textBox.Text);
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (sender as CheckBox);
            TextBox sifreBox = (TextBox)checkBox.Tag;
            if (checkBox.Checked)
            {
                if (gir)
                {
                    sifreBox.PasswordChar = '\0';
                }
                else
                {
                    checkBox.Checked = false;
                }
            }
            else
            {
                sifreBox.PasswordChar = '*';
            }
        }

        private void mouseTut(object sender, EventArgs e)
        {
            WindowsState.Durum("RiotClientUx", out Point konum);
            Cursor.Position = konum;
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }
        private void SifreGir(object sender, EventArgs e)
        {

            if (FrmAnasayfa.MouseButtons == MouseButtons.Middle || !Settings.Default.MakroKUllan)
            {
                Timer timer = sender as Timer;
                timer.Stop();
                KarakterKontrol(out var nick, out var sifre);
                while (!WindowsState.Durum("RiotClientUx", out _))
                {

                }
                if (WindowsState.Durum("RiotClientUx", out _))
                {
                    Timer timer2 = new Timer();
                    islemBitinceSilinecekler.Add(timer2);
                    timer2.Tick += new EventHandler(this.mouseTut);
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    timer2.Start();
                    if (!Settings.Default.MakroKUllan)
                    {
                        Thread.Sleep(700);
                    }

                    SendKeys.SendWait("" + nick + "{tab}" + sifre + "{ENTER}");
                    timer2.Stop();
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    if (Properties.Settings.Default.OyunAcilincaUygulamayiKapat)
                    {
                        Application.Exit();
                    }
                }

                foreach (var sil in islemBitinceSilinecekler)
                {
                    sil.Dispose();
                }
            }
        }

        private void KarakterKontrol(out string nick, out string sifre)
        {
            nick = Regex.Replace(girilicekHesap.KullaniciAdi, "[+%~()]", "{$0}");
            sifre = Regex.Replace(girilicekHesap.Sifre, "[+%~()]", "{$0}");
            foreach (var VARIABLE in girilicekHesap.Sifre)
            {
                if (VARIABLE == '^')
                {
                    Clipboard.SetText("^");
                    sifre = Regex.Replace(sifre, "[ ^ ]", "^(v)");
                    break;
                }
            }

            if (Properties.Settings.Default.CaplockKapat)
            {
                if (Control.IsKeyLocked(Keys.CapsLock))
                    keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
            }
            else
            {
                if (Control.IsKeyLocked(Keys.CapsLock))
                {
                    nick = harfDegisimi(nick);
                    sifre = harfDegisimi(sifre);
                }
            }
        }
        private string harfDegisimi(string metin)
        {
            foreach (var harf in metin)
            {
                if (harf == char.ToUpper(harf))
                {
                    metin = metin.Replace(harf, char.ToLower(harf));
                }
                else
                {
                    metin = metin.Replace(harf, char.ToUpper(harf));
                }
            }
            return metin;
        }
    }
}