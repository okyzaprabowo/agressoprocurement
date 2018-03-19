using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// main dashboard terbaru
    /// </summary>
    public class NewMainDashboard
    {
        /// <summary>
        /// user_dpb
        /// </summary>
        public string user_dpb { get; set; }
        /// <summary>
        /// jml_item
        /// </summary>
        public int jml_item { get; set; }
        /// <summary>
        /// plk
        /// </summary>
        public string plk { get; set; }
        /// <summary>
        /// divisi
        /// </summary>
        public string divisi { get; set; }
        /// <summary>
        /// job_code
        /// </summary>
        public string job_code { get; set; }
        /// <summary>
        /// job_code_t
        /// </summary>
        public string job_code_t { get; set; }
        /// <summary>
        /// dpb
        /// </summary>
        public string dpb { get; set; }
        /// <summary>
        /// spph
        /// </summary>
        public string spph { get; set; }
        /// <summary>
        /// po
        /// </summary>
        public string po { get; set; }
        /// <summary>
        /// is_import
        /// </summary>
        public int is_import { get; set; }
        /// <summary>
        /// supplier_t
        /// </summary>
        public string supplier_t { get; set; }
        /// <summary>
        /// bapb
        /// </summary>
        public string bapb { get; set; }
        /// <summary>
        /// barang_tiba
        /// </summary>
        public string barang_tiba { get; set; }
        /// <summary>
        /// spp
        /// </summary>
        public string spp { get; set; }
        /// <summary>
        /// status
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// status_po
        /// </summary>
        public string status_po { get; set; }
    }

}