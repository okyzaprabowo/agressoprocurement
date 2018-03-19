using System.Linq;
namespace LenProcurementApp.Models
{
    /// <summary>
    /// transaksi summary PO
    /// </summary>
    public class SummaryTransactionPO
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string DPBQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "1" + "?" + "query=";
        string POQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "2" + "?" + "query=";

        /// <summary>
        /// Jumlah (∑) PO yang nego 
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary1()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT nego.po_nego AS data1, po.total_po - nego.po_nego AS data2 FROM ( SELECT COUNT(*) AS po_nego FROM len_enq_po lep INNER JOIN len_option_po lop ON lep.po = lop.po WHERE lop.negotiation = 1 AND YEAR (lep.tgl_po) = YEAR (CURDATE())) nego CROSS JOIN ( SELECT COUNT(DISTINCT(po)) AS total_po FROM len_enq_po WHERE YEAR (tgl_po) = YEAR (CURDATE())) po;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) PO yang nego";
            model.name2 = "tidak nego";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep INNER JOIN len_option_po lop ON lep.po = lop.po WHERE lop.negotiation = 1 AND YEAR (lep.tgl_po) = YEAR (CURDATE());";
            model.link2 = POQUERY + "SELECT DISTINCT p.po AS result FROM len_enq_po p LEFT JOIN len_option_po op ON op.po = p.po WHERE YEAR (tgl_po) = YEAR (CURDATE()) AND ( op.negotiation <> 1 || op.negotiation IS NULL );";
            model.data1 = result.data1;
            model.data2 = result.data2;
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Prosentase (%) Jumlah (∑) Total Item yang sudah Kontrak / Total item
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary2()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL( ROUND( y.total_item_kontrak / x.total_item * 100, 2 ), 0 ) AS data1 FROM ( SELECT COUNT(DISTINCT ledp.product) AS total_item FROM len_enq_dpb_product ledp ) x CROSS JOIN ( SELECT COUNT(DISTINCT ledp.product) AS total_item_kontrak FROM len_enq_dpb_product ledp INNER JOIN len_enq_po_product lepp ON lepp.product = ledp.product ) y;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Prosentase (%) Jumlah (∑) Total Item yang sudah Kontrak / Total item";
            model.name2 = "";
            model.link1 = // POQUERY + "SELECT DISTINCT lepp.po AS result FROM len_enq_dpb_product ledp INNER JOIN len_enq_po_product lepp ON lepp.product = ledp.product WHERE YEAR (lepp.tgl_po) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Kontrak akan berakhir kurang dari 1 bulan
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary3()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep WHERE DATEDIFF( NOW(), lep.tgl_habis_kontrak ) BETWEEN - 30 AND -1 AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Kontrak akan berakhir < 1 bulan";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep WHERE DATEDIFF( NOW(), lep.tgl_habis_kontrak ) BETWEEN - 30 AND -1 AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Jaminan Pelaksanaan akan berakhir kurang dari 1 bulan
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary4()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep INNER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.jaminan_pelaksanaan = 1 AND DATEDIFF(NOW(), lop.tgl_pelaksanaan) BETWEEN - 30 AND -1 AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Jaminan Pelaksanaan akan berakhir < 1 bulan";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep INNER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.jaminan_pelaksanaan = 1 AND DATEDIFF(NOW(), lop.tgl_pelaksanaan) BETWEEN - 30 AND -1 AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Jaminan Uang Muka akan berakhir kurang dari 1 bulan
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary5()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep INNER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.jaminan_um = 1 AND DATEDIFF(NOW(), lop.tgl_um) BETWEEN - 30 AND -1 AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Jaminan Uang Muka akan berakhir < 1 bulan";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep INNER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.jaminan_um = 1 AND DATEDIFF(NOW(), lop.tgl_um) BETWEEN - 30 AND -1 AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Jaminan Pelaksanaan sudah berakhir
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary6()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep INNER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.tgl_pelaksanaan < NOW() AND lop.jaminan_pelaksanaan = 1 AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Jaminan Pelaksanaan sudah berakhir";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep INNER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.tgl_pelaksanaan < NOW() AND lop.jaminan_pelaksanaan = 1 AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Jaminan Uang Muka sudah berakhir
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary7()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep INNER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.tgl_um < NOW() AND lop.jaminan_um = 1 AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Jaminan Uang Muka sudah berakhir";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep INNER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.tgl_um < NOW() AND lop.jaminan_um = 1 AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) UM Impor yang diajukan
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary8()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep INNER JOIN len_payment lp ON lp.po = lep.po INNER JOIN len_option_po p ON p.po = lp.po WHERE lp.payment = 'UANG MUKA' AND p.po_import IS TRUE AND YEAR (lep.tgl_po) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) UM Impor yang diajukan";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep INNER JOIN len_payment lp ON lp.po = lep.po INNER JOIN len_option_po p ON p.po = lp.po WHERE lp.payment = 'UANG MUKA' AND p.po_import IS TRUE AND YEAR (lep.tgl_po) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) UM Impor yang sudah dibayar
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary9()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep INNER JOIN len_payment lp ON lp.po = lep.po INNER JOIN len_option_po p ON p.po = lp.po WHERE lp.payment = 'UANG MUKA' AND p.po_import IS TRUE AND lp. STATUS = 1 AND YEAR (lep.tgl_po) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) UM Impor yang sudah dibayar";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep INNER JOIN len_payment lp ON lp.po = lep.po INNER JOIN len_option_po p ON p.po = lp.po WHERE lp.payment = 'UANG MUKA' AND p.po_import IS TRUE AND lp. STATUS = 1 AND YEAR (lep.tgl_po) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) UM Impor yang diajukan > 1 minggu
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary10()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep INNER JOIN len_payment lp ON lp.po = lep.po INNER JOIN len_option_po lop ON lop.po = lep.po WHERE DATEDIFF(lp.claim_date, NOW()) < - 7 AND lop.po_import = 1 AND lp.payment = 'UANG MUKA' AND lp. STATUS = 0 AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) UM Impor yang diajukan > 1 minggu";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep INNER JOIN len_payment lp ON lp.po = lep.po INNER JOIN len_option_po lop ON lop.po = lep.po WHERE DATEDIFF(lp.claim_date, NOW()) < - 7 AND lop.po_import = 1 AND lp.payment = 'UANG MUKA' AND lp. STATUS = 0 AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }

        //struktural lokal
        /// <summary>
        /// Jumlah (∑) PO yang nego lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary11()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT nego.po_nego AS data1, po.total_po - nego.po_nego AS data2 FROM ( SELECT COUNT(*) AS po_nego FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lep.po = lop.po WHERE lop.negotiation = 1 AND ( (lop.po_import IS NULL) || (lop.po_import IS FALSE) ) AND YEAR (lep.tgl_po) = YEAR (CURDATE())) nego CROSS JOIN ( SELECT COUNT(DISTINCT(p.po)) AS total_po FROM len_enq_po p LEFT OUTER JOIN len_option_po lop ON lop.po = p.po WHERE YEAR (tgl_po) = YEAR (CURDATE()) AND ( (lop.po_import IS NULL) || (lop.po_import IS FALSE) ) ) po;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) PO yang nego";
            model.name2 = "tidak nego";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lep.po = lop.po WHERE lop.negotiation = 1 AND ( (lop.po_import IS NULL) || (lop.po_import IS FALSE) ) AND YEAR (lep.tgl_po) = YEAR (CURDATE());";
            model.link2 = POQUERY + "SELECT DISTINCT p.po AS result FROM len_enq_po p LEFT OUTER JOIN len_option_po op ON op.po = p.po WHERE YEAR (tgl_po) = YEAR (CURDATE()) AND ( (op.negotiation <> 1) || (op.negotiation IS NULL) ) AND ( (op.po_import IS NULL) || (op.po_import IS FALSE) );";
            model.data1 = result.data1;
            model.data2 = result.data2;
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Prosentase (%) Jumlah (∑) Total Item yang sudah Kontrak / Total item lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary12()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL( ROUND( z.total_item_po / x.total_item * 100, 2 ), 0 ) AS data1 FROM ( SELECT COUNT(lepp.product) AS total_item_po FROM len_enq_po_product lepp LEFT OUTER JOIN len_option_po o ON o.po = lepp.po WHERE YEAR (lepp.tgl_po) = YEAR (CURDATE()) AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) )) z CROSS JOIN ( SELECT COUNT(d.product) AS total_item FROM len_enq_dpb_product d INNER JOIN len_enq_spph s ON s.dpb = d.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po WHERE YEAR (updated) = YEAR (CURDATE()) AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) )) x;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Prosentase (%) Jumlah (∑) Total Item yang sudah Kontrak / Total item";
            model.name2 = "";
            model.link1 = // POQUERY + "SELECT DISTINCT lepp.po AS result FROM len_enq_dpb_product ledp INNER JOIN len_enq_po_product lepp ON lepp.product = ledp.product WHERE YEAR (lepp.tgl_po) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Kontrak akan berakhir kurang dari 1 bulan lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary13()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep LEFT OUTER JOIN len_option_po o ON o.po = lep.po WHERE DATEDIFF( NOW(), lep.tgl_habis_kontrak ) BETWEEN - 30 AND - 1 AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Kontrak akan berakhir < 1 bulan";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep LEFT OUTER JOIN len_option_po o ON o.po = lep.po WHERE DATEDIFF( NOW(), lep.tgl_habis_kontrak ) BETWEEN - 30 AND - 1 AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
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
        public SummaryModel GetSummary14()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.jaminan_pelaksanaan = 1 AND DATEDIFF(NOW(), lop.tgl_pelaksanaan) BETWEEN - 30 AND - 1 AND ( (lop.po_import IS NULL) || (lop.po_import IS FALSE) ) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Jaminan Pelaksanaan akan berakhir < 1 bulan";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.jaminan_pelaksanaan = 1 AND DATEDIFF(NOW(), lop.tgl_pelaksanaan) BETWEEN - 30 AND - 1 AND (( lop.po_import IS NULL) || (lop.po_import IS FALSE) ) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
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
        public SummaryModel GetSummary15()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.jaminan_um = 1 AND DATEDIFF(NOW(), lop.tgl_um) BETWEEN - 30 AND - 1 AND ( (lop.po_import IS NULL) || (lop.po_import IS FALSE )) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Jaminan Uang Muka akan berakhir < 1 bulan";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep LEFT JOIN len_option_po lop ON lop.po = lep.po WHERE lop.jaminan_um = 1 AND DATEDIFF(NOW(), lop.tgl_um) BETWEEN - 30 AND - 1 AND ( (lop.po_import IS NULL) || (lop.po_import IS FALSE) ) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Jaminan Pelaksanaan sudah berakhir lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary16()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.tgl_pelaksanaan < NOW() AND lop.jaminan_pelaksanaan = 1 AND ( (lop.po_import IS NULL) || (lop.po_import IS FALSE) ) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Jaminan Pelaksanaan sudah berakhir";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.tgl_pelaksanaan < NOW() AND lop.jaminan_pelaksanaan = 1 AND ( (lop.po_import IS NULL) || (lop.po_import IS FALSE) ) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Jaminan Uang Muka sudah berakhir lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary17()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.tgl_um < NOW() AND ( (lop.po_import IS NULL) || (lop.po_import IS FALSE) ) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Jaminan Uang Muka sudah berakhir";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.tgl_um < NOW() AND (( lop.po_import IS NULL )|| (lop.po_import IS FALSE) ) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }

        //struktural impor
        /// <summary>
        /// Jumlah (∑) PO yang nego impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary18()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT nego.po_nego AS data1, po.total_po - nego.po_nego AS data2 FROM ( SELECT COUNT(*) AS po_nego FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lep.po = lop.po WHERE lop.negotiation = 1 AND (lop.po_import IS TRUE) AND YEAR (lep.tgl_po) = YEAR (CURDATE())) nego CROSS JOIN ( SELECT COUNT(DISTINCT(p.po)) AS total_po FROM len_enq_po p LEFT OUTER JOIN len_option_po lop ON lop.po = p.po WHERE YEAR (tgl_po) = YEAR (CURDATE()) AND (lop.po_import IS TRUE)) po;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) PO yang nego";
            model.name2 = "tidak nego";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep INNER JOIN len_option_po lop ON lep.po = lop.po WHERE lop.negotiation = 1 AND (lop.po_import IS TRUE) AND YEAR (lep.tgl_po) = YEAR (CURDATE());";
            model.link2 = POQUERY + "SELECT DISTINCT p.po AS result FROM len_enq_po p LEFT JOIN len_option_po op ON op.po = p.po WHERE YEAR (tgl_po) = YEAR (CURDATE()) AND (op.po_import IS TRUE) AND ( op.negotiation <> 1 || op.negotiation IS NULL );";
            model.data1 = result.data1;
            model.data2 = result.data2;
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Prosentase (%) Jumlah (∑) Total Item yang sudah Kontrak / Total item impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary19()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT IFNULL( ROUND( z.total_item_po / x.total_item * 100, 2 ), 0 ) AS data1 FROM ( SELECT COUNT(lepp.product) AS total_item_po FROM len_enq_po_product lepp LEFT OUTER JOIN len_option_po o ON o.po = lepp.po WHERE YEAR (lepp.tgl_po) = YEAR (CURDATE()) AND (o.po_import IS TRUE)) z CROSS JOIN ( SELECT COUNT(d.product) AS total_item FROM len_enq_dpb_product d INNER JOIN len_enq_spph s ON s.dpb = d.dpb LEFT OUTER JOIN len_option_po o ON o.po = s.po WHERE YEAR (updated) = YEAR (CURDATE()) AND (o.po_import IS TRUE)) x;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Prosentase (%) Jumlah (∑) Total Item yang sudah Kontrak / Total item";
            model.name2 = "";
            model.link1 = "";// POQUERY + "SELECT DISTINCT lepp.po AS result FROM len_enq_dpb_product ledp INNER JOIN len_enq_po_product lepp ON lepp.product = ledp.product WHERE YEAR (lepp.tgl_po) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Kontrak akan berakhir kurang dari 1 bulan impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary20()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep LEFT OUTER JOIN len_option_po o ON o.po = lep.po WHERE DATEDIFF( NOW(), lep.tgl_habis_kontrak ) BETWEEN - 30 AND - 1 AND (o.po_import IS TRUE) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Kontrak akan berakhir < 1 bulan";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep LEFT OUTER JOIN len_option_po o ON o.po = lep.po WHERE DATEDIFF( NOW(), lep.tgl_habis_kontrak ) BETWEEN - 30 AND - 1 AND (o.po_import IS TRUE) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Jaminan Pelaksanaan akan berakhir kurang dari 1 bulan impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary21()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.jaminan_pelaksanaan = 1 AND DATEDIFF(NOW(), lop.tgl_pelaksanaan) BETWEEN - 30 AND - 1 AND (lop.po_import IS TRUE) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Jaminan Pelaksanaan akan berakhir < 1 bulan";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.jaminan_pelaksanaan = 1 AND DATEDIFF(NOW(), lop.tgl_pelaksanaan) BETWEEN - 30 AND - 1 AND (lop.po_import IS TRUE) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Jaminan Uang Muka akan berakhir kurang dari 1 bulan impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary22()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.jaminan_um = 1 AND DATEDIFF(NOW(), lop.tgl_um) BETWEEN - 30 AND - 1 AND (lop.po_import IS TRUE) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Jaminan Uang Muka akan berakhir < 1 bulan";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.jaminan_um = 1 AND DATEDIFF(NOW(), lop.tgl_um) BETWEEN - 30 AND - 1 AND (lop.po_import IS TRUE) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Jaminan Pelaksanaan sudah berakhir impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary23()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.tgl_pelaksanaan < NOW() AND lop.jaminan_pelaksanaan = 1 AND (lop.po_import IS TRUE) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Jaminan Pelaksanaan sudah berakhir";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.tgl_pelaksanaan < NOW() AND lop.jaminan_pelaksanaan = 1 AND (lop.po_import IS TRUE) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Jaminan Uang Muka sudah berakhir impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary24()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lep.po) AS data1 FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.tgl_um < NOW() AND lop.jaminan_um = 1 AND (lop.po_import IS TRUE) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Jaminan Uang Muka sudah berakhir";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_enq_po lep LEFT OUTER JOIN len_option_po lop ON lop.po = lep.po WHERE lop.tgl_um < NOW() AND lop.jaminan_um = 1 AND (lop.po_import IS TRUE) AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
    }
}