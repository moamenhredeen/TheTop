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
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ReviewService _service;
        private readonly AdvertisementService _advertisemenService;
        public EmployeesController(UserManager<ApplicationUser> userManager,
                                   RoleManager<IdentityRole> roleManager,
                                   ReviewService service,
                                    AdvertisementService advertisemenService)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._service = service;
            this._advertisemenService = advertisemenService;
        }
        
        public ActionResult Index()
        {
            List<OrderDTO> orderDTOList = _advertisemenService.GetAllOrders().ToList();
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

            List<EmployeeDTO> employeeList = programmers.Select(emp => new EmployeeDTO
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

             foreach(var emp in accountants)
            {
                employeeList.Add(new EmployeeDTO
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
            EmployeeDTO employee = new EmployeeDTO
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
            List<SelectListItem> roleList = new ();
            roles.ForEach(e =>
            {
                roleList.Add(new SelectListItem { Text = e.Name,Value = e.Name});
            });

            EmployeeDTO employee = new EmployeeDTO();
            employee.Roles = roleList;
            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeDTO employeeDto)
        {
            
                if (! ModelState.IsValid)
                {
                return View(employeeDto);            
                }
               
            var result = await _userManager.CreateAsync(new ApplicationUser
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                BirthDate = employeeDto.BirthDate,
                Email = employeeDto.Email,
                Country = employeeDto.Country,
                City = employeeDto.City,
                UserName = employeeDto.Username,
                PhoneNumber =employeeDto.Phone,
                Contract = new Contract { 
                HourSalary = employeeDto.HourSalary,
                MonthlyWorkingHours = employeeDto.MonthlyWorkingHours,
                CreatedAt  = DateTime.Now,
                
                },
            },employeeDto.Password);

            if (result.Succeeded)
            {

                var user = await _userManager.FindByEmailAsync(employeeDto.Email);
                await _userManager.AddToRoleAsync(user, employeeDto.RoleName);
                
            }
          
                return RedirectToAction("HomePage", "Home");          
            
        }

        public async Task<ActionResult> Edit(string empId)
        {
            var roles = await _roleManager.Roles.ToListAsync();
            List<SelectListItem> roleList = new();
            roles.ForEach(e =>
            {
                roleList.Add(new SelectListItem { Text = e.Name, Value = e.Name });
            });

            var emp = await _userManager.FindByIdAsync(empId);
            EmployeeDTO employee = new EmployeeDTO
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
                Password = emp.PasswordHash
            };
            employee.Roles = roleList;
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EmployeeDTO employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeDto);
            }

            var emp = await _userManager.FindByIdAsync(employeeDto.ID);
            emp.FirstName = employeeDto.FirstName;
            emp.LastName = employeeDto.LastName;
            emp.BirthDate = employeeDto.BirthDate;
            emp.Email = employeeDto.Email;
            emp.Country = employeeDto.Country;
            emp.City = employeeDto.City;
            emp.UserName = employeeDto.Username;
            emp.PhoneNumber = employeeDto.Phone;
            var result = await _userManager.UpdateAsync(emp);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(employeeDto.Email);
               
                var roles = await this._userManager.GetRolesAsync(user);
                await this._userManager.RemoveFromRolesAsync(user, roles.ToArray());

                await this._userManager.AddToRoleAsync(user, employeeDto.RoleName);

            }

            return RedirectToAction("GetAllEmp");

        }



        public async Task<ActionResult> Delete(string empId)
        {
            var emp = await _userManager.FindByIdAsync(empId);
            EmployeeDTO employee = new EmployeeDTO
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> StartWork()
        {
            var user = await _userManager.GetUserAsync(User);

            _service.StartWork(new WorkDTO
            {
                ApplicationUserId = user.Id,
                StartDate = DateTime.Now
            }); 

                return RedirectToAction("Index");            
        }

        public async Task<ActionResult> EndWork()
        {
            var user = await _userManager.GetUserAsync(User);

            _service.EndWork(new WorkDTO
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


            List<OrderDTO> orderDTOList = _advertisemenService.SearchOrder(
                new SearchDTO {
                    FromDate = modelVM.FromDate,
                    ToDate = modelVM.ToDate,
                }
                ).ToList();
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
            homeAdmin.Search = modelVM;


            return View("index", homeAdmin);
        }

        public ActionResult ReportMonthlyOfSells()
        {
            return View();
        }
        public ActionResult ReportAnnualOfSells()
        {
            return View();
        }

        public ActionResult ReportMonthly()
        {
            return View();
        }

        public ActionResult ReportAnnual()
        {
            return View();
        }

       
       

        //Accountant
        public ActionResult ReportMonthlyAccountant()
        {
            return View("Error");
        }

        public ActionResult ReportAnnualAccountant()
        {
            return View("Error");
        }

        public ActionResult GetInformationCustomer()
        {
            return View();
        }

        public ActionResult GetInformationEmployees()
        {
            return View();
        }


        //Programmer
        public ActionResult RecordsAttendance()
        {
            return View("Error");
        }

        public ActionResult RecordsExit()
        {
            return View("Error");
        }


        public ActionResult GetSalarySlip()
        {
            return View("Error");
        }
    }
}
