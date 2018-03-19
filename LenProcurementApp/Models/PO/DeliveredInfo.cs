using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Model status Barang tiba
    /// </summary>
    [Table("len_delivered")]
    public class DeliveredInfo
    {
        /// <summary>
        /// delivered_id
        /// </summary>
        [Key]
        public int delivered_id { get; set; }
        /// <summary>
        /// po
        /// </summary>
        public string po { get; set; }
        /// <summary>
        /// status
        /// </summary>
        [Display(Name = "Status")]
        public string status { get; set; }
        /// <summary>
        /// note
        /// </summary>
        [Display(Name = "Keterangan")]
        public string note { get; set; }
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