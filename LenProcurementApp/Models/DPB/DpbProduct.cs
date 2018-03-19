using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data;

namespace LenProcurementApp.Models
{

    /// <summary>
    /// Model DPB Product
    /// </summary>
    [Table("len_enq_dpb_product")]
    public class DpbProduct
    {
        /// <summary>
        /// dpb_produk_id
        /// </summary>
        [Key]
        public int dpb_produk_id { get; set; }
        /// <summary>
        /// dpb
        /// </summary>
        public string dpb { get; set; }
        /// <summary>
        /// spph
        /// </summary>
        public string spph { get; set; }
        /// <summary>
        /// product
        /// </summary>
        public string product { get; set; }
        /// <summary>
        /// qty
        /// </summary>
        public int qty { get; set; }
        /// <summary>
        /// unit
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// unit_price
        /// </summary>
        public float unit_price { get; set; }
        /// <summary>
        /// cur_amount
        /// </summary>
        public float cur_amount { get; set; }
        /// <summary>
        /// total_idr
        /// </summary>
        public float total_idr { get; set; }
        /// <summary>
        /// status
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// updated
        /// </summary>
        public DateTime updated { get; set; }
        /// <summary>
        /// account
        /// </summary>
        public string account { get; set; }
    }

}