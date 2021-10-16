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
        private readonly ILogger<HomeController> _logger;
        private readonly AdvertisementService _service;
        private readonly ReviewService _serviceReview;
        private UserManager<ApplicationUser> _userManager;
       
        public HomeController(ILogger<HomeController> logger, AdvertisementService service,
                              UserManager<ApplicationUser> userManager,
                              ReviewService serviceReview
                              )
        {
            _logger = logger;
            _service = service;
            _userManager = userManager;
            _serviceReview = serviceReview;
        }
       
       

        public async Task<IActionResult> HomePage()
        {
            var user = await _userManager.GetUserAsync(User);

            ViewBag.listC = new List<string> { "kenan11", "kenan22", "kenan33" };
            List<CategoryDTO> categoryList = _service.GetAllCategories().ToList();
             if(user != null) { 
            ViewBag.numItemCart = _service.GetNumItemShoppingCart(user.Id);
            }
            List<AdvertisementDTO> advertisementsDtoList = _service.GetAllAdvertisemensts().ToList();
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

            List<ReviewDTO> reviewDtoList = _serviceReview.GetApprovedReviews().ToList();

            List<ReviewVM> reviewVMlist = new List<ReviewVM>();
            reviewDtoList.ForEach(review =>
            {
                reviewVMlist.Add(new ReviewVM
                {
                    Customer = new CustomerDTO
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

        public async Task<IActionResult> GetAllItemToCart()
        {

            var user = await _userManager.GetUserAsync(User);
            ViewBag.numItemCart = _service.GetNumItemShoppingCart(user.Id);
            ShoppingCartDTO shoppingCartDTO = _service.GetAdvertisementsInShoppingCart(user.Id);


            // if(!(shoppingCartDTO.Advertisements is null))
            //{
            //    return View(new ShoppingCartVM());
            //}
            ShoppingCartVM ShoppingCartVM = new ShoppingCartVM()
            {
                Advertisements = shoppingCartDTO.Advertisements.Select(a => new AdvertisementVM
                {
                    Name = a.Name,
                    Price = a.Price,
                    Category = a.CategoryName,
                    PhotosNames = a.ImagesNames.ToList()
                }).ToList(),
                TotalPrice = shoppingCartDTO.TotalPrice,
                ShoppingCartId = shoppingCartDTO.ShoppingCartId
            };
            return View(ShoppingCartVM);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            _service.AddShoppingCart(id, user.Id);

            
            return RedirectToAction("HomePage");
        }

        public IActionResult DeleteFromCart(int id)
        {
            _service.RemoveFromShoppingCart(id);

          
            return RedirectToAction("GetAllItemToCart");
        }
    }
}
