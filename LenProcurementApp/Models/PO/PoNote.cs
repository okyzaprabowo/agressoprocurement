using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Script.Serialization;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Model View PO
    /// </summary>
    public class PoNote
    {
        /// <summary>
        /// po
        /// </summary>
        [Display(Name = "DPB")]
        public string po { get; set; }
        /// <summary>
        /// jenis_order
        /// </summary>
        [Display(Name = "Jenis Order")]
        public string jenis_order { get; set; }
        /// <summary>
        /// tgl_po_terbit
        /// </summary>
        [Display(Name = "Tgl Terbit")]
        public DateTime tgl_po_terbit { get; set; }
        /// <summary>
        /// tgl_habis_kontrak
        /// </summary>
        [Display(Name = "Tgl Habis Kontrak")]
        public DateTime tgl_habis_kontrak { get; set; }
        /// <summary>
        /// job_code
        /// </summary>
        [Display(Name = "Job Code")]
        public string job_code { get; set; }
        /// <summary>
        /// job_code_t
        /// </summary>
        [Display(Name = "Job Code T")]
        public string job_code_t { get; set; }
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
        /// cur
        /// </summary>
        [Display(Name = "Cur")]
        public string cur { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Qty")]
        public int qty { get; set; }
        /// <summary>
        /// unit
        /// </summary>
        [Display(Name = "Unit")]
        public string unit { get; set; }
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
        /// <summary>
        /// note
        /// </summary>
        [Display(Name = "Keterangan")]
        public string note { get; set; }
    }

}