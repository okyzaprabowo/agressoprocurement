using System.Web;
using System.Web.Mvc;

namespace LenProcurementApp
{
    /// <summary>
    /// Generate otomatis
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Generate otomatis
        /// </summary>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
