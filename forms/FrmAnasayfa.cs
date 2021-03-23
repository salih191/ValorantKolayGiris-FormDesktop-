using System;
using System.IO;
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
            if (UygulamaKontrol())
            {
                //Listele liste = new Listele(panel1, Settings.Default.UygulamaYolu, this);
                //liste.sifreCek();
                Listele2 liste = new Listele2(panel1, Settings.Default.UygulamaYolu,this);
                liste.sifreCek();
            }
            else
            {
                MessageBox.Show("Valorant Bulunamadı !!!!");
                Application.Exit();
            }
        }

        private bool UygulamaKontrol()
        {
            if (!File.Exists(Settings.Default.UygulamaYolu))
            {
                MessageBox.Show("valorant kısayolu bulunamadı");
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "valorant kısayol dosyası |*.lnk";
                openFile.InitialDirectory = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default.UygulamaYolu = openFile.FileName;
                    UygulamaKontrol();
                }
                else
                {
                    return false;
                }
                
            }
            return true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmSifreEkle frmSifreEkle = new FrmSifreEkle(panel1);
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
            makroKullanTavsiyeEdilenToolStripMenuItem.Checked = Settings.Default.MakroKUllan;
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
            if (veriYuklendi && (this.Location.X >= 0 && this.Location.Y >= 0))
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

        private bool ilkYuklenme;
        private void FrmAnasayfa_Activated(object sender, EventArgs e)
        {
            if (!ilkYuklenme)
            {
                ilkYuklenme = true;
                if (Settings.Default.ilkGiris)
                {
                    if (MessageBox.Show("Sol üstten açılır menüden şifre ekleyebilirsiniz daha sonra aç butonuna basmanız yeterlidir\nTekrar gösterilsin mi uyarı?", "", MessageBoxButtons.YesNoCancel) == DialogResult.No)
                    {
                        Settings.Default.ilkGiris = false;
                    }
                }
            }
        }

        private void makroKullanTavsiyeEdilenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makroKullanTavsiyeEdilenToolStripMenuItem.Checked = !makroKullanTavsiyeEdilenToolStripMenuItem.Checked;
            Settings.Default.MakroKUllan = makroKullanTavsiyeEdilenToolStripMenuItem.Checked;
        }
    }
}
