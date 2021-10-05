using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel1.Services.DTOs
{
    class UserDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string ImagName { get; set; }
        public double Salary { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
