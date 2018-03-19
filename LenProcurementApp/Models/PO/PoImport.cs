using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Script.Serialization;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Model PO Import
    /// </summary>
    [Table("len_po_import")]
    public class PoImport
    {
        /// <summary>
        /// po_import_id
        /// </summary>
        [Key]
        public int po_import_id { get; set; }
        /// <summary>
        /// po
        /// </summary>
        [Display(Name = "PO No.")]
        public string po { get; set; }
        /// <summary>
        /// tanggal
        /// </summary>
        [Display(Name = "Date (d/m/y)")]
        public DateTime tanggal { get; set; }
        /// <summary>
        /// ke
        /// </summary>
        [Display(Name = "To")]
        public string ke { get; set; }
        /// <summary>
        /// alamat
        /// </summary>
        [Display(Name = "Address")]
        public string alamat { get; set; }
        /// <summary>
        /// no_telp
        /// </summary>
        [Display(Name = "Phone  No.")]
        public string no_telp { get; set; }
        /// <summary>
        /// no_fax
        /// </summary>
        [Display(Name = "Fax  No.")]
        public string no_fax { get; set; }
        /// <summary>
        /// attn
        /// </summary>
        [Display(Name = "Attn")]
        public string attn { get; set; }
        /// <summary>
        /// ref_po
        /// </summary>
        [Display(Name = "Ref.")]
        public string ref_po { get; set; }
        /// <summary>
        /// product
        /// </summary>
        [Display(Name = "Description Of Goods")]
        public string product { get; set; }
        /// <summary>
        /// qty
        /// </summary>
        [Display(Name = "Qty")]
        public int qty { get; set; }
        /// <summary>
        /// unit_price
        /// </summary>
        [Display(Name = "Unit Price")]
        public float unit_price { get; set; }
        /// <summary>
        /// total
        /// </summary>
        [Display(Name = "Total Price")]
        public float total { get; set; }
        /// <summary>
        /// condition
        /// </summary>
        [Display(Name = "Condition   ")]
        public string condition { get; set; }
        /// <summary>
        /// term_of_payment
        /// </summary>
        [Display(Name = "Term of Payment")]
        public string term_of_payment { get; set; }
        /// <summary>
        /// pay_to_bank
        /// </summary>
        [Display(Name = "Payment To Bank")]
        public string pay_to_bank { get; set; }
        /// <summary>
        /// shipment
        /// </summary>
        [Display(Name = "Shipment")]
        public string shipment { get; set; }
        /// <summary>
        /// delivery_time
        /// </summary>
        [Display(Name = "Delivery Time")]
        public string delivery_time { get; set; }
        /// <summary>
        /// delivery_to
        /// </summary>
        [Display(Name = "Delivery To")]
        public string delivery_to { get; set; }
        /// <summary>
        /// attn_on_delivery
        /// </summary>
        [Display(Name = "Attn on delivery")]
        public string attn_on_delivery { get; set; }
        /// <summary>
        /// manager
        /// </summary>
        [Display(Name = "Purchaser")]
        public string manager { get; set; }
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
        /// state
        /// </summary>
        public int state { get; set; }
    }

}