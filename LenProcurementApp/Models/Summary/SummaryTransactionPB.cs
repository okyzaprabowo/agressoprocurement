using System.Linq;
namespace LenProcurementApp.Models
{
    /// <summary>
    /// transaksi summary penerima barang
    /// </summary>
    public class SummaryTransactionPB
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
            string query = "SELECT DISTINCT COUNT(lep.po) AS data1 FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po WHERE ld.`status` != 'F' AND lep.tgl_habis_kontrak - NOW() > 0 ORDER BY lep.tgl_habis_kontrak DESC;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "∑ PO belum datang barangnya & belum jatuh tempo kontrak";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT lep.po AS result FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po WHERE ld.`status` != 'F' AND lep.tgl_habis_kontrak - NOW() > 0 ORDER BY lep.tgl_habis_kontrak DESC;";
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
            string query = "SELECT DISTINCT COUNT(ld.po) AS data1 FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po WHERE ld.`status` != 'F' AND lep.tgl_habis_kontrak - NOW() <= 0 ORDER BY lep.tgl_habis_kontrak DESC;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "∑ PO belum datang barangnya & sudah jatuh tempo kontrak";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT ld.po AS result FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po WHERE ld.`status` != 'F' AND lep.tgl_habis_kontrak - NOW() <= 0 ORDER BY lep.tgl_habis_kontrak DESC;";
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
            string query = "SELECT DISTINCT COUNT(ld.po) AS data1 FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po WHERE ld.`status` = 'P' AND lep.tgl_habis_kontrak - NOW() <= 0 ORDER BY lep.tgl_habis_kontrak DESC;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Jumlah (∑) PO kedatangan barang parsial";
            model.name2 = "";
            model.link1 = POQUERY + "SELECT DISTINCT ld.po AS result FROM len_delivered ld JOIN len_enq_po lep ON lep.po = ld.po WHERE ld.`status` = 'P' AND lep.tgl_habis_kontrak - NOW() <= 0 ORDER BY lep.tgl_habis_kontrak DESC;";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = false;
            return model;
        }
        /// <summary>
        /// Prosentase(%) Jumlah (∑) item Barang datang / PO sudah jatuh tempo
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary4()
        {
            SummaryModel model = new SummaryModel();
            string query = "SELECT ROUND( x.full_item / y.habis_kontrak * 100, 2 ) AS data1 FROM ( SELECT COUNT(lep.po) AS full_item FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON lpd.product = lepp.product WHERE lep.tgl_habis_kontrak < CURDATE() AND lepp.qty = lpd.qty_delivered AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE())) x JOIN ( SELECT COUNT(lep.po) AS habis_kontrak FROM len_enq_po lep LEFT JOIN len_enq_po_product lepp ON lepp.po = lep.po LEFT JOIN len_product_delivered lpd ON lpd.product = lepp.product WHERE lep.tgl_habis_kontrak < CURDATE() AND YEAR (lep.tgl_habis_kontrak) = YEAR (CURDATE())) y;";
            var result = db.Database.SqlQuery<SummaryModel>(query).FirstOrDefault();
            model.name1 = "Prosentase(%) Jumlah (∑) item Barang datang / PO sudah jatuh tempo";
            model.name2 = "";
            model.link1 = "";
            model.link2 = "";
            model.data1 = result.data1;
            model.data2 = "";
            model.percentage = true;
            return model;
        }
        // grafik
        /// <summary>
        /// Kedatangan Barang di Penerima Barang
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary5()
        {
            SummaryModel model = new SummaryModel();
            string query = "CALL get_coming_goods(1);";
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
        /// Keterlambatan Barang di Penerima Barang
        /// </summary>
        /// <returns>hasil summary dalam bentuk model summary</returns>
        public SummaryModel GetSummary6()
        {
            SummaryModel model = new SummaryModel();
            string query = "CALL get_not_green();";
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
    }
}