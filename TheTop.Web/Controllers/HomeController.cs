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
      
        private UserManager<ApplicationUser> _userManager;
       
        public HomeController(ILogger<HomeController> logger, AdvertisementService service,
                              UserManager<ApplicationUser> userManager
                              )
        {
            _logger = logger;
            _service = service;
            _userManager = userManager;
           
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

            List<ReviewDTO> listRev = new List<ReviewDTO>()
            {
                new ReviewDTO{Name="kenan",Email="kenan@gmail.com",
                              Subject ="Erorr",Massage="Sed tamen tempor magna labore dolore dolor" +
                              " sint tempor duis magna elit veniam aliqua esse amet veniam enim" },
                new ReviewDTO{Name="Noor",Email="kenan@gmail.com",
                              Subject ="Erorr",Massage="Sed tamen tempor magna labore dolore dolor" +
                              " sint tempor duis magna elit veniam aliqua esse amet veniam enim" },
                new ReviewDTO{Name="Moamen",Email="kenan@gmail.com",
                              Subject ="Erorr",Massage="Sed tamen tempor magna labore dolore dolor" +
                              " sint tempor duis magna elit veniam aliqua esse amet veniam enim" },
                new ReviewDTO{Name="Wael",Email="kenan@gmail.com",
                              Subject ="Erorr",Massage="Sed tamen tempor magna labore dolore dolor" +
                              " sint tempor duis magna elit veniam aliqua esse amet veniam enim" },

            };

            HomeDTO model = new HomeDTO
            {
               AdvertisementsList = advertisementsVMList,
                ReviewsList = listRev,
                Review = new ReviewDTO(),               
            };
            ViewBag.numCart = 5;
            return View(model);
        }

        public async Task<IActionResult> GetAllItemToCart()
        {

           

            var user = await _userManager.GetUserAsync(User);
            ViewBag.numItemCart = _service.GetNumItemShoppingCart(user.Id);
            List<ShoppingCartDTO> ShoppingCartDtoList = _service.GetAdvertisementsInShoppingCart(user.Id).ToList();
            List<ShoppingCartVM> ShoppingCartVMList = ShoppingCartDtoList
                                 .Select(cart => new ShoppingCartVM
                                 {
                                     CreatedAt = cart.CreatedAt,
                                      Advertisement = new AdvertisementVM
                                      {
                                          Name = cart.Advertisement.Name,
                                          Price = cart.Advertisement.Price,
                                          Category =cart.Advertisement.CategoryName,
                                          PhotosNames = cart.Advertisement.ImagesNames.Select(imageName => imageName).ToList()
                                      },
                                      ShoppingCartId = cart.ShoppingCartId
                                 }).ToList();
            return View(ShoppingCartVMList);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            _service.AddShoppingCart(new ShoppingCartDTO {
             AdvertisementId = id,
             ApplicationUserId = user.Id
            });

            ViewBag.numItemCart = _service.GetNumItemShoppingCart(user.Id);
            ViewBag.listC = new List<string> { "kenan11", "kenan22", "kenan33" };
            List<CategoryDTO> categoryList = _service.GetAllCategories().ToList();


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

            List<ReviewDTO> listRev = new List<ReviewDTO>()
            {
                new ReviewDTO{Name="kenan",Email="kenan@gmail.com",
                              Subject ="Erorr",Massage="Sed tamen tempor magna labore dolore dolor" +
                              " sint tempor duis magna elit veniam aliqua esse amet veniam enim" },
                new ReviewDTO{Name="Noor",Email="kenan@gmail.com",
                              Subject ="Erorr",Massage="Sed tamen tempor magna labore dolore dolor" +
                              " sint tempor duis magna elit veniam aliqua esse amet veniam enim" },
                new ReviewDTO{Name="Moamen",Email="kenan@gmail.com",
                              Subject ="Erorr",Massage="Sed tamen tempor magna labore dolore dolor" +
                              " sint tempor duis magna elit veniam aliqua esse amet veniam enim" },
                new ReviewDTO{Name="Wael",Email="kenan@gmail.com",
                              Subject ="Erorr",Massage="Sed tamen tempor magna labore dolore dolor" +
                              " sint tempor duis magna elit veniam aliqua esse amet veniam enim" },

            };

            HomeDTO model = new HomeDTO
            {
                AdvertisementsList = advertisementsVMList,
                ReviewsList = listRev,
                Review = new ReviewDTO(),
            };
           

            return View("HomePage", model);
        }

        public async Task<IActionResult> DeleteFromCart(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            _service.RemoveFromShoppingCart(id);

            ViewBag.numItemCart = _service.GetNumItemShoppingCart(user.Id);
           
            List<ShoppingCartDTO> ShoppingCartDtoList = _service.GetAdvertisementsInShoppingCart(user.Id).ToList();
            List<ShoppingCartVM> ShoppingCartVMList = ShoppingCartDtoList
                                 .Select(cart => new ShoppingCartVM
                                 {
                                     CreatedAt = cart.CreatedAt,
                                     Advertisement = new AdvertisementVM
                                     {
                                         Name = cart.Advertisement.Name,
                                         Price = cart.Advertisement.Price,
                                         Category = cart.Advertisement.CategoryName,
                                         PhotosNames = cart.Advertisement.ImagesNames.Select(imageName => imageName).ToList()
                                     },
                                     ShoppingCartId = cart.ShoppingCartId
                                 }).ToList();
            return View("GetAllItemToCart", ShoppingCartVMList);
        }
    }
}
