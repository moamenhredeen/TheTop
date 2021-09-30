using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Models;

namespace TheTop.Controllers
{
    public class CategorysController : Controller
    {
        // GET: CategoryDTOsController
        public ActionResult Index()
        {
            List<CategoryDTO> list = new List<CategoryDTO>()
            {
                new CategoryDTO{Name ="Cars"},
                 new CategoryDTO{Name ="Restaurants"},
                  new CategoryDTO{Name ="Company"},
            };
            return View(list);
        }

        // GET: CategoryDTOsController/Details/5
        public ActionResult Details(int id)
        {
            return View(new CategoryDTO { Name = "Care", createDate = DateTime.Now});
        }

        // GET: CategoryDTOsController/Create
        public ActionResult Create()
        {
            return View(new CategoryDTO());
        }

        // POST: CategoryDTOsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryDTO model)
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

        // GET: CategoryDTOsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(new CategoryDTO { Name = "Rawashdeh"});
        }

        // POST: CategoryDTOsController/Edit/5
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

        // GET: CategoryDTOsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryDTOsController/Delete/5
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
