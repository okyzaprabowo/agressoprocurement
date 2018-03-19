using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Product Model
    /// </summary>
    [Table("len_enq_dpb")]
    public class Product
    {
        /// <summary>
        /// product_id
        /// </summary>
        [Key]
        public int product_id { get; set; }
        /// <summary>
        /// product
        /// </summary>
        public string product { get; set; }
        /// <summary>
        /// product_t
        /// </summary>
        public string product_t { get; set; }
        /// <summary>
        /// unit
        /// </summary>
        public string unit { get; set; }
    }

    /// <summary>
    /// transaksi produk
    /// </summary>
    //public class ProductTransactions
    //{
    //    private ApplicationDbContext db = new ApplicationDbContext();

    //    //public string GetDetail(string product)
    //    //{
    //    //    return db.dbProduct.FirstOrDefault(p=>p.product == product).product_t;
    //    //}

    //    //public string GetUnit(string product)
    //    //{
    //    //    return db.dbProduct.FirstOrDefault(p => p.product == product).unit;
    //    //}
    //}
}