using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTop.Application.Services.DTOs
{
    class ContractDTO
    {
        public int ID { get; set; }
        public decimal HourSalary { get; set; }
        public float MonthlyWorkingHours { get; set; }
        public DateTime CreateAt { get; set; } 
        public DateTime UpdatedAt { get; set; }
    }
}
