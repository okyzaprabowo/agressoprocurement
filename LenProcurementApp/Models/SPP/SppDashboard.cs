using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Model SPP
    /// </summary>
    [Table("len_spp")]
    public class SppDashboard
    {
        /// <summary>
        /// spp_id
        /// </summary>
        [Key]
        public int spp_id { get; set; }
        /// <summary>
        /// po
        /// </summary>
        [Display(Name = "No. PO")]
        public string po { get; set; }
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
        /// document_number
        /// </summary>
        [Display(Name = "No. Dokumen")]
        public string document_number { get; set; }
        /// <summary>
        /// address
        /// </summary>
        [Display(Name = "Alamat")]
        public string address { get; set; }
        /// <summary>
        /// npwp
        /// </summary>
        [Display(Name = "NPWP")]
        public string npwp { get; set; }
        /// <summary>
        /// invoice_number
        /// </summary>
        [Display(Name = "No. Faktur Penagihan")]
        public string invoice_number { get; set; }
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
        /// payment_for
        /// </summary>
        [Display(Name = "Untuk Pembayaran")]
        public string payment_for { get; set; }
        /// <summary>
        /// attachment
        /// </summary>
        [Display(Name = "Lampiran Penagihan")]
        public string attachment { get; set; }
        /// <summary>
        /// another
        /// </summary>
        [Display(Name = "Lampiran lain")]
        public string another { get; set; }
        /// <summary>
        /// note
        /// </summary>
        [Display(Name = "Keterangan")]
        public string note { get; set; }
        /// <summary>
        /// kabag_from
        /// </summary>
        [Display(Name = "Jabatan Penandatangan SPP")]
        public string kabag_from { get; set; }
        /// <summary>
        /// kabag_from_name
        /// </summary>
        [Display(Name = "Nama Pejabat")]
        public string kabag_from_name { get; set; }
        /// <summary>
        /// kabag_from_nik
        /// </summary>
        [Display(Name = "NIK")]
        public string kabag_from_nik { get; set; }
        /// <summary>
        /// kabag_accounting_name
        /// </summary>
        [Display(Name = "Ka. Bag. Akuntansi")]
        public string kabag_accounting_name { get; set; }
        /// <summary>
        /// supplier
        /// </summary>
        public string supplier { get; set; }
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
        /// <summary>
        /// invoice_num
        /// </summary>
        public string invoice_num { get; set; }
        /// <summary>
        /// invoice_date
        /// </summary>
        public string invoice_date { get; set; }
        /// <summary>
        /// invoice_value
        /// </summary>
        public double invoice_value { get; set; }
    }
}