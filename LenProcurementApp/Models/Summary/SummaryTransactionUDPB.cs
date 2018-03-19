using System.Linq;
namespace LenProcurementApp.Models
{
    /// <summary>
    /// transaksi summary user dpb
    /// </summary>
    public class SummaryTransactionUDPB
    {
        // database
        private ApplicationDbContext db = new ApplicationDbContext();
        string DPBQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "1" + "?" + "query=";
        string POQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "2" + "?" + "query=";

        string _userdpb = null;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="userdpb"></param>
        public SummaryTransactionUDPB(string userdpb)
        {
            _userdpb = userdpb;
        }
        /// <summary>
        /// Jumlah (∑) Total Item DPB sudah dibuat SPPH
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary1()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT( DISTINCT sp.product, sp.dpb, sp.po, sp.spph, d.user_dpb ) AS data1 FROM len_enq_spph_product sp INNER JOIN len_enq_dpb d ON d.dpb = sp.dpb WHERE YEAR (sp.mail_date) = YEAR (CURDATE()) AND d.user_dpb = '" + _userdpb + "' ORDER BY sp.dpb DESC;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Item DPB sudah dibuat SPPH";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT DISTINCT sp.dpb AS result FROM len_enq_spph_product sp INNER JOIN len_enq_dpb_product_new d ON d.dpb = sp.dpb WHERE YEAR (sp.mail_date) = YEAR (CURDATE()) AND d.user_dpb = '" + _userdpb + "' ORDER BY sp.dpb DESC;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Item DPB belum dibuat SPPH
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary2()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(d.spph) AS data1 FROM len_enq_dpb_product_new d WHERE YEAR (d.updated) = YEAR (CURDATE()) AND d.user_dpb = '" + _userdpb + "' AND d.spph = 0;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Item DPB belum dibuat SPPH";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT d.dpb AS result FROM len_enq_dpb_product_new d WHERE YEAR (d.updated) = YEAR (CURDATE()) AND d.user_dpb = '" + _userdpb + "' AND d.spph = 0;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Item  DPB sudah dibuat PO
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary3()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT( DISTINCT sp.dpb, sp.product, sp.po, sp.spph, d.user_dpb ) AS data1 FROM len_enq_spph_product sp INNER JOIN len_enq_dpb d ON d.dpb = sp.dpb WHERE YEAR (sp.mail_date) = YEAR (CURDATE()) AND sp.po IS NOT FALSE AND d.user_dpb = '" + _userdpb + "' ORDER BY sp.dpb DESC;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Item  DPB sudah dibuat PO";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT DISTINCT sp.dpb AS result FROM len_enq_spph_product sp INNER JOIN len_enq_dpb d ON d.dpb = sp.dpb WHERE YEAR (sp.mail_date) = YEAR (CURDATE()) AND sp.po IS NOT FALSE AND d.user_dpb = '" + _userdpb + "' ORDER BY sp.dpb DESC;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Item DPB belum dibuat PO
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary4()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT( DISTINCT sp.dpb, sp.product, sp.po, sp.spph, d.user_dpb ) AS data1 FROM len_enq_spph_product sp INNER JOIN len_enq_dpb d ON d.dpb = sp.dpb WHERE YEAR (sp.mail_date) = YEAR (CURDATE()) AND sp.po IS FALSE AND d.user_dpb = '" + _userdpb + "' ORDER BY sp.dpb DESC;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Item DPB belum dibuat PO";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT DISTINCT sp.dpb AS result FROM len_enq_spph_product sp INNER JOIN len_enq_dpb d ON d.dpb = sp.dpb WHERE YEAR (sp.mail_date) = YEAR (CURDATE()) AND sp.po IS FALSE AND d.user_dpb = '" + _userdpb + "' ORDER BY sp.dpb DESC;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Item DPB sudah dibuat BAPB
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary5()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT( DISTINCT sp.dpb, sp.product, sp.po, sp.spph, d.user_dpb ) AS data1 FROM len_enq_spph_product sp INNER JOIN len_enq_dpb d ON d.dpb = sp.dpb INNER JOIN len_enq_bapb b ON b.po = sp.po WHERE YEAR (sp.mail_date) = YEAR (CURDATE()) AND sp.po IS NOT FALSE AND d.user_dpb = '" + _userdpb + "' ORDER BY sp.dpb DESC;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Item DPB sudah dibuat BAPB";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT DISTINCT sp.dpb AS result FROM len_enq_spph_product sp INNER JOIN len_enq_dpb d ON d.dpb = sp.dpb INNER JOIN len_enq_bapb b ON b.po = sp.po WHERE YEAR (sp.mail_date) = YEAR (CURDATE()) AND sp.po IS NOT FALSE AND d.user_dpb = '" + _userdpb + "' ORDER BY sp.dpb DESC;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Item DPB belum dibuat BAPB
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary6()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT( DISTINCT sp.dpb, sp.product, sp.po, sp.spph, d.user_dpb ) AS data1 FROM len_enq_spph_product sp INNER JOIN len_enq_dpb d ON d.dpb = sp.dpb LEFT JOIN len_enq_bapb b ON b.po = sp.po WHERE YEAR (sp.mail_date) = YEAR (CURDATE()) AND sp.po IS NOT FALSE AND b.bapb IS NULL AND d.user_dpb = '" + _userdpb + "' ORDER BY sp.dpb DESC;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Item DPB belum dibuat BAPB";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT DISTINCT sp.dpb AS result FROM len_enq_spph_product sp INNER JOIN len_enq_dpb d ON d.dpb = sp.dpb LEFT JOIN len_enq_bapb b ON b.po = sp.po WHERE YEAR (sp.mail_date) = YEAR (CURDATE()) AND sp.po IS NOT FALSE AND b.bapb IS NULL AND d.user_dpb = '" + _userdpb + "' ORDER BY sp.dpb DESC;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Kedatangan barang user
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary7()
        {
            SummaryModel model = new SummaryModel();
            string query = "CALL get_coming_goods(1);";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Item";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Keterlambatan barang user
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary8()
        {
            SummaryModel model = new SummaryModel();
            string query = "CALL get_not_green();";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Item";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
    }
}