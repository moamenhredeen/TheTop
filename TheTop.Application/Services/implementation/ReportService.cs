using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheTop.Application.Dao;
using TheTop.Application.Entities;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{

    public class ReportService : IReportService
    {
        private readonly decimal _ProfitPercent = new decimal(0.05);
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReportService(AppDbContext appDbContext, UserManager<ApplicationUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public SellsReportDTO GetMonthlySellsReport(DateTime date)
        {
            // date : x.10
            var startOfMonth = new DateTime(date.Year, date.Month, 1); // 1.10
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1); // 31.10

            var orders = _appDbContext.Orders.AsNoTracking()
                .Where(order => order.CreatedAt >= startOfMonth &&
                                order.CreatedAt <= endOfMonth);
            return new SellsReportDTO()
            {
                orders = orders.Select(order => new OrderDTO
                {
                    CreatedAt = order.CreatedAt,
                    TotalPrice = order.TotalPrice,
                    DiscountPrice = order.DiscountPrice,
                    OrderId = order.OrderId,
                }).ToList(),
                TotalPrice = orders.Sum(order =>
                    order.DiscountPrice.HasValue ? order.DiscountPrice : order.TotalPrice),
                SellsCount = orders.Count(),
                Profit = orders.Sum(order =>
                    ((order.DiscountPrice.HasValue ? order.DiscountPrice : order.TotalPrice) * _ProfitPercent))
            };
        }
        public SellsReportDTO GetAnnualSellsReport(DateTime date)
        {
            // date : x.x.2021
            var startOfYear = new DateTime(date.Year, 1, 1); // 1.1.2021
            var endOfYear = startOfYear.AddYears(1).AddDays(-1); // 30.12.2020 

            var orders = _appDbContext.Orders.AsNoTracking()
                .Where(order => order.CreatedAt >= startOfYear &&
                                order.CreatedAt <= endOfYear);
            return new SellsReportDTO()
            {
                orders = orders.Select(order => new OrderDTO
                {
                    CreatedAt = order.CreatedAt,
                    TotalPrice = order.TotalPrice,
                    DiscountPrice = order.DiscountPrice,
                    OrderId = order.OrderId,
                }).ToList(),
                TotalPrice = orders.Sum(order =>
                    order.DiscountPrice.HasValue ? order.DiscountPrice : order.TotalPrice),
                SellsCount = orders.Count(),
                Profit = orders.Sum(order =>
                    ((order.DiscountPrice.HasValue ? order.DiscountPrice : order.TotalPrice) * _ProfitPercent))
            };
        }
        public MonthlyEmployeesSalariesReportDTO GetMonthlySalariesReport(DateTime date)
        {
            var startOfMonth = new DateTime(date.Year, date.Month, 1); // 1.10
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1); // 31.10

            var userWorkDictionary = _appDbContext.Works.AsNoTracking()
                .Where(work => work.StartDate >= startOfMonth && work.StartDate <= endOfMonth)
                .Include(work => work.ApplicationUser)
                    .ThenInclude(applicationUser => applicationUser.Contract)
                .AsEnumerable()
                .GroupBy(work => work.ApplicationUserId)
                .ToDictionary(el => el.Key, el => el.ToList());

            // Dictionary 
            // 1 [work1, work2] 
            var employeeSalaries = new List<EmployeeSalaryDTO>();
            decimal totalSalaries = 0;
            int totalWorkingHours = 0;
            foreach (var (applicationUserId, works) in userWorkDictionary)
            {
                var user = works.First().ApplicationUser;
                // exists in user table 
                int shouldWorkingHours = works.First().ApplicationUser.Contract.MonthlyWorkingHours;
                decimal hourSalary = works.First().ApplicationUser.Contract.HourSalary;
                decimal shouldSalary = (decimal) shouldWorkingHours * hourSalary;

                // must be calculated
                int workingHours = works.Sum(work => ((TimeSpan) (work.EndDate - work.StartDate)).Hours);
                decimal salary = (decimal) workingHours * hourSalary;

                employeeSalaries.Add(new EmployeeSalaryDTO()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    HourSalary = hourSalary,
                    Salary = salary,
                    ShouldSalary = shouldSalary,
                    ShouldWorkingHours = shouldWorkingHours,
                    WorkingHours = workingHours
                });

                totalSalaries += salary;
                totalWorkingHours += workingHours;
            }

            return new MonthlyEmployeesSalariesReportDTO()
            {
                Employees = employeeSalaries,
                TotalSalaries = totalSalaries,
                TotalWorkingHours = totalWorkingHours
            };
        }
        public AnnualEmployeeSalariesReport GetAnnualEmployeeSalariesReport(DateTime date)
        {
            var startOfYear = new DateTime(date.Year, 1, 1); // 1.10
            var endOfYear = startOfYear.AddYears(1).AddDays(-1); // 31.10
            endOfYear = endOfYear >= DateTime.Today ? DateTime.Today.AddMonths(-1) : endOfYear;

            var monthlyEmployeeSalariesReports = new List<MonthlyEmployeesSalariesReportDTO>();
            int totalWorkingHours = 0;
            decimal totalSalaries = 0;
            for (int i = 0; i < endOfYear.Month; i++)
            {
                var monthlyEmployeeSalariesReport = GetMonthlySalariesReport(startOfYear);
                monthlyEmployeeSalariesReports.Add(monthlyEmployeeSalariesReport);
                totalSalaries += monthlyEmployeeSalariesReport.TotalSalaries;
                totalWorkingHours += monthlyEmployeeSalariesReport.TotalWorkingHours;
                startOfYear = startOfYear.AddMonths(1);
            }

            return new AnnualEmployeeSalariesReport()
            {
                MonthlyEmployeesSalariesReporrts = monthlyEmployeeSalariesReports,
                TotalWorkingHours = totalWorkingHours,
                TotalSalaries = totalSalaries
            };
        }
        public EmployeeSalaryDTO GetMonthlySalarySlip(string userId, DateTime date)
        {
            var startOfMonth = new DateTime(date.Year, date.Month, 1); // 1.10
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1); // 31.10

            var user = _appDbContext.ApplicationUsers.AsNoTracking()
                .Where(user => user.Id == userId)
                .Include(user => user.Works)
                .Include(user => user.Contract)
                .Single();

            // exists in user table 
            int shouldWorkingHours = user.Contract.MonthlyWorkingHours;
            decimal hourSalary = user.Contract.HourSalary;
            decimal shouldSalary = (decimal) shouldWorkingHours * hourSalary;

            // must be calculated
            int workingHours = user.Works.Sum(work => ((TimeSpan) (work.EndDate - work.StartDate)).Hours);
            decimal salary = (decimal) workingHours * hourSalary;

            return new EmployeeSalaryDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                HourSalary = hourSalary,
                Salary = salary,
                ShouldSalary = shouldSalary,
                ShouldWorkingHours = shouldWorkingHours,
                WorkingHours = workingHours
            };
        }


        /// Convenient Methods 
        public SellsReportDTO GetMonthlySellsReport()
        {
            return GetMonthlySellsReport(DateTime.Today.AddMonths(-1));
        }
        public SellsReportDTO GetAnnualSellsReport()
        {
            return GetAnnualSellsReport(DateTime.Today.AddYears(-1));
        }
        public MonthlyEmployeesSalariesReportDTO GetMonthlySalariesReport()
        {
            return GetMonthlySalariesReport(DateTime.Today.AddMonths(-1));
        }
        public AnnualEmployeeSalariesReport GetAnnualEmployeeSalariesReport()
        {
            return GetAnnualEmployeeSalariesReport(DateTime.Today.AddYears(-1));
        }
        public EmployeeSalaryDTO GetMonthlySalarySlip(string userId)
        {
            return GetMonthlySalarySlip(userId, DateTime.Today.AddMonths(-1));
        }
    }
}