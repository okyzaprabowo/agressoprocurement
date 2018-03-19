using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LenProcurementApp.Models;

namespace LenProcurementApp.Controllers
{
    /// <summary>
    /// Account controller, digenerate otomatis dari visual Studio
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        // Log
        log4net.ILog Log = log4net.LogManager.GetLogger(typeof(AccountController));
        /// <summary>
        /// contructor
        /// </summary>
        public AccountController()
        {
        }
        /// <summary>
        /// User manager
        /// </summary>
        /// <param name="userManager">User Manager</param>
        /// <param name="signInManager">sign In Manager</param>
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        /// <summary>
        /// digenerate otomatis dari visual Studio
        /// </summary>
        public ApplicationSignInManager SignInManager
        {
            get
            {
                try
                {
                    return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    return null;
                }
                
            }
            private set 
            { 
                _signInManager = value; 
            }
        }
        /// <summary>
        /// digenerate otomatis dari visual Studio
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
        /// Login 
        /// </summary>
        /// <param name="returnUrl">Url jika user telah mengunjungi url dan redirect ke login</param>
        /// <returns>Login view</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                if (Request.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Post login, ketika menekan tombol submit
        /// </summary>
        /// <param name="model">LoginViewModel</param>
        /// <param name="returnUrl">Url jika user telah mengunjungi url dan redirect ke login</param>
        /// <returns>Index / main dashboard</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            try
            {
                var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Login Gagal, Silahkan coba lagi!");
                        return View(model);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Login Gagal, Silahkan hubungi Administrator.");
                Log.Error(e);
                return View(model);
            }
        }

        // keluar
        // POST: /Account/LogOff
        /// <summary>
        /// digenerate otomatis dari visual Studio
        /// </summary>
        /// <returns>RedirectToAction Login</returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return RedirectToAction("Login", "Account");
        }

        // region auto generate
        #region AutoGenerate

        // generate otomatis dan tidak digunakan dalam aplikasi
        // GET: /Account/VerifyCode
        /// <summary>
        /// RedirectToAction
        /// </summary>
        /// <param name="provider">provider</param>
        /// <param name="returnUrl">returnUrl</param>
        /// <param name="rememberMe">rememberMe</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        // POST: /Account/VerifyCode
        /// <summary>
        /// RedirectToAction
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>View</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        // GET: /Account/Register
        /// <summary>
        /// generate otomatis
        /// </summary>
        /// <returns>View</returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        // POST: /Account/Register
        /// <summary>
        /// generate otomatis
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>View</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var user = new ApplicationUser { UserName = model.Username, Email = model.Username };
                    var user = new ApplicationUser { UserName = model.Username };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Home");
                    }
                    AddErrors(result);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        // GET: /Account/ConfirmEmail
        /// <summary>
        /// generate otomatis dan tidak digunakan dalam aplikasi
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="code">code</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        // GET: /Account/ForgotPassword
        /// <summary>
        /// generate otomatis dan tidak digunakan dalam aplikasi
        /// </summary>
        /// <returns>View</returns>
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        // POST: /Account/ForgotPassword
        /// <summary>
        /// generate otomatis dan tidak digunakan dalam aplikasi
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>View</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        // GET: /Account/ForgotPasswordConfirmation
        /// <summary>
        /// generate otomatis
        /// </summary>
        /// <returns>View</returns>
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        // GET: /Account/ResetPassword
        /// <summary>
        /// generate otomatis
        /// </summary>
        /// <param name="code"></param>
        /// <returns>View</returns>
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        // POST: /Account/ResetPassword
        /// <summary>
        /// generate otomatis
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>View</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        // GET: /Account/ResetPasswordConfirmation
        /// <summary>
        /// generate otomatis
        /// </summary>
        /// <returns>View</returns>
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        // POST: /Account/ExternalLogin
        /// <summary>
        /// generate otomatis dan tidak digunakan dalam aplikasi
        /// </summary>
        /// <param name="provider">provider</param>
        /// <param name="returnUrl">returnUrl</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        // GET: /Account/SendCode
        /// <summary>
        /// generate otomatis
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="rememberMe"></param>
        /// <returns>View</returns>
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        // POST: /Account/SendCode
        /// <summary>
        /// generate otomatis
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>View</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        // GET: /Account/ExternalLoginCallback
        /// <summary>
        /// generate otomatis dan tidak digunakan dalam aplikasi
        /// </summary>
        /// <param name="returnUrl">returnUrl</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        // POST: /Account/ExternalLoginConfirmation
        /// <summary>
        /// generate otomatis dan tidak digunakan dalam aplikasi
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="returnUrl">returnUrl</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }


        // Role Add To User
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> RoleAddToUser(string UserName, string RoleName)
        //{

        //    ViewBag.a2 = UserName;
        //    ViewBag.a3 = RoleName;

        //    ApplicationUser user = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

        //    ViewBag.a1 = user.Id;

        //    //var account = new AccountController();
        //    //try
        //    //{
        //        await UserManager.AddToRoleAsync(user.Id, RoleName);
        //        ViewBag.ResultMessage = "Role created successfully !";
        //    //}
        //    //catch
        //    //{
        //    //    ViewBag.ResultMessage = "Role Error !";
        //    //}


        //    // prepopulat roles for the view dropdown
        //    var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
        //    ViewBag.Roles = list;
        //    var usersList = db.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
        //    ViewBag.Users = usersList;

        //    return View("~/SuperAdmin/ManageUserRoles");
        //}

        // generate otomatis dan tidak digunakan dalam aplikasi
        // GET: /Account/ExternalLoginFailure
        /// <summary>
        /// generate otomatis dan tidak digunakan dalam aplikasi
        /// </summary>
        /// <returns>View</returns>
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        // generate otomatis dan tidak digunakan dalam aplikasi
        /// <summary>
        /// generate otomatis
        /// </summary>
        /// <param name="disposing">disposing</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #endregion

        // region generate otomatis dan tidak digunakan dalam aplikasi
        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";
        /// <summary>
        /// generate otomatis
        /// </summary>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        /// <summary>
        /// generate otomatis
        /// </summary>
        /// <param name="result">result</param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        /// <summary>
        /// generate otomatis
        /// </summary>
        /// <param name="returnUrl">returnUrl</param>
        /// <returns>RedirectToAction</returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Dashboard");
        }
        /// <summary>
        /// generate otomatis
        /// </summary>
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            /// <summary>
            /// generate otomatis
            /// </summary>
            /// <param name="provider">provider</param>
            /// <param name="redirectUri">redirectUri</param>
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }
            /// <summary>
            /// generate otomatis
            /// </summary>
            /// <param name="provider">provider</param>
            /// <param name="redirectUri">redirectUri</param>
            /// <param name="userId">userId</param>
            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }
            /// <summary>
            /// generate otomatis
            /// </summary>
            /// <param name="context">context</param>
            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}