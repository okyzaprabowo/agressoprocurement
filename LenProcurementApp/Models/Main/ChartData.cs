using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// model untuk chart
    /// </summary>
    public class ChartData
    {
        /// <summary>
        /// year
        /// </summary>
        public int year { get; set; }
        /// <summary>
        /// month
        /// </summary>
        public int month { get; set; }
        /// <summary>
        /// day
        /// </summary>
        public int day { get; set; }
        /// <summary>
        /// data
        /// </summary>
        public int data { get; set; }
    }

}