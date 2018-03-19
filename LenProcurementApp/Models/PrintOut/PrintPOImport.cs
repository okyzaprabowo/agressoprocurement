using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Print untuk PO Impor
    /// </summary>
    public class PrintPOImport
    {
        /// <summary>
        /// po
        /// </summary>
        public string po { get; set; }
        /// <summary>
        /// tanggal
        /// </summary>
        public DateTime tanggal { get; set; }
        /// <summary>
        /// ke
        /// </summary>
        public string ke { get; set; }
        /// <summary>
        /// alamat
        /// </summary>
        public string alamat { get; set; }
        /// <summary>
        /// no_telp
        /// </summary>
        public string no_telp { get; set; }
        /// <summary>
        /// no_fax
        /// </summary>
        public string no_fax { get; set; }
        /// <summary>
        /// attn
        /// </summary>
        public string attn { get; set; }
        /// <summary>
        /// ref_po
        /// </summary>
        public string ref_po { get; set; }
        /// <summary>
        /// product
        /// </summary>
        public string product { get; set; }
        /// <summary>
        /// qty
        /// </summary>
        public int qty { get; set; }
        /// <summary>
        /// unit_price
        /// </summary>
        public float unit_price { get; set; }
        /// <summary>
        /// total
        /// </summary>
        public float total { get; set; }
        /// <summary>
        /// condition
        /// </summary>
        public string condition { get; set; }
        /// <summary>
        /// term_of_payment
        /// </summary>
        public string term_of_payment { get; set; }
        /// <summary>
        /// pay_to_bank
        /// </summary>
        public string pay_to_bank { get; set; }
        /// <summary>
        /// shipment
        /// </summary>
        public string shipment { get; set; }
        /// <summary>
        /// delivery_time
        /// </summary>
        public string delivery_time { get; set; }
        /// <summary>
        /// delivery_to
        /// </summary>
        public string delivery_to { get; set; }
        /// <summary>
        /// attn_on_delivery
        /// </summary>
        public string attn_on_delivery { get; set; }
        /// <summary>
        /// manager
        /// </summary>
        public string manager { get; set; }
    }
}