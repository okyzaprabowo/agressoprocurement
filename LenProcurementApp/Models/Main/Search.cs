using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// model pencarian
    /// </summary>
    public class Search
    {
        /// <summary>
        /// keyword
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// category
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// status
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// table
        /// </summary>
        public string table { get; set; }
        /// <summary>
        /// from
        /// </summary>
        public string from { get; set; }
        /// <summary>
        /// to
        /// </summary>
        public string to { get; set; }

    }

}