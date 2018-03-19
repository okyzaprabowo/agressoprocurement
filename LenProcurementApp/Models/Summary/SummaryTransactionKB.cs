using System.Linq;
namespace LenProcurementApp.Models
{
    /// <summary>
    /// transaksi summary kedatangan barang
    /// </summary>
    public class SummaryTransactionKB
    {
        // database
        private ApplicationDbContext db = new ApplicationDbContext();
        string DPBQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "1" + "?" + "query=";
        string POQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "2" + "?" + "query=";

        /// <summary>
        /// ∑ PO belum datang barangnya dan belum jatuh tempo kontrak
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary1()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT DISTINCT COUNT(lep.po) AS data1 FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po WHERE ld.`status` != 'F' AND lep.tgl_habis_kontrak - NOW() > 0 AND YEAR(lep.tgl_habis_kontrak) = YEAR(CURDATE()) ORDER BY lep.tgl_habis_kontrak DESC;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "∑ PO belum datang barangnya & belum jatuh tempo kontrak";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po WHERE ld.`status` != 'F' AND lep.tgl_habis_kontrak - NOW() > 0 AND YEAR(lep.tgl_habis_kontrak) = YEAR(CURDATE()) ORDER BY lep.tgl_habis_kontrak DESC;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// ∑ PO belum datang barangnya dan sudah jatuh tempo kontrak
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary2()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT( DISTINCT lepp.po, lep.tgl_habis_kontrak ) AS data1 FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON ( lpd.po = lepp.po AND lpd.product = lepp.product ) WHERE lep.tgl_habis_kontrak <= CURDATE() AND ((lpd.qty_delivered IS NULL) OR (lepp.qty - lpd.qty_delivered)) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "∑ PO belum datang barangnya & sudah jatuh tempo kontrak";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lepp.po AS result FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON ( lpd.po = lepp.po AND lpd.product = lepp.product ) WHERE lep.tgl_habis_kontrak <= CURDATE() AND ((lpd.qty_delivered IS NULL) OR (lepp.qty - lpd.qty_delivered)) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) PO kedatangan barang parsial
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary3()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT DISTINCT COUNT(ld.po) AS data1 FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po WHERE ld.`status` = 'P' AND lep.tgl_habis_kontrak - NOW() <= 0 AND YEAR(lep.tgl_habis_kontrak) = YEAR(CURDATE()) ORDER BY lep.tgl_habis_kontrak DESC;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) PO kedatangan barang parsial";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT ld.po AS result FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po WHERE ld.`status` = 'P' AND lep.tgl_habis_kontrak - NOW() <= 0 AND YEAR(lep.tgl_habis_kontrak) = YEAR(CURDATE()) ORDER BY lep.tgl_habis_kontrak DESC;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// % Jumlah item Barang datang / item yang Kontraknya sudah jatuh tempo
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary4()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT ROUND( x.full_item / y.habis_kontrak * 100, 2 ) data1 FROM ( SELECT COUNT(lep.po) AS full_item FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON lpd.product = lepp.product WHERE lep.tgl_habis_kontrak < CURDATE() AND lepp.qty = lpd.qty_delivered AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE())) x JOIN ( SELECT COUNT(lep.po) AS habis_kontrak FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON lpd.product = lepp.product WHERE lep.tgl_habis_kontrak < CURDATE() AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE())) y;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "% Jumlah item Barang datang / item yang Kontraknya sudah jatuh tempo";
            model.name2 = "";
            model.link1 = ""; // POQUERY + "SELECT lep.po AS result FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON lpd.product = lepp.product WHERE lep.tgl_habis_kontrak < CURDATE() AND lepp.qty = lpd.qty_delivered AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE())";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }
        // grafik
        /// <summary>
        /// Grafik Summary Struktural
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary5()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(payment_method) AS data1 FROM len_payment WHERE payment_method != 'T/T' AND payment_method != 'L/C';";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "xxx";
            model.name2 = "";
            model.link1 = POQUERY + "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        // grafik
        /// <summary>
        /// Grafik Summary Struktural
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary6()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(payment_method) AS data1 FROM len_payment WHERE payment_method != 'T/T' AND payment_method != 'L/C';";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "xxx";
            model.name2 = "total itemnya";
            model.link1 = DPBQUERY + "";
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
        public SummaryModel GetSummary7()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT p.po) as data1 FROM len_payment p WHERE YEAR (p.claim_date) = YEAR (CURDATE()) AND p.payment_method IS NOT NULL";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT p.po as result FROM len_payment p WHERE YEAR (p.claim_date) = YEAR (CURDATE()) AND p.payment_method IS NOT NULL";
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
        public SummaryModel GetSummary8()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lp.po) AS data1 FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po WHERE YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NULL;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk belum dibuat BAPB";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lp.po AS result FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po WHERE YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NULL;";
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
        public SummaryModel GetSummary9()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lp.po) AS data1 FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po WHERE YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NOT NULL;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk sudah dibuat BAPB";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lp.po AS result FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po WHERE YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NOT NULL;";
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
        public SummaryModel GetSummary10()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL(COUNT(DISTINCT lp.po),0) as data1 FROM len_payment lp LEFT JOIN len_spp ls ON ls.po = lp.po LEFT JOIN len_option_po o ON o.po = lp.po WHERE YEAR (lp.claim_date) = YEAR (CURDATE()) AND ( (o.po_import IS FALSE) || (o.po_import IS NULL) ) AND ls.spp_number IS NULL;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk belum dibuat SPP";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lp.po as result FROM len_payment lp LEFT JOIN len_spp ls ON ls.po = lp.po LEFT JOIN len_option_po o ON o.po = lp.po WHERE YEAR (lp.claim_date) = YEAR (CURDATE()) AND ( (o.po_import IS FALSE) || (o.po_import IS NULL) ) AND ls.spp_number IS NULL;";
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
        public SummaryModel GetSummary11()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL(COUNT(DISTINCT lp.po),0) AS data1 FROM len_payment lp LEFT JOIN len_spp ls ON ls.po = lp.po LEFT JOIN len_option_po o ON o.po = lp.po WHERE YEAR (lp.claim_date) = YEAR (CURDATE()) AND ( (o.po_import IS FALSE) || (o.po_import IS NULL) ) AND ls.spp_number IS NOT NULL;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk sudah dibuat SPP";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lp.po AS result FROM len_payment lp LEFT JOIN len_spp ls ON ls.po = lp.po LEFT JOIN len_option_po o ON o.po = lp.po WHERE YEAR (lp.claim_date) = YEAR (CURDATE()) AND ( (o.po_import IS FALSE) || (o.po_import IS NULL) ) AND ls.spp_number IS NOT NULL;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }

        //struktur lokal
        /// <summary>
        /// ∑ PO belum datang barangnya dan belum jatuh tempo kontrak (struktural lokal)
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary12()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT  lep.po) AS data1 FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po LEFT OUTER JOIN len_option_po o ON o.po = ld.po WHERE ld.`status` != 'F' AND lep.tgl_habis_kontrak - NOW() > 0 AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR(lep.tgl_habis_kontrak) = YEAR(CURDATE()) ORDER BY lep.tgl_habis_kontrak DESC;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "∑ PO belum datang barangnya & belum jatuh tempo kontrak";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po LEFT OUTER JOIN len_option_po o ON o.po = ld.po WHERE ld.`status` != 'F' AND lep.tgl_habis_kontrak - NOW() > 0 AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR(lep.tgl_habis_kontrak) = YEAR(CURDATE()) ORDER BY lep.tgl_habis_kontrak DESC;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// ∑ PO belum datang barangnya dan sudah jatuh tempo kontrak (struktural lokal)
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary13()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) as data1 FROM len_enq_po lep LEFT JOIN len_option_po o ON o.po = lep.po LEFT JOIN len_product_delivered pd ON pd.po = lep.po WHERE pd.po IS NULL AND lep.tgl_habis_kontrak - NOW() <= 0 AND ((o.po_import IS NULL) || (o.po_import IS FALSE)) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE()) ORDER BY lep.po DESC;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "∑ PO belum datang barangnya & sudah jatuh tempo kontrak";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po as result FROM len_enq_po lep LEFT JOIN len_option_po o ON o.po = lep.po LEFT JOIN len_product_delivered pd ON pd.po = lep.po WHERE pd.po IS NULL AND lep.tgl_habis_kontrak - NOW() <= 0 AND ((o.po_import IS NULL) || (o.po_import IS FALSE)) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE()) ORDER BY lep.po DESC;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) PO kedatangan barang parsial (struktural lokal)
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary14()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT  ld.po) AS data1 FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po LEFT JOIN len_option_po o ON o.po = ld.po WHERE ld.`status` = 'P' AND lep.tgl_habis_kontrak - NOW() <= 0 AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR(lep.tgl_habis_kontrak) = YEAR(CURDATE()) ORDER BY lep.tgl_habis_kontrak DESC;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) PO kedatangan barang parsial";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT ld.po AS result FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po LEFT JOIN len_option_po o ON o.po = ld.po WHERE ld.`status` = 'P' AND lep.tgl_habis_kontrak - NOW() <= 0 AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR(lep.tgl_habis_kontrak) = YEAR(CURDATE()) ORDER BY lep.tgl_habis_kontrak DESC;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// % Jumlah item Barang datang / item yang Kontraknya sudah jatuh tempo (struktural lokal)
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary15()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL( ROUND( x.full_item / y.habis_kontrak * 100, 2 ), 2 ) data1 FROM ( SELECT COUNT(lep.po) AS full_item FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON lpd.product = lepp.product LEFT JOIN len_option_po o ON o.po = lep.po WHERE lep.tgl_habis_kontrak < CURDATE() AND lepp.qty = lpd.qty_delivered AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE())) x JOIN ( SELECT COUNT(lep.po) AS habis_kontrak FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON lpd.product = lepp.product LEFT JOIN len_option_po o ON o.po = lep.po WHERE lep.tgl_habis_kontrak < CURDATE() AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE()) AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) )) y;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "% Jumlah item Barang datang / item yang Kontraknya sudah jatuh tempo";
            model.name2 = "";
            model.link1 = //POQUERY + "SELECT lep.po AS result FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON lpd.product = lepp.product WHERE lep.tgl_habis_kontrak < CURDATE() AND lepp.qty = lpd.qty_delivered AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE())";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }
        // grafik
        /// <summary>
        /// Kedatangan Barang Struktural Lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary16()
        {
            SummaryModel model = new SummaryModel();
            string query = "CALL kbslokal();";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "item";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        // grafik
        /// <summary>
        /// Keterlambatan Barang Struktural Lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary17()
        {
            SummaryModel model = new SummaryModel();
            string query = "CALL notgreenlokal();";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "item";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Tagihan masuk lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary18()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT p.po) AS data1 FROM len_payment p LEFT JOIN len_option_po o ON o.po = p.po WHERE p.payment_method != 'T/T' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (p.claim_date) = YEAR (CURDATE()) AND p.payment_method != 'L/C';";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT p.po AS result FROM len_payment p LEFT JOIN len_option_po o ON o.po = p.po WHERE p.payment_method != 'T/T' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (p.claim_date) = YEAR (CURDATE()) AND p.payment_method != 'L/C';";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Tagihan masuk belum dibuat BAPB lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary19()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT( DISTINCT lp.po ) AS data1 FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po LEFT JOIN len_option_po o ON o.po = lp.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NULL;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk belum dibuat BAPB";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lp.po AS result FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po LEFT JOIN len_option_po o ON o.po = lp.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NULL;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Tagihan masuk sudah dibuat BAPB lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary20()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT( DISTINCT lp.po ) AS data1 FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po LEFT JOIN len_option_po o ON o.po = lp.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NOT NULL;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk sudah dibuat BAPB";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lp.po AS result FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po LEFT JOIN len_option_po o ON o.po = lp.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NOT NULL;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Tagihan masuk belum dibuat SPP lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary21()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL(COUNT( DISTINCT lp.po ),0) AS data1 FROM len_payment lp LEFT JOIN len_spp ls ON ls.po = lp.po LEFT JOIN len_option_po o ON o.po = ls.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND ls.spp_number IS NULL;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk belum dibuat SPP";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lp.po  AS result FROM len_payment lp LEFT JOIN len_spp ls ON ls.po = lp.po LEFT JOIN len_option_po o ON o.po = ls.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND ls.spp_number IS NULL;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Tagihan masuk sudah dibuat SPP lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary22()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL(COUNT( DISTINCT lp.po ),0) AS data1 FROM len_payment lp LEFT JOIN len_spp ls ON ls.po = lp.po LEFT JOIN len_option_po o ON o.po = ls.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND ls.spp_number IS NOT NULL;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk sudah dibuat SPP";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lp.po AS result FROM len_payment lp LEFT JOIN len_spp ls ON ls.po = lp.po LEFT JOIN len_option_po o ON o.po = ls.po WHERE payment_method != 'T/T' AND payment_method != 'L/C' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND ls.spp_number IS NOT NULL;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }

        //struktural impor
        /// <summary>
        /// ∑ PO belum datang barangnya dan belum jatuh tempo kontrak impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary23()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON ( lpd.po = lepp.po AND lpd.product = lepp.product ) INNER JOIN len_option_po o ON o.po = lep.po AND lep.tgl_habis_kontrak > CURDATE() AND ((lpd.qty_delivered IS NULL) OR (lepp.qty - lpd.qty_delivered)) AND (o.po_import IS TRUE) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "∑ PO belum datang barangnya & belum jatuh tempo kontrak";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON ( lpd.po = lepp.po AND lpd.product = lepp.product ) INNER JOIN len_option_po o ON o.po = lep.po AND lep.tgl_habis_kontrak > CURDATE() AND ((lpd.qty_delivered IS NULL) OR (lepp.qty - lpd.qty_delivered)) AND (o.po_import IS TRUE) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// ∑ PO belum datang barangnya dan sudah jatuh tempo kontrak impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary24()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON ( lpd.po = lepp.po AND lpd.product = lepp.product ) INNER JOIN len_option_po o ON o.po = lep.po AND lep.tgl_habis_kontrak <= CURDATE() AND ((lpd.qty_delivered IS NULL) OR (lepp.qty - lpd.qty_delivered)) AND (o.po_import IS TRUE) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "∑ PO belum datang barangnya & sudah jatuh tempo kontrak";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON ( lpd.po = lepp.po AND lpd.product = lepp.product ) INNER JOIN len_option_po o ON o.po = lep.po AND lep.tgl_habis_kontrak <= CURDATE() AND ((lpd.qty_delivered IS NULL) OR (lepp.qty - lpd.qty_delivered)) AND (o.po_import IS TRUE) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) PO kedatangan barang parsial impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary25()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT ld.po) AS data1 FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po LEFT JOIN len_option_po o ON o.po = ld.po WHERE ld.`status` = 'P' AND lep.tgl_habis_kontrak - NOW() <= 0 AND (o.po_import IS TRUE) AND YEAR(lep.tgl_habis_kontrak) = YEAR(CURDATE()) ORDER BY lep.tgl_habis_kontrak DESC;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) PO kedatangan barang parsial";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT ld.po AS result FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po LEFT JOIN len_option_po o ON o.po = ld.po WHERE ld.`status` = 'P' AND lep.tgl_habis_kontrak - NOW() <= 0 AND (o.po_import IS TRUE) AND YEAR(lep.tgl_habis_kontrak) = YEAR(CURDATE()) ORDER BY lep.tgl_habis_kontrak DESC;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// % Jumlah item Barang datang / item yang Kontraknya sudah jatuh tempo impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary26()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL( ROUND( x.full_item / y.habis_kontrak * 100, 2 ), 2 ) data1 FROM ( SELECT COUNT(lep.po) AS full_item FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON lpd.product = lepp.product LEFT JOIN len_option_po o ON o.po = lep.po WHERE lep.tgl_habis_kontrak < CURDATE() AND lepp.qty = lpd.qty_delivered AND (o.po_import IS TRUE) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE())) x JOIN ( SELECT COUNT(lep.po) AS habis_kontrak FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON lpd.product = lepp.product LEFT JOIN len_option_po o ON o.po = lep.po WHERE lep.tgl_habis_kontrak < CURDATE() AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE()) AND (o.po_import IS TRUE)) y;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "% Jumlah item Barang datang / item yang Kontraknya sudah jatuh tempo";
            model.name2 = "";
            model.link1 = //POQUERY + "SELECT lep.po AS result FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON lpd.product = lepp.product WHERE lep.tgl_habis_kontrak < CURDATE() AND lepp.qty = lpd.qty_delivered AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE())";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Tagihan masuk belum dibuat BAPB impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary27()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lp.po) AS data1 FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po LEFT JOIN len_option_po o ON o.po = lp.po WHERE ( payment_method = 'T/T' || payment_method = 'L/C' ) AND (o.po_import IS TRUE) AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NULL;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk belum dibuat BAPB";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lp.po AS result FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po LEFT JOIN len_option_po o ON o.po = lp.po WHERE ( payment_method = 'T/T' || payment_method = 'L/C' ) AND (o.po_import IS TRUE) AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NULL;;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Tagihan masuk sudah dibuat BAPB impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary28()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lp.po) AS data1 FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po LEFT JOIN len_option_po o ON o.po = lp.po WHERE ( payment_method = 'T/T' || payment_method = 'L/C' ) AND (o.po_import IS TRUE) AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NOT NULL;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk sudah dibuat BAPB";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lp.po AS result FROM len_payment lp LEFT JOIN len_enq_bapb leb ON leb.po = lp.po LEFT JOIN len_option_po o ON o.po = lp.po WHERE ( payment_method = 'T/T' || payment_method = 'L/C' ) AND (o.po_import IS TRUE) AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND leb.bapb IS NOT NULL;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        // grafik
        /// <summary>
        /// Kedatangan Barang impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary29()
        {
            SummaryModel model = new SummaryModel();
            string query = "CALL kbsimpor();";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "item";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        // grafik impor
        /// <summary>
        /// keterlambatan barang impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary30()
        {
            SummaryModel model = new SummaryModel();
            string query = "CALL notgreenimpor();";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "item";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Tagihan masuk impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary31()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT p.po) AS data1 FROM len_payment p LEFT JOIN len_option_po o ON o.po = p.po WHERE ( p.payment_method = 'T/T' OR p.payment_method = 'L/C' ) AND (o.po_import IS TRUE) AND YEAR (p.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Tagihan masuk";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT p.po AS result FROM len_payment p LEFT JOIN len_option_po o ON o.po = p.po WHERE ( p.payment_method = 'T/T' OR p.payment_method = 'L/C' ) AND (o.po_import IS TRUE) AND YEAR (p.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }

    }
}