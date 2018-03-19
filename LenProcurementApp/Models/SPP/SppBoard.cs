using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// model untuk spp board
    /// </summary>
    public class SppBoard
    {
        /// <summary>
        /// spp_id
        /// </summary>
        public int spp_id { get; set; }
        /// <summary>
        /// spp_number
        /// </summary>
        [Display(Name = "No. SPP")]
        public string spp_number { get; set; }
        /// <summary>
        /// spp_date
        /// </summary>
        [Display(Name = "Tanggal")]
        public DateTime spp_date { get; set; }
        /// <summary>
        /// payment_for
        /// </summary>
        [Display(Name = "Untuk Pembayaran")]
        public string payment_for { get; set; }
        /// <summary>
        /// bank_name
        /// </summary>
        [Display(Name = "Nama Bank")]
        public string bank_name { get; set; }
        /// <summary>
        /// bill_number
        /// </summary>
        [Display(Name = "No. Rekening")]
        public string bill_number { get; set; }
        /// <summary>
        /// bill_owner
        /// </summary>
        [Display(Name = "Pemilik Rekening")]
        public string bill_owner { get; set; }
        /// <summary>
        /// claim_value
        /// </summary>
        [Display(Name = "Nilai Tagihan")]
        public double claim_value { get; set; }
        /// <summary>
        /// note
        /// </summary>
        [Display(Name = "Keterangan")]
        public string note { get; set; }

    }

}