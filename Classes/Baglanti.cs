using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValorantKolayGiris_FormDesktop_.Classes
{
    class Baglanti
    {
        string dosya= Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\salihBilişim\sifreDepo3.2.db";
        private SQLiteConnection baglanti;
        SQLiteCommand cmd = new SQLiteCommand();
        public Baglanti()
        { 
            baglanti = new SQLiteConnection("Data source=" + dosya);
            if (File.Exists(dosya) == false)
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\salihBilişim"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\salihBilişim");
                }

                SQLiteConnection.CreateFile(dosya);
                MessageBox.Show("veritabanı yok yaratılıyor");
                string sorgu =
                    "CREATE TABLE uygulamaVerisi (UygulamaKonumu	TEXT NOT NULL, OtoAcilis	INTEGER NOT NULL);" +
                    "CREATE TABLE \"sifreTut\" (\r\n\t\"Sifre\"\tTEXT,\r\n\t\"KullaniciAdi\"\tTEXT,\r\n\t\"id\"\tINTEGER,\r\n\t\"Not\"\tTEXT,\r\n\tUNIQUE(\"Sifre\",\"KullaniciAdi\"),\r\n\tPRIMARY KEY(\"id\")\r\n);" +
                    @"insert into uygulamaVerisi VALUES('C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Riot Games\valorant.lnk',0)";
                cmd.CommandText = sorgu;
                cmd.Connection = Baglan();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception )
                {

                }
                finally
                {
                    baglanti.Close();
                }
            }
        }

        public SQLiteConnection Baglan()
        {
            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }
            baglanti.Open();
            return baglanti;
        }
        public void BaglantiKapat()
        {
            baglanti.Close();
        }
    }
}
