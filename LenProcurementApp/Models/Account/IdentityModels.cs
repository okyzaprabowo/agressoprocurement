using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace LenProcurementApp.Models
{
    /// <summary>
    /// generate dari visual studio
    /// </summary>
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        /// <param name="manager">manager</param>
        /// <returns>userIdentity</returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    /// <summary>
    /// Generate Otomatis
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        // 
        /// <summary>
        /// profile
        /// </summary>
        public DbSet<Profile> profiles { get; set; }
        // 
        /// <summary>
        /// dpb
        /// </summary>
        public DbSet<Dpb> dbDpb { get; set; }
        /// <summary>
        /// db Dpb Product
        /// </summary>
        public DbSet<DpbProduct> dbDpbProduct { get; set; }
        // 
        /// <summary>
        /// po
        /// </summary>
        public DbSet<Po> dbPo { get; set; }
        /// <summary>
        /// db Po Product
        /// </summary>
        public DbSet<PoProduct> dbPoProduct { get; set; }
        //public DbSet<PoImport> dbPoImport { get; set; }
        // spph
        /// <summary>
        /// db Spph
        /// </summary>
        public DbSet<Spph> dbSpph { get; set; }
        // bapb
        // product
        //public DbSet<Product> dbProduct { get; set; }
        /// <summary>
        /// basis Data
        /// </summary>
        public DbSet<BasisDasboard> basisData { get; set; }
        //public DbSet<BapbDasboard> bapbData { get; set; }
        //public DbSet<PoDashboard> poData { get; set; }
        // supplier
        /// <summary>
        /// db Supplier
        /// </summary>
        public DbSet<Supplier> dbSupplier { get; set; }
        /// <summary>
        /// poOptionData
        /// </summary>
        public DbSet<PoOption> poOptionData { get; set; }
        /// <summary>
        /// poImportData
        /// </summary>
        public DbSet<PoImport> poImportData { get; set; }
        /// <summary>
        /// noteData
        /// </summary>
        public DbSet<Note> noteData { get; set; }
        /// <summary>
        /// paymentData
        /// </summary>
        public DbSet<Payment> paymentData { get; set; }
        /// <summary>
        /// deliveredInfoData
        /// </summary>
        public DbSet<DeliveredInfo> deliveredInfoData { get; set; }
        /// <summary>
        /// productDeliveredData
        /// </summary>
        public DbSet<ProductDelivered> productDeliveredData { get; set; }
        /// <summary>
        /// sppData
        /// </summary>
        public DbSet<SppDashboard> sppData { get; set; }
        /// <summary>
        /// executorData
        /// </summary>
        public DbSet<Executor> executorData { get; set; }
        /// <summary>
        /// invoiceData
        /// </summary>
        public DbSet<Invoice> invoiceData { get; set; }
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        /// <summary>
        /// Generate Otomatis
        /// </summary>
        public DbSet<EditUserViewModel> EditUserViewModels { get; set; }
    }
}