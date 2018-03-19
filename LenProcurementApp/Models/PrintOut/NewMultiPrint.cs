using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// multiple print PO
    /// </summary>
    public class NewMultiPrint
    {
        /// <summary>
        /// spp_id
        /// </summary>
        public int spp_id { get; set; }
        /// <summary>
        /// job_code
        /// </summary>
        public string job_code { get; set; }
        /// <summary>
        /// job_code_t
        /// </summary>
        public string job_code_t { get; set; }
        /// <summary>
        /// po
        /// </summary>
        public string po { get; set; }
        /// <summary>
        /// supplier
        /// </summary>
        public string supplier { get; set; }
        /// <summary>
        /// address
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// npwp
        /// </summary>
        public string npwp { get; set; }
        /// <summary>
        /// spp_number
        /// </summary>
        public string spp_number { get; set; }
        /// <summary>
        /// invoice_number
        /// </summary>
        public string invoice_number { get; set; }
        /// <summary>
        /// bill_number
        /// </summary>
        public string bill_number { get; set; }
        /// <summary>
        /// bank_name
        /// </summary>
        public string bank_name { get; set; }
        /// <summary>
        /// bill_owner
        /// </summary>
        public string bill_owner { get; set; }
        /// <summary>
        /// payment_for
        /// </summary>
        public string payment_for { get; set; }
        /// <summary>
        /// another
        /// </summary>
        public string another { get; set; }
        /// <summary>
        /// attachment
        /// </summary>
        public string attachment { get; set; }
        /// <summary>
        /// kabag_from
        /// </summary>
        public string kabag_from { get; set; }
        /// <summary>
        /// kabag_from_name
        /// </summary>
        public string kabag_from_name { get; set; }
        /// <summary>
        /// kabag_from_nik
        /// </summary>
        public string kabag_from_nik { get; set; }
        /// <summary>
        /// kabag_accounting_name
        /// </summary>
        public string kabag_accounting_name { get; set; }
    }
}