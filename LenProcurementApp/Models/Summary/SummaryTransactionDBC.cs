using System.Linq;
namespace LenProcurementApp.Models
{
    /// <summary>
    /// transaksi summary database center atau admin
    /// </summary>
    public class SummaryTransactionDBC
    {
        // database
        private ApplicationDbContext db = new ApplicationDbContext();
        string DPBQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "1" + "?" + "query=";
        string POQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "2" + "?" + "query=";

        /// <summary>
        /// Jumlah (∑) DPB total
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary1()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT d.dpb) AS data1 FROM len_enq_dpb_product d LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE YEAR (d.updated) = YEAR (CURDATE()) AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) DPB total";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT DISTINCT d.dpb AS result FROM len_enq_dpb_product d LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE YEAR (d.updated) = YEAR (CURDATE()) AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) DPB sudah ada Pelaksananya
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary2()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT d.dpb) AS data1 FROM len_enq_dpb d LEFT JOIN len_executor pd ON pd.dpb = d.dpb LEFT JOIN len_profile lp ON lp.user_id = pd.executor WHERE YEAR (d.tgl_pengajuan) = YEAR (CURDATE()) AND pd.executor IS NOT NULL;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) DPB sudah ada Pelaksananya";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT DISTINCT d.dpb AS result FROM len_enq_dpb d LEFT JOIN len_executor pd ON pd.dpb = d.dpb LEFT JOIN len_profile lp ON lp.user_id = pd.executor WHERE YEAR (d.tgl_pengajuan) = YEAR (CURDATE()) AND pd.executor IS NOT NULL;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) DPB belum ada Pelaksananya
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary3()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT d.dpb) as data1 FROM len_enq_dpb_product d LEFT JOIN len_executor pd ON pd.dpb = d.dpb LEFT JOIN len_profile lp ON lp.user_id = pd.executor LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE YEAR (d.updated) = YEAR (CURDATE()) AND pd.executor IS NULL AND dp.`status` != 'C' AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE()) ORDER BY d.dpb DESC";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) DPB belum ada Pelaksananya";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT DISTINCT d.dpb as result FROM len_enq_dpb_product d LEFT JOIN len_executor pd ON pd.dpb = d.dpb LEFT JOIN len_profile lp ON lp.user_id = pd.executor LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE YEAR (d.updated) = YEAR (CURDATE()) AND pd.executor IS NULL AND dp.`status` != 'C' AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE()) ORDER BY d.dpb DESC";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Impor TT total
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary4()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(*) AS data1 FROM len_payment lp LEFT JOIN len_option_po op ON op.po = lp.po WHERE payment_method = 'T/T' AND po_import IS TRUE AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Impor TT total";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lp.po AS result FROM len_payment lp LEFT JOIN len_option_po op ON op.po = lp.po WHERE payment_method = 'T/T' AND po_import IS TRUE AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Impor TT > 1 bulan belum pertanggungjawaban
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary5()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(claim_date) AS data1 FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE (claim_date) <= (NOW() - INTERVAL 30 DAY) AND o.po_import IS TRUE AND payment_method = 'T/T' AND STATUS = '0' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Impor TT > 1 bulan belum pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lp.po AS result FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE (claim_date) <= (NOW() - INTERVAL 30 DAY) AND o.po_import IS TRUE AND payment_method = 'T/T' AND STATUS = '0' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Impor TT sudah pertanggungjawaban
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary6()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(*) AS data1 FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE payment_method = 'T/T' AND o.po_import IS TRUE AND STATUS = '1' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Impor TT sudah pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lp.po AS result FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE payment_method = 'T/T' AND o.po_import IS TRUE AND STATUS = '1' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Impor LC total
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary7()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(*) AS data1 FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE payment_method = 'L/C' AND o.po_import IS TRUE AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Impor LC total";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lp.po AS result FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE payment_method = 'L/C' AND o.po_import IS TRUE AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Impor LC > 1 bulan belum pertanggungjawaban
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary8()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(lp.claim_date) AS data1 FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE (claim_date) <= (NOW() - INTERVAL 30 DAY) AND payment_method = 'L/C' AND o.po_import IS TRUE AND STATUS = '0' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Impor LC > 1 bulan belum pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lp.po AS result FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE (claim_date) <= (NOW() - INTERVAL 30 DAY) AND payment_method = 'L/C' AND o.po_import IS TRUE AND STATUS = '0' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Impor LC sudah pertanggungjawaban
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary9()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(*) AS data1 FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE payment_method = 'L/C' AND o.po_import IS TRUE  AND STATUS = '1' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Impor LC sudah pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lp.po AS result FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE payment_method = 'L/C' AND o.po_import IS TRUE AND STATUS = '1' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Pajak Impor total
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary10()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(lop.po) AS data1 FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (lp.claim_date) = YEAR (CURDATE()) ORDER BY lop.po;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Pajak Impor total";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lop.po as result FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (lp.claim_date) = YEAR (CURDATE()) ORDER BY lop.po;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Pajak Impor > 1 bulan belum pertanggungjawaban
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary11()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(lop.po) AS data1 FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (CURDATE()) AND (claim_date) <= (NOW() - INTERVAL 30 DAY) AND STATUS = '0';";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Pajak Impor > 1 bulan belum pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lop.po as result FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (CURDATE()) AND (claim_date) <= (NOW() - INTERVAL 30 DAY) AND STATUS = '0';";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Pajak Impor sudah pertanggungjawaban
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary12()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(lop.po) AS data1 FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (CURDATE()) AND STATUS = '1';";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Pajak Impor sudah pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lop.po as result FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (CURDATE()) AND STATUS = '1';";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) DPB sudah di delete datanya karena pembatalan
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary13()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT d.dpb, dp.dpb) as data1 FROM len_enq_dpb_product d LEFT JOIN len_executor pd ON pd.dpb = d.dpb LEFT JOIN len_profile lp ON lp.user_id = pd.executor LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE YEAR (d.updated) = YEAR (CURDATE()) AND pd.executor IS NULL AND dp.`status` = 'C' AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE()) ORDER BY d.dpb DESC";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) DPB sudah di delete datanya karena pembatalan";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT DISTINCT d.dpb as result FROM len_enq_dpb_product d LEFT JOIN len_executor pd ON pd.dpb = d.dpb LEFT JOIN len_profile lp ON lp.user_id = pd.executor LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE YEAR (d.updated) = YEAR (CURDATE()) AND pd.executor IS NULL AND dp.`status` = 'C' AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE()) ORDER BY d.dpb DESC";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Tagihan masuk
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary14()
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
    }
}