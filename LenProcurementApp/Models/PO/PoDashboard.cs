using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Script.Serialization;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Model PO Dashboard
    /// </summary>
    public class PoDashboard
    {
        /// <summary>
        /// po
        /// </summary>
        [Display(Name = "PO")]
        public string po { get; set; }
        /// <summary>
        /// jenis_order
        /// </summary>
        [Display(Name = "Jenis Order")]
        public string jenis_order { get; set; }
        /// <summary>
        /// tgl_po
        /// </summary>
        [Display(Name = "Tanggal PO")]
        public DateTime tgl_po { get; set; }
        /// <summary>
        /// tgl_habis_kontrak
        /// </summary>
        [Display(Name = "Tgl Habis Kontrak")]
        public DateTime tgl_habis_kontrak { get; set; }
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
        /// cur
        /// </summary>
        [Display(Name = "Cur")]
        public string cur { get; set; }
        /// <summary>
        /// unit
        /// </summary>
        [Display(Name = "Unit")]
        public string unit { get; set; }
        /// <summary>
        /// unit_price
        /// </summary>
        [Display(Name = "Unit Price")]
        public double unit_price { get; set; }
        /// <summary>
        /// cur_amount
        /// </summary>
        [Display(Name = "Cur Amount")]
        public double cur_amount { get; set; }
        /// <summary>
        /// total_idr
        /// </summary>
        [Display(Name = "Total (IDR)")]
        public double total_idr { get; set; }
        /// <summary>
        /// note
        /// </summary>
        [Display(Name = "Keterangan")]
        public string note { get; set; }
    }

}