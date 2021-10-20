using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheTop.Application.Entities;

namespace TheTop.Application.Dao
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();
            base.OnModelCreating(builder);
        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TaskEntity> TaskEntities { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<CompanyInformation> CompanyInformations { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    }
}