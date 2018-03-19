using System.Linq;
namespace LenProcurementApp.Models
{
    /// <summary>
    /// transaksi summary bapb, spp, tagihan
    /// </summary>
    public class SummaryTransactionBST
    {
        // database
        private ApplicationDbContext db = new ApplicationDbContext();
        string DPBQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "1" + "?" + "query=";
        string POQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "2" + "?" + "query=";

        /// <summary>
        /// Jumlah (∑) Total Tagihan masuk
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary1()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(payment_method) AS data1 FROM len_payment WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND YEAR(claim_date) = YEAR(CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT po AS result FROM len_payment WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND YEAR(claim_date) = YEAR(CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Tagihan masuk belum dibuat BAPB
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary2()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT( lp.po ) AS data1 FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NULL;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk belum dibuat BAPB";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lp.po AS result FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NULL;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Tagihan masuk sudah dibuat BAPB
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary3()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT( DISTINCT lp.claim_date, leb.bapb, lp.po ) AS data1 FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NOT NULL;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk sudah dibuat BAPB";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lp.po AS result FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NOT NULL;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Tagihan masuk belum dibuat SPP
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary4()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT( lp.po ) AS data1 FROM len_payment lp LEFT JOIN len_spp ls ON ls.po = lp.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND ls.spp_number IS NULL;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk belum dibuat SPP";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lp.po AS result FROM len_payment lp LEFT JOIN len_spp ls ON ls.po = lp.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND ls.spp_number IS NULL;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Tagihan masuk sudah dibuat SPP
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary5()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT( DISTINCT lp.claim_date, ls.spp_number, lp.po ) AS data1 FROM len_payment lp LEFT JOIN len_spp ls ON ls.po = lp.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND ls.spp_number IS NOT NULL;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk sudah dibuat SPP";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lp.po AS result FROM len_payment lp LEFT JOIN len_spp ls ON ls.po = lp.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND ls.spp_number IS NOT NULL;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
    }
}