using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Model untuk Database profile
    /// </summary>
    [Table("len_profile")]
    public class Profile
    {
        /// <summary>
        /// user_id
        /// </summary>
        [Key]
        [Display(Name = "ID User")]
        [StringLength(128)]
        public string user_id { get; set; }
        /// <summary>
        /// full_name
        /// </summary>
        [Display(Name = "Nama Lengkap")]
        [StringLength(50, ErrorMessage = "Jumlah huruf minimal 3 dan maksimal 50", MinimumLength = 3)]
        public string full_name { get; set; }
        /// <summary>
        /// initials
        /// </summary>
        [Display(Name = "Inisial")]
        [StringLength(5, ErrorMessage = "Jumlah huruf minimal 3 dan maksimal 5", MinimumLength = 3)]
        public string initials { get; set; }
        /// <summary>
        /// divisi
        /// </summary>
        [Display(Name = "Divisi")]
        public int divisi { get; set; }
        /// <summary>
        /// another
        /// </summary>
        [Display(Name = "Lain-lain")]
        public string another { get; set; }
    }
    
}