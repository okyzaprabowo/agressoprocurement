using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// Model edit user
    /// </summary>
    public class EditUserViewModel
    {
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        public EditUserViewModel() { }
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        /// <param name="user"></param>
        /// <param name="profile"></param>
        /// <param name="role"></param>
        public EditUserViewModel(ApplicationUser user, Profile profile, string role) {
            this.user_id = user.Id;
            this.UserName = user.UserName;
            //this.email = user.Email;
            if (profile != null) {
                this.full_name = profile.full_name;
                this.initials = profile.initials;
                this.divisi = profile.divisi;
            }
            if (role != null)
                this.position = role;
        }
        /// <summary>
        /// ID User
        /// </summary>
        [Key]
        [Display(Name = "ID User")]
        [StringLength(128)]
        public string user_id { get; set; }
        /// <summary>
        /// full_name
        /// </summary>
        [Required]
        [Display(Name = "Nama Lengkap")]
        [StringLength(50, ErrorMessage = "Jumlah huruf minimal 5", MinimumLength = 5)]
        public string full_name { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Jumlah huruf minimal 3", MinimumLength = 3)]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        /// <summary>
        /// email
        /// </summary>
        [Display(Name = "Email")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email yang Anda masukan tidak benar")]
        public string email { get; set; }
        /// <summary>
        /// initials
        /// </summary>
        [Required]
        [Display(Name = "Inisial")]
        [StringLength(5, ErrorMessage = "Jumlah huruf minimal 3 dan maksimal 5", MinimumLength = 3)]
        public string initials { get; set; }
        /// <summary>
        /// position
        /// </summary>
        [Required]
        [Display(Name = "Role")]
        [StringLength(50)]
        public string position { get; set; }
        /// <summary>
        /// divisi
        /// </summary>
        [Display(Name = "Divisi")]
        public int divisi { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "{0} minimal {2} karakter.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[!@#$%^&*])[0-9a-zA-Z!@#$%^&*0-9]{2,}$", ErrorMessage = "Password harus berisi karakter special dan angka.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
    
}