using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Application.Entities;
using TheTop.ViewModels;

namespace TheTop.Controllers
{
    public class EmployeesController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        public EmployeesController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        // GET: EmployeeDTOsController
        public ActionResult Index()

        {
          
            return View("Error");
        }

        public async Task<ActionResult> GetAllEmp()
        {


            var data =await _userManager.GetUsersInRoleAsync("Admin");
            //List<EmployeeDTO> list = new List<EmployeeDTO>() {
            // new EmployeeDTO{FirstName= "Moamen",LastName="Hraden",Email="kenan@gmi.com",Password="12345678" ,
            //                 Phone = "0789555" ,Role=RoleAdmin.SuperAdmin,Salary=1500,Username="ken",BirthDate = DateTime.Now},
            // new EmployeeDTO{FirstName= "kenan",LastName="Hassan",Email="kenan@gmi.com",Password="12345678" ,
            //                 Phone = "0789555" ,Role=RoleAdmin.Programmer,Salary=1000,Username="ken",BirthDate = DateTime.Now},
            // new EmployeeDTO{FirstName= "Bahaa",LastName="Rawashdeh",Email="kenan@gmi.com",Password="12345678" ,
            //                 Phone = "0789555" ,Role=RoleAdmin.Accountant,Salary=800,Username="ken",BirthDate = DateTime.Now}
            //};
            return View();
        }
        // GET: EmployeeDTOsController/Details/5
        public ActionResult Details(int id)
        {
           EmployeeDTO obj = new EmployeeDTO
            {
                FirstName = "Noor",
                LastName = "Rawashdeh",
                Email = "noor@gmi.com",
                Password = "12345678",
                Phone = "0789555",
                Role = RoleAdmin.Accountant,
                Salary = 800,
                Username = "ken",
                BirthDate = DateTime.Now
            };
            return View(obj);
        }

        // GET: EmployeeDTOsController/Create
        public ActionResult Create()
        {
            return View(new EmployeeDTO());
        }

        // POST: EmployeeDTOsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeDTO model)
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

        // GET: EmployeeDTOsController/Edit/5
        public ActionResult Edit(int id)
        {
            EmployeeDTO obj = new EmployeeDTO
            {
                FirstName = "Karam",
                LastName = "Rawashdeh",
                Email = "noor@gmi.com",
                Password = "12345678",
                Phone = "0789555",
                Role = RoleAdmin.Accountant,
                Salary = 800,
                Username = "ken",
                BirthDate = DateTime.Now
            };
            return View(obj);
        }

        // POST: EmployeeDTOsController/Edit/5
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



        // GET: EmployeeDTOsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(new EmployeeDTO());
        }

        // POST: EmployeeDTOsController/Delete/5
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

        public ActionResult Login()
        {
            
                return View(new EmployeeDTO());            
        }
        [HttpPost]
        public ActionResult Login(EmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                return View(new EmployeeDTO());
            }
            return View();
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

        public ActionResult GetTasks()
        {
            return View("Error");
        }

        public ActionResult GetSalarySlip()
        {
            return View("Error");
        }
    }
}
