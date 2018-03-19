using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Script.Serialization;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// model po produk
    /// </summary>
    [Table("len_enq_po_product")]
    public class PoProduct
    {
        /// <summary>
        /// po_produk_id
        /// </summary>
        [Key]
        public int po_produk_id { get; set; }
        /// <summary>
        /// po
        /// </summary>
        [Display(Name = "DPB")]
        public string po { get; set; }
        /// <summary>
        /// tgl_po
        /// </summary>
        [Display(Name = "Tanggal PO")]
        public DateTime tgl_po { get; set; }
        /// <summary>
        /// product
        /// </summary>
        [Display(Name = "Produk")]
        public string product { get; set; }
        /// <summary>
        /// qty
        /// </summary>
        [Display(Name = "Qty")]
        public int qty { get; set; }
        /// <summary>
        /// cur
        /// </summary>
        [Display(Name = "Cur")]
        public string cur { get; set; }
        /// <summary>
        /// unit_price
        /// </summary>
        [Display(Name = "Unit Price")]
        public float unit_price { get; set; }
        /// <summary>
        /// cur_amount
        /// </summary>
        [Display(Name = "Cur Amount")]
        public float cur_amount { get; set; }
        /// <summary>
        /// total_idr
        /// </summary>
        [Display(Name = "Total (IDR)")]
        public float total_idr { get; set; }
    }
    
}