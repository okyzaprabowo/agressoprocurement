using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Model Pelaksana
    /// </summary>
    [Table("len_executor")]
    public class Executor
    {
        /// <summary>
        /// executor_id
        /// </summary>
        [Key]
        public int executor_id { get; set; }
        /// <summary>
        /// executor
        /// </summary>
        public string executor { get; set; }
        /// <summary>
        /// dpb
        /// </summary>
        public string dpb { get; set; }
        /// <summary>
        /// po
        /// </summary>
        public string po { get; set; }
        /// <summary>
        /// created_at
        /// </summary>
        public DateTime created_at { get; set; }
        /// <summary>
        /// updated_at
        /// </summary>
        public DateTime updated_at { get; set; }
    }

}