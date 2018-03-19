using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Model untuk keterangan
    /// </summary>
    [Table("len_note")]
    public class Note
    {
        /// <summary>
        /// note_id
        /// </summary>
        [Key]
        public int note_id { get; set; }
        /// <summary>
        /// code
        /// </summary>
        [Display(Name = "Kode")]
        public string code { get; set; }
        /// <summary>
        /// po
        /// </summary>
        [Display(Name = "Po")]
        public string po { get; set; }
        /// <summary>
        /// note
        /// </summary>
        [Display(Name = "Keterangan")]
        public string note { get; set; }
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
    }

}