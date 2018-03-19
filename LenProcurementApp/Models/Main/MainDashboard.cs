using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Main Dashboard
    /// </summary>
    public class MainDashboard
    {
        /// <summary>
        /// user
        /// </summary>
        [Display(Name = "User")]
        public string user { get; set; }
        /// <summary>
        /// jlm_item
        /// </summary>
        [Display(Name = "Jml. Item")]
        public int jlm_item { get; set; }
        /// <summary>
        /// plk
        /// </summary>
        [Display(Name = "PLK")]
        public string plk { get; set; }
        /// <summary>
        /// divisi
        /// </summary>
        [Display(Name = "Divisi")]
        public string divisi { get; set; }
        /// <summary>
        /// job_code
        /// </summary>
        [Display(Name = "Job Code")]
        public string job_code { get; set; }
        /// <summary>
        /// job_code_t
        /// </summary>
        [Display(Name = "Job Code (T)")]
        public string job_code_t { get; set; }
        /// <summary>
        /// dpb
        /// </summary>
        [Display(Name = "DPB")]
        public string dpb { get; set; }
        /// <summary>
        /// spph
        /// </summary>
        [Display(Name = "SPPH")]
        public string spph { get; set; }
        /// <summary>
        /// po
        /// </summary>
        [Display(Name = "PO")]
        public string po { get; set; }
        /// <summary>
        /// supplier_t
        /// </summary>
        [Display(Name = "Supplier (T)")]
        public string supplier_t { get; set; }
        /// <summary>
        /// bapb
        /// </summary>
        [Display(Name = "BAPB")]
        public string bapb { get; set; }
        /// <summary>
        /// b_tiba
        /// </summary>
        [Display(Name = "B. Tiba")]
        public string b_tiba { get; set; }
        /// <summary>
        /// spp
        /// </summary>
        [Display(Name = "SPP")]
        public string spp { get; set; }
    }

}