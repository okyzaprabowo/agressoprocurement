using System.Web.Mvc;
using LenProcurementApp.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Collections.Generic;

namespace LenProcurementApp.Controllers
{
    /// <summary>
    /// Controller template untuk inisialisasi beberapa variable
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// ADMIN
        /// </summary>
        public const string ADMIN = "Admin";
        /// <summary>
        /// DEVELOPER
        /// </summary>
        public const string DEVELOPER = "Developer";
        /// <summary>
        /// SPPTAGIHAN
        /// </summary>
        public const string SPPTAGIHAN = "SPP dan Tagihan";
        /// <summary>
        /// BAPBSPP
        /// </summary>
        public const string BAPBSPP = "BAPB dan SPP";
        /// <summary>
        /// USERDBP
        /// </summary>
        public const string USERDBP = "User DPB";
        /// <summary>
        /// PELAKSANA
        /// </summary>
        public const string PELAKSANA = "Pelaksana";
        /// <summary>
        /// BAPB
        /// </summary>
        public const string BAPB = "BAPB";
        /// <summary>
        /// ADMINDEVELOPER
        /// </summary>
        public const string ADMINDEVELOPER = "Admin, Developer";
        /// <summary>
        /// STRUKTURAL
        /// </summary>
        public const string STRUKTURAL = "Reporting";
        /// <summary>
        /// STRUKTURALLOKAL
        /// </summary>
        public const string STRUKTURALLOKAL = "Manager PDN";
        /// <summary>
        /// STRUKTURALIMPOR
        /// </summary>
        public const string STRUKTURALIMPOR = "Manager PLN";
        /// <summary>
        /// PENERIMABARANG
        /// </summary>
        public const string PENERIMABARANG = "Penerima Barang";
        /// <summary>
        /// BAPBSPPDEVELOPER
        /// </summary>
        public const string BAPBSPPDEVELOPER = "BAPB dan SPP, Developer";
        /// <summary>
        /// SUPERADMIN
        /// </summary>
        public const string SUPERADMIN = "Super Admin";
        /// <summary>
        /// SUPERADMINDEVELOPER
        /// </summary>
        public const string SUPERADMINDEVELOPER = "Developer, Super Admin";

        // Database
        ApplicationDbContext db = new ApplicationDbContext();
        // Log file
        log4net.ILog Log = log4net.LogManager.GetLogger(typeof(BaseController));
        /// <summary>
        /// Ketika route berjalan
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                ViewBag.useDataTable = false;
                ViewBag.useDateRangePicker = true;
                ViewBag.useDatePicker = false;
                ViewBag.userFullName = "";
                ViewBag.initial = "";
                ViewBag.position = "";
                ViewBag.noData = false;
                ViewBag.useAjax = false;
                ViewBag.useIcheck = false;
                ViewBag.useChart = false;
                ViewBag.error = null;
                ViewBag.showUsers = false;
                ViewBag.showExcel = false;
                ViewBag.full = "F";
                ViewBag.partial = "P";
                ViewBag.none = "N";
                ViewBag.developerRole = DEVELOPER;
                ViewBag.isUserDpb = false;
                if (Request.IsAuthenticated)
                {
                    if (User.IsInRole(SUPERADMIN) || User.IsInRole(DEVELOPER))
                    {
                        ViewBag.showUsers = true;
                    }
                    if (User.IsInRole(ADMIN) || User.IsInRole(DEVELOPER))
                    {
                        ViewBag.showExcel = true;
                    }
                    if (User.IsInRole(USERDBP))
                    {
                        ViewBag.isUserDpb = true;
                    }
                    getUserProfile();
                }
                base.OnActionExecuting(filterContext);
            }
            catch (System.Exception e)
            {
                Log.Error(e);
            }
        }
        /// <summary>
        /// mendapatkan informasi profile
        /// </summary>
        private void getUserProfile()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (userId != null)
                {
                    Profile profile = db.profiles.Find(userId);
                    if(profile != null)
                    {
                        ViewBag.userFullName = profile.full_name;
                        ViewBag.position = db.Roles.FirstOrDefault(u => u.Users.Any(i => i.UserId == userId)).Name;
                        ViewBag.initial = profile.initials;
                    }
                }
            }
            catch (System.Exception e)
            {
                Log.Error(e);
            }
        }

        /// <summary>
        /// mendapatkan error dari model state
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        // http://yassershaikh.com/how-to-read-all-errors-from-modelstate-in-asp-net-mvc/
        public List<string> GetErrorListFromModelState(ModelStateDictionary modelState)
        {
            try
            {
                var query = from state in modelState.Values
                            from error in state.Errors
                            select error.ErrorMessage;

                var errorList = query.ToList();
                return errorList;
            }
            catch (System.Exception e)
            {
                Log.Error(e);
                return null;
            }
        }
    }
}