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
       
        public ActionResult Index()
        {
            List<CategoryDTO> categoryList = _service.GetAllCategories().ToList();

            List<CategoryVM> list = categoryList.Select(category => new CategoryVM
            {
                Name = category.Name,
                ID = category.ID
            }).ToList();
             
           
            return View(list);
        }

        public ActionResult Create()
        {
            return View(new CategoryVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryVM viewModel)
        {
           
            try
            {
                if (ModelState.IsValid)
                {
                    _service.AddCategory(new CategoryDTO() { Name = viewModel.Name});
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(viewModel);
                }
            }
            catch
            {
                return View();
            }

        }

        public ActionResult Edit(int id)
        {
            CategoryDTO category = _service.GetCategoryById(id);

            return View(new CategoryVM { Name = category.Name});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryVM viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.UpdateCategory(new CategoryDTO() { Name = viewModel.Name ,ID = viewModel.ID });
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(viewModel);
                }
            }
            catch
            {
                return View();
            }
        }    

        public ActionResult Delete(int id)
        {
            CategoryDTO category = _service.GetCategoryById(id);

            return View(new CategoryVM { Name = category.Name,ID= category.ID });
        }       

        public ActionResult DeleteCat(int id)
        {
            try
            {
                _service.RemoveCategory(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
