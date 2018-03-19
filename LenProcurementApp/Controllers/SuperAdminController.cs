using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LenProcurementApp.Models;
using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace LenProcurementApp.Controllers
{
    /// <summary>
    /// controller untuk super admin
    /// </summary>
    [Authorize(Roles = SUPERADMINDEVELOPER)]
    public class SuperAdminController : BaseController
    {
        const string ERRORLIST = "errorList";
        // Log file
        log4net.ILog Log = log4net.LogManager.GetLogger(typeof(SuperAdminController));
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        /// <summary>
        /// Constructor
        /// </summary>
        public SuperAdminController()
        {
        }
        /// <summary>
        /// user manager
        /// </summary>
        /// <param name="userManager"></param>
        public SuperAdminController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        /// <summary>
        /// user manager dari framework
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get
            {
                try
                {
                    return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    return null;
                }
            }
            private set
            {
                _userManager = value;
            }
        }
        
        /// <summary>
        /// menampilkan semua list user
        /// </summary>
        /// <param name="userName">digunakan jika ada perubahan data di username tertentu</param>
        /// <returns></returns>
        public ActionResult ShowUsers(string userName)
        {
            ViewBag.useDataTable = true;
            ViewBag.roleUserDpb = USERDBP;
            try
            {
                var users = db.Users.ToList();
                var model = new List<EditUserViewModel>();
                foreach (var user in users)
                {
                    var profile = db.profiles.FirstOrDefault(p => p.user_id == user.Id);
                    var dataSource = db.Roles.Where(s => s.Users.Any(r => r.UserId == user.Id)).ToList();
                    var role = GetOneRole(dataSource);
                    var currentUser = new EditUserViewModel(user, profile, role);
                    model.Add(currentUser);
                }
                ViewBag.Model = model;
                ViewBag.Roles = null;
                var list = db.Roles.OrderBy(o => o.Name).Select(rr => new SelectListItem { Value = rr.Name, Text = rr.Name });
                ViewBag.Roles = list;
                if (TempData[ERRORLIST] != null)
                {
                    ViewBag.errorList = TempData[ERRORLIST];
                }

                if (userName != null)
                {
                    var editUser = db.Users.FirstOrDefault(u => u.UserName == userName);
                    string uid = editUser.Id;
                    var profile = db.profiles.Find(uid);
                    string role = db.Roles.FirstOrDefault(f => f.Users.Any(a => a.UserId == uid)).Name;
                    JavascriptStatic.Log(role);
                    EditUserViewModel editUserModel = new EditUserViewModel()
                    {
                        user_id = editUser.Id,
                        email = editUser.Email,
                        full_name = profile.full_name,
                        initials = profile.initials,
                        position = role,
                        divisi = profile.divisi
                    };
                    return View(editUserModel);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return View();
        }
        
        /// <summary>
        /// membuat user baru
        /// </summary>
        /// <param name="model">model user dari framework identity</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateUser(EditUserViewModel model)
        {
            List<string> errorList = new List<string>();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new ApplicationUser { UserName = model.UserName, Email = model.UserName + "@len.co.id" };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        if (model.position != null)
                        {
                            //try
                            //{
                            await UserManager.AddToRoleAsync(user.Id, model.position);
                            //}
                            //catch
                            //{

                            //}
                        }
                        // Save profile
                        db.profiles.Add(new Profile
                        {
                            user_id = user.Id,
                            full_name = model.full_name,
                            initials = model.initials,
                            divisi = model.divisi
                        });
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
            else
            {
                errorList = GetErrorListFromModelState(ModelState);
                TempData["errorList"] = errorList;
            }
            return RedirectToAction("ShowUsers");
        }
        
        /// <summary>
        /// Fungsi untuk mengedit user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> EditUser(EditUserViewModel model)
        {
            try
            {
                var editUser = db.Users.FirstOrDefault(u => u.Id == model.user_id);
                var editProfile = db.profiles.FirstOrDefault(p => p.user_id == model.user_id);
                string oldRole = GetRoleFromUID(model.user_id);
                // User
                editUser.UserName = model.UserName;
                // Profile
                editProfile.full_name = model.full_name;
                editProfile.initials = model.initials;
                editProfile.divisi = model.divisi;

                if (model.position != null && model.position != oldRole)
                {
                    await ChangeRoleWithUID(model.user_id, model.position);
                }

                if (model.Password != null)
                {
                    //await UserManager.AddPasswordAsync(model.user_id, model.Password);
                    //var tes1 = await UserManager.RemovePasswordAsync(model.user_id);
                    //System.Diagnostics.Debug.WriteLine(tes1);
                    //await UserManager.AddPasswordAsync(model.user_id, model.Password);

                    var newHashPass = UserManager.PasswordHasher.HashPassword(model.Password);
                    editUser.PasswordHash = newHashPass;

                }

                db.Entry(editUser).State = System.Data.Entity.EntityState.Modified;
                db.Entry(editProfile).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return RedirectToAction("ShowUsers");
        }
        
        /// <summary>
        /// menghapus user
        /// </summary>
        /// <param name="userName">username yang akan di hapus</param>
        /// <returns></returns>
        public ActionResult DeleteUser(string userName)
        {
            try
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == userName);
                db.Users.Remove(user);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
            return RedirectToAction("ShowUsers");
        }
        
        /// <summary>
        /// menampilkan role
        /// fungsi role ini dan fungsi-fungsi dibawahnya tidak digunakan di aplikasi produksi karena role tidak berubah
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowRoles()
        {
            // prepopulat roles for the view dropdown
            try
            {
                var usersList = db.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
                ViewBag.Users = usersList;
                var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return View(db.Roles.OrderBy(o=>o.Name).ToList());
        }

        /// <summary>
        /// tidak dibutuhkan kecuali ada penambahan role diluar requirement Add new role
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateRole()
        {
            return View();
        }

        /// <summary>
        /// Add new role post
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(FormCollection collection)
        {
            try
            {
                db.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                db.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                // prepopulat roles for the view dropdown
                var usersList = db.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
                ViewBag.Users = usersList;
                var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
                return RedirectToAction("ShowRoles", db.Roles.OrderBy(o => o.Name).ToList());
            }
            catch (Exception e)
            {
                Log.Error(e);
                return View();
            }
        }
        /// <summary>
        /// Edit role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public ActionResult EditRole(string roleName)
        {
            try
            {
                var thisRole = db.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                return View(thisRole);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return View();
            }
            
        }

        /// <summary>
        /// post edit role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                db.Entry(role).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                // prepopulat roles for the view dropdown
                var usersList = db.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
                ViewBag.Users = usersList;
                var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
                return RedirectToAction("ShowRoles", db.Roles.OrderBy(o => o.Name).ToList());
            }
            catch (Exception e)
            {
                Log.Error(e);
                return RedirectToAction("ShowRoles", db.Roles.OrderBy(o => o.Name).ToList());
            }
        }
        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns> 
        public ActionResult DeleteRole(string roleName)
        {
            try
            {
                var thisRole = db.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                db.Roles.Remove(thisRole);
                db.SaveChanges();
                // prepopulat roles for the view dropdown
                var usersList = db.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
                ViewBag.Users = usersList;
                var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
                return RedirectToAction("ShowRoles", db.Roles.OrderBy(o => o.Name).ToList());
            }
            catch (Exception e)
            {
                Log.Error(e);
                return RedirectToAction("ShowRoles", db.Roles.OrderBy(o => o.Name).ToList());
            }
        }
        /// <summary>
        /// Manage role
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageUserRoles()
        {
            // prepopulat roles for the view dropdown
            var usersList = db.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
            ViewBag.Users = usersList;
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        /// <summary>
        /// Role Add To User
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RoleAddToUser(string userName, string roleName)
        {
            try
            {
                ViewBag.a2 = userName;
                ViewBag.a3 = roleName;

                ApplicationUser user = db.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                ViewBag.a1 = user.Id;
                await UserManager.AddToRoleAsync(user.Id, roleName);
                ViewBag.ResultMessage = "Role created successfully !";

                // prepopulat roles for the view dropdown
                var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
                var usersList = db.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
                ViewBag.Users = usersList;

                return View("ShowRoles", db.Roles.OrderBy(o => o.Name).ToList());
            }
            catch (Exception e)
            {
                Log.Error(e);
                ViewBag.ResultMessage = "Role Error !";
                return View("ShowRoles", db.Roles.OrderBy(o => o.Name).ToList());
            }
        }
        /// <summary>
        /// Get Roles
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetRoles(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                ApplicationUser user = db.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                //var account = new AccountController();

                ViewBag.RolesForThisUser = await UserManager.GetRolesAsync(user.Id);

                // prepopulat roles for the view dropdown
                var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
                var usersList = db.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
                ViewBag.Users = usersList;
            }

            return View("ShowRoles", db.Roles.OrderBy(o => o.Name).ToList());
        }
        /// <summary>
        /// Get User from role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetUserFromRole(string roleName)
        {
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                var roleId = db.Roles.FirstOrDefault(i => i.Name.Contains(roleName)).Id;
                ViewBag.UserForThisRole = db.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(roleId)).ToList();
                // prepopulat roles for the view dropdown
                var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
                var usersList = db.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
                ViewBag.Users = usersList;
            }

            return View("ShowRoles", db.Roles.OrderBy(o => o.Name).ToList());
        }
        /// <summary>
        /// Delete Role For User
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRoleForUser(string userName, string roleName)
        {
            //var account = new AccountController();
            ApplicationUser user = db.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            bool isInRole = await UserManager.IsInRoleAsync(user.Id, roleName);
            if (isInRole)
            {
                await UserManager.RemoveFromRoleAsync(user.Id, roleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            var usersList = db.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
            ViewBag.Users = usersList;

            return View("ShowRoles", db.Roles.OrderBy(o => o.Name).ToList());
        }
        /// <summary>
        /// Ambil Role
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        private string GetOneRole(List<Microsoft.AspNet.Identity.EntityFramework.IdentityRole> roles)
        {
            string role = null;
            foreach (var theRole in roles)
            {
                role = theRole.Name;
            }
            return role;
        }

        /// <summary>
        /// Ambil role berdasarkan uid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private string GetRoleFromUID(string userId)
        {
            var dataSource = db.Roles.Where(s => s.Users.Any(r => r.UserId == userId)).ToList();
            string role = GetOneRole(dataSource);
            return role;
        }

        /// <summary>
        /// Ganti role mnggunakan uid
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        private async Task ChangeRoleWithUID(string userId, string roleName)
        {
            var uRoles = await UserManager.GetRolesAsync(userId);
            foreach (var role in uRoles)
            {
                await UserManager.RemoveFromRoleAsync(userId, role);
            }
            await UserManager.AddToRoleAsync(userId, roleName);
        }
    }
}