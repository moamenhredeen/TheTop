using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using TheTop.Application.Entities;
using TheTop.Application.Services;
using TheTop.ViewModels;

namespace TheTop.Controllers
{
    public class ReportsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReportService _reportService;

        public ReportsController(
            UserManager<ApplicationUser> userManager,
            IReportService reportService
        )
        {
            _userManager = userManager;
            _reportService = reportService;
        }


        public IActionResult GetMonthlySellsReport(bool asPdf)
        {
            var report = _reportService.GetMonthlySellsReport();
            var sellsReportVM = new SellsReportVM()
            {
                Orders = report.orders.Select(order => new OrderVM()
                {
                    CreatedAT = order.CreatedAt,
                    TotalPrice = order.TotalPrice,
                    DiscountPrice = order.DiscountPrice
                }).ToList(),
                TotalPrice = report.TotalPrice,
                SellsCount = report.SellsCount,
                Profit = report.Profit, 
                asPdf = asPdf
            };
            
            return asPdf ? new ViewAsPdf(sellsReportVM) : View(sellsReportVM);
        }

        public IActionResult GetAnnualSellsReport(bool asPdf)
        {
            var report = _reportService.GetAnnualSellsReport();
            var sellsReportVM = new SellsReportVM()
            {
                Orders = report.orders.Select(order => new OrderVM()
                {
                    CreatedAT = order.CreatedAt,
                    TotalPrice = order.TotalPrice,
                    DiscountPrice = order.DiscountPrice
                }).ToList(),
                TotalPrice = report.TotalPrice,
                SellsCount = report.SellsCount,
                Profit = report.Profit
            };

            return asPdf ? new ViewAsPdf(sellsReportVM) : View(sellsReportVM);
        }

        public IActionResult GetMonthlyEmployeeSalariesReport()
        {
            var report = _reportService.GetMonthlySalariesReport();
            var employeeSalariesReportVM = new MonthlyEmployeeSalariesReportVM()
            {
                TotalSalaries = report.TotalSalaries,
                TotalWorkingHours = report.TotalWorkingHours,
                Employees = report.Employees.Select(employee => new EmployeeSalaryVM()
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    ShouldSalary = employee.ShouldSalary,
                    HourSalary = employee.HourSalary,
                    ShouldWorkingHours = employee.ShouldWorkingHours,
                    WorkingHours = employee.WorkingHours
                }).ToList()
            };

            return View(employeeSalariesReportVM);
        }

        public IActionResult GetAnnualEmployeeSalariesReport()
        {
            var annualEmployeeSalariesReport = _reportService.GetAnnualEmployeeSalariesReport();
            var annualEmployeeSalariesReportVM = new AnnualEmployeeSalariesReportVM()
            {
                TotalWorkingHours = annualEmployeeSalariesReport.TotalWorkingHours,
                TotalSalaries = annualEmployeeSalariesReport.TotalSalaries,
                MonthlyEmployeesSalariesReporrtsVM =
                    annualEmployeeSalariesReport.MonthlyEmployeesSalariesReporrts.Select(report =>
                        new MonthlyEmployeeSalariesReportVM()
                        {
                            TotalSalaries = report.TotalSalaries,
                            TotalWorkingHours = report.TotalWorkingHours,
                            Employees = report.Employees.Select(employee => new EmployeeSalaryVM()
                            {
                                FirstName = employee.FirstName,
                                LastName = employee.LastName,
                                Email = employee.Email,
                                Salary = employee.Salary,
                                ShouldSalary = employee.ShouldSalary,
                                HourSalary = employee.HourSalary,
                                ShouldWorkingHours = employee.ShouldWorkingHours,
                                WorkingHours = employee.WorkingHours
                            }).ToList()
                        }).ToList()
            };

            return View(annualEmployeeSalariesReportVM);
        }

        public IActionResult GetMonthlySalarySlip()
        {
            var report = _reportService.GetMonthlySalarySlip(_userManager.GetUserId(User));
            var monthlySlip = new EmployeeSalaryVM()
            {
                FirstName = report.FirstName,
                LastName = report.LastName,
                Email = report.Email,
                HourSalary = report.HourSalary,
                Salary = report.Salary,
                ShouldSalary = report.ShouldSalary,
                ShouldWorkingHours = report.ShouldWorkingHours,
                WorkingHours = report.WorkingHours
            };
            return View(monthlySlip);
        }
    }
}