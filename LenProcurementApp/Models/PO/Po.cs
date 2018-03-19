using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Script.Serialization;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// model po
    /// </summary>
    [Table("len_enq_po")]
    public class Po
    {
        /// <summary>
        /// po_id
        /// </summary>
        [Key]
        public int po_id { get; set; }
        /// <summary>
        /// po
        /// </summary>
        public string po { get; set; }
        /// <summary>
        /// spph
        /// </summary>
        public string spph { get; set; }
        /// <summary>
        /// status
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// tgl_po
        /// </summary>
        public DateTime tgl_po { get; set; }
        /// <summary>
        /// tgl_habis_kontrak
        /// </summary>
        public DateTime tgl_habis_kontrak { get; set; }
        /// <summary>
        /// jenis_order
        /// </summary>
        public string jenis_order { get; set; }
        /// <summary>
        /// supplier_t
        /// </summary>
        public string supplier_t { get; set; }
    }
}