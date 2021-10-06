using System;

namespace TheTop.Application.Entities
{
    public class Contract
    {
        public int ContractId { get; set;  }
        public decimal HourSalary { get; set; }
        public float MonthlyWorkingHours { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}