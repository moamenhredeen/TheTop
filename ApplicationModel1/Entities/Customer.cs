using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.Models
{
    public class Customer : User
    {
        public RoleCustomer Role { get; set; }

        //Niv
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Customer()
        {
            Reviews = new HashSet<Review>();
            Advertisements = new HashSet<Advertisement>();
            Orders = new HashSet<Order>();
        }
    }
}
