using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// generate otomatis dari visual studio
    /// saya masih merasa aneh dengan permintaan Pak Windy kalo class harus dibuat per file, karena generate ini aja tidak per file
    /// </summary>
    public class ExternalLoginConfirmationViewModel
    {
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    /// <summary>
    /// generate otomatis dari visual studio
    /// </summary>
    public class ExternalLoginListViewModel
    {
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        public string ReturnUrl { get; set; }
    }
    /// <summary>
    /// generate otomatis dari visual studio
    /// </summary>
    public class SendCodeViewModel
    {
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        public string SelectedProvider { get; set; }
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        public bool RememberMe { get; set; }
    }
    /// <summary>
    /// generate otomatis dari visual studio
    /// </summary>
    public class VerifyCodeViewModel
    {
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        [Required]
        public string Provider { get; set; }
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        public bool RememberMe { get; set; }
    }
    /// <summary>
    /// generate otomatis dari visual studio
    /// </summary>
    public class ForgotViewModel
    {
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    /// <summary>
    /// generate otomatis dari visual studio
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    /// <summary>
    /// generate otomatis dari visual studio
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    /// <summary>
    /// generate otomatis dari visual studio
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        public string Code { get; set; }
    }
    /// <summary>
    /// generate otomatis dari visual studio
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// generate otomatis dari visual studio
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
