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
    [Table("len_enq_spph")]
    public class Spph
    {
        /// <summary>
        /// spph_id
        /// </summary>
        [Key]
        public int spph_id { get; set; }
        /// <summary>
        /// spph
        /// </summary>
        public string spph { get; set; }
        /// <summary>
        /// dpb
        /// </summary>
        public string dpb { get; set; }
        /// <summary>
        /// po
        /// </summary>
        public string po { get; set; }
    }
}