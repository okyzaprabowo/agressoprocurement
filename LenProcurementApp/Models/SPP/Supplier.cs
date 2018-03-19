using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// model spph
    /// </summary>
    [Table("len_enq_supplier")]
    public class Supplier
    {
        /// <summary>
        /// supplier_id
        /// </summary>
        [Key]
        public int supplier_id { get; set; }
        /// <summary>
        /// supp_id
        /// </summary>
        public string supp_id { get; set; }
        /// <summary>
        /// supp_id_t
        /// </summary>
        public string supp_id_t { get; set; }
        /// <summary>
        /// address
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// npwp
        /// </summary>
        public string npwp { get; set; }
        /// <summary>
        /// bill_number
        /// </summary>
        public string bill_number { get; set; }
    }
}