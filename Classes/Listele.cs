using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValorantKolayGiris_FormDesktop_.Entity;
using ValorantKolayGiris_FormDesktop_.forms;


namespace ValorantKolayGiris_FormDesktop_.Classes
{

    class Listele
    {
        #region değişkenler

        private DB db = new DB();
        Sifreleme sifrecoz = new Sifreleme();
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        public Panel panel1;
        private string uygulamaYolu;
        private Form _form;
        #endregion


        public Listele(Panel panel, string uygulamaYolu, Form form)
        {
            panel1 = panel;
            backgroundWorker.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.uygulamaYolu = uygulamaYolu;
            _form = form;
        }
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        const int KEYEVENTF_EXTENDEDKEY = 0x1;
        const int KEYEVENTF_KEYUP = 0x2;

        private void timer_Tick(object sender, EventArgs e)
        {
            if (FrmAnasayfa.MouseButtons == MouseButtons.Middle)
            {
                Timer timer = sender as Timer;
                TimerEleman timerEleman = (TimerEleman)timer.Tag;
                timer.Stop();
                string nick = Regex.Replace(timerEleman.Nick, "[+%~()]", "{$0}");
                string sifre = Regex.Replace(timerEleman.Sifre, "[+%~()]", "{$0}");
                foreach (var VARIABLE in timerEleman.Sifre)
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
                        harfDegisimi(ref nick);
                        harfDegisimi(ref sifre);
                    }
                }

                SendKeys.SendWait("" + nick + "{tab}" + sifre + "{ENTER}");
                if (Properties.Settings.Default.OyunAcilincaUygulamayiKapat)
                {
                    Application.Exit();
                }
            }
        }

        private void harfDegisimi(ref string metin)
        {
            foreach (var harf in metin)
            {
                if (harf==char.ToUpper(harf))
                {
                    metin = metin.Replace(harf, char.ToLower(harf));
                }
                else
                {
                    metin = metin.Replace(harf, char.ToUpper(harf));
                }
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Process.Start(uygulamaYolu);
        }
        public void sifreCek()
        {
            int top = 5;
            int left = 10;
            List<sifreTut> sifreler = db.set<sifreTut>();
            foreach (var sifreTut in sifreler)
            {
                sifreTut.Sifre = sifrecoz.sifreCoz(sifreTut.Sifre);
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
            panel1.Controls.Add(nick);
            panel1.Controls.Add(sifre);
            panel1.Controls.Add(not);
            panel1.Controls.Add(check);
            panel1.Controls.Add(button);
            panel1.Controls.Add(btnsil);
            panel1.Controls.Add(btnDuzenle);
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
            //TimerEleman
            //
            TimerEleman timerEleman = new TimerEleman();
            timerEleman.Nick = nick.Text;
            timerEleman.Sifre = sifre.Text;
            //
            //button
            //
            button.Top = top;
            button.Left = not.Width + not.Left + fark;
            button.Click += new EventHandler(this.button_Click);
            button.Text = "aç";
            button.Size = new Size(30, 20);
            button.Tag = timerEleman;
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
            btnDuzenle.Tag =sifreTut;
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            sifreTut btnDEleman = (sifreTut)((sender as Button).Tag);
            FrmSifreGuncelle frmSifreGuncelle = new FrmSifreGuncelle(btnDEleman);
            frmSifreGuncelle.ShowDialog();
            panel1.Controls.Clear();
            FrmAnasayfa frmAnasayfa = (FrmAnasayfa)Application.OpenForms["FrmAnasayfa"];
            frmAnasayfa.listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            Baglanti baglanti = new Baglanti();
            int id = Convert.ToInt32((sender as Button).Tag);
            SQLiteCommand cmd2 = new SQLiteCommand();
            try
            {
                if (MessageBox.Show("emin misin", "silme", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    db.Delete(new sifreTut{id=id});
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            panel1.Controls.Clear();
            FrmAnasayfa frmAnasayfa = (FrmAnasayfa)Application.OpenForms["FrmAnasayfa"];
            frmAnasayfa.listele();
        }
        private void button_Click(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
            Timer timer = new Timer();
            timer.Tag = (sender as Button).Tag;
            timer.Tick += new System.EventHandler(this.timer_Tick);
            timer.Start();
            _form.WindowState = FormWindowState.Minimized;
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

        private bool gir = true;
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
    }
}
