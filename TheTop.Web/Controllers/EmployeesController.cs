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
        public EmployeesController(UserManager<ApplicationUser> userManager,
                                   RoleManager<IdentityRole> roleManager,
                                   ReviewService service)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._service = service;
        }
        // GET: EmployeeDTOsController
        public ActionResult Index()

        {
          
            return View("Error");
        }

        public async Task<ActionResult> GetAllEmp()
        {


            var data =await _userManager.GetUsersInRoleAsync("Admin");
           
            return View();
        }

        public ActionResult Details(int id)
        {
           EmployeeDTO obj = new EmployeeDTO
            {
                FirstName = "Noor",
                LastName = "Rawashdeh",
                Email = "noor@gmi.com",
                Password = "12345678",
                Phone = "0789555",
                Salary = 800,
                Username = "ken",
                BirthDate = DateTime.Now
            };
            return View(obj);
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
                Contract = new Contract { 
                HourSalary = employeeDto.HourSalary,
                MonthlyWorkingHours = employeeDto.MonthlyWorkingHours,
                CreatedAt  = DateTime.Now
                },
            },employeeDto.Password);

            if (result.Succeeded)
            {

                var user = await _userManager.FindByEmailAsync(employeeDto.Email);
                await _userManager.AddToRoleAsync(user, employeeDto.RoleName);
                
            }
          
                return RedirectToAction("HomePage", "Home");          
            
        }

        public ActionResult Edit(int id)
        {
            EmployeeDTO obj = new EmployeeDTO
            {
                FirstName = "Karam",
                LastName = "Rawashdeh",
                Email = "noor@gmi.com",
                Password = "12345678",
                Phone = "0789555",
                Salary = 800,
                Username = "ken",
                BirthDate = DateTime.Now
            };
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View();
            }
        }



        public ActionResult Delete(int id)
        {
            return View(new EmployeeDTO());
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

                return View("Index");            
        }

        public async Task<ActionResult> EndWork()
        {
            var user = await _userManager.GetUserAsync(User);

            _service.EndWork(new WorkDTO
            {
                ApplicationUserId = user.Id,
                EndDate = DateTime.Now
            });

            return View("Index");
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
