using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data;

namespace LenProcurementApp.Models
{

    /// <summary>
    /// Transaksi BAPB
    /// </summary>
    public class BapbTransactions
    {
        // Log
        log4net.ILog Log = log4net.LogManager.GetLogger(typeof(BapbTransactions));
        // database
        private ApplicationDbContext db = new ApplicationDbContext();
        string _bapb;
        /// <summary>
        /// construct
        /// </summary>
        /// <param name="bapb"></param>
        public BapbTransactions(string bapb)
        {
            _bapb = bapb;
        }
        /// <summary>
        /// mendapatkan DPB dari kode PO
        /// </summary>
        /// <param name="poCode">kode PO</param>
        /// <returns></returns>
        public string getDpb(string poCode)
        {
            try
            {
                return db.dbSpph.FirstOrDefault(p => p.po == poCode).dpb;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }

        }
        /// <summary>
        /// mendapatkan produk untuk BAPB
        /// </summary>
        /// <returns></returns>
        public List<BapbDasboard> getProducts()
        {
            try
            {
                var query = "SELECT DISTINCT "
                           + " ba.po, "
                           + " ba.bapb, "
                           + " ba.tgl_print, "
                           + " ba.product, "
                           + " p.product_t, "
                           + " ba.qty, "
                           + " p.unit "
                           + " FROM len_enq_bapb ba "
                           + " JOIN len_enq_product p ON p.product = ba.product "
                           + " WHERE ba.bapb = '" + _bapb + "' ";
                var newModel = db.Database.SqlQuery<BapbDasboard>(query).ToList();
                return newModel;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new List<BapbDasboard>();
            }
        }
        /// <summary>
        /// mendapatkan catatan BAPB
        /// </summary>
        /// <returns></returns>
        public string getNote()
        {
            try
            {
                var data = db.noteData.FirstOrDefault(c => c.code == _bapb);
                if(data != null)
                {
                    return data.note;
                }
                return null;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }
    }
}