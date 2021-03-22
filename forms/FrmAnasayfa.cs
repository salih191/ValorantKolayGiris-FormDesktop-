using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using ValorantKolayGiris_FormDesktop_.Classes;
using ValorantKolayGiris_FormDesktop_.Properties;


namespace ValorantKolayGiris_FormDesktop_.forms
{
    public partial class FrmAnasayfa : Form
    {
        public FrmAnasayfa()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            uygulamaVerisi();
            listele();
        }

        private bool veriYuklendi = false;
        public void listele()
        {
            if (Settings.Default.ilkGiris)
            {
                if (MessageBox.Show("otomatik giriş için kullanıcı adı kısmında mousenin tekerlek tuşuna basınız\nTekrar gösterilsin mi uyarı?","",MessageBoxButtons.YesNoCancel) == DialogResult.No)
                {
                    Settings.Default.ilkGiris = false;
                }
            }
            if (!File.Exists(Settings.Default.UygulamaYolu))
            {
                MessageBox.Show("dosya bulunamadı");
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "kısayol dosyası |*.lnk";
                openFile.InitialDirectory = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default.UygulamaYolu = openFile.FileName;
                }
            }

            Listele liste = new Listele(panel1, Settings.Default.UygulamaYolu,this);
            liste.sifreCek();
        }
        
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmSifreEkle frmSifreEkle=new FrmSifreEkle(panel1);
            frmSifreEkle.ShowDialog();
        }

        private void bilgisayarİleBirlikteAçılsınToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bilgisayarİleBirlikteAçılsınToolStripMenuItem.Checked =
                !bilgisayarİleBirlikteAçılsınToolStripMenuItem.Checked;
            Settings.Default.FormOtoAcilsin = bilgisayarİleBirlikteAçılsınToolStripMenuItem.Checked;
            if (bilgisayarİleBirlikteAçılsınToolStripMenuItem.Checked)        // program oto başlatma işaretlenirse
            {

                //işaretlendi ise Regedit e açılışta çalıştır olarak ekle
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                key.SetValue("Kalan Zaman", "\"" + Application.ExecutablePath + "\"");
            }
            else              //program oto çalıştırma iptal edilirse
            {

                //işaret kaldırıldı ise Regeditten açılışta çalıştırılacaklardan kaldır
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                key.DeleteValue("Kalan Zaman");

            }
        }

        private void uygulamaVerisi()
        {
            if ((Settings.Default.FormLocation.X >= 0 && Settings.Default.FormLocation.Y >= 0))
            {
                this.Location = Settings.Default.FormLocation;
            }
            this.Size = Settings.Default.FormSize;
            bilgisayarİleBirlikteAçılsınToolStripMenuItem.Checked = Settings.Default.FormOtoAcilsin;
            oyunAçılıncaUygulamayıKapatToolStripMenuItem.Checked = Settings.Default.OyunAcilincaUygulamayiKapat;
            capslockKapatToolStripMenuItem.Checked = Settings.Default.CaplockKapat;
            veriYuklendi = true;
        }

        private void FrmAnasayfa_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private void oyunAçılıncaUygulamayıKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oyunAçılıncaUygulamayıKapatToolStripMenuItem.Checked =
                !oyunAçılıncaUygulamayıKapatToolStripMenuItem.Checked;
            Settings.Default.OyunAcilincaUygulamayiKapat = oyunAçılıncaUygulamayıKapatToolStripMenuItem.Checked;
        }

        private void capslockKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            capslockKapatToolStripMenuItem.Checked = !capslockKapatToolStripMenuItem.Checked;
            Settings.Default.CaplockKapat = capslockKapatToolStripMenuItem.Checked;
        }

        private void FrmAnasayfa_LocationChanged(object sender, EventArgs e)
        {
            if (veriYuklendi && (this.Location.X>=0&&this.Location.Y>=0))
            { 
                Settings.Default.FormLocation = this.Location;
            }
        }

        private void FrmAnasayfa_SizeChanged(object sender, EventArgs e)
        {
            if (veriYuklendi && this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Maximized)
            {
                Settings.Default.FormSize = this.Size;
            }
        }

    }
}
