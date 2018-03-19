using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// multiple print PO
    /// </summary>
    public class MultiplePrint
    {
        /// <summary>
        /// multiple_po
        /// </summary>
        public List<string> multiple_po { get; set; }
        /// <summary>
        /// spp_number
        /// </summary>
        public string spp_number { get; set; }
        /// <summary>
        /// spp_date
        /// </summary>
        public DateTime spp_date { get; set; }
        /// <summary>
        /// 
        /// </summary>document_number
        public List<string> document_number { get; set; }
        /// <summary>
        /// address
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// npwp
        /// </summary>
        public string npwp { get; set; }
        /// <summary>
        /// invoice_number
        /// </summary>
        public List<InvoiceFormat> invoice_number { get; set; }
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
        public List<string> payment_for { get; set; }
        /// <summary>
        /// attachment
        /// </summary>
        public string attachment { get; set; }
        /// <summary>
        /// another
        /// </summary>
        public List<string> another { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> job_code_x { get; set; }
        /// <summary>
        /// supplier
        /// </summary>
        public string supplier { get; set; }
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
        /// <summary>
        /// state
        /// </summary>
        public int state { get; set; }
        /// <summary>
        /// total
        /// </summary>
        public double total { get; set; }
        /// <summary>
        /// intWord
        /// </summary>
        public string intWord { get; set; }

    }
}