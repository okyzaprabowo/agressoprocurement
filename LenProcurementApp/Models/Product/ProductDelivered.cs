using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Model penyimpanan jumlah barang yang sampai
    /// </summary>
    [Table("len_product_delivered")]
    public class ProductDelivered
    {
        /// <summary>
        /// product_delivered_id
        /// </summary>
        [Key]
        public int product_delivered_id { get; set; }
        /// <summary>
        /// po
        /// </summary>
        public string po { get; set; }
        /// <summary>
        /// product
        /// </summary>
        [Display(Name = "Produk")]
        public string product { get; set; }
        /// <summary>
        /// qty_delivered
        /// </summary>
        [Display(Name = "Qty Datang")]
        public int qty_delivered { get; set; }
        /// <summary>
        /// created_at
        /// </summary>
        [Display(Name = "Dibuat Pada")]
        public DateTime created_at { get; set; }
        /// <summary>
        /// updated_at
        /// </summary>
        [Display(Name = "Diubah Pada")]
        public DateTime updated_at { get; set; }
    }

}