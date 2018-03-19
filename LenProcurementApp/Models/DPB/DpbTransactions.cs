using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data;

namespace LenProcurementApp.Models
{

    /// <summary>
    /// Transaksi Data
    /// </summary>
    public class DpbTransactions
    {
        // Log
        log4net.ILog Log = log4net.LogManager.GetLogger(typeof(DpbTransactions));
        // database
        private ApplicationDbContext db = new ApplicationDbContext();
        string _dpb;
        /// <summary>
        /// construct
        /// </summary>
        /// <param name="dpb"></param>
        public DpbTransactions(string dpb)
        {
            _dpb = dpb;
        }
        /// <summary>
        /// data main dashboard
        /// </summary>
        /// <returns>list model dpb board</returns>
        public List<DpbDashboard> getDashboardData()
        {
            try
            {
                var query = "SELECT DISTINCT "
                        + " dp.dpb_produk_id,"
                        + " d.dpb, "
                        + " d.tgl_pengajuan, "
                        + " d.tgl_dibutuhkan, "
                        + " dp.product, "
                        + " (SELECT p.product_t FROM len_enq_product p WHERE dp.product = p.product LIMIT 1) as product_t, "
                        + " dp.cur as cur, "
                        + " dp.qty, "
                        + " (SELECT p.unit FROM len_enq_product p WHERE dp.product = p.product LIMIT 1) as unit, "
                        + " dp.unit_price, "
                        + " dp.cur_amount, "
                        + " dp.total_idr, "
                        + " dp.account ,"
                        + " d.status"
                        + " FROM len_enq_dpb d "
                        + " JOIN len_enq_dpb_product dp ON dp.dpb = d.dpb "
                        + " WHERE d.dpb = '" + _dpb + "' "
                        + " AND d.status != 'C';";
                var newModel = db.Database.SqlQuery<DpbDashboard>(query).ToList();
                return newModel;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new List<DpbDashboard>();
            }
        }
        /// <summary>
        /// catatan untuk dpb
        /// </summary>
        /// <returns></returns>
        public string getNote()
        {
            try
            {
                var data = db.noteData.FirstOrDefault(c => c.code == _dpb);
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
        /// <summary>
        /// mendapatkan pelaksanan untuk dpb
        /// </summary>
        /// <returns>list profile pelaksanan</returns>
        public List<Profile> getExecutor()
        {
            try
            {
                var model = (from x in db.executorData
                             join p in db.profiles
                             on x.executor equals p.user_id
                             where x.dpb == _dpb
                             select new
                             {
                                 full_name = p.full_name,
                                 initial = p.initials,
                                 user_id = p.user_id
                             }
                             )
                             .Distinct()
                .AsEnumerable().Select(x => new Profile
                {
                    full_name = x.full_name,
                    initials = x.initial,
                    user_id = x.user_id
                }).ToList();
                return model;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }
        /// <summary>
        /// List eksekutor
        /// </summary>
        /// <returns></returns>
        public List<Profile> executorList()
        {
            List<Profile> result = new List<Profile>();
            try
            {
                string query = "SELECT p.* FROM len_profile p JOIN aspnetuserroles ur ON ur.UserId = p.user_id JOIN aspnetroles r ON r.Id = ur.RoleId WHERE r.`Name` = 'Pelaksana';";
                result = db.Database.SqlQuery<Profile>(query).ToList();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return result;
        }
    }
}