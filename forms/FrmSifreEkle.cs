using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValorantKolayGiris_FormDesktop_.Classes;
using ValorantKolayGiris_FormDesktop_.Entity;

namespace ValorantKolayGiris_FormDesktop_.forms
{
    public partial class FrmSifreEkle : Form
    {
        private Panel panel;
        public FrmSifreEkle(Panel panel)
        {
            InitializeComponent();
            this.panel = panel;
        }

        private void buttonEkle_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            Sifreleme sifrele=new Sifreleme();
            try
            {
                string sifre= sifrele.sifrele(textBoxSifre.Text);
                sifreTut sifreTut = new sifreTut {Sifre = sifre,KullaniciAdi = textBoxNick.Text,Not = txtNot.Text};
                db.Add(sifreTut);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            FrmAnasayfa frmAnasayfa = (FrmAnasayfa) Application.OpenForms["FrmAnasayfa"];
            panel.Controls.Clear();
            frmAnasayfa.listele();
            this.Close();
        }
    }
}
