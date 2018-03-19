using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data;

namespace LenProcurementApp.Models
{

    /// <summary>
    /// Model DPB
    /// </summary>
    [Table("len_enq_dpb")]
    public class Dpb
    {
        /// <summary>
        /// dpb_id
        /// </summary>
        [Key]
        public int dpb_id { get; set; }
        /// <summary>
        /// dpb
        /// </summary>
        public string dpb { get; set; }
        /// <summary>
        /// tgl_pengajuan
        /// </summary>
        public DateTime tgl_pengajuan { get; set; }
        /// <summary>
        /// tgl_dibutuhkan
        /// </summary>
        public DateTime tgl_dibutuhkan { get; set; }
        /// <summary>
        /// divisi_t
        /// </summary>
        public string divisi_t { get; set; }
        /// <summary>
        /// dept
        /// </summary>
        public int dept { get; set; }
        /// <summary>
        /// job_code
        /// </summary>
        public string job_code { get; set; }
        /// <summary>
        /// job_code_t
        /// </summary>
        public string job_code_t { get; set; }
        /// <summary>
        /// user_dpb
        /// </summary>
        public string user_dpb { get; set; }
        /// <summary>
        /// status
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// cur
        /// </summary>
        public string cur { get; set; }
    }

}