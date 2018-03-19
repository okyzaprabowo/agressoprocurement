using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data;

namespace LenProcurementApp.Models
{

    /// <summary>
    /// Dashboard untuk DPB
    /// </summary>
    public class DpbDashboard
    {
        /// <summary>
        /// dpb
        /// </summary>
        [Display(Name = "DPB")]
        public string dpb { get; set; }
        /// <summary>
        /// tgl_pengajuan
        /// </summary>
        [Display(Name = "Tanggal Pengajuan")]
        public DateTime tgl_pengajuan { get; set; }
        /// <summary>
        /// tgl_dibutuhkan
        /// </summary>
        [Display(Name = "Dibutuhkan Tanggal")]
        public DateTime tgl_dibutuhkan { get; set; }
        /// <summary>
        /// product
        /// </summary>
        [Display(Name = "Product")]
        public string product { get; set; }
        /// <summary>
        /// product_t
        /// </summary>
        [Display(Name = "Product(T)")]
        public string product_t { get; set; }
        /// <summary>
        /// cur
        /// </summary>
        [Display(Name = "Cur")]
        public string cur { get; set; }
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
        /// account
        /// </summary>
        [Display(Name = "Account")]
        public string account { get; set; }
    }

}