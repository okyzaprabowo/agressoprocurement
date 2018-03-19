using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Script.Serialization;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Opsi untuk PO
    /// </summary>
    [Table("len_option_po")]
    public class PoOption
    {
        /// <summary>
        /// option_po_id
        /// </summary>
        [Key]
        public int option_po_id { get; set; }
        /// <summary>
        /// po
        /// </summary>
        [Display(Name = "PO")]
        public string po { get; set; }
        /// <summary>
        /// amendment
        /// </summary>
        [Display(Name = "Amandemen")]
        public bool amendment { get; set; }
        /// <summary>
        /// po_import
        /// </summary>
        [Display(Name = "PO Impor")]
        public bool po_import { get; set; }
        /// <summary>
        /// negotiation
        /// </summary>
        [Display(Name = "Negosiasi")]
        public bool negotiation { get; set; }
        /// <summary>
        /// jaminan_um
        /// </summary>
        [Display(Name = "Jaminan UM")]
        public bool jaminan_um { get; set; }
        /// <summary>
        /// jaminan_pelaksanaan
        /// </summary>
        [Display(Name = "Jaminan Pelaksanaan")]
        public bool jaminan_pelaksanaan { get; set; }
        /// <summary>
        /// jaminan_pemeliharaan
        /// </summary>
        [Display(Name = "Jaminan Pemeliharaan")]
        public bool jaminan_pemeliharaan { get; set; }
        /// <summary>
        /// tgl_um
        /// </summary>
        [Display(Name = "Tanggal UM")]
        public DateTime tgl_um { get; set; }
        /// <summary>
        /// tgl_pelaksanaan
        /// </summary>
        [Display(Name = "Tanggal Pelaksanaan")]
        public DateTime tgl_pelaksanaan { get; set; }
        /// <summary>
        /// tgl_pemeliharaan
        /// </summary>
        [Display(Name = "Tanggal Pemeliharaan")]
        public DateTime tgl_pemeliharaan { get; set; }
        /// <summary>
        /// is_done
        /// </summary>
        [Display(Name = "Selesai")]
        public bool is_done { get; set; }
    }

}