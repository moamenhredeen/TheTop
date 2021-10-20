using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TheTop.Application.Services;
using TheTop.Application.Services.DTOs;
using TheTop.ViewModels;

namespace TheTop.Controllers
{
    public class CategorysController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategorysController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
       
        public ActionResult Index()
        {
            List<CategoryDTO> categoryList = _categoryService.GetAllCategories().ToList();

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
                    _categoryService.AddCategory(new CategoryDTO() { Name = viewModel.Name});
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
            CategoryDTO category = _categoryService.GetCategoryById(id);

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
                    _categoryService.UpdateCategory(new CategoryDTO() { Name = viewModel.Name ,ID = viewModel.ID });
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
            CategoryDTO category = _categoryService.GetCategoryById(id);

            return View(new CategoryVM { Name = category.Name,ID= category.ID });
        }       

        public ActionResult DeleteCat(int id)
        {
            _categoryService.RemoveCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
