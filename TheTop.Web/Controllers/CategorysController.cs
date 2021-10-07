using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Application.Services;
using TheTop.Application.Services.DTOs;
using TheTop.Models;

namespace TheTop.Controllers
{
    public class CategorysController : Controller
    {
        private readonly AdvertisementService _service;

        public CategorysController(AdvertisementService service)
        {
            this._service = service;
        }
        // GET: CategoryDTOsController
        public ActionResult Index()
        {
            List<CategoryDTO> categoryList = _service.GetAllCategories().ToList();

            List<CategoryVM> list = categoryList.Select(category => new CategoryVM
            {
                Name = category.Name
            }).ToList();
             
           
            return View(list);
        }

        

        // GET: CategoryDTOsController/Create
        public ActionResult Create()
        {
            return View(new CategoryVM());
        }

        // POST: CategoryDTOsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryVM model)
        {
           
            try
            {
                if (ModelState.IsValid)
                {
                    _service.AddCategory(new CategoryDTO() { Name = model.Name});
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
            return View(new CategoryVM { Name = "Rawashdeh"});
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
