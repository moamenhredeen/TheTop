using System.Collections;
using System.Collections.Generic;

namespace TheTop.Application.Services.DTOs
{
    public class AnnualEmployeeSalariesReport
    {
        public ICollection<MonthlyEmployeesSalariesReportDTO>  MonthlyEmployeesSalariesReporrts { get; set; }
        public decimal TotalSalaries { get; set; }
        public long TotalWorkingHours { get; set;  }
    }
}