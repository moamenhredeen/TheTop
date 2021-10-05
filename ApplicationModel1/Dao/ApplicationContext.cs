using ApplicationModel1.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel1.Dao
{
    class ApplicationContext : DbContext
    {
        public DbSet<Advertisement> Advertisements { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }

        public DbSet<Offer>Offers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<TaskE> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Work> Works { get; set; }

        public DbSet<Coupon> Coupons { get; set; }

    }
}
