using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheTop.Application.Entities;
using TheTop.Application.Services;
using TheTop.Application.Services.DTOs;
using TheTop.ViewModels;

namespace TheTop.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly IReviewService _advertisementServiceReview;
        private readonly IWebHostEnvironment _wepHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICategoryService _categoryService;
        private readonly ICouponService _couponService;
        private readonly IShoppingCartService _shoppingCartService;

        public AdvertisementsController(
            IWebHostEnvironment wepHostEnvironment,
            IAdvertisementService advertisementService,
            UserManager<ApplicationUser> userManager,
            ICategoryService categoryService,
            ICouponService couponService,
            IShoppingCartService shoppingCartService
        )
        {
            _advertisementService = advertisementService;
            _wepHostEnvironment = wepHostEnvironment;
            _userManager = userManager;
            _categoryService = categoryService;
            _shoppingCartService = shoppingCartService;
            _couponService = couponService;
        }


        public ActionResult Index()
        {
            List<CategoryDTO> categoryList = _categoryService.GetAllCategories().ToList();
            ViewBag.listC = categoryList.Select(categ => categ.Name);


            List<AdvertisementDTO> advertisementsDtoList = _advertisementService.GetAllAdvertisemensts().ToList();
            List<AdvertisementVM> advertisementsVMList = advertisementsDtoList
                .Select(advertisement => new AdvertisementVM
                {
                    Name = advertisement.Name,
                    Price = advertisement.Price,
                    Category = advertisement.CategoryName,
                    CreatedAT = advertisement.CreatedAt,
                    PhotosNames = advertisement.ImagesNames.Select(img => img).ToList(),
                }).ToList();
            return View(advertisementsVMList);
        } //

        public async Task<ActionResult> GetById(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            List<AdvertisementDTO> advertisementsDtoList =
                _advertisementService.GetCustomerAdvertisements(user.Id).ToList();
            List<AdvertisementVM> advertisementsVMList = advertisementsDtoList
                .Select(advertisement => new AdvertisementVM
                {
                    ID = advertisement.ID,
                    Name = advertisement.Name,
                    Price = advertisement.Price,
                    Category = advertisement.CategoryName,
                    CreatedAT = advertisement.CreatedAt,
                    PhotosNames = advertisement.ImagesNames.Select(img => img).ToList(),
                }).ToList();
            return View(advertisementsVMList);
        } //

        public async Task<ActionResult> Search()
        {
            //Coupon
            CouponDTO couponDTO = _couponService.GetValidCoupon();
            CouponVM couponVM = new CouponVM
            {
                Code = couponDTO.Code,
                Ratio = couponDTO.Ratio,
                ValidityDate = couponDTO.ValidityDate,
                CreatedAT = couponDTO.CreatedAt,
            };
            ViewBag.Coupon = couponVM;

            var user = await _userManager.GetUserAsync(User);
            ViewBag.numItemCart = _shoppingCartService.GetNumItemShoppingCart(user.Id);
            SearchVM modelVM = new SearchVM();
            List<CategoryDTO> categoryList = _categoryService.GetAllCategories().ToList();

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
        public ActionResult Search(SearchVM modelVM)
        {
            List<CategoryDTO> categoryList = _categoryService.GetAllCategories().ToList();

            List<CategoryVM> categorylist = categoryList.Select(category => new CategoryVM
            {
                Name = category.Name,
                ID = category.ID
            }).ToList();

            modelVM.Categorys = categorylist;
            List<AdvertisementDTO> searchAdvertisementsDtoList = _advertisementService.SearchAdvertisemenst(
                new SearchDTO
                {
                    Name = modelVM.Name,
                    CategoryId = modelVM.CategoryId,
                    FromDate = modelVM.FromDate,
                    ToDate = modelVM.ToDate,
                    FromPrice = modelVM.FromPrice,
                    ToPrice = modelVM.ToPrice,
                }).ToList();

            List<AdvertisementVM> advertisementsVMList = searchAdvertisementsDtoList
                .Select(advertisement => new AdvertisementVM
                {
                    ID = advertisement.ID,
                    Name = advertisement.Name,
                    Price = advertisement.Price,
                    Category = advertisement.CategoryName,
                    CreatedAT = advertisement.CreatedAt,
                    PhotosNames = advertisement.ImagesNames.ToList(),
                }).ToList();

            modelVM.Advertisements = advertisementsVMList;

            //Coupon
            CouponDTO couponDTO = _couponService.GetValidCoupon();
            CouponVM couponVM = new CouponVM
            {
                Code = couponDTO.Code,
                Ratio = couponDTO.Ratio,
                ValidityDate = couponDTO.ValidityDate,
                CreatedAT = couponDTO.CreatedAt,
            };
            ViewBag.Coupon = couponVM;

            return View(modelVM);
        }


        public ActionResult Details(int id)
        {
            AdvertisementDTO advertisement = _advertisementService.GetAdvertisementById(id);
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
        } //


        public ActionResult Create()
        {
            AdvertisementVM model = new AdvertisementVM();

            List<CategoryDTO> categoryList = _categoryService.GetAllCategories().ToList();

            List<CategoryVM> list = categoryList.Select(category => new CategoryVM
            {
                Name = category.Name,
                ID = category.ID
            }).ToList();
            model.Categorys = list;
            return View(model);
        } //


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdvertisementVM viewModel)
        {
            var user = await _userManager.GetUserAsync(User);

            AdvertisementDTO modelDto = new AdvertisementDTO()
            {
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
                    foreach (var photo in viewModel.PhotosFiles)
                    {
                        var fullPath = $"{_wepHostEnvironment.WebRootPath}\\Images\\{Path.GetFileName(photo.FileName)}";
                        await using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await photo.CopyToAsync(stream);
                        }

                        imagesNames.Add($"/images/{Path.GetFileName(photo.FileName)}");
                    }
                }

                modelDto.ImagesNames = imagesNames;

                _advertisementService.CreateNewAdvertisement(modelDto);
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                return View();
            }
        } //


        public ActionResult Edit(int id)
        {
            List<CategoryDTO> categoryList = _categoryService.GetAllCategories().ToList();
            List<CategoryVM> list = categoryList.Select(category => new CategoryVM
            {
                Name = category.Name,
                ID = category.ID
            }).ToList();
            AdvertisementDTO advertisement = _advertisementService.GetAdvertisementById(id);
            AdvertisementVM advertisementVM = new AdvertisementVM
            {
                ID = advertisement.ID,
                Name = advertisement.Name,
                Price = advertisement.Price,
                Categorys = list,
                PhotosNames = advertisement.ImagesNames.ToList()
            };

            return View(advertisementVM);
        } //


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

                _advertisementService.UpdateAdvertisement(modelDto);
                return RedirectToAction("GetById");
            }
            else
            {
                return View();
            }
        } //


        public ActionResult Delete(int id)
        {
            AdvertisementDTO advertisement = _advertisementService.GetAdvertisementById(id);
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
        } //


        public ActionResult DeleteAdv(int id)
        {
            _advertisementService.RemoveAdvertisement(id);
            return RedirectToAction("GetById");
        } //
    }
}