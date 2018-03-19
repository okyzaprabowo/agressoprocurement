using System.Web;
using System.Web.UI;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Menentukan variabel yang akan digunakan
    /// </summary>
    public static class Naming
    {
        // Informasi Aplikasi
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string AppTitle = "Monitoring Pengadaan Len";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string AppName = "Monitoring Pengadaan";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string AppVersion = "Alpha 1.0";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string Instance = "PT. Len Industri (Persero)";
        // Konten
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string Dashboard = "Dashboard";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string MainDashBoard = "Main Dashboard";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string Home = "Beranda";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string Profile = "Profil";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string Users = "Pengguna";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string UserAll = "Daftar Pengguna";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string UserAdd = "Tambah Pengguna";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string Roles = "Role";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string RoleDelete = "Hapus Role";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string RoleUpdate = "Role Terbaru";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string RoleChoose = "Pilih Role";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string RoleEnterName = "Masukan Nama Role";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string RoleSearch = "Cari Role";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string RoleNew = "Buat Role Baru";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string RoleAll = "Role Terbaru";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string RoleAdd = "Tambah Role";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string SignOut = "Keluar";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string SignIn = "Masuk";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string SeeAllNotif = "Lihat semua notifikasi";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string RememberMe = "Simpan Sesi Saya";

        // Role level
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string SuperAdmin = "SuperAdmin";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string Admin = "Admin";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string Bapb = "BAPB";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string Pelaksana = "Pelaksana";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string PenerimaBarang = "Penerima Barang";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string SppTagihan = "SPP dan Tagihan";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string Struktural = "Struktural";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string UserBpd = "User DPB";

        // Another
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string None = "Kosong";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string Empty = "Kosong";
        /// <summary>
        /// Konstanta umum
        /// </summary>
        public const string NoData = "Data tidak tersedia";


    }

    /// <summary>
    /// untuk log javascript
    /// tidak dibutuhkan ketika aplikasi produksi
    /// </summary>
    //http://stackoverflow.com/questions/14713782/asp-net-mvc-console-writeline-to-browser
    public static class JavascriptStatic
    {
        static string scriptTag = "<script type=\"\" language=\"\">{0}</script>";
        /// <summary>
        /// Log
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            string function = "console.log('{0}');";
            string log = string.Format(GenerateCodeFromFunction(function), message);
                HttpContext.Current.Response.Write(log);
        }
        static string GenerateCodeFromFunction(string function)
        {
            return string.Format(scriptTag, function);
        }
    }
}