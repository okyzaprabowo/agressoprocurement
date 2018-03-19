using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data;

namespace LenProcurementApp.Models
{

    /// <summary>
    /// Model BAPB untuk board
    /// </summary>
    public class BapbDasboard
    {
        /// <summary>
        /// po
        /// </summary>
        [Display(Name = "PO")]
        public string po { get; set; }
        /// <summary>
        /// tgl_print
        /// </summary>
        [Display(Name = "Tgl Terbit")]
        public DateTime tgl_print { get; set; }
        /// <summary>
        /// bapb
        /// </summary>
        [Display(Name = "BAPB")]
        public string bapb { get; set; }
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
    }

}