using System.Collections.Generic;

namespace TheTop.ViewModels
{
    public class AnnualEmployeeSalariesReportVM
    {
        public ICollection<MonthlyEmployeeSalariesReportVM> MonthlyEmployeesSalariesReporrtsVM { get; set; }
        public decimal TotalSalaries { get; set; }
        public long TotalWorkingHours { get; set; }
    }
}