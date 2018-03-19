using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Script.Serialization;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// View Pembayaran
    /// </summary>
    public class PoDelivered
    {
        /// <summary>
        /// po
        /// </summary>
        [Display(Name = "No. PO")]
        public string po { get; set; }
        /// <summary>
        /// product
        /// </summary>
        [Display(Name = "Produk")]
        public string product { get; set; }
        /// <summary>
        /// product_t
        /// </summary>
        [Display(Name = "Detail Produk")]
        public string product_t { get; set; }
        /// <summary>
        /// qty
        /// </summary>
        [Display(Name = "Qty")]
        public int qty { get; set; }
        /// <summary>
        /// unit
        /// </summary>
        [Display(Name = "Unit")]
        public string unit { get; set; }
        /// <summary>
        /// qty_delivered
        /// </summary>
        [Display(Name = "Qty Datang")]
        public int qty_delivered { get; set; }
    }

}