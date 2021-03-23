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
    public partial class FrmSifreGuncelle : Form
    {
        private int id;
        private sifreTut _sifreTut;
        public FrmSifreGuncelle(sifreTut sifreTut)
        {
            InitializeComponent();
            _sifreTut = sifreTut;
            txtNick.Text = sifreTut.KullaniciAdi;
            txtSifre.Text = sifreTut.Sifre;
            txtNot.Text = sifreTut.Not;
            this.id = sifreTut.id;
        }
        
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            try
            {
                _sifreTut.KullaniciAdi = txtNick.Text;
                _sifreTut.Not = txtNot.Text;
                _sifreTut.Sifre = Sifreleme.sifrele(txtSifre.Text);
                db.Update(_sifreTut);
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
