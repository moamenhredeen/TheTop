using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Application.Entities;
using TheTop.Application.Services;
using TheTop.Application.Services.DTOs;
using TheTop.ViewModels;

namespace TheTop.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWorkService _workService;
        private readonly IOrderService _orderService;

        public EmployeesController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOrderService orderService, 
            IWorkService workService
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _orderService = orderService;
            _workService = workService;
        }

        public ActionResult Index()
        {
            List<OrderDTO> orderDTOList = _orderService.GetAllOrders().ToList();
            List<OrderVM> orderVMList = orderDTOList.Select(order => new OrderVM
            {
                Advertisements = order.Advertisements
                    .Select(advertisement => new AdvertisementVM
                    {
                        ID = advertisement.ID,
                        Name = advertisement.Name,
                        Price = advertisement.Price,
                        Category = advertisement.CategoryName,
                        CreatedAT = advertisement.CreatedAt,
                        PhotosNames = advertisement.ImagesNames.Select(img => img).ToList(),
                    }).ToList(),
                CreatedAT = order.CreatedAt,
                TotalPrice = order.TotalPrice,
                DiscountPrice = order.DiscountPrice,
                ID = order.OrderId,
            }).ToList();

            HomeAdminVM homeAdmin = new HomeAdminVM();
            homeAdmin.Orders = orderVMList;
            homeAdmin.Search = new SearchVM();
            return View(homeAdmin);
        }

        public async Task<ActionResult> GetAllEmp()
        {
            var programmers = await _userManager.GetUsersInRoleAsync("Programmer");
            var accountants = await _userManager.GetUsersInRoleAsync("Accountant");

            List<EmployeeVM> employeeList = programmers.Select(emp => new EmployeeVM
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                BirthDate = emp.BirthDate,
                Country = emp.Country,
                City = emp.City,
                Phone = emp.PhoneNumber,
                ID = emp.Id,
                RoleName = "Programmer",
                Username = emp.UserName,
            }).ToList();

            foreach (var emp in accountants)
            {
                employeeList.Add(new EmployeeVM
                {
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Email = emp.Email,
                    BirthDate = emp.BirthDate,
                    Country = emp.Country,
                    City = emp.City,
                    Phone = emp.PhoneNumber,
                    ID = emp.Id,
                    RoleName = "Accountant",
                    Username = emp.UserName,
                });
            }


            return View(employeeList);
        }

        public async Task<ActionResult> Details(string empId)
        {
            var emp = await _userManager.FindByIdAsync(empId);
            var employee = new EmployeeVM
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                BirthDate = emp.BirthDate,
                Country = emp.Country,
                City = emp.City,
                Phone = emp.PhoneNumber,
                ID = emp.Id,
                RoleName = "Accountant",
                Username = emp.UserName,
            };
            return View(employee);
        }

        public async Task<ActionResult> Create()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleList = new List<SelectListItem>();
            roles.ForEach(e => { roleList.Add(new SelectListItem {Text = e.Name, Value = e.Name}); });

            var employee = new EmployeeVM();
            employee.Roles = roleList;
            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }

            var result = await _userManager.CreateAsync(new ApplicationUser
            {
                FirstName = employeeVM.FirstName,
                LastName = employeeVM.LastName,
                BirthDate = employeeVM.BirthDate,
                Email = employeeVM.Email,
                Country = employeeVM.Country,
                City = employeeVM.City,
                UserName = employeeVM.Username,
                PhoneNumber = employeeVM.Phone,
                Contract = new Contract
                {
                    HourSalary = employeeVM.HourSalary,
                    MonthlyWorkingHours = employeeVM.MonthlyWorkingHours,
                    CreatedAt = DateTime.Now,
                },
            }, employeeVM.Password);

            // TODO : show user error 
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(employeeVM.Email);
                await _userManager.AddToRoleAsync(user, employeeVM.RoleName);
            }

            return RedirectToAction("HomePage", "Home");
        }

        public async Task<ActionResult> Edit(string empId)
        {
            var roles = await _roleManager.Roles.ToListAsync();
             var roleList = new List<SelectListItem>();
            roles.ForEach(e => { roleList.Add(new SelectListItem {Text = e.Name, Value = e.Name}); });

            var emp = await _userManager.FindByIdAsync(empId);
            var employee = new EmployeeVM
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                BirthDate = emp.BirthDate,
                Country = emp.Country,
                City = emp.City,
                Phone = emp.PhoneNumber,
                ID = emp.Id,
                Username = emp.UserName,
                Password = emp.PasswordHash,
                Roles = roleList
            };
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }

            var emp = await _userManager.FindByIdAsync(employeeVM.ID);
            emp.FirstName = employeeVM.FirstName;
            emp.LastName = employeeVM.LastName;
            emp.BirthDate = employeeVM.BirthDate;
            emp.Email = employeeVM.Email;
            emp.Country = employeeVM.Country;
            emp.City = employeeVM.City;
            emp.UserName = employeeVM.Username;
            emp.PhoneNumber = employeeVM.Phone;
            var result = await _userManager.UpdateAsync(emp);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(employeeVM.Email);

                var roles = await this._userManager.GetRolesAsync(user);
                await this._userManager.RemoveFromRolesAsync(user, roles.ToArray());

                await this._userManager.AddToRoleAsync(user, employeeVM.RoleName);
            }

            return RedirectToAction("GetAllEmp");
        }


        public async Task<ActionResult> Delete(string empId)
        {
            var emp = await _userManager.FindByIdAsync(empId);
            var employee = new EmployeeVM
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                BirthDate = emp.BirthDate,
                Country = emp.Country,
                City = emp.City,
                Phone = emp.PhoneNumber,
                ID = emp.Id,
                RoleName = "Accountant",
                Username = emp.UserName,
            };
            return View(employee);
        }


        public async Task<ActionResult> StartWork()
        {
            var user = await _userManager.GetUserAsync(User);
            _workService.StartWork(new WorkDTO
            {
                ApplicationUserId = user.Id,
                StartDate = DateTime.Now
            });
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> EndWork()
        {
            var user = await _userManager.GetUserAsync(User);
            _workService.EndWork(new WorkDTO
            {
                ApplicationUserId = user.Id,
                EndDate = DateTime.Now
            });
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(SearchVM modelVM)
        {
            List<OrderDTO> orderDTOList = _orderService.SearchOrder(
                new SearchDTO
                {
                    FromDate = modelVM.FromDate,
                    ToDate = modelVM.ToDate,
                }
            ).ToList();
            
            var orderVMList = orderDTOList.Select(order => new OrderVM
            {
                Advertisements = order.Advertisements
                    .Select(advertisement => new AdvertisementVM
                    {
                        ID = advertisement.ID,
                        Name = advertisement.Name,
                        Price = advertisement.Price,
                        Category = advertisement.CategoryName,
                        CreatedAT = advertisement.CreatedAt,
                        PhotosNames = advertisement.ImagesNames.Select(img => img).ToList(),
                    }).ToList(),
                CreatedAT = order.CreatedAt,
                TotalPrice = order.TotalPrice,
                DiscountPrice = order.DiscountPrice,
                ID = order.OrderId,
            }).ToList();
            
            var homeAdmin = new HomeAdminVM();
            homeAdmin.Orders = orderVMList;
            homeAdmin.Search = modelVM;
            return View("index", homeAdmin);
        }

    }
}