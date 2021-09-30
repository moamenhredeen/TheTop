using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.Models
{
    public class CustomerDTO : User
    {
        public RoleCustomer Role { get; set; }

        
    }
}
