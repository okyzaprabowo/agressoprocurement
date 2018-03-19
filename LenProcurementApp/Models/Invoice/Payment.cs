using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Model Pembayaran
    /// </summary>
    [Table("len_payment")]
    public class Payment
    {
        /// <summary>
        /// payment_id
        /// </summary>
        [Key]
        public int payment_id { get; set; }
        /// <summary>
        /// po
        /// </summary>
        public string po { get; set; }
        /// <summary>
        /// payment
        /// </summary>
        [Display(Name = "Pembayaran")]
        public string payment { get; set; }
        /// <summary>
        /// payment_method
        /// </summary>
        [Display(Name = "Metode")]
        public string payment_method { get; set; }
        /// <summary>
        /// payment_schedule
        /// </summary>
        //[DisplayFormat(DataFormatString = "{0:dd/MM/YYYY}", ApplyFormatInEditMode = true)]
        [Display(Name = "Jadwal Pembayaran")]
        public DateTime payment_schedule { get; set; }
        /// <summary>
        /// claim_value
        /// </summary>
        [Display(Name = "Nilai Tagihan")]
        public double claim_value { get; set; }
        /// <summary>
        /// claim_date
        /// </summary>
        //[DisplayFormat(DataFormatString = "{0:dd/MM/YYYY}", ApplyFormatInEditMode = true)]
        [Display(Name = "Tgl. Tagihan Masuk")]
        public DateTime claim_date { get; set; }
        /// <summary>
        /// status
        /// </summary>
        [Display(Name = "Status")]
        public bool status { get; set; }
        /// <summary>
        /// note
        /// </summary>
        [Display(Name = "Keterangan")]
        public string note { get; set; }
        /// <summary>
        /// import_tax
        /// </summary>
        [Display(Name = "Pajak Impor")]
        public bool import_tax { get; set; }
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