using System;
using System.Threading.Tasks;
using TheTop.Application.Services.DTOs;


namespace TheTop.Application.Services
{
    public interface IReportService
    {
        SellsReportDTO GetMonthlySellsReport(DateTime date);


        SellsReportDTO GetAnnualSellsReport(DateTime date);
        MonthlyEmployeesSalariesReportDTO GetMonthlySalariesReport(DateTime date);
        AnnualEmployeeSalariesReport GetAnnualEmployeeSalariesReport(DateTime date);
        EmployeeSalaryDTO GetMonthlySalarySlip(string userId, DateTime date);
        /// Convenient Methods 
        SellsReportDTO GetMonthlySellsReport();
        SellsReportDTO GetAnnualSellsReport();
        MonthlyEmployeesSalariesReportDTO GetMonthlySalariesReport();
        AnnualEmployeeSalariesReport GetAnnualEmployeeSalariesReport();
        EmployeeSalaryDTO GetMonthlySalarySlip(string userId);
    }
}