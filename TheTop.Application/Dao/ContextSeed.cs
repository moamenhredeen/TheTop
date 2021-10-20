using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheTop.Application.Entities;

namespace TheTop.Application.Dao
{
    public static class ContextSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole {Id = "admin-role-id", Name = "Admin", NormalizedName = "ADMIN".ToUpper()},
                new IdentityRole {Id = "customer-role-id", Name = "Customer", NormalizedName = "Customer".ToUpper()},
                new IdentityRole
                    {Id = "Programmer-role-id", Name = "Programmer", NormalizedName = "Programmer".ToUpper()},
                new IdentityRole
                    {Id = "accountant-role-id", Name = "Accountant", NormalizedName = "Accountant".ToUpper()}
            );

            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser()
                {
                    Id = "administrator-user-id", UserName = "admin", NormalizedUserName = "ADMIN",
                    PasswordHash = hasher.HashPassword(null, "adminadmin")
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> {RoleId = "admin-role-id", UserId = "administrator-user-id"}
            );

            modelBuilder.Entity<CompanyInformation>().HasData(
                new CompanyInformation() {CompanyInformationId = 1, Key = "xxx", Value = "kenan"}
            );
        }
    }
}