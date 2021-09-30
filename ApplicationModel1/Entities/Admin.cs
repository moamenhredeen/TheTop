using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.Models
{
    public class Admin : User
    {
        public RoleAdmin Role { get; set; }

        public double Salary { get; set; }
    }
}
