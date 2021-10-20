using System.Collections.Generic;

namespace TheTop.ViewModels
{
    public class MonthlyEmployeeSalariesReportVM
    {
        public ICollection<EmployeeSalaryVM> Employees { get; set; }
        public decimal TotalSalaries { get; set; }
        public int TotalWorkingHours { get; set; }
    }
}