using System.Linq;
namespace LenProcurementApp.Models
{
    /// <summary>
    /// transaksi summary dpb
    /// </summary>
    public class SummaryTransactionDPB
    {
        // database
        private ApplicationDbContext db = new ApplicationDbContext();
        //const string URL = "/Dashboard/SearchDetail/";
        //const string DPBq = "1";
        //const string POq = "2";
        //const string MARK = "?";
        //const string MARKq = "query=";
        string DPBQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "1" + "?" + "query=";
        string POQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "2" + "?" + "query=";
        /// <summary>
        /// Jumlah (∑) DPB total pada hari ini 
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary1()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL(COUNT(DISTINCT led.dpb),0) AS data1, IFNULL(COUNT( DISTINCT led.dpb, ledp.product ),0) AS data2 FROM len_enq_dpb led INNER JOIN len_enq_dpb_product ledp ON ledp.dpb = led.dpb WHERE tgl_pengajuan = CURDATE();";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) DPB total pada hari ini";
            model.name2 = "total itemnya";
            model.link1 = DPBQUERY + "SELECT DISTINCT led.dpb AS result FROM len_enq_dpb led INNER JOIN len_enq_dpb_product ledp ON ledp.dpb = led.dpb WHERE tgl_pengajuan = CURDATE();";
            model.link2 = DPBQUERY + "SELECT DISTINCT led.dpb AS result FROM len_enq_dpb led INNER JOIN len_enq_dpb_product ledp ON ledp.dpb = led.dpb WHERE tgl_pengajuan = CURDATE();";
            model.data1 = result.data1;
            model.data2 = result.data2;
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) DPB total bulan ini dan dpb total yang masuk pada tahun ini
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary2()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT * FROM ( SELECT COUNT(DISTINCT d.dpb) AS data1 FROM len_enq_dpb_product d LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE YEAR (d.updated) = YEAR (CURDATE()) AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE())) x CROSS JOIN ( SELECT COUNT(DISTINCT d.dpb) AS data2 FROM len_enq_dpb_product d LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE MONTH (d.updated) = MONTH (CURDATE()) AND MONTH (dp.tgl_pengajuan) = MONTH (CURDATE()) AND YEAR (d.updated) = YEAR (CURDATE()) AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE())) z;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) DPB total bulan ini";
            model.name2 = "dpb total yang masuk pada tahun ini";
            model.link1 = DPBQUERY + "SELECT DISTINCT d.dpb AS result FROM len_enq_dpb_product d LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE MONTH (d.updated) = MONTH (CURDATE()) AND MONTH (dp.tgl_pengajuan) = MONTH (CURDATE()) AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE()) AND YEAR (updated) = YEAR (CURDATE());";
            model.link2 = DPBQUERY + "SELECT DISTINCT d.dpb AS result FROM len_enq_dpb_product d LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE YEAR (d.updated) = YEAR (CURDATE()) AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE());";
            model.data1 = result.data2;
            model.data2 = result.data1;
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Prosentase (%) Jumlah (∑) Total item sudah SPPH/ Total item
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary3()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT ROUND( total_produk_spph / total_produk * 100, 2 ) AS data1 FROM ( SELECT COUNT(product) AS total_produk FROM len_enq_dpb_product WHERE YEAR (updated) = YEAR (CURDATE())) x CROSS JOIN ( SELECT COUNT(product) AS total_produk_spph FROM len_enq_dpb_product WHERE spph <> 0 AND YEAR (updated) = YEAR (CURDATE())) z;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Prosentase (%) Jumlah (∑) Total item sudah SPPH/ Total item";
            model.name2 = "";
            model.link1 = ""; //DPBQUERY + "SELECT DISTINCT dpb AS result FROM len_enq_dpb_product WHERE spph <> 0 AND YEAR (updated) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }
        /// <summary>
        /// Prosentase (%) Jumlah (∑) Total item sudah PO/ Total item
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary4()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT ROUND( z.total_item_po / x.total_item * 100, 2 ) AS data1 FROM ( SELECT COUNT(lepp.product) AS total_item_po FROM len_enq_po_product lepp WHERE YEAR (lepp.tgl_po) = YEAR (CURDATE())) z CROSS JOIN ( SELECT COUNT(ledp.product) AS total_item FROM len_enq_dpb_product ledp WHERE YEAR (ledp.updated) = YEAR (CURDATE())) x;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Prosentase (%) Jumlah (∑) Total item sudah PO/ Total item";
            model.name2 = "";
            model.link1 = ""; // POQUERY + "SELECT DISTINCT lepp.po AS result FROM len_enq_po_product lepp WHERE YEAR (lepp.tgl_po) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }
        /// <summary>
        /// Prosentase (%) Efektifitas/ Kecepatan Layanan
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary5()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL( ROUND(efektif / total * 100, 2), 0 ) AS data1 FROM ( SELECT COUNT( DISTINCT led.dpb, led.po, le.created_at, lp.tgl_po, DATEDIFF(le.created_at, lp.tgl_po)) AS efektif FROM len_enq_spph_product led JOIN len_executor le ON le.dpb = led.dpb JOIN len_enq_dpb d ON d.dpb = le.dpb JOIN len_enq_po lp ON lp.po = led.po WHERE DATEDIFF(le.created_at, lp.tgl_po) BETWEEN 0 AND 21 AND YEAR (d.tgl_pengajuan) = YEAR (CURDATE()) AND le.updated_at IS NOT NULL ) x CROSS JOIN ( SELECT COUNT(DISTINCT led.dpb) AS total FROM len_enq_dpb led INNER JOIN len_executor le ON le.dpb = led.dpb WHERE YEAR (led.tgl_pengajuan) = YEAR (CURDATE()) AND le.updated_at IS NOT NULL ) y;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Prosentase (%) Efektifitas/ Kecepatan Layanan";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }

        // struktural lokal
        /// <summary>
        /// Jumlah (∑) DPB total pada hari ini (Struktural Lokal)
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary6()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL(COUNT(DISTINCT led.dpb),0) AS data1, IFNULL(COUNT( DISTINCT led.dpb, ledp.product ),0) AS data2 FROM len_enq_dpb led INNER JOIN len_enq_dpb_product ledp ON ledp.dpb = led.dpb INNER JOIN len_enq_spph s ON ledp.dpb = s.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po WHERE YEAR (led.tgl_pengajuan) = (CURDATE()) AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) );";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) DPB total pada hari ini";
            model.name2 = "total itemnya";
            model.link1 = DPBQUERY + "SELECT DISTINCT led.dpb AS result FROM len_enq_dpb led INNER JOIN len_enq_dpb_product ledp ON ledp.dpb = led.dpb INNER JOIN len_enq_spph s ON ledp.dpb = s.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po WHERE YEAR (tgl_pengajuan) = (CURDATE()) AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) );";
            model.link2 = DPBQUERY + "SELECT DISTINCT led.dpb AS result FROM len_enq_dpb led INNER JOIN len_enq_dpb_product ledp ON ledp.dpb = led.dpb INNER JOIN len_enq_spph s ON ledp.dpb = s.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po WHERE YEAR (tgl_pengajuan) = (CURDATE()) AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) );";
            model.data1 = result.data1;
            model.data2 = result.data2;
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) DPB total bulan ini dan dpb total yang masuk pada tahun ini (Struktural Lokal)
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary7()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT * FROM ( SELECT COUNT(DISTINCT d.dpb) AS data1 FROM len_enq_dpb_product d LEFT JOIN len_enq_spph s ON s.dpb = d.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE YEAR (updated) = YEAR (CURDATE()) AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE()) AND (o.po_import IS NULL) || (o.po_import IS FALSE) ) x CROSS JOIN ( SELECT COUNT(DISTINCT d.dpb) AS data2 FROM len_enq_dpb_product d LEFT JOIN len_enq_spph s ON s.dpb = d.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE MONTH (updated) = MONTH (CURDATE()) AND MONTH (dp.tgl_pengajuan) = MONTH (CURDATE()) AND YEAR (updated) = YEAR (CURDATE()) AND ( (o.po_import IS NULL) || (o.po_import IS FALSE ))) z;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) DPB total bulan ini";
            model.name2 = "dpb total yang masuk pada tahun ini";
            model.link1 = DPBQUERY + "SELECT DISTINCT d.dpb AS result FROM len_enq_dpb_product d LEFT JOIN len_enq_spph s ON s.dpb = d.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE MONTH (updated) = MONTH (CURDATE()) AND MONTH (dp.tgl_pengajuan) = MONTH (CURDATE()) AND YEAR (updated) = YEAR (CURDATE()) AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) );";
            model.link2 = DPBQUERY + "SELECT DISTINCT d.dpb AS result FROM len_enq_dpb_product d LEFT JOIN len_enq_spph s ON s.dpb = d.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE YEAR (updated) = YEAR (CURDATE()) AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE()) AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) );";
            model.data1 = result.data2;
            model.data2 = result.data1;
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Prosentase (%) Jumlah (∑) Total item sudah SPPH/ Total item (Struktural Lokal)
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary8()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL( ROUND( total_produk_spph / total_produk * 100, 2 ), 0 ) AS data1 FROM ( SELECT COUNT(d.product) AS total_produk FROM len_enq_dpb_product d INNER JOIN len_enq_spph s ON s.dpb = d.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po WHERE YEAR (updated) = YEAR (CURDATE()) AND (( o.po_import IS NULL) || (o.po_import IS FALSE) ) ) x CROSS JOIN ( SELECT COUNT(d.product) AS total_produk_spph FROM len_enq_dpb_product d INNER JOIN len_enq_spph s ON s.dpb = d.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po WHERE d.spph <> 0 AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (updated) = YEAR (CURDATE())) z;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Prosentase (%) Jumlah (∑) Total item sudah SPPH/ Total item";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }
        /// <summary>
        /// Prosentase (%) Jumlah (∑) Total item sudah PO/ Total item (Struktural Lokal)
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary9()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL( ROUND( z.total_item_po / x.total_item * 100, 2 ), 0 ) AS data1 FROM ( SELECT COUNT(lepp.product) AS total_item_po FROM len_enq_po_product lepp LEFT OUTER JOIN len_option_po o ON o.po = lepp.po WHERE YEAR (lepp.tgl_po) = YEAR (CURDATE()) AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) )) z CROSS JOIN ( SELECT COUNT(d.product) AS total_item FROM len_enq_dpb_product d INNER JOIN len_enq_spph s ON s.dpb = d.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po WHERE YEAR (updated) = YEAR (CURDATE()) AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) )) x;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Prosentase (%) Jumlah (∑) Total item sudah PO/ Total item";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }

        //summary strukural impor
        /// <summary>
        /// Jumlah (∑) DPB total pada hari ini (Struktural Impor)
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary10()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL(COUNT(DISTINCT led.dpb, led.tgl_pengajuan),0) AS data1, IFNULL(COUNT( DISTINCT led.dpb, ledp.product ),0) AS data2 FROM len_enq_dpb led INNER JOIN len_enq_dpb_product ledp ON ledp.dpb = led.dpb INNER JOIN len_enq_spph s ON ledp.dpb = s.dpb INNER JOIN len_option_po o ON o.po = s.po WHERE YEAR (tgl_pengajuan) = (CURDATE()) AND o.po_import IS TRUE;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) DPB total pada hari ini";
            model.name2 = "total itemnya";
            model.link1 = DPBQUERY + "SELECT DISTINCT led.dpb AS result FROM len_enq_dpb led INNER JOIN len_enq_dpb_product ledp ON ledp.dpb = led.dpb INNER JOIN len_enq_spph s ON ledp.dpb = s.dpb INNER JOIN len_option_po o ON o.po = s.po WHERE YEAR (tgl_pengajuan) = (CURDATE()) AND o.po_import IS TRUE;";
            model.link2 = DPBQUERY + "SELECT DISTINCT led.dpb AS result FROM len_enq_dpb led INNER JOIN len_enq_dpb_product ledp ON ledp.dpb = led.dpb INNER JOIN len_enq_spph s ON ledp.dpb = s.dpb INNER JOIN len_option_po o ON o.po = s.po WHERE YEAR (tgl_pengajuan) = (CURDATE()) AND o.po_import IS TRUE;";
            model.data1 = result.data1;
            model.data2 = result.data2;
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) DPB total bulan ini dan dpb total yang masuk pada tahun ini (Struktural Impor)
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary11()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT * FROM ( SELECT COUNT(DISTINCT d.dpb) AS data1 FROM len_enq_dpb_product d INNER JOIN len_enq_spph s ON s.dpb = d.dpb INNER JOIN len_option_po o ON o.po = s.po LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE YEAR (updated) = YEAR (CURDATE()) AND o.po_import IS TRUE AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE())) x CROSS JOIN ( SELECT COUNT(DISTINCT d.dpb) AS data2 FROM len_enq_dpb_product d INNER JOIN len_enq_spph s ON s.dpb = d.dpb INNER JOIN len_option_po o ON o.po = s.po LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE MONTH (updated) = MONTH (CURDATE()) AND MONTH (dp.tgl_pengajuan) = MONTH (CURDATE()) AND YEAR (updated) = YEAR (CURDATE()) AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE()) AND o.po_import IS TRUE ) z;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) DPB total bulan ini";
            model.name2 = "dpb total yang masuk pada tahun ini";
            model.link1 = DPBQUERY + "SELECT DISTINCT d.dpb AS result FROM len_enq_dpb_product d INNER JOIN len_enq_spph s ON s.dpb = d.dpb INNER JOIN len_option_po o ON o.po = s.po LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE MONTH (updated) = MONTH (CURDATE()) AND MONTH (dp.tgl_pengajuan) = MONTH (CURDATE()) AND YEAR (updated) = YEAR (CURDATE()) AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE()) AND o.po_import IS TRUE";
            model.link2 = DPBQUERY + "SELECT DISTINCT d.dpb AS result FROM len_enq_dpb_product d INNER JOIN len_enq_spph s ON s.dpb = d.dpb INNER JOIN len_option_po o ON o.po = s.po LEFT JOIN len_enq_dpb dp ON dp.dpb = d.dpb WHERE YEAR (updated) = YEAR (CURDATE()) AND o.po_import IS TRUE AND YEAR (dp.tgl_pengajuan) = YEAR (CURDATE())";
            model.data1 = result.data2;
            model.data2 = result.data1;
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Prosentase (%) Jumlah (∑) Total item sudah SPPH/ Total item (Struktural Impor)
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary12()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL( ROUND( total_produk_spph / total_produk * 100, 2 ), 0 ) AS data1 FROM ( SELECT COUNT(d.product) AS total_produk FROM len_enq_dpb_product d INNER JOIN len_enq_spph s ON s.dpb = d.dpb INNER JOIN len_option_po o ON o.po = s.po WHERE YEAR (updated) = YEAR (CURDATE()) AND o.po_import IS TRUE ) x CROSS JOIN ( SELECT COUNT(d.product) AS total_produk_spph FROM len_enq_dpb_product d INNER JOIN len_enq_spph s ON s.dpb = d.dpb INNER JOIN len_option_po o ON o.po = s.po WHERE d.spph <> 0 AND o.po_import IS TRUE AND YEAR (updated) = YEAR (CURDATE())) z;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Prosentase (%) Jumlah (∑) Total item sudah SPPH/ Total item";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }
        /// <summary>
        /// Prosentase (%) Jumlah (∑) Total item sudah PO/ Total item (struktural impor)
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary13()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL( ROUND( z.total_item_po / x.total_item * 100, 2 ), 0 ) AS data1 FROM ( SELECT COUNT(lepp.product) AS total_item_po FROM len_enq_po_product lepp LEFT OUTER JOIN len_option_po o ON o.po = lepp.po WHERE YEAR (lepp.tgl_po) = YEAR (CURDATE()) AND (o.po_import IS TRUE)) z CROSS JOIN ( SELECT COUNT(d.product) AS total_item FROM len_enq_dpb_product d INNER JOIN len_enq_spph s ON s.dpb = d.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po WHERE YEAR (updated) = YEAR (CURDATE()) AND (o.po_import IS TRUE)) x;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Prosentase (%) Jumlah (∑) Total item sudah PO/ Total item";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }

        //maintenance
        //lokal
        /// <summary>
        /// Prosentase (%) Efektifitas/ Kecepatan Layanan (lokal)
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummarym1()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL( ROUND(efektif / total * 100, 2), 0 ) AS data1, x.efektif, y.total FROM ( SELECT COUNT( DISTINCT led.dpb, led.po, le.created_at, lp.tgl_po, DATEDIFF(le.created_at, lp.tgl_po)) AS efektif FROM len_enq_spph_product led JOIN len_executor le ON le.dpb = led.dpb JOIN len_enq_dpb d ON d.dpb = le.dpb JOIN len_enq_po lp ON lp.po = led.po LEFT OUTER JOIN len_option_po o ON o.po = lp.po WHERE DATEDIFF(le.created_at, lp.tgl_po) BETWEEN 0 AND 21 AND YEAR (d.tgl_pengajuan) = YEAR (CURDATE()) AND le.updated_at IS NOT NULL AND ((o.po_import IS NULL) || (o.po_import IS FALSE))) x CROSS JOIN ( SELECT COUNT(DISTINCT led.dpb) AS total FROM len_enq_dpb led INNER JOIN len_executor le ON le.dpb = led.dpb WHERE YEAR (led.tgl_pengajuan) = YEAR (CURDATE()) AND le.updated_at IS NOT NULL ) y;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Prosentase (%) Efektifitas/ Kecepatan Layanan";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }

        //impor
        /// <summary>
        /// Prosentase (%) Efektifitas/ Kecepatan Layanan (impor)
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummarym2()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL( ROUND(efektif / total * 100, 2), 0 ) AS data1, x.efektif, y.total FROM ( SELECT COUNT( DISTINCT led.dpb, led.po, le.created_at, lp.tgl_po, DATEDIFF(le.created_at, lp.tgl_po)) AS efektif FROM len_enq_spph_product led JOIN len_executor le ON le.dpb = led.dpb JOIN len_enq_po lp ON lp.po = led.po JOIN len_enq_dpb d ON d.dpb = le.dpb JOIN len_option_po o ON o.po = lp.po WHERE DATEDIFF(le.created_at, lp.tgl_po) BETWEEN 0 AND 21 AND YEAR (d.dpb) = YEAR (CURDATE()) AND le.updated_at IS NOT NULL AND o.po_import IS TRUE ) x CROSS JOIN ( SELECT COUNT(DISTINCT led.dpb) AS total FROM len_enq_dpb led INNER JOIN len_executor le ON le.dpb = led.dpb WHERE YEAR (led.tgl_pengajuan) = YEAR (CURDATE()) AND le.updated_at IS NOT NULL ) y;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Prosentase (%) Efektifitas/ Kecepatan Layanan";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }

    }
}