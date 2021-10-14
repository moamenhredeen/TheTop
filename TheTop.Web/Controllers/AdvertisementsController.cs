using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Application.Entities;
using TheTop.Application.Services;
using TheTop.Application.Services.DTOs;
using TheTop.ViewModels;

namespace TheTop.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly AdvertisementService _service;
        private readonly IWebHostEnvironment _wepHostEnvironment;
        private UserManager<ApplicationUser> _userManager;

        public AdvertisementsController(AdvertisementService service,
                                        IWebHostEnvironment wepHostEnvironment,
                                       UserManager<ApplicationUser> userManager )
        {                               
            this._service = service;
            this._wepHostEnvironment = wepHostEnvironment;
            this._userManager = userManager;
        }

       
        public ActionResult Index()
        {

            
            List<CategoryDTO> categoryList = _service.GetAllCategories().ToList();
            ViewBag.listC = categoryList.Select(categ => categ.Name);
          

            List<AdvertisementDTO> advertisementsDtoList = _service.GetAllAdvertisemensts().ToList();
            List<AdvertisementVM> advertisementsVMList = advertisementsDtoList
                                  .Select(advertisement => new AdvertisementVM { 
                                  Name = advertisement.Name,
                                  Price= advertisement.Price,
                                  Category = advertisement.CategoryName,
                                  CreatedAT = advertisement.CreatedAt,
                                  PhotosNames = advertisement.ImagesNames.Select(img => img).ToList(),
                                  }).ToList();
            return View(advertisementsVMList);
        }//

        public async Task<ActionResult> GetById(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            List<AdvertisementDTO> advertisementsDtoList = _service.GetCustomerAdvertisements(user.Id).ToList();
            List<AdvertisementVM> advertisementsVMList = advertisementsDtoList
                       .Select(advertisement => new AdvertisementVM
                         {    ID = advertisement.ID,
                              Name = advertisement.Name,
                              Price = advertisement.Price,
                              Category = advertisement.CategoryName,
                              CreatedAT = advertisement.CreatedAt,
                              PhotosNames = advertisement.ImagesNames.Select(img => img).ToList(),
                              }).ToList();
            return View(advertisementsVMList);
        }//

        public async Task<ActionResult> Search()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.numItemCart = _service.GetNumItemShoppingCart(user.Id);
            SearchVM modelVM = new SearchVM();
            List<CategoryDTO> categoryList = _service.GetAllCategories().ToList();

            List<CategoryVM> categorylist = categoryList.Select(category => new CategoryVM
            {
                Name = category.Name,
                ID = category.ID
            }).ToList();

            modelVM.Categorys = categorylist;
            return View(modelVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Search(SearchVM modelVM)
        {
            List<CategoryDTO> categoryList = _service.GetAllCategories().ToList();

            List<CategoryVM> categorylist = categoryList.Select(category => new CategoryVM
            {
                Name = category.Name,
                ID = category.ID
            }).ToList();

            modelVM.Categorys = categorylist;
            List<AdvertisementDTO> searchAdvertisementsDtoList = _service.SearchAdvertisemenst(new SearchDTO {
                Name = modelVM.Name,
                CategoryId =modelVM.CategoryId,
                FromDate =modelVM.FromDate,
                ToDate = modelVM.ToDate,
                FromPrice = modelVM.FromPrice,
                ToPrice =modelVM.ToPrice,
                
            }).ToList();

            List<AdvertisementVM> advertisementsVMList = searchAdvertisementsDtoList
                                  .Select(advertisement => new AdvertisementVM
                                  {  ID = advertisement.ID,
                                      Name = advertisement.Name,
                                      Price = advertisement.Price,
                                      Category = advertisement.CategoryName,
                                      CreatedAT = advertisement.CreatedAt,
                                      PhotosNames = advertisement.ImagesNames.ToList(),
                                  }).ToList();

            modelVM.Advertisements = advertisementsVMList;

            return View(modelVM);
        }

        
        public ActionResult Details(int id)
        {
            AdvertisementDTO advertisement = _service.GetAdvertisementById(id);
            AdvertisementVM advertisementVM = new AdvertisementVM
            {
                ID = advertisement.ID,
                Name = advertisement.Name,
                Price = advertisement.Price,
                Category = advertisement.CategoryName,
                CreatedAT = advertisement.CreatedAt,
                PhotosNames = advertisement.ImagesNames.Select(img => img).ToList(),
            };
          
            return View(advertisementVM);
        }//

       
        public ActionResult Create()
        {
          AdvertisementVM model = new AdvertisementVM();

            List<CategoryDTO> categoryList = _service.GetAllCategories().ToList();

            List<CategoryVM> list = categoryList.Select(category => new CategoryVM
            {
                Name = category.Name,
                ID = category.ID
            }).ToList();
            model.Categorys = list;
            return View(model);
        }//

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdvertisementVM viewModel)
        {
            var user = await _userManager.GetUserAsync(User);

            AdvertisementDTO modelDto = new AdvertisementDTO() {
                Name = viewModel.Name,
                Price = viewModel.Price,
                CategoryId = viewModel.CategoryId,
                UserId = user.Id            
            };
           
                if (ModelState.IsValid)
                {

                    var imagesNames = new List<string>();

                    if (viewModel.PhotosFiles.Count > 0)
                    {
                        foreach (IFormFile photo in viewModel.PhotosFiles)
                        {
                            var fullPath = $"{_wepHostEnvironment.WebRootPath}\\Images\\{Path.GetFileName(photo.FileName)}";
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                await photo.CopyToAsync(stream);
                            }
                          imagesNames.Add($"/images/{Path.GetFileName(photo.FileName)}");
                        }
                       
                    }

                    modelDto.ImagesNames = imagesNames;

                   _service.CreateNewAdvertisement(modelDto);
                    return RedirectToAction("HomePage","Home");
                }
                else
                {
                    return View();
                }
           
        }//

      
        public ActionResult Edit(int id)
        {
            List<CategoryDTO> categoryList = _service.GetAllCategories().ToList();
            List<CategoryVM> list = categoryList.Select(category => new CategoryVM
            {
                Name = category.Name,
                ID = category.ID
            }).ToList();
            AdvertisementDTO advertisement = _service.GetAdvertisementById(id);
            AdvertisementVM advertisementVM = new AdvertisementVM
            {
                ID = advertisement.ID,
                Name = advertisement.Name,
                Price = advertisement.Price,
                Categorys = list,
                PhotosNames = advertisement.ImagesNames.ToList()
            };

            return View(advertisementVM);
        }//

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AdvertisementVM viewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            AdvertisementDTO modelDto = new AdvertisementDTO()
            {
                ID = viewModel.ID,
                Name = viewModel.Name,
                Price = viewModel.Price,
                CategoryId = viewModel.CategoryId,
                UserId = user.Id
            };

            if (ModelState.IsValid)
            {

                var imagesNames = new List<string>();

                if (viewModel.PhotosFiles.Count > 0)
                {
                    foreach (IFormFile photo in viewModel.PhotosFiles)
                    {
                        var fullPath = $"{_wepHostEnvironment.WebRootPath}\\Images\\{Path.GetFileName(photo.FileName)}";
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await photo.CopyToAsync(stream);
                        }
                        imagesNames.Add($"/images/{Path.GetFileName(photo.FileName)}");
                    }

                }

                modelDto.ImagesNames = imagesNames;

                _service.UpdateAdvertisement(modelDto);
                return RedirectToAction("GetById");
            }
            else
            {
                return View();
            }

        }//

        
        public ActionResult Delete(int id)
        {
            AdvertisementDTO advertisement = _service.GetAdvertisementById(id);
            AdvertisementVM advertisementVM = new AdvertisementVM
            {
                ID = advertisement.ID,
                Name = advertisement.Name,
                Price = advertisement.Price,
                Category = advertisement.CategoryName,
                CreatedAT = advertisement.CreatedAt,
                PhotosNames = advertisement.ImagesNames.Select(img => img).ToList(),
            };

            return View(advertisementVM);
        }//

        
        public ActionResult DeleteAdv(int id)
        {
            try
            {
                _service.RemoveAdvertisement(id);
                return RedirectToAction("GetById");
            }
            catch
            {
                return View();
            }
        }//



        
    }
}
