using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationModel1.Entities
{
    public class User
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(55)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(55)")]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        [Column(TypeName = "nvarchar(55)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Password { get; set; }

        [Column(TypeName = "nvarchar(55)")]
        public string Username { get; set; }

        [Column(TypeName = "nvarchar(15)")]
        public string Phone { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string City { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Country { get; set; }
        public string ImagName { get; set; }

        public RoleAdmin Role { get; set; }

        public double Salary { get; set; }
        //Niv
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Advertisement> Advertisements { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<TaskE> Tasks { get; set; }
        public IEnumerable<BankAccount> BankAccounts { get; set; }
        public User()
        {
            Reviews = new HashSet<Review>();
            Advertisements = new HashSet<Advertisement>();
            Orders = new HashSet<Order>();
            Tasks = new HashSet<TaskE>();
        }

    }
}


