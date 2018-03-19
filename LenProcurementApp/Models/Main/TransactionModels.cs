using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LenProcurementApp.Models;
using System.Data.Entity;

namespace LenProcurementApp.Models
{
    
    //public class BasisTransaction
    //{
    //    private ApplicationDbContext db = new ApplicationDbContext();
    //    string _dpb;

    //    public string _userDpb;
    //    public string _divisi;
    //    public string _jobCode;
    //    public string _jobCodeT;
    //    public string _spph;
    //    public string _po;
    //    public string _supplierT;

    //    public BasisTransaction(string dpb)
    //    {
    //        _dpb = dpb;
    //        try
    //        {
    //            BasisDasboard bd = db.basisData.FirstOrDefault(d => d.dpb == dpb);
    //            _userDpb = bd.user_dpb;
    //            _divisi = bd.divisi;
    //            _jobCode = bd.job_code;
    //            _jobCodeT = bd.job_code_t;
    //            _spph = bd.spph;
    //            _po = bd.po;
    //            _supplierT = bd.supplier_t;
    //        }
    //        catch (Exception)
    //        {
                
    //        }
    //    }

    //    //public string getUserDPB(string dpb)
    //    //{
    //    //    try
    //    //    {
    //    //        return db.basisData.First(d => d.dpb == dpb).user_dpb;
    //    //    }
    //    //    catch (Exception)
    //    //    {
    //    //        return null;
    //    //    }
    //    //}
    //}
    
    /// <summary>
    /// Main Dasboard transaction
    /// </summary>
    public class MainTransaction
    {
        // Log file
        log4net.ILog Log = log4net.LogManager.GetLogger(typeof(MainTransaction));
        // database
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// data untuk main dashboard umum
        /// </summary>
        /// <returns></returns>
        public List<NewMainDashboard> getMainTableContents()
        {
            string yearNow = DateTime.Now.Year.ToString();
            string dateNow = DateTime.Now.ToString("MM");
            string cDate = yearNow + "-" + dateNow;
            string dateFrom = DateTime.Now.AddMonths(-2).ToString("MM");
            string cDateFrom = yearNow + "-" + dateFrom;
            //JavascriptStatic.Log(cDate);
            //JavascriptStatic.Log(cDateFrom);
            var query = " SELECT DISTINCT "
                        + " d.user_dpb "
                        + " ,d.tgl_pengajuan "
                        + " ,(SELECT count(dp.product) FROM len_enq_dpb_product dp WHERE d.dpb = dp.dpb LIMIT 1) AS jml_item "
                        + " ,d.divisi_t AS divisi "
                        + " ,d.job_code "
                        + " ,d.job_code_t "
                        + " ,d.dpb "
                        + " ,b.spph "
                        + " ,b.po "
                        + " ,(SELECT DISTINCT COUNT(op.po) FROM len_option_po op WHERE op.po_import = TRUE AND op.po = b.po) AS is_import "
                        + " ,(SELECT DISTINCT p.supplier_t FROM len_enq_po p WHERE b.po = p.po LIMIT 1) AS supplier_t "
                        + " , bap.bapb "
                        + " ,(SELECT DISTINCT st.`status` FROM len_delivered st WHERE b.po = st.po LIMIT 1) AS barang_tiba "
                        + " ,(SELECT DISTINCT sp.spp_number FROM len_spp sp WHERE b.po = sp.po LIMIT 1) AS spp "
                        + " ,d.status "
                        + " ,(SELECT DISTINCT p.`status` FROM len_enq_po p WHERE b.po = p.po LIMIT 1) AS status_po "
                        + " FROM len_enq_dpb d "
                        + " LEFT JOIN len_enq_spph b ON b.dpb = d.dpb "
                        + " LEFT JOIN len_enq_bapb bap ON bap.po = b.po "
                        + " WHERE LEFT(d.tgl_pengajuan, 10) BETWEEN NOW() - INTERVAL 2 MONTH AND NOW() "
                        + " ORDER BY d.tgl_pengajuan DESC, d.dpb DESC ";
            //Log.Debug(query);
            try
            {
                // increasing time out
                db.Database.CommandTimeout = 270;
                var newModel = db.Database.SqlQuery<NewMainDashboard>(query).ToList();
                foreach (var item in newModel)
                {
                    var plk = (from dp in db.executorData
                               join bd in db.profiles
                               on dp.executor equals bd.user_id
                               where dp.dpb == item.dpb
                               select bd.initials
                         )
                            .Distinct()
                            .ToList();
                    if (plk != null && plk.Count() > 0)
                    {
                        item.plk = String.Join(", ", plk);
                    }
                }
                return newModel;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }

        }

