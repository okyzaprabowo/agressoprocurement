﻿using System.Linq;
namespace LenProcurementApp.Models
{
    /// <summary>
    /// transaksi summary Pertanggungjawaban
    /// </summary>
    public class SummaryTransactionPJ
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string DPBQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "1" + "?" + "query=";
        string POQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "2" + "?" + "query=";

        /// <summary>
        /// Jumlah (∑) Impor TT total
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary1()
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
        public SummaryModel GetSummary2()
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
        public SummaryModel GetSummary3()
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
        public SummaryModel GetSummary4()
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
        public SummaryModel GetSummary5()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(claim_date) AS data1 FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE (claim_date) <= (NOW() - INTERVAL 30 DAY) AND payment_method = 'L/C' AND o.po_import IS TRUE AND STATUS = '0' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
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
        public SummaryModel GetSummary6()
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
        public SummaryModel GetSummary7()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(lop.po) AS data1 FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (lp.claim_date) = YEAR (CURDATE()) ORDER BY lop.po;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Pajak Impor total";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lop.po AS result FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (lp.claim_date) = YEAR (CURDATE()) ORDER BY lop.po;";
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
        public SummaryModel GetSummary8()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(lop.po) AS data1 FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (CURDATE()) AND (lp.claim_date) <= (NOW() - INTERVAL 30 DAY) AND lp.import_tax = 0;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Pajak Impor > 1 bulan belum pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lop.po AS result FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (CURDATE()) AND (lp.claim_date) <= (NOW() - INTERVAL 30 DAY) AND lp.import_tax = 0;";
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
        public SummaryModel GetSummary9()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(lop.po) AS data1 FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND lp.import_tax = 1;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Pajak Impor sudah pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT lop.po AS result FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (lp.claim_date) = YEAR (CURDATE()) AND lp.import_tax = 1;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Total Gabungan TT, LC, Pajak Impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary10()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT SUM(result) AS data1 FROM ( SELECT COUNT(*) AS result FROM len_payment lp WHERE lp.payment_method = 'L/C' AND YEAR (CURDATE()) UNION ALL SELECT COUNT(*) AS result FROM len_payment lp WHERE lp.payment_method = 'T/T' AND YEAR (CURDATE()) UNION ALL SELECT COUNT(lop.po) AS result FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (CURDATE())) AS X;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Total Gabungan TT, LC, Pajak Impor";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT po AS result FROM len_payment lp WHERE lp.payment_method = 'L/C' AND YEAR (CURDATE()) UNION ALL SELECT po AS result FROM len_payment lp WHERE lp.payment_method = 'T/T' AND YEAR (CURDATE()) UNION ALL SELECT lop.po AS result FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) Gabungan TT, LC, Pajak Impor > 1 bulan belum pertanggungjawaban
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary11()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT SUM(result) AS data1 FROM ( SELECT COUNT(claim_date) AS result FROM len_payment lp WHERE (lp.claim_date) <= (NOW() - INTERVAL 30 DAY) AND lp.payment_method = 'T/T' AND lp. STATUS = '0' AND YEAR (claim_date) = YEAR (CURDATE()) UNION ALL SELECT COUNT(claim_date) AS result FROM len_payment lp WHERE (lp.claim_date) <= (NOW() - INTERVAL 30 DAY) AND lp.payment_method = 'L/C' AND lp. STATUS = '0' AND YEAR (claim_date) = YEAR (CURDATE()) UNION ALL SELECT COUNT(lop.po) AS result FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND (lp.import_tax = 0) AND YEAR (CURDATE()) AND (lp.claim_date) <= (NOW() - INTERVAL 30 DAY) AND ( lp.payment_method = 'L/C' OR lp.payment_method = 'T/T' )) AS t;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Gabungan TT, LC, Pajak Impor > 1 bulan belum pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT result AS result FROM ( SELECT po AS result FROM len_payment lp WHERE (lp.claim_date) <= (NOW() - INTERVAL 30 DAY) AND lp.payment_method = 'T/T' AND lp. STATUS = '0' AND YEAR (claim_date) = YEAR (CURDATE()) UNION ALL SELECT po AS result FROM len_payment lp WHERE (lp.claim_date) <= (NOW() - INTERVAL 30 DAY) AND lp.payment_method = 'L/C' AND lp. STATUS = '0' AND YEAR (claim_date) = YEAR (CURDATE()) UNION ALL SELECT lop.po AS result FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND (lp.import_tax = 0) AND YEAR (CURDATE()) AND (lp.claim_date) <= (NOW() - INTERVAL 30 DAY) AND ( lp.payment_method = 'L/C' OR lp.payment_method = 'T/T' )) AS t;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }

        /*
        public SummaryModel GetSummary12()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT SUM(result) AS data1 FROM ( SELECT COUNT(*) AS result FROM len_payment lp WHERE lp.payment_method = 'T/T' AND lp. STATUS = '1' AND YEAR (CURDATE()) UNION ALL SELECT COUNT(*) AS result FROM len_payment lp WHERE lp.payment_method = 'L/C' AND lp. STATUS = '1' AND YEAR (CURDATE()) UNION ALL SELECT COUNT(lop.po) AS result FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND (lp.import_tax = 1) AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (CURDATE()) AND STATUS = '1' ) AS h;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) Gabungan TT, LC, Impor sudah pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT result AS result FROM ( SELECT po AS result FROM len_payment lp WHERE lp.payment_method = 'T/T' AND lp. STATUS = '1' AND YEAR (CURDATE()) UNION ALL SELECT po AS result FROM len_payment lp WHERE lp.payment_method = 'L/C' AND lp. STATUS = '1' AND YEAR (CURDATE()) UNION ALL SELECT lop.po AS result FROM len_option_po lop JOIN len_payment lp ON lp.po = lop.po WHERE lop.po_import = 1 AND (lp.import_tax = 1) AND ( lp.payment_method = 'T/T' OR lp.payment_method = 'L/C' ) AND YEAR (CURDATE()) AND STATUS = '1' ) AS h;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        */
        /// <summary>
        /// Jumlah (∑) SKBDN total
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary13()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(*) AS data1 FROM len_payment lp WHERE payment_method = 'SKBDN' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) SKBDN total";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT po AS result FROM len_payment lp WHERE payment_method = 'SKBDN' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) SKBDN > 1 bulan belum pertanggungjawaban
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary14()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(claim_date) AS data1 FROM len_payment lp WHERE (claim_date) <= (NOW() - INTERVAL 30 DAY) AND payment_method = 'SKBDN' AND STATUS = '0' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) SKBDN > 1 bulan belum pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT po AS result FROM len_payment lp WHERE (claim_date) <= (NOW() - INTERVAL 30 DAY) AND payment_method = 'SKBDN' AND STATUS = '0' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) SKBDN sudah pertanggungjawaban
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary15()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(*) AS data1 FROM len_payment lp WHERE payment_method = 'SKBDN' AND STATUS = '1' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) SKBDN sudah pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT po AS result FROM len_payment lp WHERE payment_method = 'SKBDN' AND STATUS = '1' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) UM total
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary16()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(*) AS data1 FROM len_payment lp WHERE payment = 'Uang Muka' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) UM total";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT po AS result FROM len_payment lp WHERE payment = 'Uang Muka' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) UM > 1 bulan belum pertanggungjawaban
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary17()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(claim_date) AS data1 FROM len_payment lp WHERE (claim_date) <= (NOW() - INTERVAL 30 DAY) AND payment = 'Uang Muka' AND STATUS = '0' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) UM > 1 bulan belum pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT po AS result FROM len_payment lp WHERE (claim_date) <= (NOW() - INTERVAL 30 DAY) AND payment = 'Uang Muka' AND STATUS = '0' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) UM sudah pertanggungjawaban
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary18()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(*) AS data1 FROM len_payment lp WHERE payment = 'Uang Muka' AND STATUS = '1' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) UM sudah pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT po AS result FROM len_payment lp WHERE payment = 'Uang Muka' AND STATUS = '1' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }

        //uang muka strukural lokal
        /// <summary>
        /// Jumlah (∑) UM total lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary19()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT p.po) AS data1, p.claim_date FROM len_payment p LEFT JOIN len_option_po o ON o.po = p.po WHERE payment = 'Uang Muka' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (p.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) UM total";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT p.po AS result FROM len_payment p LEFT JOIN len_option_po o ON o.po = p.po WHERE payment = 'Uang Muka' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (p.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) UM > 1 bulan belum pertanggungjawaban lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary20()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lp.po) AS data1, lp.claim_date FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE (claim_date) <= (NOW() - INTERVAL 30 DAY) AND lp.payment = 'Uang Muka' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND STATUS = '0' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) UM > 1 bulan belum pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lp.po AS data1 FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE (claim_date) <= (NOW() - INTERVAL 30 DAY) AND lp.payment = 'Uang Muka' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND STATUS = '0' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) UM sudah pertanggungjawaban lokal
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary21()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lp.po) AS data1, lp.claim_date FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE payment = 'Uang Muka' AND STATUS = '1' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) UM sudah pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lp.po AS result FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE payment = 'Uang Muka' AND STATUS = '1' AND ( (o.po_import IS NULL) || (o.po_import IS FALSE) ) AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }

        //uang muka strukural impor
        /// <summary>
        /// Jumlah (∑) UM total impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary22()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT p.po) AS data1 FROM len_payment p LEFT JOIN len_option_po o ON o.po = p.po WHERE payment = 'Uang Muka' AND (o.po_import IS TRUE) AND YEAR (p.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) UM total";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT p.po AS result FROM len_payment p LEFT JOIN len_option_po o ON o.po = p.po WHERE payment = 'Uang Muka' AND (o.po_import IS TRUE) AND YEAR (p.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) UM > 1 bulan belum pertanggungjawaban impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary23()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lp.po) AS data1 FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE (claim_date) <= (NOW() - INTERVAL 30 DAY) AND lp.payment = 'Uang Muka' AND (o.po_import IS TRUE) AND STATUS = '0' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) UM > 1 bulan belum pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lp.po AS result FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE (claim_date) <= (NOW() - INTERVAL 30 DAY) AND lp.payment = 'Uang Muka' AND (o.po_import IS TRUE) AND STATUS = '0' AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Jumlah (∑) UM sudah pertanggungjawaban impor
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary24()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT COUNT(DISTINCT lp.po) AS data1 FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE payment = 'Uang Muka' AND STATUS = '1' AND (o.po_import IS TRUE) AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) UM sudah pertanggungjawaban";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT COUNT(DISTINCT lp.po) AS data1 FROM len_payment lp LEFT JOIN len_option_po o ON o.po = lp.po WHERE payment = 'Uang Muka' AND STATUS = '1' AND (o.po_import IS TRUE) AND YEAR (lp.claim_date) = YEAR (CURDATE());";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }

    }
}