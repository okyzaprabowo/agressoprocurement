using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// invoice model
    /// </summary>
    [Table("len_invoice")]
    public class Invoice
    {
        /// <summary>
        /// invoice_id
        /// </summary>
        [Key]
        public int invoice_id { get; set; }
        /// <summary>
        /// po
        /// </summary>
        [Display(Name = "No. PO")]
        public string po { get; set; }
        /// <summary>
        /// invoice_number
        /// </summary>
        [Display(Name = "No. Faktur")]
        public string invoice_number { get; set; }
        /// <summary>
        /// invoice_date
        /// </summary>
        [Display(Name = "Tanggal")]
        public DateTime invoice_date { get; set; }
        /// <summary>
        /// invoice_total
        /// </summary>
        [Display(Name = "Total")]
        public int invoice_total { get; set; }
        /// <summary>
        /// created_at
        /// </summary>
        [Display(Name = "Dibuat Pada")]
        public DateTime created_at { get; set; }
        /// <summary>
        /// updated_at
        /// </summary>
        [Display(Name = "Diubah Pada")]
        public DateTime updated_at { get; set; }
    }

}