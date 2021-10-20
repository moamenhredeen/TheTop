using System;

namespace TheTop.Application.Entities
{
    public class Contract
    {
        public int ContractId { get; set;  }
        public decimal HourSalary { get; set; }
        public int MonthlyWorkingHours { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}