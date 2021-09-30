﻿using Microsoft.AspNetCore.Http;
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
            return View();
        }

        // GET: OrderDTOsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderDTOsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderDTOsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: OrderDTOsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderDTOsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: OrderDTOsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderDTOsController/Delete/5
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
    }
}
