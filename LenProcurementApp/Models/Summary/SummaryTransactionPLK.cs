using System.Linq;
namespace LenProcurementApp.Models
{
    /// <summary>
    /// transaksi summary pelaksana
    /// </summary>
    public class SummaryTransactionPLK
    {
        // database
        private ApplicationDbContext db = new ApplicationDbContext();
        string DPBQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "1" + "?" + "query=";
        string POQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "2" + "?" + "query=";

        string _initial = null;
        /// <summary>
        /// contructor
        /// </summary>
        /// <param name="initial"></param>
        public SummaryTransactionPLK(string initial)
        {
            _initial = initial;
        }
        /// <summary>
        /// Jumlah (∑) DPB total
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary1()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.dpb) AS data1 FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_option_po op ON op.po = s.po WHERE p.initials = '" + _initial + "' AND ( (op.po_import IS NULL) || (op.po_import IS FALSE) ) AND YEAR (e.created_at) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) DPB total";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT DISTINCT s.dpb AS result FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_option_po op ON op.po = s.po WHERE p.initials = '" + _initial + "' AND ( (op.po_import IS NULL) || (op.po_import IS FALSE) ) AND YEAR (e.created_at) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) SPPH total
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary2()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.spph) AS data1 FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_option_po op ON op.po = s.po WHERE p.initials = '" + _initial + "' AND ( (op.po_import IS NULL) || (op.po_import IS FALSE) ) AND YEAR (e.created_at) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) SPPH total";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT DISTINCT s.dpb AS result, s.spph FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_option_po op ON op.po = s.po WHERE p.initials = '" + _initial + "' AND ( (op.po_import IS NULL) || (op.po_import IS FALSE) ) AND YEAR (e.created_at) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) DPB belum SPPH
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary3()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT e.dpb) AS data1 FROM len_executor e JOIN len_profile p ON p.user_id = e.executor LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb WHERE p.initials = '" + _initial + "' AND s.spph IS NULL AND YEAR (e.created_at) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) DPB belum SPPH";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT e.dpb AS result FROM len_executor e JOIN len_profile p ON p.user_id = e.executor LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb WHERE p.initials = '" + _initial + "' AND s.spph IS NULL AND YEAR (e.created_at) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) PO total
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary4()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.po) AS data1 FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po WHERE p.initials = '" + _initial + "' AND s.po IS NOT FALSE AND YEAR (e.created_at) = YEAR (CURDATE()) AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) );";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) PO total";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT s.po AS result FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po WHERE p.initials = '" + _initial + "' AND s.po IS NOT FALSE AND YEAR (e.created_at) = YEAR (CURDATE()) AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) );";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) DPB belum PO
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary5()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT e.dpb) AS data1 FROM len_executor e JOIN len_profile p ON p.user_id = e.executor LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po WHERE p.initials = '" + _initial + "' AND ((s.po = '0') || (s.po IS NULL)) AND YEAR (e.created_at) = YEAR (CURDATE()) AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) );";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) DPB belum PO";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT DISTINCT e.dpb AS result FROM len_executor e JOIN len_profile p ON p.user_id = e.executor LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po WHERE p.initials = '" + _initial + "' AND ((s.po = '0') || (s.po IS NULL)) AND YEAR (e.created_at) = YEAR (CURDATE()) AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) );";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }

        // Lokal
        /// <summary>
        /// Jumlah (∑) Kontrak akan berakhir kurang dari 1 bulan lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary8()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.po) AS data1 FROM len_executor e LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_po p ON p.po = s.po LEFT JOIN len_profile o ON o.user_id = e.executor LEFT JOIN len_option_po op ON op.po = p.po WHERE o.initials = '" + _initial + "' AND DATEDIFF(NOW(), p.tgl_habis_kontrak) BETWEEN - 30 AND - 1 AND YEAR (p.tgl_habis_kontrak) = YEAR (CURDATE()) AND ( (op.po_import IS NULL) || (op.po_import IS FALSE) );";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Kontrak akan berakhir < 1 bulan";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT s.po AS result FROM len_executor e LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_po p ON p.po = s.po LEFT JOIN len_profile o ON o.user_id = e.executor LEFT JOIN len_option_po op ON op.po = p.po WHERE o.initials = '" + _initial + "' AND DATEDIFF(NOW(), p.tgl_habis_kontrak) BETWEEN - 30 AND -1 AND YEAR (p.tgl_habis_kontrak) = YEAR (CURDATE()) AND ( (op.po_import IS NULL) || (op.po_import IS FALSE) );";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Jaminan Pelaksanaan akan berakhir kurang dari 1 bulan lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary9()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.po, p.tgl_habis_kontrak) AS data1 FROM len_executor e LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_po p ON p.po = s.po LEFT JOIN len_profile o ON o.user_id = e.executor LEFT JOIN len_option_po op ON op.po = s.po WHERE o.initials = '" + _initial + "' AND op.jaminan_pelaksanaan = 1 AND op.po_import = 0 AND DATEDIFF(NOW(), op.tgl_pelaksanaan) BETWEEN - 30 AND -1 AND YEAR (p.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Jaminan Pelaksanaan akan berakhir < 1 bulan";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT s.po AS result FROM len_executor e LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_po p ON p.po = s.po LEFT JOIN len_profile o ON o.user_id = e.executor LEFT JOIN len_option_po op ON op.po = s.po WHERE o.initials = '" + _initial + "' AND op.jaminan_pelaksanaan = 1 AND op.po_import = 0 AND DATEDIFF(NOW(), op.tgl_pelaksanaan) BETWEEN - 30 AND -1 AND YEAR (p.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Jaminan Uang Muka akan berakhir kurang dari 1 bulan lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary10()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.po) AS data1 FROM len_executor e LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_po p ON p.po = s.po LEFT JOIN len_profile o ON o.user_id = e.executor LEFT JOIN len_option_po op ON op.po = s.po WHERE o.initials = '" + _initial + "' AND op.jaminan_um = 1 AND op.po_import = 0 AND DATEDIFF(NOW(), op.tgl_um) BETWEEN - 30 AND -1 AND YEAR (p.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Jaminan Uang Muka akan berakhir < 1 bulan";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT s.po AS result FROM len_executor e LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_po p ON p.po = s.po LEFT JOIN len_profile o ON o.user_id = e.executor LEFT JOIN len_option_po op ON op.po = s.po WHERE o.initials = '" + _initial + "' AND op.jaminan_um = 1 AND op.po_import = 0 AND DATEDIFF(NOW(), op.tgl_um) BETWEEN - 30 AND -1 AND YEAR (p.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// ∑ Jaminan Pelaksanaan akan berakhir kurang dari tgl habis kontrak amandemen lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary11()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.po) AS data1 FROM len_executor e INNER JOIN len_enq_spph s ON s.dpb = e.dpb INNER JOIN len_enq_po p ON p.po = s.po INNER JOIN len_profile o ON o.user_id = e.executor INNER JOIN len_option_po op ON op.po = s.po WHERE o.initials = '" + _initial + "' AND op.amendment IS TRUE AND op.jaminan_pelaksanaan = 1 AND op.po_import = 0 AND op.tgl_pelaksanaan < p.tgl_habis_kontrak AND YEAR (p.tgl_habis_kontrak) = YEAR (CURDATE())";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "∑ Jaminan Pelaksanaan akan berakhir < tgl habis kontrak amandemen";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT s.po AS result FROM len_executor e INNER JOIN len_enq_spph s ON s.dpb = e.dpb INNER JOIN len_enq_po p ON p.po = s.po INNER JOIN len_profile o ON o.user_id = e.executor INNER JOIN len_option_po op ON op.po = s.po WHERE o.initials = '" + _initial + "' AND op.amendment IS TRUE AND op.jaminan_pelaksanaan = 1 AND op.po_import = 0 AND op.tgl_pelaksanaan < p.tgl_habis_kontrak AND YEAR (p.tgl_habis_kontrak) = YEAR (CURDATE())";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Prosentase (%) Jumlah (∑) item barang datang / Kontrak sudah jatuh tempo lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary12()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL( ROUND( y.habis_kontrak / x.full_item * 100, 2 ), 0 ) AS data1 FROM ( SELECT COUNT( DISTINCT e.dpb, s.po, lepp.product, lepp.qty ) AS full_item FROM len_executor e LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_po lep ON lep.po = s.po LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_profile o ON o.user_id = e.executor LEFT JOIN len_option_po op ON op.po = s.po WHERE o.initials = '" + _initial + "' AND op.po_import IS FALSE AND DATE(lep.tgl_habis_kontrak) <= DATE(CURDATE()) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE())) x JOIN ( SELECT COUNT( DISTINCT e.dpb, s.po, lepp.product, lepp.qty ) AS habis_kontrak FROM len_executor e LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_po lep ON lep.po = s.po LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_profile o ON o.user_id = e.executor LEFT JOIN len_product_delivered pd ON (pd.po = s.po AND lepp.product = pd.product) LEFT JOIN len_option_po op ON op.po = s.po WHERE o.initials = '" + _initial + "' AND DATE(lep.tgl_habis_kontrak) <= DATE(CURDATE()) AND lepp.qty = pd.qty_delivered AND op.po_import IS FALSE AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE())) y;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Prosentase (%) Jumlah (∑) item barang datang / Kontrak sudah jatuh tempo";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total PO sudah dibuat BAPB lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary13()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.po) AS data1 FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_bapb ba ON ba.po = s.po JOIN len_option_po op ON op.po = s.po WHERE p.initials = '" + _initial + "' AND op.po_import = 0 AND ba.bapb IS NOT NULL AND YEAR (e.created_at) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total PO sudah dibuat BAPB";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT s.po AS result FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_bapb ba ON ba.po = s.po JOIN len_option_po op ON op.po = s.po WHERE p.initials = '" + _initial + "' AND op.po_import = 0 AND ba.bapb IS NOT NULL AND YEAR (e.created_at) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total PO belum dibuat BAPB lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary14()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT e.dpb, s.po) AS data1 FROM len_executor e JOIN len_profile p ON p.user_id = e.executor LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_bapb ba ON ba.po = s.po JOIN len_option_po op ON op.po = s.po WHERE p.initials = '" + _initial + "' AND s.po != 0 AND op.po_import = 0 AND ba.bapb IS NULL AND YEAR (e.created_at) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total PO belum dibuat BAPB";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT s.po AS result FROM len_executor e JOIN len_profile p ON p.user_id = e.executor LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_bapb ba ON ba.po = s.po JOIN len_option_po op ON op.po = s.po WHERE p.initials = '" + _initial + "' AND s.po != 0 AND op.po_import = 0 AND ba.bapb IS NULL AND YEAR (e.created_at) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total PO sudah dibuat SPP lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary15()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.po) AS data1 FROM len_executor e JOIN len_profile p ON p.user_id = e.executor LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_spp sp ON sp.po = s.po LEFT JOIN len_option_po op ON op.po = s.po WHERE p.initials = '" + _initial + "' AND op.po_import IS FALSE AND sp.spp_number IS NOT NULL AND YEAR (e.created_at) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total PO sudah dibuat SPP";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT s.po AS result FROM len_executor e JOIN len_profile p ON p.user_id = e.executor LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_spp sp ON sp.po = s.po LEFT JOIN len_option_po op ON op.po = s.po WHERE p.initials = '" + _initial + "' AND op.po_import IS FALSE AND sp.spp_number IS NOT NULL AND YEAR (e.created_at) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total PO belum dibuat SPP lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary16()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.po) AS data1 FROM len_executor e JOIN len_profile p ON p.user_id = e.executor LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_spp sp ON sp.po = s.po LEFT JOIN len_option_po op ON op.po = s.po WHERE p.initials = '" + _initial + "' AND op.po_import IS FALSE AND sp.spp_number IS NULL AND YEAR (e.created_at) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total PO belum dibuat SPP";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT s.po AS result FROM len_executor e JOIN len_profile p ON p.user_id = e.executor LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_spp sp ON sp.po = s.po LEFT JOIN len_option_po op ON op.po = s.po WHERE p.initials = '" + _initial + "' AND op.po_import IS FALSE AND sp.spp_number IS NULL AND YEAR (e.created_at) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }

        // Impor
        /// <summary>
        /// Jumlah (∑) DPB Total impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary17()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT e.dpb) as data1 FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb JOIN len_option_po po ON po.po = s.po WHERE p.initials = '" + _initial + "' AND YEAR (e.created_at) = YEAR (CURDATE()) AND po.po_import IS TRUE;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) DPB Total";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT DISTINCT e.dpb as result FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb JOIN len_option_po po ON po.po = s.po WHERE p.initials = '" + _initial + "' AND YEAR (e.created_at) = YEAR (CURDATE()) AND po.po_import IS TRUE;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) SPPH Total impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary18()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.spph) AS data1 FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb JOIN len_option_po po ON po.po = s.po WHERE p.initials = '" + _initial + "' AND YEAR (e.created_at) = YEAR (CURDATE()) AND po.po_import IS TRUE;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) SPPH Total";
            model.name2 = "";
            model.link1 = DPBQUERY + "SELECT DISTINCT s.dpb AS result FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb JOIN len_option_po po ON po.po = s.po WHERE p.initials = '" + _initial + "' AND YEAR (e.created_at) = YEAR (CURDATE()) AND po.po_import IS TRUE;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) PO Total impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary19()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.po) AS data1 FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb JOIN len_option_po po ON po.po = s.po WHERE p.initials = '" + _initial + "' AND YEAR (e.created_at) = YEAR (CURDATE()) AND po.po_import IS TRUE;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) PO Total";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT s.po AS result FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb JOIN len_option_po po ON po.po = s.po WHERE p.initials = '" + _initial + "' AND YEAR (e.created_at) = YEAR (CURDATE()) AND po.po_import IS TRUE;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) UM Impor yang diajukan impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary22()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.po) AS data1 FROM len_executor e INNER JOIN len_enq_spph_product s ON s.dpb = e.dpb INNER JOIN len_profile o ON o.user_id = e.executor INNER JOIN len_option_po lop ON lop.po = s.po INNER JOIN len_payment pay ON pay.po = s.po WHERE o.initials = '" + _initial + "' AND lop.po_import = 1 AND pay.payment = 'Uang Muka' AND YEAR (s.mail_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) UM Impor yang diajukan";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT s.po AS result FROM len_executor e INNER JOIN len_enq_spph_product s ON s.dpb = e.dpb INNER JOIN len_profile o ON o.user_id = e.executor INNER JOIN len_option_po lop ON lop.po = s.po INNER JOIN len_payment pay ON pay.po = s.po WHERE o.initials = '" + _initial + "' AND lop.po_import = 1 AND pay.payment = 'Uang Muka' AND YEAR (s.mail_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) UM Impor sudah dibayar impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary23()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.po) AS data1 FROM len_executor e INNER JOIN len_enq_spph_product s ON s.dpb = e.dpb INNER JOIN len_profile o ON o.user_id = e.executor INNER JOIN len_option_po lop ON lop.po = s.po INNER JOIN len_payment pay ON pay.po = s.po WHERE o.initials = '" + _initial + "' AND lop.po_import = 1 AND pay.payment = 'Uang Muka' AND pay.`status` IS TRUE AND YEAR (s.mail_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) UM Impor sudah dibayar";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT s.po AS result FROM len_executor e INNER JOIN len_enq_spph_product s ON s.dpb = e.dpb INNER JOIN len_profile o ON o.user_id = e.executor INNER JOIN len_option_po lop ON lop.po = s.po INNER JOIN len_payment pay ON pay.po = s.po WHERE o.initials = '" + _initial + "' AND lop.po_import = 1 AND pay.payment = 'Uang Muka' AND pay.`status` IS TRUE AND YEAR (s.mail_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) UM Impor yang diajukan > 1 minggu impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary24()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.po) AS data1 FROM len_executor e INNER JOIN len_enq_spph_product s ON s.dpb = e.dpb INNER JOIN len_profile o ON o.user_id = e.executor INNER JOIN len_option_po lop ON lop.po = s.po INNER JOIN len_payment pay ON pay.po = s.po WHERE o.initials = '" + _initial + "' AND lop.po_import = 1 AND pay.payment = 'Uang Muka' AND pay. STATUS IS FALSE AND DATEDIFF(pay.claim_date, NOW()) < - 7 AND YEAR (s.mail_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) UM Impor yang diajukan > 1 minggu";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT s.po AS result FROM len_executor e INNER JOIN len_enq_spph_product s ON s.dpb = e.dpb INNER JOIN len_profile o ON o.user_id = e.executor INNER JOIN len_option_po lop ON lop.po = s.po INNER JOIN len_payment pay ON pay.po = s.po WHERE o.initials = '" + _initial + "' AND lop.po_import = 1 AND pay.payment = 'Uang Muka' AND pay. STATUS IS FALSE AND DATEDIFF(pay.claim_date, NOW()) < - 7 AND YEAR (s.mail_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Prosentase (%) Jumlah (∑) item barang datang / PO sudah jatuh tempo impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary25()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL( ROUND( y.habis_kontrak / x.full_item * 100, 2 ), 0 ) data1 FROM ( SELECT COUNT(lep.po) AS full_item FROM len_executor e INNER JOIN len_profile o ON o.user_id = e.executor INNER JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_po lep ON lep.po = s.po LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON lpd.product = lepp.product INNER JOIN len_option_po op ON op.po = lep.po WHERE o.initials = '" + _initial + "' AND DATE(lep.tgl_habis_kontrak) <= DATE(CURDATE()) AND op.po_import IS TRUE AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE())) x JOIN ( SELECT COUNT(lep.po) AS habis_kontrak FROM len_executor e INNER JOIN len_profile o ON o.user_id = e.executor INNER JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_po lep ON lep.po = s.po LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON lpd.product = lepp.product INNER JOIN len_option_po op ON op.po = lep.po WHERE o.initials = '" + _initial + "' AND DATE(lep.tgl_habis_kontrak) <= DATE(CURDATE()) AND op.po_import IS TRUE AND lepp.qty = lpd.qty_delivered AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE())) y;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Prosentase (%) Jumlah (∑) item barang datang / PO sudah jatuh tempo";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total PO sudah dibuat BAPB impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary26()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.po) AS data1 FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb JOIN len_option_po po ON po.po = s.po LEFT JOIN len_enq_bapb ba ON ba.po = s.po WHERE p.initials = '" + _initial + "' AND ba.bapb IS NOT NULL AND po.po_import IS TRUE AND YEAR (e.created_at) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total PO sudah dibuat BAPB";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT s.po AS result FROM len_executor e JOIN len_profile p ON p.user_id = e.executor JOIN len_enq_spph_product s ON s.dpb = e.dpb JOIN len_option_po po ON po.po = s.po LEFT JOIN len_enq_bapb ba ON ba.po = s.po WHERE p.initials = '" + _initial + "' AND ba.bapb IS NOT NULL AND po.po_import IS TRUE AND YEAR (e.created_at) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total PO belum dibuat BAPB
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary27()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT s.po) AS data1 FROM len_executor e JOIN len_profile p ON p.user_id = e.executor LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_bapb ba ON ba.po = s.po JOIN len_option_po po ON po.po = s.po WHERE p.initials = '" + _initial + "' AND ba.bapb IS NULL AND po.po_import IS TRUE AND YEAR (e.created_at) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total PO belum dibuat BAPB";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT s.po AS result FROM len_executor e JOIN len_profile p ON p.user_id = e.executor LEFT JOIN len_enq_spph_product s ON s.dpb = e.dpb LEFT JOIN len_enq_bapb ba ON ba.po = s.po JOIN len_option_po po ON po.po = s.po WHERE p.initials = '" + _initial + "' AND ba.bapb IS NULL AND po.po_import IS TRUE AND YEAR (e.created_at) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
    }
}