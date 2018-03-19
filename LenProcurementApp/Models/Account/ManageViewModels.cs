using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// generate dari visual studio
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// HasPassword
        /// </summary>
        public bool HasPassword { get; set; }
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        public IList<UserLoginInfo> Logins { get; set; }
        /// <summary>
        /// PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// TwoFactor
        /// </summary>
        public bool TwoFactor { get; set; }
        /// <summary>
        /// BrowserRemembered
        /// </summary>
        public bool BrowserRemembered { get; set; }
    }
    /// <summary>
    /// Generate Otomatis
    /// </summary>
    public class ManageLoginsViewModel
    {
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
    /// <summary>
    /// Generate Otomatis
    /// </summary>
    public class FactorViewModel
    {
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        public string Purpose { get; set; }
    }
    /// <summary>
    /// Generate Otomatis
    /// </summary>
    public class SetPasswordViewModel
    {
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    /// <summary>
    /// Generate Otomatis
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    /// <summary>
    /// Generate Otomatis
    /// </summary>
    public class AddPhoneNumberViewModel
    {
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
    /// <summary>
    /// Generate Otomatis
    /// </summary>
    public class VerifyPhoneNumberViewModel
    {
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
    /// <summary>
    /// Generate Otomatis
    /// </summary>
    public class ConfigureTwoFactorViewModel
    {
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        public string SelectedProvider { get; set; }
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}