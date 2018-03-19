using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// format invoice
    /// </summary>
    public class InvoiceFormat
    {
        /// <summary>
        /// number
        /// </summary>
        public string number { get; set; }
        /// <summary>
        /// date
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// total
        /// </summary>
        public double total { get; set; }
    }
}