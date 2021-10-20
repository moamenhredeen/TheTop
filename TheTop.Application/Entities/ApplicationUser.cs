using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TheTop.Application.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(55)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(55)")]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string City { get; set; }
        
        [Column(TypeName = "nvarchar(255)")]
        public string Country { get; set; }
        
        public string ImagName { get; set; }
        public DateTime BirthDate { get; set; }
       
        public int? ContractId { get; set; }
        public Contract Contract { get; set; }

        public int? ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<TaskEntity> TaskEntities { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; }
        
        public ICollection<Work> Works { get; set; }

        public ApplicationUser()
        {
            Reviews = new HashSet<Review>();
            Advertisements = new HashSet<Advertisement>();
            Orders = new HashSet<Order>();
            TaskEntities = new HashSet<TaskEntity>();
            Works = new HashSet<Work>();
        }
    }
}


