﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Classs;

namespace TheTop.ViewModels
{
    public class EmployeeDTO : User
    {
        

        [Required(ErrorMessage = "Salary is required")]
        [Range(300, 10000, ErrorMessage = "Salary should be between 300 and 10000")]
        public double Salary { get; set; }
       

        public string City { get; set; }
        public string Country { get; set; }

        public string RoleName { get; set; }
        
        public ICollection<SelectListItem> Roles { get; set; }
    }
}
