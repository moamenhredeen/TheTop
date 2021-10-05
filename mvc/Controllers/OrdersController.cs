using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheTop.Controllers
{
    public class OrdersController : Controller
    {
        // GET: OrderDTOsController
        public ActionResult Index()
        {
            return View("Error");
        }

        // GET: OrderDTOsController/Details/5
        public ActionResult Details(int id)
        {
            return View("Error");
        }

        // GET: OrderDTOsController/Create
        public ActionResult Create()
        {
            return View("Error");
        }

        // POST: OrderDTOsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            return View("Error");
        }
        

        // GET: OrderDTOsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View("Error");
        }

        // POST: OrderDTOsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            return View("Error");
        }

        // GET: OrderDTOsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View("Error");
        }

        // POST: OrderDTOsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            return View("Error");
        }
    }
}
