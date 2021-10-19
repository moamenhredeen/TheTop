using Microsoft.AspNetCore.Mvc.Rendering;
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
        public string RoleName { get; set; }

        [Display(Name = "Hour Salary")]
        [Required(ErrorMessage = "Hour Salary is required")]
        public decimal HourSalary { get; set; }

        [Display(Name = "Monthly Working Hours")]
        [Required(ErrorMessage = "Monthly Working Hours is required")]
        public float MonthlyWorkingHours { get; set; }

        public ICollection<SelectListItem> Roles { get; set; }
    }
}
