using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Database Basis dashboard sebagai acuan Main view
    /// </summary>
    [Table("len_agresso_basis")]
    public class BasisDasboard
    {
        /// <summary>
        /// basis_id
        /// </summary>
        [Key]
        public int basis_id { get; set; }
        /// <summary>
        /// dpb
        /// </summary>
        [Display(Name = "DPB")]
        public string dpb { get; set; }
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
        /// user_dpb
        /// </summary>
        [Display(Name = "User DPB")]
        public string user_dpb { get; set; }
        /// <summary>
        /// divisi
        /// </summary>
        [Display(Name = "Divisi")]
        public string divisi { get; set; }
        /// <summary>
        /// divisi_t
        /// </summary>
        [Display(Name = "Detail Divisi")]
        public string divisi_t { get; set; }
        /// <summary>
        /// job_code
        /// </summary>
        [Display(Name = "Job Code")]
        public string job_code { get; set; }
        /// <summary>
        /// job_code_t
        /// </summary>
        [Display(Name = "Detail Divisi")]
        public string job_code_t { get; set; }
        /// <summary>
        /// deliv_date_dpb
        /// </summary>
        [Display(Name = "Deliv Date DPB")]
        public string deliv_date_dpb { get; set; }
        /// <summary>
        /// spph
        /// </summary>
        [Display(Name = "spph")]
        public string spph { get; set; }
        /// <summary>
        /// po
        /// </summary>
        [Display(Name = "PO")]
        public string po { get; set; }
        /// <summary>
        /// supplier_t
        /// </summary>
        [Display(Name = "Supplier")]
        public string supplier_t { get; set; }
    }

}