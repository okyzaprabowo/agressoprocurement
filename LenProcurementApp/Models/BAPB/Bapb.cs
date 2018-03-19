using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Model BAPB
    /// </summary>
    [Table("len_enq_bapb")]
    public class Bapb
    {
        /// <summary>
        /// bapb_id
        /// </summary>
        [Key]
        public int bapb_id { get; set; }
        /// <summary>
        /// po
        /// </summary>
        [Display(Name = "PO")]
        public string po { get; set; }
        /// <summary>
        /// bapb
        /// </summary>
        [Display(Name = "BAPB")]
        public string bapb { get; set; }
        /// <summary>
        /// tgl_print
        /// </summary>
        [Display(Name = "Tgl Terbit")]
        public DateTime tgl_print { get; set; }
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
    }

}