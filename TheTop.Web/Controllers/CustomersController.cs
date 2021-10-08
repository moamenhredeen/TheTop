using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.ViewModels;

namespace TheTop.Controllers
{
    public class CustomersController : Controller
    {
        // GET: CustomerDTOController1
        public ActionResult GetAllCust()
        {
            List<CustomerDTO> list = new List<CustomerDTO>() {
             new CustomerDTO{FirstName= "Moamen",LastName="Hraden",Email="kenan@gmi.com",
                             Phone = "0789555",Username="ken",BirthDate = DateTime.Now},
             new CustomerDTO{FirstName= "kenan",LastName="Hassan",Email="kenan@gmi.com",
                             Phone = "0789555" ,Username="ken",BirthDate = DateTime.Now},
             new CustomerDTO{FirstName= "Bahaa",LastName="Rawashdeh",Email="kenan@gmi.com",
                             Phone = "0789555" ,Username="ken",BirthDate = DateTime.Now}
            };
            return View(list);
        }

        // GET: CustomerDTOController1/Details/5
        public ActionResult Details(int id)
        {
            CustomerDTO obj = new CustomerDTO
            {
                FirstName = "Noor",
                LastName = "Rawashdeh",
                Email = "noor@gmi.com",
                Phone = "0789555",
                Username = "ken",
                BirthDate = DateTime.Now
            };
            return View(obj);
        }

        // GET: CustomerDTOController1/Create
        // public ActionResult Register()
        // {
        //     return View(new CustomerDTO());
        // }

        // POST: CustomerDTOController1/Create
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Register(CustomerDTO model)
        // {
        //     try
        //     {
        //         return RedirectToAction(nameof(Index));
        //     }
        //     catch
        //     {
        //         return View();
        //     }
        // }
        //
        // public ActionResult Login()
        // {
        //     return View(new CustomerDTO());
        // }
        //
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Login(IFormCollection collection)
        // {
        //     try
        //     {
        //         return RedirectToAction(nameof(Index));
        //     }
        //     catch
        //     {
        //         return View();
        //     }
        // }


       


        // GET: CustomerDTOController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View("Error");
        }

        // POST: CustomerDTOController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            return View("Error");
        }

        // GET: CustomerDTOController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View("Error");
        }

        // POST: CustomerDTOController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            return View("Error");
        }
    }
}
