using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberText {
    /// <summary>
    /// membuat auto generate angka ke tulisan
    /// referensi: belum ketemu lagi
    /// </summary>
    public static class NumberTextExtensionMethod {
        /// <summary>
        /// ToText
        /// </summary>
        /// <param name="num">num</param>
        /// <returns>ToText</returns>
        public static string ToText(this double num) {
            var numberText = new NumberText();
            return numberText.ToText(num);
        }
        /// <summary>
        /// Convert To Dot
        /// </summary>
        /// <param name="num">num</param>
        /// <returns>ToDot</returns>
        public static string ConvertToDot(this double num)
        {
            var numberText = new NumberText();
            return numberText.ToDot(num);
        }
    }
    /// <summary>
    /// Number to Text
    /// </summary>
    public class NumberText {

        private Dictionary<double, string> textStrings = new Dictionary<double, string>();
        private Dictionary<double, string> scales = new Dictionary<double, string>();
        //private StringBuilder builder;

        //public NumberText() {
        //    Initialize();
        //}

        // dari mas Windy tanggal 8 desember 2016 10.35
        /// <summary>
        /// dari mas Windy tanggal 8 desember 2016 10.35
        /// </summary>
        /// <param name="nilai">nilai</param>
        /// <returns>string</returns>
        public static string Terbilang(decimal nilai)
        {
            string terbilang = "";

            if (nilai < 0m)
                terbilang = "Minus ";

            decimal nilaiPositif = Math.Abs(nilai);

            decimal nilaiBulat = Math.Truncate(nilaiPositif);//Decimal.ToInt64(nilaiPositif);

            terbilang += TerbilangBulat(Decimal.ToInt64(nilaiBulat));

            decimal nilaiBelakangKoma = nilaiPositif - nilaiBulat;

            if (nilaiBelakangKoma > 0m)
                terbilang += " Koma " + TerbilangBelakangKoma(nilaiBelakangKoma);

            return terbilang;
        }

        private static string TerbilangBulat(long nilaiBulat, bool tulisNol = true)
        {
            string[] bilangan = { "NOL", "SATU", "DUA", "TIGA", "EMPAT", "LIMA", "ENAM", "TUJUH", "DELAPAN", "SEMBILAN", "SEPULUH", "SEBELAS" };

            string terbilang = "";

            if (nilaiBulat < 12)
            {
                if (nilaiBulat > 0 || tulisNol)
                    terbilang = " " + bilangan[nilaiBulat];
            }
            else if (nilaiBulat < 20)
            {
                terbilang = TerbilangBulat(nilaiBulat - 10).ToString() + " BELAS";

            }
            else if (nilaiBulat < 100)
            {
                terbilang = TerbilangBulat(nilaiBulat / 10) + " PULUH" + TerbilangBulat(nilaiBulat % 10, false);
            }
            else if (nilaiBulat < 200)
            {
                terbilang = " SERATUS" + TerbilangBulat(nilaiBulat - 100, false);
            }
            else if (nilaiBulat < 1000)
            {
                terbilang = TerbilangBulat(nilaiBulat / 100) + " RATUS" + TerbilangBulat(nilaiBulat % 100, false);
            }
            else if (nilaiBulat < 2000)
            {
                terbilang = " SERIBU" + TerbilangBulat(nilaiBulat - 1000);
            }
            else if (nilaiBulat < 1000000)
            {
                terbilang = TerbilangBulat(nilaiBulat / 1000) + " RIBU" + TerbilangBulat(nilaiBulat % 1000, false);
            }
            else if (nilaiBulat < 1000000000)
            {
                terbilang = TerbilangBulat(nilaiBulat / 1000000) + " JUTA" + TerbilangBulat(nilaiBulat % 1000000, false);
            }
            else if (nilaiBulat < 1000000000000)
            {
                terbilang = TerbilangBulat(nilaiBulat / 1000000000) + " MILYAR" + TerbilangBulat(nilaiBulat % 1000000000, false);
            }
            else if (nilaiBulat < 1000000000000000)
            {
                terbilang = TerbilangBulat(nilaiBulat / 1000000000000) + " TRILIUN" + TerbilangBulat(nilaiBulat % 1000000000000, false);
            }

            return terbilang;
        }

        private static string TerbilangBelakangKoma(decimal nilaiBelakangKoma)
        {
            string[] bilangan = { "NOL", "SATU", "DUA", "TIGA", "EMPAT", "LIMA", "ENAM", "TUJUH", "DELAPAN", "SEMBILAN", "SEPULUH", "SEBELAS" };

            string terbilang = "";

            string stringBelakangKoma = Math.Round(nilaiBelakangKoma, 4).ToString("0.####");

            for (int i = 2; i < stringBelakangKoma.Length; i++)
            {
                terbilang += " " + bilangan[int.Parse(stringBelakangKoma[i].ToString())];
            }

            return terbilang;
        }

        // end dari mas Windy
        /// <summary>
        /// To Text
        /// </summary>
        /// <param name="num">num</param>
        /// <returns>string</returns>
        public string ToText(double num)
        {
            decimal data = (decimal)num;
            return Terbilang(data);
        }
        /// <summary>
        /// To Dot
        /// </summary>
        /// <param name="num">num</param>
        /// <returns>string</returns>
        public string ToDot(double num)
        {
            string result = num.ToString();
            result = result.Replace(',', '.');
            return result;
        }
        ////public string ToText(double num) {
        ////    builder = new StringBuilder();

        ////    if (num == 0) {
        ////        builder.Append(textStrings[num]);
        ////        return builder.ToString();
        ////    }

        ////    num = scales.Aggregate(num, (current, scale) => Append(current, scale.Key));
        ////    AppendLessThanOneThousand(num);

        ////    return builder.ToString().Trim();
        ////}

        //private double Append(double num, double scale) {
        //    if (num > scale - 1) {
        //        var baseScale = ((double)(num / scale));
        //        AppendLessThanOneThousand(baseScale);
        //        builder.AppendFormat("{0} ", scales[scale]);
        //        num = num - (baseScale * scale);
        //    }
        //    return num;
        //}

        //private double AppendLessThanOneThousand(double num) {
        //    num = AppendHundreds(num);
        //    num = AppendTens(num);
        //    AppendUnits(num);
        //    return num;
        //}

        //private void AppendUnits(double num) {
        //    if (num > 0) {
        //        builder.AppendFormat("{0} ", textStrings[num]);
        //    }
        //}

        //private double AppendTens(double num) {
        //    if (num > 20) {
        //        var tens = ((double) (num/10))*10;
        //        builder.AppendFormat("{0} ", textStrings[tens]);
        //        num = num - tens;
        //    }
        //    return num;
        //}

        //private double AppendHundreds(double num) {
        //    if (num > 99) {
        //        var hundreds = ((double) (num/100));
        //        if (hundreds == 1)
        //        {
        //            builder.AppendFormat("Seratus ", textStrings[hundreds]);
        //        }
        //        else
        //        {
        //            builder.AppendFormat("{0} Ratus ", textStrings[hundreds]);
        //        }

        //        num = num - (hundreds*100);
        //    }
        //    return num;
        //}

        //private void Initialize() {
        //    textStrings.Add(0, "Nol");
        //    textStrings.Add(1, "Satu");
        //    textStrings.Add(2, "Dua");
        //    textStrings.Add(3, "Tiga");
        //    textStrings.Add(4, "Empat");
        //    textStrings.Add(5, "Lima");
        //    textStrings.Add(6, "Enam");
        //    textStrings.Add(7, "Tujuh");
        //    textStrings.Add(8, "Delapan");
        //    textStrings.Add(9, "Sembilan");
        //    textStrings.Add(10, "Sepuluh");
        //    textStrings.Add(11, "Sebelas");
        //    textStrings.Add(12, "Dua Belas");
        //    textStrings.Add(13, "Tiga Belas");
        //    textStrings.Add(14, "Empat Belas");
        //    textStrings.Add(15, "Lima Belas");
        //    textStrings.Add(16, "Enam Belas");
        //    textStrings.Add(17, "Tujuh Belas");
        //    textStrings.Add(18, "Delapan Belas");
        //    textStrings.Add(19, "Sembilan Belas");
        //    textStrings.Add(20, "Dua Puluh");
        //    textStrings.Add(30, "Tiga Puluh");
        //    textStrings.Add(40, "Empat Puluh");
        //    textStrings.Add(50, "Lima Puluh");
        //    textStrings.Add(60, "Enam Puluh");
        //    textStrings.Add(70, "Tujuh Puluh");
        //    textStrings.Add(80, "Delapan Puluh");
        //    textStrings.Add(90, "Sembilan Puluh");
        //    textStrings.Add(100, "Seratus");

        //    scales.Add(1000000000, "Milyar");
        //    scales.Add(1000000, "Juta");
        //    scales.Add(1000, "Ribu");
        //}
    }
}
