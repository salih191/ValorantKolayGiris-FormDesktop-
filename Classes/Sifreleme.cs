using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorantKolayGiris_FormDesktop_.Classes
{
    class Sifreleme
    {
        public string sifrele(string sifre)//şifre başkası tarafından veri tabanından çekilirse yanlış bilgi vermek için şifrele
        {
            string sifreliSifre = null;//şifreli halini tutacak string
            int[] asciiSifre = new int[sifre.Length];//ascii tablosu halini tutacak dizi

            for (int i = 0; i < sifre.Length; i++)//şifre elemanarını tek tek gez
            {
                asciiSifre[i] = ((char.ConvertToUtf32(sifre, i)) - 10);//her karekteri utf32 formatına çevir ve sayısal değerinden 10 çıkart

            }

            for (int i = 0; i < sifre.Length; i++)
            {
                sifreliSifre += Convert.ToChar(asciiSifre[i]);//her karekteri tekrar çevir 

            }

            return sifreliSifre;//sonucu döndür
        }

        public string sifreCoz(string sifreliSifre)//şifreyi göstereceği zaman şifreli metni çöz
        {
            string sifre = "";//şifremizi tutacak string

            int[] asciiSifre = new int[sifreliSifre.Length];//ascii halini tutacak dizi
            for (int i = 0; i < sifreliSifre.Length; i++)
            {
                asciiSifre[i] = ((char.ConvertToUtf32(sifreliSifre, i)) + 10);//ascii haline çevirip 10 ekle
            }

            for (int i = 0; i < sifreliSifre.Length; i++)
            {
                sifre += Convert.ToChar(asciiSifre[i]);//normale çevirip şifreye ekle
            }

            return sifre;//sonucu döndür
        }
    }
}
