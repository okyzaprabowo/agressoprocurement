using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Script.Serialization;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// menampilkan produk untuk po
    /// </summary>
    public class PrintPoProduct
    {
        /// <summary>
        /// product
        /// </summary>
        public string product { get; set; }
        /// <summary>
        /// product_t
        /// </summary>
        public string product_t { get; set; }
        /// <summary>
        /// qty
        /// </summary>
        public int qty { get; set; }
        /// <summary>
        /// cur
        /// </summary>
        public string cur { get; set; }
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
    }

}