        /// <summary>
        /// data main dashboard untuk pelaksana
        /// </summary>
        /// <param name="initial"></param>
        /// <returns></returns>
        public List<NewMainDashboard> GetMainExecutor(string initial)
        {
            string yearNow = DateTime.Now.Year.ToString();
            string dateNow = DateTime.Now.ToString("MM");
            string dateFrom = DateTime.Now.AddMonths(-2).ToString("MM");
            string cDate = yearNow + "-" + dateNow;
            string cDateFrom = yearNow + "-" + dateFrom;

            var init = (from dp in db.executorData
                       join bd in db.profiles
                       on dp.executor equals bd.user_id
                       where bd.initials == initial
                       select dp.dpb
                         )
                        .Distinct()
                        .ToList();
            if (init != null && init.Count() > 0)
            {
                var query = " SELECT DISTINCT "
                            + " d.user_dpb "
                            + " ,d.tgl_pengajuan "
                            + " ,(SELECT DISTINCT count(dp.product) FROM len_enq_dpb_product dp WHERE d.dpb = dp.dpb LIMIT 1) AS jml_item "
                            + " ,d.divisi_t AS divisi "
                            + " ,d.job_code "
                            + " ,d.job_code_t "
                            + " ,d.dpb "
                            + " ,b.spph "
                            + " ,b.po "
                            + " ,(SELECT DISTINCT COUNT(op.po) FROM len_option_po op WHERE op.po_import = TRUE AND op.po = b.po) AS is_import "
                            + " ,(SELECT DISTINCT p.supplier_t FROM len_enq_po p WHERE b.po = p.po LIMIT 1) AS supplier_t "
                            + " , bap.bapb "
                            + " ,(SELECT DISTINCT st.`status` FROM len_delivered st WHERE b.po = st.po LIMIT 1) AS barang_tiba "
                            + " ,(SELECT DISTINCT sp.spp_number FROM len_spp sp WHERE b.po = sp.po LIMIT 1) AS spp "
                            + " ,d.status "
                            + " ,(SELECT DISTINCT p.`status` FROM len_enq_po p WHERE b.po = p.po LIMIT 1) AS status_po "
                            + " FROM len_enq_dpb d "
                            + " LEFT JOIN len_enq_spph b ON b.dpb = d.dpb "
                            + " LEFT JOIN len_enq_bapb bap ON bap.po = b.po "
                            + " WHERE LEFT(d.tgl_pengajuan, 4) = '" + yearNow + "' ";
                bool firstTime = true;
                foreach (var dpbP in init)
                {
                    if (firstTime)
                    {
                        firstTime = false;
                        query += " AND d.dpb = '" + dpbP + "' ";
                    }
                    else
                    {
                        query += " OR d.dpb = '" + dpbP + "' ";
                    }
                }

                query += " ORDER BY d.tgl_pengajuan DESC, d.dpb DESC ";

                try
                {
                    // increasing time out
                    db.Database.CommandTimeout = 270;
                    var newModel = db.Database.SqlQuery<NewMainDashboard>(query).ToList();
                    foreach (var item in newModel)
                    {
                        var plk = (from dp in db.executorData
                                   join bd in db.profiles
                                   on dp.executor equals bd.user_id
                                   where dp.dpb == item.dpb
                                   select bd.initials
                             )
                                .Distinct()
                                .ToList();
                        if (plk != null && plk.Count() > 0)
                        {
                            item.plk = String.Join(", ", plk);
                        }
                    }
                    return newModel;
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// data main dashboard untuk user dpb
        /// </summary>
        /// <param name="userDpb"></param>
        /// <returns></returns>
        public List<NewMainDashboard> GetMainUserDpb(string userDpb)
        {
            string yearNow = DateTime.Now.Year.ToString();
            string dateNow = DateTime.Now.ToString("MM");
            string dateFrom = DateTime.Now.AddMonths(-2).ToString("MM");
            string cDate = yearNow + "-" + dateNow;
            string cDateFrom = yearNow + "-" + dateFrom;

            var query = " SELECT DISTINCT "
                        + " d.user_dpb "
                        + " ,d.tgl_pengajuan "
                        + " ,(SELECT DISTINCT count(dp.product) FROM len_enq_dpb_product dp WHERE d.dpb = dp.dpb LIMIT 1) AS jml_item "
                        + " ,d.divisi_t AS divisi "
                        + " ,d.job_code "
                        + " ,d.job_code_t "
                        + " ,d.dpb "
                        + " ,b.spph "
                        + " ,b.po "
                        + " ,(SELECT DISTINCT COUNT(op.po) FROM len_option_po op WHERE op.po_import = TRUE AND op.po = b.po) AS is_import "
                        + " ,(SELECT DISTINCT p.supplier_t FROM len_enq_po p WHERE b.po = p.po LIMIT 1) AS supplier_t "
                        + " , bap.bapb "
                        + " ,(SELECT DISTINCT st.`status` FROM len_delivered st WHERE b.po = st.po LIMIT 1) AS barang_tiba "
                        + " ,(SELECT DISTINCT sp.spp_number FROM len_spp sp WHERE b.po = sp.po LIMIT 1) AS spp "
                        + " ,d.status "
                        + " ,(SELECT DISTINCT p.`status` FROM len_enq_po p WHERE b.po = p.po LIMIT 1) AS status_po "
                        + " FROM len_enq_dpb d "
                        + " LEFT JOIN len_enq_spph b ON b.dpb = d.dpb "
                        + " LEFT JOIN len_enq_bapb bap ON bap.po = b.po "
                        + " WHERE LEFT(d.tgl_pengajuan, 4) = '" + yearNow + "' "
                        + " AND d.user_dpb = '"+ userDpb +"' "
                        + " ORDER BY d.tgl_pengajuan DESC, d.dpb DESC ";
            try
            {
                // increasing time out
                db.Database.CommandTimeout = 270;
                var newModel = db.Database.SqlQuery<NewMainDashboard>(query).ToList();
                foreach (var item in newModel)
                {
                    var plk = (from dp in db.executorData
                               join bd in db.profiles
                               on dp.executor equals bd.user_id
                               where dp.dpb == item.dpb
                               select bd.initials
                         )
                            .Distinct()
                            .ToList();
                    if (plk != null && plk.Count() > 0)
                    {
                        item.plk = String.Join(", ", plk);
                    }
                }
                return newModel;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }

        }

        /// <summary>
        /// Ekspor ke excel
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public List<ExcelActive> GetExcelActive(string from, string to)
        {
            try
            {
                var query = "SELECT DISTINCT d.user_dpb AS pengguna, ( SELECT GROUP_CONCAT(lp.initials SEPARATOR ', ') FROM len_executor ex JOIN len_profile lp ON lp.user_id = ex.executor WHERE ex.dpb = d.dpb ) AS plk, d.dpb, ( SELECT ex.created_at FROM len_executor ex WHERE ex.dpb = d.dpb LIMIT 1 ) AS tgl_masuk, d.divisi_t AS divisi, d.job_code AS kode_proyek, dp.product, d.job_code_t AS nama_proyek, ( SELECT pr.product_t FROM len_enq_product pr WHERE pr.product = dp.product LIMIT 1 ) AS description, dp.account, dp.unit, dp.cur, dp.unit_price, dp.qty AS 'order', dp.cur_amount, dp.total_idr, d.tgl_pengajuan, d.tgl_dibutuhkan, b.spph, b.po, ( SELECT p.jenis_order FROM len_enq_po p WHERE b.po = p.po LIMIT 1 ) AS jenis_order, ( SELECT p.supplier_t FROM len_enq_po p WHERE b.po = p.po LIMIT 1 ) AS supplier, dp.qty AS 'order2', ( SELECT pp.cur FROM len_enq_po_product pp WHERE b.po = pp.po LIMIT 1 ) AS cur2, ( SELECT ppp.unit_price FROM len_enq_po_product ppp WHERE ppp.po = b.po AND ppp.product = dp.product LIMIT 1 ) AS harga, ( SELECT pp.cur_amount FROM len_enq_po_product pp WHERE pp.po = b.po AND pp.product = dp.product LIMIT 1 ) AS total, ( SELECT p.tgl_po FROM len_enq_po p WHERE b.po = p.po LIMIT 1 ) AS tanggal_po, ( SELECT p.tgl_habis_kontrak FROM len_enq_po p WHERE b.po = p.po LIMIT 1 ) AS tanggal_kirim, IF (( SELECT op.amendment FROM len_option_po op WHERE op.po = b.po LIMIT 1 ) IS TRUE, 'YA', 'TIDAK' ) AS eta, ( SELECT bap.tgl_print FROM len_enq_bapb bap WHERE bap.po = b.po AND bap.product = dp.product LIMIT 1 ) AS barang_tiba, IF (( SELECT op.negotiation FROM len_option_po op WHERE op.po = b.po ) IS TRUE, 'YA', 'TIDAK' ) AS negosiasi, ( SELECT ( dp.cur_amount - total ) FROM len_enq_po_product pp WHERE pp.po = b.po LIMIT 1 ) AS efisiensi, ( SELECT bap.bapb FROM len_enq_bapb bap WHERE bap.po = b.po AND bap.product = dp.product LIMIT 1 ) AS bapb, ( SELECT bap.qty FROM len_enq_bapb bap WHERE bap.po = b.po AND bap.product = dp.product LIMIT 1 ) AS jml_brg, '' AS 'status', '' AS 'keterangan' FROM len_enq_dpb d JOIN len_enq_dpb_product dp ON (dp.dpb = d.dpb AND dp.product = d.product) LEFT JOIN len_enq_spph_product b ON ( b.dpb = dp.dpb AND b.product = dp.product ) WHERE LEFT (d.tgl_pengajuan, 10) BETWEEN '" + from + "' AND '" + to + "' ORDER BY d.tgl_pengajuan ASC";
                db.Database.CommandTimeout = 1800;
                var newModel = db.Database.SqlQuery<ExcelActive>(query).ToList();
                return newModel;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }

        /// <summary>
        /// alert untuk pelaksana
        /// </summary>
        /// <param name="initial"></param>
        /// <returns></returns>
        public SummaryModel GetDangerTask(string initial)
        {
            try
            {
                string POQUERY = @System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + "Dashboard/SearchDetail/" + "2" + "?" + "query=";
                string query = "SELECT COUNT(DISTINCT s.po, op.amendment, op.tgl_pelaksanaan, op.tgl_um, p.tgl_habis_kontrak) as data1 FROM len_executor e LEFT JOIN len_enq_spph s ON s.dpb = e.dpb LEFT JOIN len_enq_po p ON p.po = s.po LEFT JOIN len_profile o ON o.user_id = e.executor LEFT JOIN len_option_po op ON op.po = s.po WHERE o.initials = '" + initial + "' AND ((( op.jaminan_pelaksanaan IS TRUE ) AND ( p.tgl_habis_kontrak > op.tgl_pelaksanaan )) OR ( op.jaminan_um IS TRUE AND ( p.tgl_habis_kontrak > op.tgl_um ))) AND op.is_done IS FALSE AND YEAR (p.tgl_habis_kontrak) = YEAR (CURDATE());";
                var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
                result.link1 = "SELECT DISTINCT s.po as result FROM len_executor e LEFT JOIN len_enq_spph s ON s.dpb = e.dpb LEFT JOIN len_enq_po p ON p.po = s.po LEFT JOIN len_profile o ON o.user_id = e.executor LEFT JOIN len_option_po op ON op.po = s.po WHERE o.initials = '" + initial + "' AND ((( op.jaminan_pelaksanaan IS TRUE ) AND ( p.tgl_habis_kontrak > op.tgl_pelaksanaan )) OR ( op.jaminan_um IS TRUE AND ( p.tgl_habis_kontrak > op.tgl_um ))) AND op.is_done IS FALSE AND YEAR (p.tgl_habis_kontrak) = YEAR (CURDATE());";
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return null;
        }

        /// <summary>
        /// mengambil data grafik
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public List<SummaryModel> GetChart(int number)
        {
            List<SummaryModel> result = new List<SummaryModel>();
            try
            {
                string query = null;
                if (number == 1)
                {
                    query = "CALL cominggoods();";
                }
                else if (number == 2)
                {
                    query = "CALL notgreen();";
                }
                else if (number == 3)
                {
                    query = "CALL kbslokal();";
                }
                else if (number == 4)
                {
                    query = "CALL notgreenlokal();";
                }
                else if (number == 5)
                {
                    query = "CALL kbsimpor();";
                }
                else if (number == 6)
                {
                    query = "CALL notgreenimpor();";
                }

                result = db.Database.SqlQuery<SummaryModel>(query).ToList();
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return result;
        }

        /// <summary>
        /// Kedatangan Barang untuk User DPB Tertentu
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>SummaryModel</returns>
        public List<SummaryModel> GetPosUserChart(string username)
        {
            List<SummaryModel> result = new List<SummaryModel>();
            try
            {
                string query = null;
                query = "CALL GetComingPerUser('" + username + "');";
                db.Database.CommandTimeout = 360;
                result = db.Database.SqlQuery<SummaryModel>(query).ToList();
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return result;
        }

        /// <summary>
        /// Keterlambatan Barang untuk User DPB Tertentu
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>SummaryModel</returns>
        public List<SummaryModel> GetNegUserChart(string username)
        {
            List<SummaryModel> result = new List<SummaryModel>();
            try
            {
                string query = null;
                query = "CALL GetNotComingPerUser('" + username + "');";
                db.Database.CommandTimeout = 270;
                result = db.Database.SqlQuery<SummaryModel>(query).ToList();
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return result;
        }
    }
}