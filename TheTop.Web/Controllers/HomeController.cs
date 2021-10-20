using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheTop.Application.Entities;
using TheTop.Application.Services;
using TheTop.Application.Services.DTOs;
using TheTop.ViewModels;

namespace TheTop.Controllers
{
    // [Authorize] // -> authenticated 
    /*[Authorize(Roles="Admin")]*/ // -> authenticated 
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdvertisementService _advertisementService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IReviewService _reviewService;
        private readonly ICouponService _couponService;
        private readonly ICategoryService _categoryService;
        private readonly ICompanyInformationService _companyInformationService; 

        public HomeController(
            UserManager<ApplicationUser> userManager,
            IAdvertisementService advertisementService,
            IReviewService reviewService,
            IShoppingCartService shoppingCartService,
            ICouponService couponService,
            ICategoryService categoryService,
            ICompanyInformationService companyInformationService
        )
        {
            _userManager = userManager;
            _advertisementService = advertisementService;
            _reviewService = reviewService;
            _shoppingCartService = shoppingCartService;
            _couponService = couponService;
            _categoryService = categoryService;
            _companyInformationService = companyInformationService;
        }


        public async Task<IActionResult> HomePage()
        {
            ViewBag.xxx = _companyInformationService.get("xxx");
            var user = await _userManager.GetUserAsync(User);

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


            ViewBag.listC = new List<string> {"kenan11", "kenan22", "kenan33"};
            List<CategoryDTO> categoryList = _categoryService.GetAllCategories().ToList();
            if (user != null)
            {
                ViewBag.numItemCart = _shoppingCartService.GetNumItemShoppingCart(user.Id);
            }

            List<AdvertisementDTO> advertisementsDtoList = _advertisementService.GetAllAdvertisemensts().ToList();
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

            List<ReviewDTO> reviewDtoList = _reviewService.GetApprovedReviews().ToList();

            var reviewVMlist = new List<ReviewVM>();
            reviewDtoList.ForEach(review =>
            {
                reviewVMlist.Add(new ReviewVM
                {
                    Customer = new CustomerVM
                    {
                        FirstName = review.User.FirstName,
                        LastName = review.User.LastName,
                        Email = review.User.Email,
                        //ImageName = review.User.ImagName
                    },
                    Massage = review.Massage,
                    Subject = review.Subject,
                    ID = review.ID
                });
            });
            HomeDTO model = new HomeDTO
            {
                AdvertisementsList = advertisementsVMList,
                ReviewsList = reviewVMlist,
                Review = new ReviewVM(),
            };

            return View(model);
        }
    }
}